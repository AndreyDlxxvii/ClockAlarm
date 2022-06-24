using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : IOnController, IOnUpdate, IDisposable
{
    public int[] Time => _time;

    private Transform _hour;
    private Transform _minute;
    private Transform _second;
    private TMP_Text _timeText;
    private GetTimeFromServer _getTimeFromServer;

    private int[] _time;
    private string _timeString;
    private MyCoroutine _coroutine;
    private bool _flag = true;
    private Button _showAlarm;
    public TimeController(GetTimeFromServer getTimeFromServer, Button buttonShowAlarm, Transform hour, Transform minute,
        Transform second, TMP_Text tmpText)
    {
        _showAlarm = buttonShowAlarm;
        _hour = hour;
        _minute = minute;
        _second = second;
        _timeText = tmpText;
        _getTimeFromServer = getTimeFromServer;
        _time = _getTimeFromServer.GetTime();
        TimerCount();
        _coroutine = new MyCoroutine();
        _coroutine.StartMyCoroutine(1f);
        _coroutine.End += () => TimerCount();
        
        _showAlarm.onClick.AddListener(SetActiveArrows);

    }
    private void TimerCount()
    {
        if (_time[2] < 59)
        {
            _time[2]++;
        }
        else
        {
            _time[2] = 0;
            _time[1]++;
            if (_time[1] == 60)
            {
                _time = _getTimeFromServer.GetTime();
                _time[1] = 0;
                _time[0]++;
            }

            if (_time[0] == 24)
            {
                _time[0] = 0;
            }
        }
        _hour.rotation = Quaternion.Euler(0f, 0f,-_time[0] * 30f + -_time[1]/2f);
        _minute.rotation = Quaternion.Euler(0f, 0f,-_time[1] * 6f + -_time[2]/12f);
        _second.rotation = Quaternion.Euler(0f, 0f,-_time[2] * 6f);
        _timeString = $"{_time[0]:D2}:{_time[1]:D2}:{_time[2]:D2}";
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

    public void OnUpdate(float deltaTime)
    {
        float ratio = (float) Screen.height / Screen.width;
        float ortSize = Screen.width * ratio / 200f;
        Camera.main.orthographicSize = ortSize;
    }
    
    

    public void Dispose()
    {
        _coroutine.End -= () => TimerCount();
        _coroutine.StopMyCoroutine();
        _showAlarm.onClick.RemoveAllListeners();
    }
}
