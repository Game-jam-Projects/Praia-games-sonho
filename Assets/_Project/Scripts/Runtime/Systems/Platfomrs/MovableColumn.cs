using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableColumn : MonoBehaviour
{
    public GameObject column;
    public Transform pointA;
    public Transform pointB;
    public bool startPointB;
    public float moveSpeed;
    private int idPosition;
    // Start is called before the first frame update
    void Start()
    {
        
        if(startPointB == true)
        {
            column.transform.position = pointB.position;
        }
        else
        {
            column.transform.position = pointA.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(idPosition)
        {
            case 0:
                column.transform.position = Vector3.MoveTowards(column.transform.position, pointB.position, moveSpeed * Time.deltaTime);
                if(column.transform.position == pointB.position)
                {
                    idPosition = 1;
                }
                break;

            case 1:
                column.transform.position = Vector3.MoveTowards(column.transform.position, pointA.position, moveSpeed * Time.deltaTime);
                if (column.transform.position == pointA.position)
                {
                    idPosition = 0;
                }
                break;
        }
        
    }
}
