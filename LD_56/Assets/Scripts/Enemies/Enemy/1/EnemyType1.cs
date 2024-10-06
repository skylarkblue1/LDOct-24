using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1 : Enemy
{
    [SerializeField] Transform attackPos;
    [SerializeField] ObjectPooler ammoPool;
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
        PerformAttack();
        canAttack = false;
    }
    protected override void Movement()
    {
        Debug.Log("Moving");
    }
    void PerformAttack()
    {
        GameObject curProjectile = ammoPool.GetPooledObject();
        curProjectile.transform.forward = player.position - transform.position;
        curProjectile.transform.position = attackPos.position;
        curProjectile.SetActive(true);
        Debug.Log("Attacking");
    }
}
