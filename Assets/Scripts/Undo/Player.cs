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
    Vector3 currentPosition;
    private void Awake()
    {
        trans = GetComponent<Transform>();
        changePlayer = FindObjectOfType<ChangePlayer>();
        animator = GetComponentInChildren<Animator>();
        planeExplosion = FindObjectOfType<PlaneExplosion>();
        planes = new List<GameObject>();    
        currentPosition = transform.position;
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
        InputHandler.Instance.IsMoving = true;
        changePlayer.AddPlayer(this);
        playerBody.position = position;

       
        gameObject.SetActive(true);
        playerBody.rotation = playerBodyRotation;
        Death.canDie = false;
        if (isCurrentPlayer)
        {
            playerBody.position = moveToPosition;
            changePlayer.UndoChangePlayer();
        }
        gameObject.transform.position = playerBody.position;
        InputHandler.Instance.IsMoving = false;
        Invoke("ReactivateColliderAfterRessuraction", 1.5f);
    }
    void ReactivateColliderAfterRessuraction()
    {
        Death.canDie = true;
    }
    private void DeactivatePlayer()
    {
        if(changePlayer.playersCount == 1)
        {
            print("Game over");
            InputHandler.Instance.GameOver();
            return;
        }
        gameObject.SetActive(false);
        changePlayer.RemovePlayer(this);
        InputHandler.Instance.IsMoving = false;
    }
    public void DestroyPlayer()
    {
        isMoving = false;
        animator.SetBool("Death", true);
        Invoke("DeactivatePlayer", 0.5f);
    }

    public void Move(Vector3 moveToPosition, Quaternion rotateTo, Vector3 undoPosition)
    {
        movingForward = true;
        isMoving = true;
        this.moveToPosition = moveToPosition;
        this.rotateTo = rotateTo;
        currentPosition = undoPosition;
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
        currentPosition = moveToPosition;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Plane") && movingForward)
        {
            if (Vector3.Distance(other.transform.position, currentPosition) > 0.4f) return;
            planes.Add(other.gameObject);
            other.gameObject.SetActive(false);
            planeExplosion.CallCreateExplosion(other.transform.position);
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("InactiveEnemy"))
        {
            EnemyMovement enemyMovement = other.GetComponentInParent<EnemyMovement>();
            enemyMovement.enabled = true;
            enemyMovement.Moved = true;
            other.gameObject.SetActive(false);
        }

    }

}
