using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1 : Enemy
{
    protected override void Attack()
    {
        Debug.Log("Attacking");
    }
    protected override void Movement()
    {
        Debug.Log("Moving");
    }
}
