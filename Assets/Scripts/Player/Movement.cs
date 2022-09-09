using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool IsMoving { get; set; } = false;
    [SerializeField]
    LayerMask layerMask;
    //[SerializeField]
    //float rotationSpeed;
    [SerializeField]
    float moveSpeed;
    RaycastHit hit;
    Rigidbody rigidbody;
    Transform trans;
    Transform playerBody;
    Vector3 moveToPosition;
    Vector3 moveInDirection;
    private void Awake()
    {
        trans = base.transform;
        rigidbody = GetComponent<Rigidbody>();
        playerBody = base.transform.GetChild(0);
    }
    void Update()
    {
        if (IsMoving)
        {
            trans.Translate(moveSpeed * Time.deltaTime * moveInDirection);
            return;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (Physics.Raycast(trans.position, Vector3.left, out hit, 2f, layerMask))
            {
                MoveToPosition(hit.transform.position, Vector3.left);
                //playerBody.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-90, 90, 0), Time.deltaTime * rotationSpeed);
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (Physics.Raycast(trans.position, Vector3.forward, out hit, 2f, layerMask))
            {
                MoveToPosition(hit.transform.position, Vector3.forward);
                //playerBody.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-90, 90, 0), Time.deltaTime * rotationSpeed);
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (Physics.Raycast(trans.position, Vector3.right, out hit, 2f, layerMask))
            {
                MoveToPosition(hit.transform.position, Vector3.right);
                //playerBody.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-90, 90, 0), Time.deltaTime * rotationSpeed);
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (Physics.Raycast(trans.position, Vector3.back, out hit, 2f, layerMask))
            {
                MoveToPosition(hit.transform.position, Vector3.back);
                //playerBody.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-90, 90, 0), Time.deltaTime * rotationSpeed);
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
        moveInDirection = direction;
        IsMoving = true;
        moveToPosition = endPosition;
    }
}
