using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    public  static Builder Instance { get; private set; }
    public  List<CardObj>   BlendedItem = new List<CardObj>();
    private bool           _isWait = false;
    [SerializeField]
    private RecipeBuilder  _recipeBuilder;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("Instance Builder over 1");
    }

    public void AddItem(CardObj item) 
    {
        if (!BlendedItem.Contains(item))
        {
            BlendedItem.Add(item);
            if(!_isWait)
                StartCoroutine(WaitMoreItems());
            
        }
    }

    private IEnumerator WaitMoreItems() {
        _isWait = true;
        yield return new WaitForSeconds(2);
        if (BlendedItem.Count != 0 && BlendedItem.Count > 1) 
        {
            List<RecipeItems> items = new List<RecipeItems>();
            foreach (var item in BlendedItem)
            {
                RecipeItems recipeItem;
                recipeItem.Item = item.GetItem();
                recipeItem.Temperature = item.Temperature;
                items.Add(recipeItem);
            }
            CardObj callback = BlendedItem[Random.Range(0, BlendedItem.Count - 1)];
            _recipeBuilder.Combine(items, callback);
        }
            
        BlendedItem.Clear();
        _isWait = false;
    }
}
