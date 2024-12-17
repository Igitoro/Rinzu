using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack: MonoBehaviour
{
    [SerializeField] Transform attackRadiusTransform;
    [SerializeField] float attackRadius = 1f;
    [SerializeField] int damageAmount;

    public void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackRadiusTransform.position, attackRadius); 

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy") || collider.CompareTag("EnemyShoot"))
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damageAmount);
                }
            }
            else if (collider.CompareTag("EnemyBoss"))
            {
                EnemyBoss boss = collider.GetComponent<EnemyBoss>();
                if (boss != null)
                {
                    boss.BossTakeDamage(damageAmount);
                    Debug.Log("Took Damage");
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackRadiusTransform.position, attackRadius);
    }

    public void AddAtkOnLvlUp()
    {
        damageAmount+= 5;
    }
}
