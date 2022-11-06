using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SystemPersonel;
using UnityEngine;

public class GameStateManager : Singleton<GameStateManager>
{
    #region PRIVATE PROPERITES

    private WheelZoneStates stateCurrent;

    #endregion


    #region INSPECTOR PROPERTIES

    [SerializeField] private WheelZoneStates stateBeginning;
    [SerializeField] private GameStateEvents gameStateEvents;

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
        gameStateEvents.TriggerOnSpinButtonClickedEvent();
    }

    public void TriggerSpinReadyEvent(WheelItemsContentData contentWheelItems, float duration, int numberRotate,
        Ease easeSpin)
    {
        gameStateEvents.TriggerSpinReadyEvent(contentWheelItems, duration, numberRotate, easeSpin);
    }

    public void TriggerCreateItemsEvent(WheelItemsContentData contentDataCurrent)
    {
        gameStateEvents.TriggerOnCreateWheelItemsEvent(contentDataCurrent);
    }

    public void TriggerOnWheelRotateDone()
    {
        gameStateEvents.TriggerOnWheelRotateDoneEvent();
    }

    public void TriggerOnWheelItemsSpawned()
    {
        gameStateEvents.TriggerOnWheelItemsSpawnedEvent();
    }
}