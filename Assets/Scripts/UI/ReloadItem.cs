using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadItem : MonoBehaviour
{
    private List<CardObj> _allCards = new List<CardObj>();

    private void Start()
    {
        var cardsGamesObj = GameObject.FindGameObjectsWithTag(Tags.ITEM);
        foreach (var item in cardsGamesObj)
        {
            CardObj temp;
            item.TryGetComponent<CardObj>(out temp);
            if (temp != null)
                _allCards.Add(temp);
        }
    }

    public void Reload() 
    {
        foreach (var item in _allCards)
        {
            item.SetDefault();
        }
    }
}
