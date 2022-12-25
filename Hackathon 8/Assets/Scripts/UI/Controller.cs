using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public Camera mainCamera;
    public RectTransform rectTransform;
    public Transform point;

    [NonSerialized]public float dirX = 0;
    [NonSerialized]public bool jump = false;

    public EventTrigger jumpButton;

    // Update is called once per frame
    void Update()
    {
        var fourCornersArray = new Vector3[4];
        rectTransform.GetWorldCorners(fourCornersArray);

        float width = fourCornersArray[2].x - fourCornersArray[0].x;
        float height = fourCornersArray[2].y - fourCornersArray[0].y;

        float x = fourCornersArray[0].x + width / 2;
        float y = fourCornersArray[0].y + height / 2;
        bool wasTouch=false;
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                var mousePosition = touch.position;

                if (mousePosition.x < fourCornersArray[2].x && mousePosition.y < fourCornersArray[2].y &&
                    mousePosition.x > fourCornersArray[0].x && mousePosition.y > fourCornersArray[0].y)
                {
                    wasTouch = true;
                    point.position = mousePosition;
                    if (x - mousePosition.x > width / 6)
                    {
                        dirX = -1;
                    }
                    else if (mousePosition.x - x > width / 6)
                    {
                        dirX = 1;
                    }
                    else
                    {
                        dirX = 0;
                    }
                }
            }
        }
        if(!wasTouch)
        {
            dirX = 0;
            point.position = new Vector3(x, y, 0);
        }
    }

    public void Jump()
    {
        jump = true;
    }
}
