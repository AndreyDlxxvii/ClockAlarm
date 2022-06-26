using TMPro;

public static class SetAlarmStaitment
{
    private static bool _isAlarmSet = false;
    private static bool _flag = false;

    public static bool SetAlarm(TMP_Text _alarmStatusText, TMP_Text _buttonSetAlarmText)
    {
        if (!_flag)
        {
            _isAlarmSet = true;
            _alarmStatusText.text = "Alarm set";
            _buttonSetAlarmText.text = "Alarm Cancel";
            _flag = true;
        }
        else
        {
            _isAlarmSet = false;
            _alarmStatusText.text = "Alarm cancel";
            _buttonSetAlarmText.text = "Alarm Set";
            _flag = false;
        }
        return _isAlarmSet;
    }
}
