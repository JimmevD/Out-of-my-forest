using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField] private float speed = 12f;
    private float branchSpeed;
    private float groundSpeed;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private float coyoteJumpTime;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask branchMask;
    private bool isOnbranch;
    private Tree currentClimbTree;
    private float coyoteJumpTimer;

    public TextMeshProUGUI interactText;

    private Vector3 velocity;
    private bool isGrounded;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        branchSpeed = speed * 2;
        groundSpeed = speed;
        interactText.enabled = false;
    }

    void Update()
    {
        if (currentClimbTree && Input.GetButtonDown("Jump"))
        {
            ClimbTree();
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isOnbranch = Physics.CheckSphere(groundCheck.position, groundDistance, branchMask);

        if (isGrounded)
        {
            speed = groundSpeed;
            coyoteJumpTimer = coyoteJumpTime;
        }
        else
        {
            coyoteJumpTimer -= Time.deltaTime;
        }

        if (isOnbranch)
        {
            speed = branchSpeed;
        }


        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && coyoteJumpTimer > 0f)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            coyoteJumpTimer = 0;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tree")
        {
            currentClimbTree = null;
            currentClimbTree = other.gameObject.GetComponent<Tree>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentClimbTree.gameObject)
        {
            currentClimbTree = null;
        }
        else
        {
            Debug.Log("Error: " + other.gameObject.name + " current object should be " + currentClimbTree);
        }
    }

    private void ClimbTree()
    {
        Transform shortestBranch = null;
        float shortestDistance = Mathf.Infinity;
        
        for (int i = 0; i < currentClimbTree.branch.Length; i++)
        {
            if (Vector3.Distance(transform.position, currentClimbTree.branch[i].position) < shortestDistance)
            {
                shortestBranch = currentClimbTree.branch[i];
                shortestDistance = Vector3.Distance(transform.position, currentClimbTree.branch[i].position);
            }

        }

        controller.enabled = false;
        transform.position = new Vector3(shortestBranch.position.x, shortestBranch.position.y + 0.5f, shortestBranch.position.z);
        currentClimbTree = null;
        controller.enabled = true;
    }
}
