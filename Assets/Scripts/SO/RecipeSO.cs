using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New recipe", menuName = "Item/Recipe")]
public class RecipeSO : ScriptableObject
{
    public List<RecipeItems> ItemsForCraft;
    public ItemSO Result;
    public int    TimeRecipe;
    public int    Price;
}

[Serializable]
public struct RecipeItems
{
    public ItemSO Item;
    [Range(-100,100)]
    public int    Temperature;
}