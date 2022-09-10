using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool IsMoving { get; set; } = false;
    [SerializeField]
    LayerMask layerMask;
    [SerializeField]
    Transform playerBody;
    [SerializeField]
    float rotationSpeed;
    [SerializeField]
    float moveSpeed;
    RaycastHit hit;
    Transform trans;
    Vector3 moveToPosition;
    Quaternion rotateTo;
    private void Awake()
    {
        trans = transform;
    }
    private void FixedUpdate()
    {
        if (IsMoving)
        {           
            trans.position = Vector3.MoveTowards(trans.position, moveToPosition, moveSpeed * Time.deltaTime);
            if(Vector3.Distance(playerBody.position, moveToPosition) < 0.2f)
                StopMoving();
            playerBody.rotation =  Quaternion.RotateTowards(playerBody.rotation, rotateTo, rotationSpeed * Time.deltaTime);          

        }
    }
    void Update()
    {
        if (IsMoving) return;
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (Physics.Raycast(trans.position, Vector3.left, out hit, 2f, layerMask))
            {
                MoveToPosition(hit.transform.position, Vector3.left);               
                rotateTo = Quaternion.Euler(-90f, 90f, 0);
                return;
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (Physics.Raycast(trans.position, Vector3.forward, out hit, 2f, layerMask))
            {
                MoveToPosition(hit.transform.position, Vector3.forward);               
                rotateTo = Quaternion.Euler(-90f, 180f, 0);
                return;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (Physics.Raycast(trans.position, Vector3.right, out hit, 2f, layerMask))
            {
                MoveToPosition(hit.transform.position, Vector3.right);             
                rotateTo = Quaternion.Euler(-90f, 270f, 0);
                return;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (Physics.Raycast(trans.position, Vector3.back, out hit, 2f, layerMask))
            {
                MoveToPosition(hit.transform.position, Vector3.back);              
                rotateTo = Quaternion.Euler(-90f, 0, 0);
                return;
            }
        }
    }
    public void StopMoving()
    {
        IsMoving = false;
        trans.position = moveToPosition;
    }
    private void MoveToPosition(Vector3 endPosition, Vector3 direction)
    {
        IsMoving = true;
        moveToPosition = endPosition;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Plane"))
            Destroy(other.gameObject);
    }
}