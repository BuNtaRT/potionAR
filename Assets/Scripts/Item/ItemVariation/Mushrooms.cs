using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushrooms : ItemBase
{
    private void Awake()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Mushrooms enther");
    }
}
