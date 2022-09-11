using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneExplosion : MonoBehaviour
{
    [SerializeField]
    GameObject planeExplosion;

    Queue<GameObject> planeExplosionsStack;
    Vector3 planePosition;

    private void Awake()
    {
        planeExplosionsStack = new Queue<GameObject>();
        for (int i = 0; i < 4; i++)
        {
            GameObject explosion = Instantiate<GameObject>(planeExplosion, Vector3.zero , Quaternion.Euler(-90, 0, 0), transform);
            explosion.SetActive(false);
            planeExplosionsStack.Enqueue(explosion);
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
    
}
