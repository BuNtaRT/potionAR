using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName = "Item/Item")]
public class ItemSO : ScriptableObject
{
    public string Name;
    public TypeObj Type;
    public GameObject Prefab;
    [Range(-100, 100)]
    public int Temperature;
}
