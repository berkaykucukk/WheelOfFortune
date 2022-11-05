using System.Collections;
using System.Collections.Generic;
using SystemPersonel;
using UnityEngine;

public class WheelOfFortuneStateManager : Singleton<WheelOfFortuneStateManager>
{
    #region PRIVATE PROPERITES



    private WheelZoneStates stateCurrent;

    #endregion


    #region INSPECTOR PROPERTIES

    [SerializeField] private WheelZoneStates stateBeginning;
    [SerializeField] private WheelOfFortuneEvents wheelOfFortuneEvents;

    #endregion

    #region PUBLIC PROPERTIES

    public WheelZoneStates StateCurrent => stateCurrent;

    #endregion

    protected override void Awake()
    {
        base.Awake();
        stateCurrent = stateBeginning;
    }

    public void TriggerSpinButtonClickEvent()
    {
        wheelOfFortuneEvents.TriggerOnSpinButtonClickedEvent();
    }

    public void TriggerSpinReadyEvent(int numberOfItems, float duration, int numberRotate,
        AnimationCurve curveSpin)
    {
        wheelOfFortuneEvents.TriggerSpinReadyEvent(numberOfItems, duration, numberRotate, curveSpin);
    }

    public void TriggerCreateItemsEvent(WheelItemsContentData contentDataCurrent)
    {
        wheelOfFortuneEvents.TriggerOnCreateWheelItemsEvent(contentDataCurrent);
    }
}