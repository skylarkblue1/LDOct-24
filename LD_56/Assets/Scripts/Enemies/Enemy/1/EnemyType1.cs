using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1 : Enemy
{
    bool canAttack = false;
    float attackCounter = 0;
    protected override void Attack()
    {
        attackCounter += Time.deltaTime;
        if(attackCounter > attackDelay)
        {
            canAttack = true;
            attackCounter = 0;
        }
        if(canAttack == false)
        {
            return;
        }
        Debug.Log("Attacking");
        canAttack = false;
    }
    protected override void Movement()
    {
        Debug.Log("Moving");
    }
}
