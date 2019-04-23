using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Joystick : Controller, IPointerDownHandler
{
    public GameObject stick;

    public bool autoActive = true;

    int fingerId;
    public float joystickRadius;

    private void Awake()
    {
        gameObject.active = autoActive;
        fingerId = -1;

        stick = transform.Find("stick").gameObject;
        joystickRadius = GetComponent<RectTransform>().rect.width / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(fingerId >= 0)
        {
            Touch touch = GetTouchToFingetId(fingerId);
            if (touch.phase == TouchPhase.Began)
                OnJoystickEnter();
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                OnJoystickStay();
            else if (touch.phase == TouchPhase.Ended)
            {
                OnJoystickExit();
                fingerId = -1;
            }
        }
    }

    public virtual void OnJoystickEnter()
    {
    }

    public virtual void OnJoystickStay()
    {
        Touch touch = GetTouchToFingetId(fingerId);
        stick.transform.position = touch.position;
        Vector3 vector = stick.transform.localPosition;
        float distance = vector.sqrMagnitude;
        Vector3 dir = vector.normalized;

        if(distance > joystickRadius * joystickRadius)
        {
            stick.transform.localPosition = dir * joystickRadius;
            distance = joystickRadius * joystickRadius;
        }

    }

    public virtual void OnJoystickExit()
    {
        stick.transform.localPosition = Vector3.zero;
        gameObject.active = autoActive;
    }

    public Touch GetTouchToFingetId(int fingerId)
    {
        foreach(Touch touch in Input.touches)
        {
            if(touch.fingerId == fingerId)
            {
                return touch;
            }
        }

        return Input.GetTouch(0);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (fingerId < 0)
        {
            fingerId = eventData.pointerId;
        }
    }
}
