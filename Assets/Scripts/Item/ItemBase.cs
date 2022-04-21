using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    private int _temperature = 0;
    protected int Temperature  
    { 
        get{ return _temperature; } 
        set {_temperature = Mathf.Clamp(value, -100, 100);} 
    }

    protected TypeObj ItemType;
    protected string Name;

    protected void StartI()
    {

    }
    protected void StopI() 
    {
        ItemType = TypeObj.None;
        Name = "";
        Temperature = 0;
    }

}
