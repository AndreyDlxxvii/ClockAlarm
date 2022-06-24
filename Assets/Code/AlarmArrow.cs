using System;
using TMPro;
using UnityEngine;

public class AlarmArrow : MonoBehaviour
{

    private const string _minuteName = "Minute";
    private Transform _parentTransform;
    private float hour;
    private float minute;
    private void OnMouseDrag()
    {
        _parentTransform = transform.parent;
        Vector3 rot = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _parentTransform.position;
        float rotate = Mathf.Atan2(rot.x, rot.y) * Mathf.Rad2Deg;
        _parentTransform.rotation = Quaternion.Euler(0f,0f, -rotate);
    }

    private void OnMouseUp()
    {
        if (transform.parent.CompareTag(_minuteName))
        {
            minute = Mathf.Round(60 - _parentTransform.rotation.eulerAngles.z / 6f);
           if (minute > 60)
            { 
                minute = 0;
            }
            _parentTransform.rotation = Quaternion.Euler(0f,0f,-minute*6f);
        }
        else
        {
            hour = Mathf.Round(12 - _parentTransform.rotation.eulerAngles.z / 30f);
            if (hour >= 12)
            { 
                hour = 0;
            }
            _parentTransform.rotation = Quaternion.Euler(0f,0f,-hour*30f);
        }
    }
}
