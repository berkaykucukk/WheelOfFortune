using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "WheelOfFortuneEvents", menuName = "Events/WheelOfFortuneEvents", order = 1)]
public class WheelOfFortuneEvents : ScriptableObject
{
    #region ACTIONS

    public Action OnSpinButtonClicked;
    public Action<int, float, int, AnimationCurve> OnSpinReady;

    #endregion


    #region EVENTS TRIGGER METHODS

    public void TriggerSpinReadyEvent(int numberOfItems, float duration, int numberRotate,
        AnimationCurve curveSpin) => OnSpinReady?.Invoke(numberOfItems, duration, numberRotate, curveSpin);

    public void TriggerOnSpinButtonClickedEvent() => OnSpinButtonClicked?.Invoke();

    #endregion
}