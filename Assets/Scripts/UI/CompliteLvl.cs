using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompliteLvl : MonoBehaviour
{
    [SerializeField]
    private GameObject _finishWindow;
    [SerializeField]
    private GameObject _failText;
    [SerializeField]
    private GameObject _compliteText;
    private bool       _fail;

    private void Awake() => GlobalEventsManager.OnPause.AddListener(ShowFinishWindow);

    private void ShowFinishWindow(PauseStatus status, bool enable) 
    {
        if (status == PauseStatus.LvlComplite && enable)
        {
            _fail = false;
            _finishWindow.SetActive(true);
            _compliteText.SetActive(true);
            _failText.SetActive(false);
        }
        else if (status == PauseStatus.LvlFail && enable) 
        {
            _fail = true;
            _finishWindow.SetActive(true);
            _compliteText.SetActive(false);
            _failText.SetActive(true);
        }
    }

    public void Continue() 
    {
        if (_fail)
            GlobalEventsManager.InvokPause(PauseStatus.LvlFail, false);
        else
            GlobalEventsManager.InvokPause(PauseStatus.LvlComplite, false);
        _finishWindow.SetActive(false);
    }
}
