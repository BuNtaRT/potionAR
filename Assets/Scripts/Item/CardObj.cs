using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardObj : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private ItemSO    _item;
    private ItemSO    _baseItem;
    [SerializeField]
    private Transform _prefabContainer;
    private Transform _curentObj = null;

    private string    _nameF;
    private string _name { 
        get { return _nameF; }
        set {
            _nameF = value;
            UICard.Instance.ChangeName(transform, _nameF); 
        } 
    }
    [SerializeField]
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

    #endregion

    private void Start()
    {
        UICard.Instance.AddContent(transform);
        SetNewItem(_item);
        _baseItem = _item;
    }

    public void SetDefault() => SetNewItem(_baseItem);

    //--------------------------простое изменение цвета, к сожалению не было моделек 
    public void SetFailStatus()
    {
        //- так же можно сделать путем создания скрипта на обьекте префаба и обращением к нему что бы он сам менял цвет у обьектов где они надо
        //- если все бы было mobile diffuce можно было бы создавать новый материал и задавать сериую 1х1 текстуру для всех обьектов префаба
        _item = null;
        var render = _curentObj.GetComponent<Renderer>();
        foreach (var item in render.materials)
        {
            Color iC = item.color;
            item.color = new Color(iC.r - 0.3f, iC.g - 0.3f, iC.b - 0.3f);
        }
        UICard.Instance.SetStatus(transform, "Испорчено");
    }
    //---------------------------------------------------------------------------------

    public void SetNewItem(ItemSO item/*,StatusItem status*/) 
    {
        /*if(status == StatusItem.Fail) 
            SetFailStatus();
        */
        //-----------------------------------------------------
        if (item.Type == TypeObj.BadPotion)                     //к сожалению найденные модельки очень не опимизированны
            UICard.Instance.SetStatus(transform, "Испорчено");   //+ они очень разные, у кого то mobile diffuse, где то просто много обьектов как наример у гриба
        else                                                    //грубая реализация(потому что на телефоне так бы делать не стал), по замене цвета материала
            UICard.Instance.SetStatus(transform, "В норме");    //находится выше и работает в случае fish.prefab 
        //-----------------------------------------------------

        if (_curentObj != null)
            ObjPool.Instance.Destroy(_item.Type, _curentObj.gameObject);
        _item = item;
        Temperature = _item.Temperature;
        _name = item.Name;
        _curentObj = ObjPool.Instance.SpawnObj(item.Type,Vector3.zero);
        _curentObj.SetParent(_prefabContainer);
    }

    public void ClearItem() 
    {
        if (_curentObj != null)
            ObjPool.Instance.Destroy(_item.Type, _curentObj.gameObject);
        _curentObj = null;
        _name = "";
        Temperature = 0;
        _item = null;
        UICard.Instance.SetDefaultUI(transform);
    }

    public void OnCardFound() => GlobalEventsManager.AddActiveCard(transform);
    public void OnCardLost()  => GlobalEventsManager.DisableCard(transform);
}
