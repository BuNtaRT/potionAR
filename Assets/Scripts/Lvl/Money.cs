using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField]
    private int _moneyTarget;
    private int _money;

    public UInterface Interface;

    private void Awake()
    {
        GlobalEventsManager.OnTargetComplite.AddListener(OnComplite);
        GlobalEventsManager.OnPause.AddListener(OnPause);
        SetNewTarget();
    }

    private void OnPause(PauseStatus status, bool enable) 
    {
        if (status == PauseStatus.LvlFail && !enable)
            SetNewTarget();
    }

    private void SetNewTarget()
    {
        _moneyTarget = Random.Range(1500, 3000);
        Interface.SetTargetMoney(_moneyTarget);
    }

    private void OnComplite(RecipeSO item, float clearFactor) 
    {
        _money += (int)(item.Price + item.Price * clearFactor);

        if (_money >= _moneyTarget) 
        {
            GlobalEventsManager.InvokPause(PauseStatus.LvlComplite, true);
            SetNewTarget();
            _money = 0;
        }
        GlobalEventsManager.InvokeMoneyChange(_money);
    }
}
