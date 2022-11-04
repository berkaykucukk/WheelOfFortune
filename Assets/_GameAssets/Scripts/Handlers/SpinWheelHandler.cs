using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWheelHandler : MonoBehaviour
{
    #region INSPECTOR PROPERTIES

    [SerializeField] private int numberOfSpinWheelItems = 8;
    [SerializeField] private float durationSpin = 2f;
    [SerializeField] private int countRotate = 3;
    [SerializeField] private AnimationCurve spinCurve;
    #endregion

    #region PUBLIC PROPERTIES

    public int NumberOfSpinWheelItems => numberOfSpinWheelItems;

    #endregion
}