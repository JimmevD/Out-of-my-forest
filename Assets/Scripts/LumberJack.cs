using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumberJack : Enemy
{
    
   
    void Update()
    {
        navMeshAgent.SetDestination(playerTransform.position);
    }
}
