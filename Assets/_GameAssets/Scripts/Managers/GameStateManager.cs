using System.Collections;
using System.Collections.Generic;
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

    public void TriggerSpinReadyEvent(int numberOfItems, float duration, int numberRotate,
        AnimationCurve curveSpin)
    {
        gameStateEvents.TriggerSpinReadyEvent(numberOfItems, duration, numberRotate, curveSpin);
    }

    public void TriggerCreateItemsEvent(WheelItemsContentData contentDataCurrent)
    {
        gameStateEvents.TriggerOnCreateWheelItemsEvent(contentDataCurrent);
    }

    public void TriggerOnWheelRotateDone(int itemIndex)
    {
        gameStateEvents.TriggerOnWheelRotateDoneEvent(itemIndex);
    }

    public void TriggerOnWheelItemsSpawned(List<WheelItemData> itemsCurrentlySpawned)
    {
        gameStateEvents.TriggerOnWheelItemsSpawnedEvent(itemsCurrentlySpawned);
    }
}