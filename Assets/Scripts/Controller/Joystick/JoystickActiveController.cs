using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickActiveController : MonoBehaviour, IPointerDownHandler
{
    public Joystick target;

    public void OnPointerDown(PointerEventData eventData)
    {
        target.gameObject.SetActive(true);
        target.transform.position = eventData.position;
        target.OnPointerDown(eventData);
    }
}
