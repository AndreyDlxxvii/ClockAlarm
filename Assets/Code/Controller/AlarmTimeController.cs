using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlarmTimeController : IOnController, IOnUpdate, IDisposable
{
    private bool _flag;
    private bool _setAlarm;
    private bool _setPmAm;

    private readonly Transform _hourAlarm;
    private readonly Transform _minuteAlarm;
    
    private readonly Button _buttonShowAlarm;
    private readonly Button _buttonSetAlarm;
    private readonly Button _buttonSetPmAm;
    
    private readonly TMP_Text _hourAlarmText;
    private readonly TMP_Text _minuteAlarmText;
    private readonly TMP_Text _alarmStatus;
    private readonly TMP_InputField _inputField;
    private readonly TimeController _timeController;
    
    private int _hour;
    private int _minute;

    public AlarmTimeController(Button buttonShowAlarm, Transform hourAlarm, Transform minuteAlarm,
        TMP_Text hourAlarmText, TMP_Text minuteAlarmText, TimeController timeController, Button buttonSetPmAm,
        Button buttonSetAlarm, TMP_InputField inputField, TMP_Text alarmStatus)
    {
        _alarmStatus = alarmStatus;
        _hourAlarm = hourAlarm;
        _minuteAlarm = minuteAlarm;
        _buttonShowAlarm = buttonShowAlarm;
        _hourAlarmText = hourAlarmText;
        _minuteAlarmText = minuteAlarmText;
        _timeController = timeController;
        _buttonSetAlarm = buttonSetAlarm;
        _buttonSetPmAm = buttonSetPmAm;
        _inputField = inputField;
        _buttonShowAlarm.onClick.AddListener(SetActiveAlarmInterface);
        _buttonSetAlarm.onClick.AddListener(() =>
        {
            _setAlarm = SetAlarmStaitment.SetAlarm(_alarmStatus, _buttonSetAlarm.GetComponentInChildren<TMP_Text>());
        });
        _buttonSetPmAm.onClick.AddListener( () =>
        {
            _setPmAm = SetAmPm.ChangePmAm(_buttonSetPmAm.GetComponentInChildren<TMP_Text>());
        });
        _inputField.onDeselect.AddListener(CheckSettingAlarm);
        
    }

    private void CheckSettingAlarm(string timeNum)
    {
        Regex myReg = new Regex(@"\d{2}:\d{2}");
        if (myReg.IsMatch(timeNum))
        {
            string[] alarm = timeNum.Split(':');
            for (int k = 0; k < alarm.Length; k++)
            {
                int number = int.Parse(alarm[k]);
                if (number <= 24 && k == 0)
                {
                    _hourAlarm.rotation = Quaternion.Euler(0f, 0f, -number * 30f);
                    if (number > 12 && !_setPmAm)
                    {
                        _setPmAm = SetAmPm.ChangePmAm(_buttonSetPmAm.GetComponentInChildren<TMP_Text>());
                    }

                    if (number == 24)
                    {
                        _hour = 0;
                    }
                }
                else if (number <= 60 && k ==1)
                {
                    _minuteAlarm.rotation = Quaternion.Euler(0f, 0f, -number * 6f);
                    if (number == 60)
                    {
                        _minute = 0;
                    }
                }
                else
                {
                    _inputField.text = "Wrong time use"; 
                }
            }

            _inputField.text = $"{_hour:D2}:{_minute:D2}";
        }
        else
        {
            _inputField.text = "Wrong format, use format 15:20";
        }

        _inputField.text = null;
    }
    private void SetActiveAlarmInterface()
    {
        
        _flag = !_flag;
        _hourAlarm.gameObject.SetActive(_flag);
        _minuteAlarm.gameObject.SetActive(_flag);
        _hourAlarmText.gameObject.SetActive(_flag);
        _minuteAlarmText.gameObject.SetActive(_flag);
        _buttonSetAlarm.gameObject.SetActive(_flag);
        _buttonSetPmAm.gameObject.SetActive(_flag);
        _inputField.gameObject.SetActive(_flag);
        
    }
    
    public void OnUpdate()
    {
        _hour = (int)Mathf.Round(12 - _hourAlarm.rotation.eulerAngles.z / 30f);
        _minute = (int) Mathf.Round(60 - _minuteAlarm.rotation.eulerAngles.z / 6f);
        if (_setPmAm)
        {
            _hour += 12;
        }
        if (_hour == 24 || _hour == 12)
        {
            _hour = 0;
        }
        if (_minute == 60)
        {
            _minute = 0;
        }

        _hourAlarmText.text = _hour.ToString("D2") + ':';
        _minuteAlarmText.text = _minute.ToString("D2");
        
        if (_setAlarm && _timeController.HourInt == _hour && _timeController.MinuteInt == _minute)
        {
            Debug.Log("Дилиньк");
        }
    }
    
    public void Dispose()
    {
        _buttonShowAlarm.onClick.RemoveAllListeners();
        _buttonSetAlarm.onClick.RemoveAllListeners();
        _buttonSetPmAm.onClick.RemoveAllListeners();
        _inputField.onDeselect.RemoveAllListeners();
    }
}
