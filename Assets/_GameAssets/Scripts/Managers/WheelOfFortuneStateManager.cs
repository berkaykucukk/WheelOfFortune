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
        wheelOfFortuneEvents.TriggerOnSpinButtonClickedEvent();
    }

    public void TriggerSpinReadyEvent(int numberOfItems, float duration, int numberRotate,
        AnimationCurve curveSpin)
    {
        wheelOfFortuneEvents.TriggerSpinReadyEvent(numberOfItems, duration, numberRotate, curveSpin);
    }

    public void TriggerCreateItemsEvent(int numberOfItems)
    {
        wheelOfFortuneEvents.TriggerOnCreateWheelItemsEvent(numberOfItems);
    }
}