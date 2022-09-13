using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEnemy : MonoBehaviour
{
    EnemyMovement enemyMovement;
    private void Awake()
    {
        enemyMovement = GetComponentInParent<EnemyMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyMovement.enabled = true;
            gameObject.SetActive(false);
        }
    }
}
