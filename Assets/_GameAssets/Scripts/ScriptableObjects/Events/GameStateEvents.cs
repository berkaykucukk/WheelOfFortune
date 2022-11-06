using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "WheelOfFortuneEvents", menuName = "Events/WheelOfFortuneEvents", order = 1)]
public class GameStateEvents : ScriptableObject
{
    #region ACTIONS

    public Action OnSpinButtonClicked;
    public Action<WheelItemsContentData, float, int, Ease> OnSpinReady;
    public Action<WheelItemsContentData> OnCreateWheelItems;
    public Action OnWheelRotateDone;
    public Action OnWheelItemsSpawned;

    #endregion


    #region EVENTS TRIGGER METHODS

    public void TriggerSpinReadyEvent(WheelItemsContentData contentWheelItems, float duration, int numberRotate,
        Ease easeSpin) => OnSpinReady?.Invoke(contentWheelItems, duration, numberRotate, easeSpin);

    public void TriggerOnSpinButtonClickedEvent() => OnSpinButtonClicked?.Invoke();

    public void TriggerOnCreateWheelItemsEvent(WheelItemsContentData contentDataCurrent) =>
        OnCreateWheelItems?.Invoke(contentDataCurrent);

    public void TriggerOnWheelRotateDoneEvent() => OnWheelRotateDone?.Invoke();

    public void TriggerOnWheelItemsSpawnedEvent() =>
        OnWheelItemsSpawned?.Invoke();

    #endregion
}