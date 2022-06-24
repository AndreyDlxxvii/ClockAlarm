
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

    private Transform _hourAlarm;
    private Transform _minuteAlarm;
    
    private Button _buttonShowAlarm;
    private Button _buttonSetAlarm;
    private Button _buttonSetPmAm;
    
    private TMP_Text _hourAlarmText;
    private TMP_Text _minuteAlarmText;
    private TMP_Text _alarmStatus;
    private TMP_InputField _inputField;
    private TimeController _timeController;
    
    private int _hour;
    private int _minute;
    private int i = 0;
    private int j = 0;

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
        _buttonSetAlarm.onClick.AddListener(SetAllarm);
        _buttonSetPmAm.onClick.AddListener(SetPmAm);
        _inputField.onDeselect.AddListener((num => { CheckSettingAlarm(num); }) );
    }

    private void CheckSettingAlarm(string num)
    {
        Regex myReg = new Regex(@"\d{2}:\d{2}");
        if (myReg.IsMatch(num))
        {
            string[] alarm = num.Split(':');
            for (int k = 0; k < alarm.Length; k++)
            {
                var number = int.Parse(alarm[k]);
                if (number <= 24 && k == 0)
                {
                    _hourAlarm.rotation = Quaternion.Euler(0f, 0f, -number * 30f);
                    if (number > 12)
                    {
                        SetPmAm();
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
    }
    private void SetActiveAlarmInterface()
    {
        
        if (!_flag)
        {
            _flag = true;
        }
        else
        {
            _flag = false;
        }
        _hourAlarm.gameObject.SetActive(_flag);
        _minuteAlarm.gameObject.SetActive(_flag);
        _hourAlarmText.gameObject.SetActive(_flag);
        _minuteAlarmText.gameObject.SetActive(_flag);
        _buttonSetAlarm.gameObject.SetActive(_flag);
        _buttonSetPmAm.gameObject.SetActive(_flag);
        _inputField.gameObject.SetActive(_flag);
        
    }

    private void SetAllarm()
    {
        if (i==0)
        {
            _setAlarm = true;
            _alarmStatus.text = "Alarm set";
            _buttonSetAlarm.GetComponentInChildren<TMP_Text>().text = "Alarm Cancel";
            i++;
        }
        else
        {
            _setAlarm = false;
            _alarmStatus.text = "Alarm cancel";
            _buttonSetAlarm.GetComponentInChildren<TMP_Text>().text = "Alarm Set";
            i = 0;
        }
    }

    private void SetPmAm()
    {
        if (j==0)
        {
            _setPmAm = true;
            _buttonSetPmAm.GetComponentInChildren<TMP_Text>().text = "PM";
            j++;
        }
        else
        {
            _setPmAm = false;
            _buttonSetPmAm.GetComponentInChildren<TMP_Text>().text = "AM";
            j = 0;
        }
    }
    public void OnUpdate(float deltaTime)
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
        
        if (_setAlarm && _timeController.Time[0] == _hour && _timeController.Time[1] == _minute)
        {
            Debug.Log("Дилиньк");
        }

        
    }
    
    public void Dispose()
    {
        _buttonShowAlarm.onClick.RemoveAllListeners();
        _buttonSetAlarm.onClick.RemoveAllListeners();
        _buttonSetPmAm.onClick.RemoveAllListeners();
    }
}
