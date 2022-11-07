using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameEventsListener))]
public class SpinButtonController : MonoBehaviour
{
    #region PRIVATE PROPERTIES

    private Button SpinButton => GetComponent<Button>();
    private GameStateManager stateManager;
    private GameEventsListener gameEventsListener;

    #endregion

    private void Awake()
    {
        stateManager = GameStateManager.instance;
        gameEventsListener = GetComponent<GameEventsListener>();
    }

    private void OnEnable()
    {
        gameEventsListener.onGameOver += SetUnInteractable;
        gameEventsListener.onIncreaseWheelItemValues += SetInteractable;
        gameEventsListener.onPlayAgain += SetInteractable;
    }

    private void OnDisable()
    {
        gameEventsListener.onGameOver -= SetUnInteractable;
        gameEventsListener.onIncreaseWheelItemValues -= SetInteractable;
        gameEventsListener.onPlayAgain -= SetInteractable;
    }

    private void OnValidate()
    {
        SpinButton.onClick.AddListener(OnClickedSpinButton);
    }

    private void OnClickedSpinButton()
    {
        SetUnInteractable();
        stateManager.TriggerSpinButtonClickEvent();
    }

    private void SetUnInteractable()
    {
        SpinButton.interactable = false;
    }

    private void SetInteractable()
    {
        SpinButton.interactable = true;
    }
}