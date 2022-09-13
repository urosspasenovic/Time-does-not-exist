using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public bool Moved { get; set; }
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    LayerMask layerMask;


    bool isMoving = false;
    Transform trans;
    Vector3 moveToPosition;
    RaycastHit hit;
    List<Vector3> availableDirections = new List<Vector3>();
    private void Awake()
    {
        ResetDirections();
        trans = transform;      
    }
    private void FixedUpdate()
    {
        if (isMoving)
        {
            trans.position = Vector3.MoveTowards(trans.position, moveToPosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(trans.position, moveToPosition) < 0.2f)
            {
                StopMoving();
                ResetDirections();
                Move();
            }
        }
        
    }
    private void Start()
    {
        Move();
    }
    private void Move()
    {      
        int indexDirection = Random.Range(0, availableDirections.Count);
        if (Physics.Raycast(trans.position, availableDirections[indexDirection], out hit, 2f, layerMask))
        {
            MoveToPosition(hit.transform.position);
            return;
        }
        else
        {
            availableDirections.RemoveAt(indexDirection);
            if (availableDirections.Count == 0)
            {
                ResetDirections();
                Invoke("Move", 1f);
                return;
            }
            Move();
        }
    }
   
    
    public void StopMoving()
    {
        isMoving = false;
        trans.position = moveToPosition;
    }
    private void MoveToPosition(Vector3 endPosition)
    {
        isMoving = true;
        moveToPosition = endPosition;
    }
    private void ResetDirections()
    {
        availableDirections.Clear();
        availableDirections.AddRange(new List<Vector3> {
            Vector3.forward,
            Vector3.left,
            Vector3.back,
            Vector3.right
        });
    }
}
