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

    protected GameObject Prefab;
    protected string Name;

    protected void Init(int temperature, GameObject prefab, string name) 
    {
        Temperature = temperature;
        Prefab = prefab;
        Name = name;
    }



}
