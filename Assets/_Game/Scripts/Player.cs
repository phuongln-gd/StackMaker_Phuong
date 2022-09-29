using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask brickLayer;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;

    private Vector3 currentDirect;
    private Vector2 startTouch, swipeDelta;
    private Vector3 lastMovingPosition;

    private bool isDraging=false,isMoving = false;

    public Direct eCurrentDirect;

    void Update()
    {
        if (!isMoving)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startTouch = Input.mousePosition;
                isDraging = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isDraging = false;
                Reset();
            }

            // Calculate the distance
            swipeDelta = Vector2.zero;
            if (isDraging)
            {
                if (Input.GetMouseButton(0))
                {
                    swipeDelta = (Vector2)Input.mousePosition - startTouch;
                }
            }

            if (swipeDelta.magnitude > 125)
            {
                float x = swipeDelta.x;
                float y = swipeDelta.y;
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    //left or right
                    if (x > 0)
                    {
                        eCurrentDirect = Direct.Right;
                        currentDirect = Vector3.right;
                    }
                    else
                    {
                        eCurrentDirect = Direct.Left;
                        currentDirect = Vector3.left;

                    }
                }
                else
                {
                    //up or down
                    if (y > 0)
                    {
                        eCurrentDirect = Direct.Forward;
                        currentDirect = Vector3.forward;
                    }
                    else
                    {
                        eCurrentDirect = Direct.Back;
                        currentDirect = Vector3.back;
                    }
                }
                CheckFinalPosition(currentDirect);
                Reset();
            }
        }
        else
        {
            Moving();
        }
    }

    private void Moving()
    {
        transform.position = Vector3.MoveTowards(transform.position, lastMovingPosition, Time.deltaTime * speed);
        if(Vector3.Distance(transform.position,lastMovingPosition) < 0.1f)
        {
            isMoving = false;
        }
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }

    private void CheckFinalPosition(Vector3 dir)
    {
        int i = 1;
        isMoving = false;
        while (Physics.Raycast(transform.position + dir * i, Vector3.down, 2f, brickLayer))
        {
            i++;
            isMoving = true;
        }
        if (isMoving)
        {
            lastMovingPosition = transform.position + dir * (i-1);
        }
        else
        {
            lastMovingPosition = transform.position;
        }
    }
}

