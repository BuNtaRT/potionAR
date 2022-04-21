using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New lvl target", menuName = "Lvl/Lvl target")]
public class LvlTargetSO : ScriptableObject
{
    public List<IItem> TargetItems = new List<IItem>();
}