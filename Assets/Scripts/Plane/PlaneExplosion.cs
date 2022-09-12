using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneExplosion : MonoBehaviour
{
    [SerializeField]
    GameObject planeExplosion;
    [SerializeField]
    GameObject reverseExplosionPrefab;


    Queue<GameObject> planeExplosionsStack;
    Queue<GameObject> planeReverseExplosionsStack;
    Vector3 planePosition;
    GameObject plane;

    private void Awake()
    {
        planeExplosionsStack = new Queue<GameObject>();
        planeReverseExplosionsStack = new Queue<GameObject>();
        for (int i = 0; i < 4; i++)
        {
            GameObject explosion = Instantiate<GameObject>(planeExplosion, Vector3.zero , Quaternion.Euler(-90, 0, 0), transform);
            explosion.SetActive(false);
            planeExplosionsStack.Enqueue(explosion);

            //reverse explosion
            GameObject reverseExplosion = Instantiate<GameObject>(reverseExplosionPrefab, Vector3.zero, Quaternion.Euler(-90, 0, 0), transform);
            reverseExplosion.SetActive(false);
            planeReverseExplosionsStack.Enqueue(reverseExplosion);
        }
    }

    IEnumerator CreateExplosion()
    {
        GameObject explosion = planeExplosionsStack.Dequeue();
        explosion.transform.position = planePosition;
        explosion.SetActive(true);
        yield return new WaitForSeconds(1.1f);
        explosion.SetActive(false);
        planeExplosionsStack.Enqueue(explosion);
    }

 

    public void CallCreateExplosion(Vector3 position)
    {
         planePosition= position;
         StartCoroutine(CreateExplosion());
    }
    IEnumerator CreateReverseExplosion()
    {
        GameObject explosion = planeReverseExplosionsStack.Dequeue();
        explosion.transform.position = planePosition;
        explosion.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        explosion.SetActive(false);
        plane.SetActive(true);
        planeReverseExplosionsStack.Enqueue(explosion);
    }
    public void CallReverseExplosion(Vector3 position, GameObject plane)
    {
        planePosition = position;
        this.plane = plane;
        StartCoroutine(CreateReverseExplosion());
    }
}
