using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    [SerializeField]
    Material seenMaterial;
    [SerializeField]
    Material darkMaterial;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Plane"))
        {
            other.GetComponent<Renderer>().material = seenMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Plane"))
        {
            other.GetComponent<Renderer>().material = darkMaterial;
        }
    }
}
