using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New recipe", menuName = "Craft/Recipe")]


public class RecipeSO : ScriptableObject
{
    public List<ReciprItem> ItemsForCraft;
    public ItemBase Result;
}

[Serializable]
public struct ReciprItem
{
    public ItemBase ItemBase;
    [Range(-100,100)]
    public int Temperature;
}