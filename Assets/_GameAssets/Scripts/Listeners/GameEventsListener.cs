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
    public Action<float, int, Ease> onSpinReady;
    public Action<WheelItemsContentData> onCreateItems;
    public Action onWheelRotateDone;
    public Action onWheelItemsCreated;
    public Action<int, Image> onCollectAreaIconCreate;
    public Action<int, int> onCollectAreaValueUpdate;
    public Action onChangeWheelState;
    public Action onCheckNextSpin;
    public Action onIncreaseWheelItemValues;
    public Action onGameOver;
    public Action onResetGame;
    public Action onPlayAgain;
    public Action onCollectItems;

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
        gameStateEvents.OnCheckNextSpin += OnCheckNextSpin;
        gameStateEvents.OnChangeWheelState += OnChangeWheelState;
        gameStateEvents.OnIncreaseWheelItemValues += OnIncreaseWheelItemValues;
        gameStateEvents.OnGameOver += OnGameOver;
        gameStateEvents.OnPlayAgain += OnPlayAgain;
        gameStateEvents.OnResetGame += OnResetGame;
        gameStateEvents.OnCollectItems += OnCollectItems;
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
        gameStateEvents.OnCheckNextSpin -= OnCheckNextSpin;
        gameStateEvents.OnChangeWheelState -= OnChangeWheelState;
        gameStateEvents.OnIncreaseWheelItemValues -= OnIncreaseWheelItemValues;
        gameStateEvents.OnGameOver -= OnGameOver;
        gameStateEvents.OnPlayAgain -= OnPlayAgain;
        gameStateEvents.OnResetGame -= OnResetGame;
        gameStateEvents.OnCollectItems -= OnCollectItems;
    }

    #endregion

    #region LISTENER METHODS

    private void OnSpinButtonClicked()
    {
        onSpinButtonClicked?.Invoke();
    }

    private void OnSpinReady(float duration, int numberRotate,
        Ease easeSpin)
    {
        onSpinReady?.Invoke(duration, numberRotate, easeSpin);
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

    private void OnCheckNextSpin()
    {
        onCheckNextSpin?.Invoke();
    }

    private void OnChangeWheelState()
    {
        onChangeWheelState?.Invoke();
    }

    private void OnIncreaseWheelItemValues()
    {
        onIncreaseWheelItemValues?.Invoke();
    }

    private void OnGameOver()
    {
        onGameOver?.Invoke();
    }

    private void OnResetGame()
    {
        onResetGame?.Invoke();
    }

    private void OnPlayAgain()
    {
        onPlayAgain?.Invoke();
    }

    private void OnCollectItems()
    {
        onCollectItems?.Invoke();
    }

    #endregion
}