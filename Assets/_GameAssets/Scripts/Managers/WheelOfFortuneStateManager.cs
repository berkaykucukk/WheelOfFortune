using System.Collections;
using System.Collections.Generic;
using SystemPersonel;
using UnityEngine;

public class WheelOfFortuneStateManager : Singleton<WheelOfFortuneStateManager>
{
    #region INSPECTOR PROPERTIES

    [SerializeField] private WheelOfFortuneEvents wheelOfFortuneEvents;

    #endregion

    public void TriggerSpinButtonClickEvent()
    {
        wheelOfFortuneEvents.OnSpinButtonClicked?.Invoke();
    }

    public void TriggerSpinReadyEvent(int numberOfItems, float duration, int numberRotate,
        AnimationCurve curveSpin)
    {
        wheelOfFortuneEvents.OnSpinReady?.Invoke(numberOfItems, duration, numberRotate, curveSpin);
    }
}