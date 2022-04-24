using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObjInteraction : MonoBehaviour
{
    [SerializeField]
    private BuilderItem     _itemBuilder;
    private List<Transform> _visibleItems = new List<Transform>();

    #region Events
    private void Awake()
    {
        GlobalEventsManager.OnActiveCard.AddListener(AddCard);
        GlobalEventsManager.OnDisableCard.AddListener(RemoveCard);
    }
    public void AddCard(Transform card) =>    _visibleItems.Add(card);
    public void RemoveCard(Transform card) => _visibleItems.Remove(card);
    #endregion

    [SerializeField]
    private int _updateSteap = 20;
    private int _updateCount = 0;
    private int _i;
    private void Update()
    {
        _updateCount++;
        if (_updateCount >= _updateSteap)
        {
            _updateCount = 0;
            if (_visibleItems.Count >= 2)
            {
                if (_i >= _visibleItems.Count)
                    _i = 0;
                if (_visibleItems[_i].CompareTag(Tags.ITEM))
                    foreach (var item in _visibleItems)
                    {
                        if (item != _visibleItems[_i])
                        {
                            var distance = (item.position - _visibleItems[_i].position).sqrMagnitude;
                            if (distance <= 1.5f && item.CompareTag(_visibleItems[_i].tag))
                            {
                                _itemBuilder.AddToCraft(_visibleItems[_i].GetComponent<CardObj>());
                                _itemBuilder.AddToCraft(item.GetComponent<CardObj>());
                            }
                        }
                    }
                _i++;
            }
        }
    }
}
