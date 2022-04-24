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
    private RecipeSO       _target;
    [SerializeField]
    private ItemSO         _badPotion;
    private float          _clearFactor = 1;

    private void Awake()
    {
        _allRecipe = Resources.LoadAll<RecipeSO>(ResourcePath.RECIPE).ToList();
        GlobalEventsManager.OnPause.AddListener(OnPause);
    }

    private void Start() => SetNewTarget();

    private void OnPause(PauseStatus status, bool enable) 
    {
        if ((status == PauseStatus.LvlFail || status == PauseStatus.LvlComplite) && !enable) 
            SetNewTarget();
    } 

    private void ItemComplite() 
    {
        GlobalEventsManager.InvokeTargetComplite(_target, _clearFactor);
        SetNewTarget();
    }

    private void SetNewTarget() 
    {
        _clearFactor = 1;
        _target = _allRecipe[Random.Range(0, _allRecipe.Count)];
        GlobalEventsManager.InvokeTargetSet(_target);
        _curentListRecipe = new List<RecipeSO>();
        RecipeDeserialize(_target);
    }

    private void RecipeDeserialize(RecipeSO target) 
    {
        _curentListRecipe.Add(target);
        for (int i = 0; i < target.ItemsForCraft.Count; i++)
        {
            var rec = _allRecipe.FirstOrDefault(x => x.Result == target.ItemsForCraft[i].Item);
            if (rec != null)
                RecipeDeserialize(rec);
        }
    }

    private void SetItemOnCard(List<CardObj> cards, ItemSO item) 
    {
        cards[0].SetNewItem(item);
        cards.RemoveAt(0);
        foreach (var card in cards)
        {
            card.ClearItem();
        }
    }

    public void Combine(List<RecipeItems> items, List<CardObj> callback) 
    {
        var recipe = _curentListRecipe
            .Where(x => x.ItemsForCraft.OrderBy(q => q.Item.Name)
                .Select(s => s.Item)
                .SequenceEqual(items.OrderBy(q1 => q1.Item.Name).Select(s1 => s1.Item)))
            .FirstOrDefault();


        Debug.Log("recipe found " + recipe);
        if (recipe != null)
        {
            float clearFactorBackup = _clearFactor;
            foreach (var itemFC in items)
            {
                TempResult tempEnumItem = Temperarute.GetTemperature(itemFC.Temperature);
                var itemRec = recipe.ItemsForCraft
                    .Where(i => i.Item == itemFC.Item)
                    .Where(t => Temperarute.GetTemperature(t.Temperature) == Temperarute.GetTemperature(itemFC.Temperature))
                    .FirstOrDefault();

                if (itemRec.Item == null)
                {
                    SetItemOnCard(callback,_badPotion);
                    _clearFactor = clearFactorBackup;
                    return;
                }

                _clearFactor -= (itemFC.Temperature - itemRec.Temperature) / 100;
            }

            SetItemOnCard(callback, recipe.Result);
            if (recipe == _target)
                ItemComplite();
        }
        else 
        {
            SetItemOnCard(callback,_badPotion);
        }
    }
}
