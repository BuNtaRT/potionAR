using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipeBuilder : MonoBehaviour
{
    [SerializeField]
    private List<RecipeSO> _allRecipe = new List<RecipeSO>();

    [SerializeField]
    private List<RecipeSO> _curentListRecipe = new List<RecipeSO>();
    [SerializeField]
    private RecipeSO _target;

    [SerializeField]
    List<RecipeItems> devTest;

    private void Start()
    {
        RecipeDeserialize(_target);
        Combine(devTest, null);
    }

    public void SetTarget(RecipeSO recipe) 
    {
        _target = recipe;
        RecipeDeserialize(_target);
    }

    private void RecipeDeserialize(RecipeSO target) 
    {
        _curentListRecipe.Add(target);
        for (int i = 0; i < target.ItemsForCraft.Count; i++)
        {
            var rec = _allRecipe.FirstOrDefault(x => x.Result == target.ItemsForCraft[i].Item);
            Debug.LogWarning(rec);
            if (rec != null)
                RecipeDeserialize(rec);

        }
    }

    public void Combine(List<RecipeItems> items, CardObj callback) 
    {
        var recipe = _curentListRecipe
            .Where(x => x.ItemsForCraft
                .Select(s => s.Item)
                .SequenceEqual(items.Select(s1 => s1.Item)))
            .FirstOrDefault();
        Debug.LogError(recipe);
        Debug.LogError(recipe.Result.Name);
    }
}
