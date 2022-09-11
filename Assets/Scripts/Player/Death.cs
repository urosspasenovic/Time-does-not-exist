using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    ChangePlayer changePlayer;
    Animator animator;
    private void Awake()
    {
        changePlayer = FindObjectOfType<ChangePlayer>();
        animator = GetComponentInChildren<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            animator.SetBool("Death", true);
            changePlayer.RemovePlayer(gameObject);
            Invoke("DestroyPlayer", 1f);
        }
    }
    void DestroyPlayer()
    {
        gameObject.SetActive(false);
        //animator.SetBool("Reset", true);
    }
}
