using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPick : ItemState
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = Vector3.MoveTowards(manager.transform.localPosition, new Vector3(0.0f, 2.5f, 0.0f), 1.0f * Time.deltaTime);
        manager.transform.localPosition = move;
    }
}
