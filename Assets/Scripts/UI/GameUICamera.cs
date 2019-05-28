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

    private void Start()
    {
        if(target.GetComponent<UnitManager>().spawnRotation.y > 100.0f)
        {
            offset.z *= -1.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position + offset;
    }
}
