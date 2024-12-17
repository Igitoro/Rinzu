using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingZone : MonoBehaviour
{
    public int healthRestoreRate = 3;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                StartCoroutine(RestoreHealth(playerHealth));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                StopCoroutine(RestoreHealth(playerHealth));
            }
        }
    }

    private IEnumerator RestoreHealth(Health playerHealth)
    {
        while (playerHealth.health < playerHealth.maxHealth)
        {
            playerHealth.health = Mathf.Clamp(playerHealth.health + healthRestoreRate, 0, playerHealth.maxHealth);

            yield return null;
        }
    }
}

