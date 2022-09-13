using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopEnemies : MonoBehaviour
{
    static EnemyMovement[] enemies;

    private void Awake()
    {
        enemies = FindObjectsOfType<EnemyMovement>();
    }

    public static void StopEnemiesMovement()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].enabled = false;
        }
    }

    internal static void EnableEnemyMovement()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].enabled = true;
        }
    }
}
