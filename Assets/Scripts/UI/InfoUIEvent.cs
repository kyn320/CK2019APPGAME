using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfoUIEvent : MonoBehaviour
{
    int currentIndex = 0;

    public Transform modelRotater;
    public GameObject[] models;

    public EventTrigger eventTrigger;
    private Vector2 oldTouchPos = Vector2.zero;

    private void Start()
    {
        currentIndex = PlayerPrefs.GetInt("ModelViewIndex", 0);
        OnChangeModel(currentIndex);

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Drag;
        entry.callback.AddListener((data) => { OnDragRotate((PointerEventData)data); } );
        eventTrigger.triggers.Add(entry);
    }

    public void OnChangeModel(int modelIndex)
    {
        models[currentIndex].SetActive(false);

        PlayerPrefs.SetInt("ModelViewIndex", modelIndex);

        currentIndex = modelIndex;
        models[currentIndex].SetActive(true);
    }

    public void OnDragRotate(PointerEventData eventData)
    {

        float distance = oldTouchPos.x - eventData.position.x;

        modelRotater.Rotate(Vector3.up * distance, Space.Self);

        oldTouchPos = eventData.position;

    }

    public void GoToMain() {
        SceneManager.LoadScene("MainScene");
    }

}
