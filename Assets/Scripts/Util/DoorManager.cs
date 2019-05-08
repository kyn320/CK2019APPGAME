using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    bool isOpen;

    private void Awake()
    {
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen) return;
        if (!GameManager.Instance.isBegin)
        {
            transform.position -= new Vector3(0, 4.6f, 0) * Time.deltaTime;
            if(transform.position.y < -4.6f)
            {
                Vector3 pos = transform.position;
                pos.y = -4.6f;
                transform.position = pos;
                isOpen = true;
            }
        }
    }
}
