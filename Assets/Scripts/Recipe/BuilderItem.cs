using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderItem : MonoBehaviour
{

    private List<CardObj> _itemForBuild = new List<CardObj>();
    private bool          _isWait = false;
    [SerializeField]
    private RecipeBuilder _recipeBuilder;

    public void AddToCraft(CardObj item) 
    {
        Debug.LogError(item.GetItem() + " " + item);
        if (!_itemForBuild.Contains(item) && item.GetItem() != null)
        {
            Debug.LogWarning(item.GetItem() + " " + item);
            _itemForBuild.Add(item);
            if(!_isWait)
                StartCoroutine(WaitMoreItems());
        }
    }

    private IEnumerator WaitMoreItems() {
        _isWait = true;
        yield return new WaitForEndOfFrame();
        if (_itemForBuild.Count > 1) 
        {

            List<RecipeItems> items = new List<RecipeItems>();
            foreach (var item in _itemForBuild)
            {
                RecipeItems recipeItem;
                recipeItem.Item = item.GetItem();
                recipeItem.Temperature = item.Temperature;
                items.Add(recipeItem);
            }
            _recipeBuilder.Combine(items, _itemForBuild);
        }

        _itemForBuild.Clear();
        _isWait = false;
    }
}
