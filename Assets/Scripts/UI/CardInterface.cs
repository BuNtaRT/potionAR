using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInterface : MonoBehaviour
{
    public Text TempText;
    public Text TempNum;
    public Text ItemName;
    public Text Status;

    public void SetTemperatureText(string text) => TempText.text = text;
    public void SetTemperatureNum(string text) => TempNum.text = text+ "°";
    public void SetItemName(string text) => ItemName.text = text;
    public void SetStatus(string text) => Status.text = text;
}
