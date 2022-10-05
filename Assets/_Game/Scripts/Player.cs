using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask brickLayer;
    [SerializeField] private GameObject PlayerImage;
    [SerializeField] private GameObject addBrickPrefab;
    [SerializeField] private GameObject StackBrick;
    [SerializeField] private float speed = 10;

    public int currentLevel;
    public List<GameObject> list;
    public Direct ECurDirect;

    private Vector3 currentDirect, lastMovingPosition;
    private Vector2 startTouch, swipeDelta;
    private bool isDraging, isMoving;

    private void Start()
    {
        OnInit();
        LevelManager.instance.LoadFirstLevel();
        currentLevel = 1;
        UIManager.instance.SetTextLevel(currentLevel);
    }

    void Update()
    {
        if (!isMoving)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startTouch = Input.mousePosition;
                isDraging = true; // kiem tra dang keo
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isDraging = false;
                Reset();
            }

            // tinh toan khoang cach
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
                float x = swipeDelta.x,
                    y = swipeDelta.y;
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    // left or right
                    if (x > 0)
                    {
                        ECurDirect = Direct.Right;
                        currentDirect = Vector3.right;
                    }
                    else
                    {
                        ECurDirect = Direct.Left;
                        currentDirect = Vector3.left;
                    }
                }
                else
                {
                    // up or down
                    if (y > 0)
                    {
                        ECurDirect = Direct.Forward;
                        currentDirect = Vector3.forward;
                    }
                    else
                    {
                        ECurDirect = Direct.Back;
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
    
    private void OnInit()
    {
        isMoving = false;
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
        setInitPoint();
    }
    private void ClearStackBrick()
    {
        for(int i = 0; i< list.Count; i++)
        {
            Destroy(list[i]);
        }
        list.Clear();
    }
    public void PlayAgain()
    {
        LevelManager.instance.LoadLevel(currentLevel);
        UIManager.instance.SetTextLevel(currentLevel);
        PassLevel();
    }
    public void PassLevel()
    {
        PlayerImage.transform.localPosition = Vector3.zero;
        ClearStackBrick();
        OnInit();
    }
    public void setInitPoint()
    {
        transform.position = new Vector3(0, (float)0.83583498, 0);
    }
    // them gach
    public void AddBrick()
    {
        PlayerImage.transform.position += new Vector3(0, (float)0.5, 0);
        GameObject newBrick;
        if (list.Count > 0)
        {
            newBrick = Instantiate(addBrickPrefab, list[list.Count - 1].transform.position + new Vector3(0, (float)0.5, 0)
              , Quaternion.identity);
        }
        else
        {
            newBrick = Instantiate(addBrickPrefab, StackBrick.transform.position + new Vector3(0, (float)0.5, 0)
             , Quaternion.identity);
        }
        newBrick.transform.SetParent(StackBrick.transform);
        list.Add(newBrick);
    }

    // xoa gach
    public void DeleteBrick()
    {
        if(list.Count > 0)
        {
            Destroy(list[list.Count - 1]);
            list.Remove(list[list.Count - 1]);
            PlayerImage.transform.position += new Vector3(0, (float)-0.5, 0);
        }
    }

    internal void AddFirstBrick()
    {
        GameObject newBrick;
        newBrick = Instantiate(addBrickPrefab, StackBrick.transform.position
            , Quaternion.identity);
        newBrick.transform.SetParent(StackBrick.transform);
        list.Add(newBrick);
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
        while (Physics.Raycast(transform.position + dir * i, Vector3.down, 5f, brickLayer))
        {
            i++;
            isMoving = true;
        }
        if (isMoving)
        {
            lastMovingPosition = transform.position + dir * (i - 1);
        }
        else
        {
            lastMovingPosition = transform.position;
        }
    }
    private void Moving()
    {
        transform.position = Vector3.MoveTowards(transform.position, lastMovingPosition, Time.deltaTime * speed);
        if (Vector3.Distance(transform.position, lastMovingPosition) < 0.03f)
        {
            isMoving = false;
        }
    }

}
