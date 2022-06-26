using TMPro;

public static class SetAmPm
{
    
    private static bool _flag = false;
    private static bool _isChange;
    
    public static bool ChangePmAm(TMP_Text textTMP)
    {
        if (!_flag)
        {
            _isChange = true;
            textTMP.text = "PM";
            _flag = true;
        }
        else
        {
            _isChange = false;
            textTMP.text = "AM";
            _flag = false;
        }
        return _isChange;
    }
}

