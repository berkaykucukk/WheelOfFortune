using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "WheelOfFortuneEvents", menuName = "Events/WheelOfFortuneEvents", order = 1)]
public class GameStateEvents : ScriptableObject
{
    #region ACTIONS

    public Action OnSpinButtonClicked;
    public Action<float, int, Ease> OnSpinReady;
    public Action<WheelItemsContentData> OnCreateWheelItems;
    public Action OnWheelRotateDone;
    public Action OnWheelItemsCreated;
    public Action<int, Image> OnCollectAreaIconCreate;
    public Action<int, int> OnCollectAreaValueUpdate;
    public Action OnCheckNextSpin;
    public Action OnChangeWheelState;
    public Action OnIncreaseWheelItemValues;
    public Action OnGameOver;
    public Action OnResetGame;
    public Action OnPlayAgain;
    public Action OnCollectItems;

    #endregion


    #region EVENTS TRIGGER METHODS

    public void TriggerSpinReadyEvent(float duration, int numberRotate,
        Ease easeSpin) => OnSpinReady?.Invoke(duration, numberRotate, easeSpin);

    public void TriggerOnSpinButtonClickedEvent() => OnSpinButtonClicked?.Invoke();

    public void TriggerOnCreateWheelItemsEvent(WheelItemsContentData contentDataCurrent) =>
        OnCreateWheelItems?.Invoke(contentDataCurrent);

    public void TriggerOnWheelRotateDoneEvent() => OnWheelRotateDone?.Invoke();

    public void TriggerOnWheelItemsCreatedEvent() =>
        OnWheelItemsCreated?.Invoke();

    public void TriggerOnCollectAreaIconCreateEvent(int id, Image icon) => OnCollectAreaIconCreate?.Invoke(id, icon);

    public void TriggerOnCollectAreaValueUpdateEvent(int itemId, int value) =>
        OnCollectAreaValueUpdate?.Invoke(itemId, value);

    public void TriggerOnCheckNextSpinEvent() => OnCheckNextSpin?.Invoke();
    public void TriggerOnChangeWheelStateEvent() => OnChangeWheelState?.Invoke();

    public void TriggerOnIncreaseWheelItemValuesEvent() => OnIncreaseWheelItemValues?.Invoke();

    public void TriggerGameOverEvent() => OnGameOver?.Invoke();
    public void TriggerGameResetEvent() => OnResetGame?.Invoke();
    public void TriggerPlayAgainEvent() => OnPlayAgain?.Invoke();
    public void TriggerOnCollectItems() => OnCollectItems?.Invoke();

    #endregion
}