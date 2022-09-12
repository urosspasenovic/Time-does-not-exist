using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float movingBackwardsSpeed;
    [SerializeField]
    float rotationSpeed;
    [SerializeField]
    Transform playerBody;
    
    bool isMoving;
    List<GameObject> planes;
    bool movingForward;
    PlaneExplosion planeExplosion;
    Animator animator;
    Quaternion rotateTo;
    Vector3 moveToPosition;
    Transform trans;
    ChangePlayer changePlayer;
    
    private void Awake()
    {
        trans = GetComponent<Transform>();
        changePlayer = FindObjectOfType<ChangePlayer>();
        animator = GetComponentInChildren<Animator>();
        planeExplosion = FindObjectOfType<PlaneExplosion>();
        planes = new List<GameObject>();    
    }

    internal void UndoMove(Vector3 undoPosition, Quaternion rotation)
    {
        movingForward = false;
        isMoving = true;
        rotateTo = rotation;
        this.moveToPosition = undoPosition;
        planeExplosion.CallReverseExplosion(undoPosition, planes[^1]);
        planes.RemoveAt(planes.Count - 1);
    }

    public void RessuractePlayer(Vector3 position, bool isCurrentPlayer, Quaternion playerBodyRotation)
    {
        //add animation
        changePlayer.AddPlayer(this);
        if (isCurrentPlayer)
        {
            //change camera to that player
            changePlayer.UndoChangePlayer();
        }
        gameObject.SetActive(true);
        playerBody.position = position;
        playerBody.rotation = playerBodyRotation;
        InputHandler.Instance.IsMoving = false;
    }

    private void DeactivatePlayer()
    {
        gameObject.SetActive(false);
        InputHandler.Instance.IsMoving = false;
    }
    public void DestroyPlayer()
    {
        animator.SetBool("Death", true);
        changePlayer.RemovePlayer(this);
        Invoke("DeactivatePlayer", 1f);
    }

    public void Move(Vector3 moveToPosition, Quaternion rotateTo)
    {
        movingForward = true;
        isMoving = true;
        this.moveToPosition = moveToPosition;
        this.rotateTo = rotateTo;
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            if (!movingForward)
            {
                trans.position = Vector3.MoveTowards(trans.position, moveToPosition, moveSpeed * Time.deltaTime / 1.3f);
                if (Vector3.Distance(playerBody.position, moveToPosition) < 0.15f)
                    StopMoving();
            }
            else
            {
                trans.position = Vector3.MoveTowards(trans.position, moveToPosition, moveSpeed * Time.deltaTime);
                if (Vector3.Distance(playerBody.position, moveToPosition) < 0.2f)
                    StopMoving();
            }
            playerBody.rotation = Quaternion.RotateTowards(playerBody.transform.rotation, rotateTo, rotationSpeed * Time.deltaTime);
        }
    }
    private void StopMoving()
    {
        isMoving = false;
        movingForward = false;
        trans.position = moveToPosition;
        InputHandler.Instance.CurrentPosition = moveToPosition;
        InputHandler.Instance.IsMoving = false;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Plane") && movingForward)
        {
            planes.Add(other.gameObject);
            planeExplosion.CallCreateExplosion(other.transform.position);
            other.gameObject.SetActive(false);
        }
    }

}
