using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class GameEventsListener : MonoBehaviour
{
    #region EVENTS

    public Action onSpinButtonClicked;
    public Action<WheelItemsContentData, float, int, Ease> onSpinReady;
    public Action<WheelItemsContentData> onCreateItems;
    public Action onWheelRotateDone;
    public Action onWheelItemsCreated;
    public Action<int, Image> onCollectAreaIconCreate;
    public Action<int, int> onCollectAreaValueUpdate;

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
        gameStateEvents.OnWheelItemsCreated += OnWheelItemsCreated;
        gameStateEvents.OnCollectAreaIconCreate += OnCollectAreaCreate;
        gameStateEvents.OnCollectAreaValueUpdate += OnCollectAreaValueUpdate;
    }

    private void OnDisable()
    {
        gameStateEvents.OnSpinButtonClicked -= OnSpinButtonClicked;
        gameStateEvents.OnSpinReady -= OnSpinReady;
        gameStateEvents.OnCreateWheelItems -= OnCreateItems;
        gameStateEvents.OnWheelRotateDone -= OnWheelRotateDone;
        gameStateEvents.OnWheelItemsCreated -= OnWheelItemsCreated;
        gameStateEvents.OnCollectAreaIconCreate -= OnCollectAreaCreate;
        gameStateEvents.OnCollectAreaValueUpdate -= OnCollectAreaValueUpdate;
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

    private void OnWheelRotateDone()
    {
        onWheelRotateDone?.Invoke();
    }

    private void OnWheelItemsCreated()
    {
        onWheelItemsCreated?.Invoke();
    }

    private void OnCollectAreaCreate(int id, Image icon)
    {
        onCollectAreaIconCreate?.Invoke(id, icon);
    }

    private void OnCollectAreaValueUpdate(int itemId, int value)
    {
        onCollectAreaValueUpdate?.Invoke(itemId, value);
    }

    #endregion
}