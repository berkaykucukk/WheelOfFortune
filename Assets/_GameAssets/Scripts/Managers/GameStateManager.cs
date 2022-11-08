using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using SystemPersonel;
using UnityEngine;
using UnityEngine.UI;

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
        Application.targetFrameRate = 60;
    }

    public void SetWheelState(WheelZoneStates wheelZoneState)
    {
        stateCurrent = wheelZoneState;
    }

    public void TriggerSpinButtonClickEvent()
    {
        gameStateEvents.TriggerOnSpinButtonClickedEvent();
    }

    public void TriggerSpinReadyEvent(float duration, int numberRotate,
        Ease easeSpin)
    {
        gameStateEvents.TriggerSpinReadyEvent(duration, numberRotate, easeSpin);
    }

    public void TriggerCreateItemsEvent(WheelItemsContentData contentDataCurrent)
    {
        gameStateEvents.TriggerOnCreateWheelItemsEvent(contentDataCurrent);
    }

    public void TriggerOnWheelRotateDone()
    {
        gameStateEvents.TriggerOnWheelRotateDoneEvent();
    }

    public void TriggerOnWheelItemsCreatedEvent()
    {
        gameStateEvents.TriggerOnWheelItemsCreatedEvent();
    }

    public void TriggerOnCollectAreaIconCreateEvent(int id, Image icon)
    {
        gameStateEvents.TriggerOnCollectAreaIconCreateEvent(id, icon);
    }

    public void TriggerOnCollectAreaValueUpdateEvent(int itemId, int value)
    {
        gameStateEvents.TriggerOnCollectAreaValueUpdateEvent(itemId, value);
    }

    public void TriggerOnCheckNextSpinEvent()
    {
        gameStateEvents.TriggerOnCheckNextSpinEvent();
    }

    public void TriggerOnChangeWheelStateEvent()
    {
        gameStateEvents.TriggerOnChangeWheelStateEvent();
    }

    public void TriggerOnIncreaseWheelItemValuesEvent()
    {
        gameStateEvents.TriggerOnIncreaseWheelItemValuesEvent();
    }

    [Button("GameOVer")]
    public void GameOver()
    {
        TriggerGameResetEvent();
        gameStateEvents.TriggerGameOverEvent();
    }

    private void ResetGame()
    {
        stateCurrent = stateBeginning;
    }

    public void TriggerGameResetEvent()
    {
        ResetGame();
        gameStateEvents.TriggerGameResetEvent();
    }

    public void TriggerPlayAgainEvent()
    {
        gameStateEvents.TriggerPlayAgainEvent();
    }

    public void TriggerCollectItemsEvent()
    {
        gameStateEvents.TriggerOnCollectItems();
    }
}