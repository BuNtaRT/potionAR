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
}

