using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventsManager : MonoBehaviour
{
    private static bool  _pauseStatus = false;
    public static UnityEvent<PauseStatus,bool> OnPause = new UnityEvent<PauseStatus,bool>();
    public static void InvokPause(PauseStatus status, bool enable)
    {
        if (enable != _pauseStatus)
        {
            _pauseStatus = enable;
            OnPause.Invoke(status, enable);
        }
    }
    //---------------------------------------------------------------------------------------------------
    public static UnityEvent<Transform> OnActiveCard = new UnityEvent<Transform>();
    public static void AddActiveCard(Transform card) => OnActiveCard.Invoke(card);
    //------------------------------------------------
    public static UnityEvent<Transform> OnDisableCard = new UnityEvent<Transform>();
    public static void DisableCard(Transform card) => OnDisableCard.Invoke(card);
    //---------------------------------------------------------------------------------------------------
    public static UnityEvent<RecipeSO, float> OnTargetComplite = new UnityEvent<RecipeSO, float>();
    public static void InvokeTargetComplite(RecipeSO item, float clearFactor) => OnTargetComplite.Invoke(item, clearFactor);
    //---------------------------------------------------------------------------------------------------
    public static UnityEvent<RecipeSO> OnTargetSet = new UnityEvent<RecipeSO>();
    public static void InvokeTargetSet(RecipeSO item) => OnTargetSet.Invoke(item);
    //---------------------------------------------------------------------------------------------------
    public static UnityEvent<int> OnMoneyChange = new UnityEvent<int>();
    public static void InvokeMoneyChange(int money) => OnMoneyChange.Invoke(money);
}

