using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_Text _alarmStatus;
    [SerializeField] private TMP_Text _alarmText;
    [SerializeField] private TMP_InputField _inputField;
    
    [SerializeField] private Button _buttonShowAlarm;
    [SerializeField] private Button _buttonSetAlarm;
    [SerializeField] private Button _buttonSetPmAm;
    
    [SerializeField] private Transform _hour;
    [SerializeField] private Transform _minute;
    [SerializeField] private Transform _second;
    [SerializeField] private Transform _hourAlarm;
    [SerializeField] private Transform _minuteAlarm;
    
    
    
    private Controller _controllers;
    private void Start()
    {
        _controllers = new Controller();
        new GameInit(_controllers, _timeText, _buttonShowAlarm, _hour, _minute, _second,
            _hourAlarm, _minuteAlarm, _alarmText, _buttonSetAlarm, 
            _buttonSetPmAm, _inputField, _alarmStatus);
        _controllers.OnStart();
    }

    private void Update()
    {
        _controllers.OnUpdate();
    }
}