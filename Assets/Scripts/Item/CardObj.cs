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

    //--------------------------������� ��������� �����, � ��������� �� ���� ������� 
    public void SetFailStatus()
    {
        //- ��� �� ����� ������� ����� �������� ������� �� ������� ������� � ���������� � ���� ��� �� �� ��� ����� ���� � �������� ��� ��� ����
        //- ���� ��� �� ���� mobile diffuce ����� ���� �� ��������� ����� �������� � �������� ������ 1�1 �������� ��� ���� �������� �������
        _item = null;
        var render = _curentObj.GetComponent<Renderer>();
        foreach (var item in render.materials)
        {
            Color iC = item.color;
            item.color = new Color(iC.r - 0.3f, iC.g - 0.3f, iC.b - 0.3f);
        }
        UICard.Instance.SetStatus(transform, "���������");
    }
    //---------------------------------------------------------------------------------

    public void SetNewItem(ItemSO item/*,StatusItem status*/) 
    {
        /*if(status == StatusItem.Fail) 
            SetFailStatus();
        */
        //-----------------------------------------------------
        if (item.Type == TypeObj.BadPotion)                     //� ��������� ��������� �������� ����� �� ��������������
            UICard.Instance.SetStatus(transform, "���������");   //+ ��� ����� ������, � ���� �� mobile diffuse, ��� �� ������ ����� �������� ��� ������� � �����
        else                                                    //������ ����������(������ ��� �� �������� ��� �� ������ �� ����), �� ������ ����� ���������
            UICard.Instance.SetStatus(transform, "� �����");    //��������� ���� � �������� � ������ fish.prefab 
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
