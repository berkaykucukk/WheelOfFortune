using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GameEventsListener : MonoBehaviour
{
    #region EVENTS

    public Action onSpinButtonClicked;
    public Action<int, float, int, AnimationCurve> onSpinReady;
    public Action<WheelItemsContentData> onCreateItems;
    public Action<int> onWheelRotateDone;
    public Action<List<WheelItemData>> onWheelItemsSpawned;

    #endregion

    #region INSPECTOR PROPERTIES

    [SerializeField] private GameStateEvents gameStateEvents;

    #endregion

    #region UNITY METHODS

    private void OnEnable()
    {
        gameStateEvents.OnSpinButtonClicked += OnSpinButtonClicked;
        gameStateEvents.OnSpinReady += OnSpinReady;
        gameStateEvents.OnCreateWheelItems += OnCreateItems;
        gameStateEvents.OnWheelRotateDone += OnWheelRotateDone;
        gameStateEvents.OnWheelItemsSpawned += OnWheelItemsSpawned;
    }

    private void OnDisable()
    {
        gameStateEvents.OnSpinButtonClicked -= OnSpinButtonClicked;
        gameStateEvents.OnSpinReady -= OnSpinReady;
        gameStateEvents.OnCreateWheelItems -= OnCreateItems;
        gameStateEvents.OnWheelRotateDone -= OnWheelRotateDone;
        gameStateEvents.OnWheelItemsSpawned -= OnWheelItemsSpawned;
    }

    #endregion

    #region LISTENER METHODS

    private void OnSpinButtonClicked()
    {
        onSpinButtonClicked?.Invoke();
    }

    private void OnSpinReady(int numberOfItems, float duration, int numberRotate,
        AnimationCurve curveSpin)
    {
        onSpinReady?.Invoke(numberOfItems, duration, numberRotate, curveSpin);
    }

    private void OnCreateItems(WheelItemsContentData contentDataCurrent)
    {
        onCreateItems?.Invoke(contentDataCurrent);
    }

    private void OnWheelRotateDone(int itemIndex)
    {
        onWheelRotateDone?.Invoke(itemIndex);
    }

    private void OnWheelItemsSpawned(List<WheelItemData> itemsCurrentlySpawned)
    {
        onWheelItemsSpawned?.Invoke(itemsCurrentlySpawned);
    }

    #endregion
}