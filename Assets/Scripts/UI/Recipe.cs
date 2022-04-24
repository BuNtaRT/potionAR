using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    public void Open(bool open) => GlobalEventsManager.InvokPause(PauseStatus.MenuOpen, open);
}
