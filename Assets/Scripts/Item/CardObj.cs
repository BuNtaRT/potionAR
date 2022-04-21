using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardObj : MonoBehaviour
{
    [SerializeField]
    private ItemSO    _item;
    [SerializeField]
    private Transform _prefabContainer;
    private Transform _curentObj = null;

    private string _nameF;
    private string _name { 
        get { return _nameF; }
        set {
            _nameF = value;
            UICard.Instance.ChangeName(transform, _nameF); 
        } 
    }

    private int         _temperature = 0;
    public int Temperature
    {
        get { return _temperature; }
        set { 
            _temperature = Mathf.Clamp(value, -100, 100);
            UICard.Instance.ChangeTemp(transform,_temperature);
        }
    }
    public ItemSO GetItem() => _item;

    private void Start()
    {
        UICard.Instance.AddContent(transform);
        SetNewItem(_item);
    }

    public void SetNewItem(ItemSO item) 
    {
        _item = item;
        _temperature = _item.Temperature;
        _name = item.Name;
        _curentObj = ObjPool.Instance.SpawnObj(item.Type,Vector3.zero);
        _curentObj.SetParent(_prefabContainer);
    }

    public void ClearItem() 
    {
        _name = "";
        _temperature = 0;
        _item = null;
        UICard.Instance.SetDefaultUI(transform);
        ObjPool.Instance.Destroy(_item.Type, _curentObj.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_item != null)
        {
            if (other.gameObject.tag == Tags.ITEM)
            {
                Builder.Instance.AddItem(this);
            }
            else if (other.gameObject.tag == Tags.CRYSTAL) 
            {
                other.GetComponent<Crystal>().AddObj(this);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == Tags.CRYSTAL) 
        {
            bool operation = other.GetComponent<Crystal>().RemoveObj(this);
            if (_item == null && operation)
                UICard.Instance.SetDefaultUI(transform);
        }
    }
}
