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
    public Action<WheelItemsContentData, float, int, Ease> OnSpinReady;
    public Action<WheelItemsContentData> OnCreateWheelItems;
    public Action OnWheelRotateDone;
    public Action OnWheelItemsCreated;
    public Action<int, Image> OnCollectAreaIconCreate;
    public Action<int, int> OnCollectAreaValueUpdate;
    public Action OnCheckNextSpin;
    public Action OnChangeWheelZone;
    public Action OnIncreaseWheelItemValues;

    #endregion


    #region EVENTS TRIGGER METHODS

    public void TriggerSpinReadyEvent(WheelItemsContentData contentWheelItems, float duration, int numberRotate,
        Ease easeSpin) => OnSpinReady?.Invoke(contentWheelItems, duration, numberRotate, easeSpin);

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
    public void TriggerOnChangeWheelStateEvent() => OnChangeWheelZone?.Invoke();

    public void TriggerOnIncreaseWheelItemValues() => OnIncreaseWheelItemValues?.Invoke();
    
    #endregion
}