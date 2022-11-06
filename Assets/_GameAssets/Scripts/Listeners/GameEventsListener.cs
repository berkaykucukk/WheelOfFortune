using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;


public class GameEventsListener : MonoBehaviour
{
    #region EVENTS

    public Action onSpinButtonClicked;
    public Action<WheelItemsContentData, float, int, Ease> onSpinReady;
    public Action<WheelItemsContentData> onCreateItems;
    public Action<WheelItemData> onWheelRotateDone;
    public Action<List<WheelItemData>, List<GameObject>> onWheelItemsSpawned;

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

    private void OnSpinReady(WheelItemsContentData contentWheelItems, float duration, int numberRotate,
        Ease easeSpin)
    {
        onSpinReady?.Invoke(contentWheelItems, duration, numberRotate, easeSpin);
    }

    private void OnCreateItems(WheelItemsContentData contentDataCurrent)
    {
        onCreateItems?.Invoke(contentDataCurrent);
    }

    private void OnWheelRotateDone(WheelItemData item)
    {
        onWheelRotateDone?.Invoke(item);
    }

    private void OnWheelItemsSpawned(List<WheelItemData> itemDatasCurrentlySpawned,
        List<GameObject> itemsGameObjectsCurrentlySpawned)
    {
        onWheelItemsSpawned?.Invoke(itemDatasCurrentlySpawned, itemsGameObjectsCurrentlySpawned);
    }

    #endregion
}