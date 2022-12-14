using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHelp : MonoBehaviour
{
    [SerializeField]
    GameObject objectToSpawn;
    [SerializeField]
    Material planeMaterial;

    ChangePlayer changePlayer;
    AudioSource audioSource;
    private void Awake()
    {
        changePlayer = FindObjectOfType<ChangePlayer>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objectToSpawn.SetActive(true);
            GetComponent<MeshRenderer>().material = planeMaterial;
            if(objectToSpawn.CompareTag("Player"))
                changePlayer.AddPlayer(objectToSpawn.GetComponent<Player>());
            this.enabled = false;
            audioSource.Play();
        }
    }
}
