using UnityEngine;

public class AlarmArrow : MonoBehaviour
{
    private const string MinuteName = "Minute";
    private Transform _parentTransform;
    private float _hour;
    private float _minute;
    private void OnMouseDrag()
    {
        _parentTransform = transform.parent;
        Vector3 rot = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _parentTransform.position;
        float rotate = Mathf.Atan2(rot.x, rot.y) * Mathf.Rad2Deg;
        _parentTransform.rotation = Quaternion.Euler(0f,0f, -rotate);
    }

    private void OnMouseUp()
    {
        if (transform.parent.CompareTag(MinuteName))
        {
            _minute = Mathf.Round(60 - _parentTransform.rotation.eulerAngles.z / 6f);
           if (_minute > 60)
            { 
                _minute = 0;
            }
            _parentTransform.rotation = Quaternion.Euler(0f,0f,-_minute*6f);
        }
        else
        {
            _hour = Mathf.Round(12 - _parentTransform.rotation.eulerAngles.z / 30f);
            if (_hour >= 12)
            { 
                _hour = 0;
            }
            _parentTransform.rotation = Quaternion.Euler(0f,0f,-_hour*30f);
        }
    }
}
