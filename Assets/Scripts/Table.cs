using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour
{
    [SerializeField]
    private Text      _name;
    [SerializeField]
    private Transform _prefabContainer;
    private Transform _itemPrefab;
    [SerializeField]
    private ItemSO    _item;

    private void Awake() => GlobalEventsManager.OnTargetComplite.AddListener(OnComplite);

    private void OnComplite(RecipeSO recipe, float clearFactor) => ClearTable();

    private void SetItem(ItemSO item) 
    {
        _item = item;
        _name.text = item.Name;
        _itemPrefab = ObjPool.Instance.SpawnObj(item.Type, Vector3.zero);
        _itemPrefab.SetParent(_prefabContainer);
    }

    private void RemoveItem() 
    {
        ObjPool.Instance.Destroy(_item.Type,_itemPrefab.gameObject);
        _item = null;
        _name.text = "";
    }

    public void ClearTable() 
    {
        if (_item != null)
            RemoveItem();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.ITEM)) 
        {
            CardObj item = other.GetComponent<CardObj>();
            if (item.GetItem() == null && _item != null)
            {
                item.SetNewItem(_item);
                RemoveItem();
            }
            else if (item.GetItem() != null && _item == null) 
            {
                SetItem(item.GetItem());
                item.ClearItem();
            }
        }
    }
}
