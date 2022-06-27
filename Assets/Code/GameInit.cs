using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameInit
{
    public GameInit(Controller controllers, TMP_Text timeText, Button buttonShowAlarm, Transform hour, Transform minute,
        Transform second, Transform hourAlarm, Transform minuteAlarm,
        TMP_Text alarmText, Button setAlarm, Button buttonSetPmAm,
        TMP_InputField tmpInputField, TMP_Text alarmStatus)
    {
        var timeController = new TimeController(new GetTimeFromServer(), buttonShowAlarm, hour, minute, second, timeText);
        var alarmTimeController = new AlarmTimeController(buttonShowAlarm, hourAlarm, minuteAlarm, 
            alarmText, timeController, buttonSetPmAm, 
            setAlarm, tmpInputField, alarmStatus);
        
        
        controllers.Add(timeController);
        controllers.Add(alarmTimeController);
    }
}
