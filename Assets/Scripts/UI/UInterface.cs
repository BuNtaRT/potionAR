using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UInterface : MonoBehaviour
{
    [SerializeField]
    private Text  _money;
    [SerializeField]
    private Image _targetIco;
    [SerializeField]
    private Text  _targetName;
    [SerializeField]
    private Text  _targetMoney;

    private void Awake()
    {
        GlobalEventsManager.OnTargetSet.AddListener(SetNewItem);
        GlobalEventsManager.OnMoneyChange.AddListener(SetMoney);
    }

    public void SetMoney(int money) => _money.text = money.ToString() + "$";

    private void SetNewItem(RecipeSO item) 
    {
        _targetIco.sprite = item.Result.Ico;
        _targetName.text = item.Result.Name;
    }

    public void SetTargetMoney(int money) => _targetMoney.text = "Цель: " + money + "$";  
}
