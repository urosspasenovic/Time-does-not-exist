using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField]
    Transform playerBodyRotation;

    Player player;
    ActionHistory actionHistory;
    bool isCurrentPlayer;
    private void Awake()
    {
        actionHistory = FindObjectOfType<ActionHistory>();
        player = GetComponent<Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if(player == InputHandler.Instance.CurrentPlayer) isCurrentPlayer = true;
            else isCurrentPlayer = false;
            Command command = new DeathCommand(player, player.transform.position, isCurrentPlayer, playerBodyRotation.rotation);
            actionHistory.SaveAction(command);
            //InputHandler.Instance.IsMoving = false;
        }
    }
    void DestroyPlayer()
    {
        gameObject.SetActive(false);
        //animator.SetBool("Reset", true);
    }
}
