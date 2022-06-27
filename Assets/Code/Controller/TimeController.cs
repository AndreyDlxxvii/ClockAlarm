using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : IOnController, IOnUpdate, IDisposable
{
    public int HourInt => _hourInt;

    public int MinuteInt => _minuteInt;

    //public int SecondInt => _secondInt;

    private readonly Transform _hour;
    private readonly Transform _minute;
    private readonly Transform _second;
    private readonly TMP_Text _timeText;
    private int _hourInt;
    private int _minuteInt;
    private int _secondInt;
    private readonly GetTimeFromServer _getTimeFromServer;

    private TimeSpan _time;
    private string _timeString;
    private readonly MyCoroutine _coroutine;
    private bool _flag = true;
    private readonly Button _showAlarm;
    public TimeController(GetTimeFromServer getTimeFromServer, Button buttonShowAlarm, Transform hour, Transform minute,
        Transform second, TMP_Text tmpText)
    {
        _showAlarm = buttonShowAlarm;
        _hour = hour;
        _minute = minute;
        _second = second;
        _timeText = tmpText;
        _getTimeFromServer = getTimeFromServer;
        UpdateTime();
        TimerCount();
        _coroutine = new MyCoroutine();
        _coroutine.StartMyCoroutine(1f);
        _coroutine.End += () => TimerCount();
        
        _showAlarm.onClick.AddListener(SetActiveArrows);

    }
    private void TimerCount()
    {
        if (_secondInt < 59)
        {
            _secondInt++;
        }
        else
        {
            _secondInt = 0;
            _minuteInt++;
            if (_minuteInt == 60)
            {
                UpdateTime();
                _minuteInt = 0;
                _hourInt++;
            }

            if (_hourInt == 24)
            {
                _hourInt = 0;
            }
        }
        _hour.rotation = Quaternion.Euler(0f, 0f,-_hourInt * 30f + -_minuteInt/2f);
        _minute.rotation = Quaternion.Euler(0f, 0f,-_minuteInt * 6f + -_secondInt/12f);
        _second.rotation = Quaternion.Euler(0f, 0f,-_secondInt * 6f);
        _timeString = $"{_hourInt:D2}:{_minuteInt:D2}:{_secondInt:D2}";
        _timeText.text = _timeString;
    }

    private void SetActiveArrows()
    {
        
        if (!_flag)
        {
            _flag = true;
            _showAlarm.GetComponentInChildren<TMP_Text>().text = "Alarm";
        }
        else
        {
            _flag = false;
            _showAlarm.GetComponentInChildren<TMP_Text>().text = "Clock";
        }
        _hour.gameObject.SetActive(_flag);
        _minute.gameObject.SetActive(_flag);
        _second.gameObject.SetActive(_flag);
    }

    public void OnUpdate()
    {
        float ratio = (float) Screen.height / Screen.width;
        float ortSize = Screen.width * ratio / 200f;
        Camera.main.orthographicSize = ortSize;
    }

    private void UpdateTime()
    {
        _time = _getTimeFromServer.TryGetTimeFromServer();
        _hourInt = _time.Hours;
        _minuteInt = _time.Minutes;
        _secondInt = _time.Seconds;
    }
    public void Dispose()
    {
        _coroutine.End -= TimerCount;
        _coroutine.StopMyCoroutine();
        _showAlarm.onClick.RemoveAllListeners();
    }
}
