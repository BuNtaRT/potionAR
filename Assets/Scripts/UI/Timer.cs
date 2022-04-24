using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    [SerializeField]
    private Text        _timerText;
    [SerializeField]
    private float       _timeLeft = 0f;
    private Coroutine   _timer;
    private bool        _pause;
    public static Timer Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("Instance timer over 1");

        GlobalEventsManager.OnPause.AddListener(Pause);
        GlobalEventsManager.OnTargetSet.AddListener(StartTimer);
    }

    private void Pause(PauseStatus status, bool enable) =>  _pause = enable;

    private void StartTimer(RecipeSO itemSet)
    {
        _timeLeft = itemSet.TimeRecipe;

        if (_timer == null)
            _timer = StartCoroutine(TimerIE());
    }

    private IEnumerator TimerIE()
    {
        while (true)
        {
            if (!_pause)
            {
                _timeLeft -= Time.deltaTime;
                UpdateTimeText();
            }
            yield return null;
        }
    }

    private void UpdateTimeText()
    {
        if (_timeLeft < 0)
        {
            _timeLeft = 0;
            GlobalEventsManager.InvokPause(PauseStatus.LvlFail, true);
        }

        float minutes = Mathf.FloorToInt(_timeLeft / 60);
        float seconds = Mathf.FloorToInt(_timeLeft % 60);
        _timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    public void DevPlusTime() => _timeLeft += 30;

}
