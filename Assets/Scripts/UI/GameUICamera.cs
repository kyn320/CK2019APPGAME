using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUICamera : MonoBehaviour
{
    GameObject target;
    Vector3 offset;

    private void Awake()
    {
        target = GameManager.Instance.localPlayer.gameObject;
        offset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position + offset;
    }
}
