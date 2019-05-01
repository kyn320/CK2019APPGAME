using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODUnit : MonoBehaviour
{
    UnitManager manager;

    [FMODUnity.EventRef]
    Dictionary<UnitStateCode, string> eventPath = new Dictionary<UnitStateCode, string>();
    public FMOD.Studio.EventInstance medieval;
    public FMOD.Studio.ParameterInstance location;

    private void Awake()
    {
        manager = GetComponent<UnitManager>();
        eventPath[UnitStateCode.JUMP] = "event:/SFM/SFM_jump";
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
