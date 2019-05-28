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

    string[] characterNames = {"Zeus", "Hera" };
    string[] stateNames = { "Normal", "Cry", "Angry", "Run", "Victory" };

    private void Start()
    {
        currentIndex = PlayerPrefs.GetInt("ModelViewIndex", 0);
        OnChangeModel(currentIndex);

        EventTrigger.Entry touchEntry = new EventTrigger.Entry();
        touchEntry.eventID = EventTriggerType.PointerDown;
        touchEntry.callback.AddListener((data) => { OnTouchChange((PointerEventData)data); });

        eventTrigger.triggers.Add(touchEntry);

        EventTrigger.Entry dragEntry = new EventTrigger.Entry();
        dragEntry.eventID = EventTriggerType.Drag;
        dragEntry.callback.AddListener((data) => { OnDragRotate((PointerEventData)data); });

        eventTrigger.triggers.Add(dragEntry);
    }


    public void OnChangeModel(int modelIndex)
    {
        models[currentIndex].SetActive(false);

        PlayerPrefs.SetInt("ModelViewIndex", modelIndex);

        currentIndex = modelIndex;
        models[currentIndex].SetActive(true);
    }

    public void OnTouchChange(PointerEventData eventData)
    {
        if (currentIndex == 2)
            return;

        models[currentIndex].GetComponent<FaceChanger>().ChangeFace(characterNames[currentIndex]
            , stateNames[Random.Range(0, 5)]);
    }

    public void OnDragRotate(PointerEventData eventData)
    {

        float distance = oldTouchPos.x - eventData.position.x;

        modelRotater.Rotate(Vector3.up * distance, Space.Self);

        oldTouchPos = eventData.position;

    }

    public void GoToMain()
    {
        SceneManager.LoadScene("MainScene");
    }

}
