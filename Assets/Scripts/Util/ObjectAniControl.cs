using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAniControl : MonoBehaviour
{
    public Vector3 originPos;
    public float sinDir = 0.0f;
    public float moveSpeed = 0.0f;
    public float moveDistance = 0.0f;
    public float rotationSpeed = 0.0f;

    private void Awake()
    {
        originPos = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sinDir = sinDir + Time.deltaTime * moveSpeed;
        if (sinDir > Mathf.PI) sinDir -= Mathf.PI;
        transform.position = originPos + Vector3.up * Mathf.Sin(sinDir) * moveDistance;

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
