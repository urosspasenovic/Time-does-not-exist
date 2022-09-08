using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneInit : MonoBehaviour
{
    [SerializeField] GameObject plane;
    [SerializeField] int dimension;
    [SerializeField] float offset;

    Vector3 positionOfPlane;
    private void Awake()
    {
        positionOfPlane = Vector3.zero;

        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                Instantiate<GameObject>(plane, positionOfPlane, Quaternion.identity, transform);
                positionOfPlane += new Vector3(1 + offset, 0, 0);
            }

            positionOfPlane = new Vector3(0, 0, positionOfPlane.z + 1 + offset);

        }
    }
}
