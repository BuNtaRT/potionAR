using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICard : MonoBehaviour
{
    public static UICard Instance { get; private set; }
    private Dictionary<Transform, CardInterface> _interfacePool = new Dictionary<Transform, CardInterface>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("Instance UICard over 1");
    }

    public void AddContent(Transform card) 
    {
        try
        {
            CardInterface tempInterface = card.Find("Content").GetComponent<CardInterface>();
            _interfacePool.Add(card, tempInterface);
        }
        catch (Exception e) { Debug.LogError(e.Message); }
    }

    public void ChangeTemp(Transform card,int temp) 
    {
        CardInterface tempCardInterface;
        if (_interfacePool.TryGetValue(card, out tempCardInterface))
        {
            var text = Temperarute.GetTemperature(temp);
            tempCardInterface.SetTemperatureNum(temp.ToString());
            tempCardInterface.SetTemperatureText(text.ToString());
        }
    }

    public void ChangeName(Transform card, string name) 
    {
        CardInterface tempCardInterface;
        if (_interfacePool.TryGetValue(card, out tempCardInterface))
        {
            tempCardInterface.SetItemName(name);
        }
    }

    public void SetStatus(Transform card, string status)
    {
        CardInterface tempCardInterface;
        if (_interfacePool.TryGetValue(card, out tempCardInterface))
        {
            tempCardInterface.SetStatus(status);
        }
    }

    public void SetDefaultUI(Transform card) 
    {
        CardInterface tempCardInterface;
        if (_interfacePool.TryGetValue(card, out tempCardInterface))
        {
            tempCardInterface.SetStatus("");
            tempCardInterface.SetItemName("");
            tempCardInterface.SetTemperatureNum("0");
            tempCardInterface.SetTemperatureText("");
        }
    }
}
