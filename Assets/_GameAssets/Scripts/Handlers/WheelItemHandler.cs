using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WheelItemHandler : MonoBehaviour
{
    #region INSPECTOR PROPERTIES

    [SerializeField] private TextMeshProUGUI valueText;

    #endregion


    public void SetValue(float value)
    {
        valueText.text = value.ToString();
    }
}