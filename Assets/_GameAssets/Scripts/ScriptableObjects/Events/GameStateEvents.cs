using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "WheelOfFortuneEvents", menuName = "Events/WheelOfFortuneEvents", order = 1)]
public class GameStateEvents : ScriptableObject
{
    #region ACTIONS

    public Action OnSpinButtonClicked;
    public Action<int, float, int, AnimationCurve> OnSpinReady;
    public Action<WheelItemsContentData> OnCreateWheelItems;
    public Action<int> OnWheelRotateDone;
    public Action<List<WheelItemData>> OnWheelItemsSpawned;

    #endregion


    #region EVENTS TRIGGER METHODS

    public void TriggerSpinReadyEvent(int numberOfItems, float duration, int numberRotate,
        AnimationCurve curveSpin) => OnSpinReady?.Invoke(numberOfItems, duration, numberRotate, curveSpin);

    public void TriggerOnSpinButtonClickedEvent() => OnSpinButtonClicked?.Invoke();

    public void TriggerOnCreateWheelItemsEvent(WheelItemsContentData contentDataCurrent) =>
        OnCreateWheelItems?.Invoke(contentDataCurrent);

    public void TriggerOnWheelRotateDoneEvent(int itemIndex) => OnWheelRotateDone?.Invoke(itemIndex);

    public void TriggerOnWheelItemsSpawnedEvent(List<WheelItemData> itemsCurrentlySpawned) =>
        OnWheelItemsSpawned?.Invoke(itemsCurrentlySpawned);
    
    

    #endregion
}