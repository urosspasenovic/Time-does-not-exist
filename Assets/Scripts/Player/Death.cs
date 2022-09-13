using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField]
    Transform playerBodyRotation;
    public static bool canDie = true;

    Player player;
    ActionHistory actionHistory;
    bool isCurrentPlayer;
    AudioSource audioSource;
    private void Awake()
    {
        actionHistory = FindObjectOfType<ActionHistory>();
        player = GetComponent<Player>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && canDie)
        {
            audioSource.Play();
            if(player == InputHandler.Instance.CurrentPlayer) isCurrentPlayer = true;
            else isCurrentPlayer = false;
            InputHandler.Instance.IsMoving = true;
            Command command = new DeathCommand(player, player.transform.position, isCurrentPlayer, playerBodyRotation.rotation);
            actionHistory.SaveAction(command);
            //InputHandler.Instance.IsMoving = false;
        }
    }

}
