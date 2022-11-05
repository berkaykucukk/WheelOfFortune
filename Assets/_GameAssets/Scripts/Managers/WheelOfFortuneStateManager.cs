using System.Collections;
using System.Collections.Generic;
using SystemPersonel;
using UnityEngine;

public class WheelOfFortuneStateManager : Singleton<WheelOfFortuneStateManager>
{
    #region PRIVATE PROPERITES

    public enum StatesWheel
    {
        bronze,
        silver,
        gold
    }

    private StatesWheel stateCurrent;

    #endregion


    #region INSPECTOR PROPERTIES

    [SerializeField] private StatesWheel stateBeginning;
    [SerializeField] private WheelOfFortuneEvents wheelOfFortuneEvents;

    #endregion

    #region PUBLIC PROPERTIES

    public StatesWheel StateCurrent => stateCurrent;

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

    public void TriggerCreateItemsEvent(int numberOfItems, WheelItemsContentData contentDataCurrent)
    {
        wheelOfFortuneEvents.TriggerOnCreateWheelItemsEvent(numberOfItems, contentDataCurrent);
    }
}