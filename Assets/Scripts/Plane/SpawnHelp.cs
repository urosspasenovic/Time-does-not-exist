using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHelp : MonoBehaviour
{
    [SerializeField]
    GameObject objectToSpawn;
    [SerializeField]
    Material planeMaterial;
    private void OnTriggerEnter(Collider other)
    {
        objectToSpawn.SetActive(true);
        GetComponent<MeshRenderer>().material = planeMaterial;
        this.enabled = false;
    }
}
