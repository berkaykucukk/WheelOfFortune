using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameEventsListener))]
public class SpinButtonController : MonoBehaviour
{
    #region PRIVATE PROPERTIES

    private Button SpinButton;
    private GameStateManager gameStateManager;
    private GameEventsListener gameEventsListener;

    #endregion

    #region UNITY METHODS

    private void Awake()
    {
        gameStateManager = GameStateManager.instance;
        gameEventsListener = GetComponent<GameEventsListener>();
    }


    private void OnEnable()
    {
        gameEventsListener.onGameOver += SetUnInteractable;
        gameEventsListener.onIncreaseWheelItemValues += SetInteractable;
        gameEventsListener.onPlayAgain += SetInteractable;
    }

    private void Start()
    {
        SpinButton = GetComponent<Button>();
        SpinButton.onClick.AddListener(OnClickedSpinButton);
    }

    private void OnDisable()
    {
        gameEventsListener.onGameOver -= SetUnInteractable;
        gameEventsListener.onIncreaseWheelItemValues -= SetInteractable;
        gameEventsListener.onPlayAgain -= SetInteractable;
    }

    #endregion


    private void OnValidate()
    {
        //print("çalış");
    }

    private void OnClickedSpinButton()
    {
        SetUnInteractable();
        gameStateManager.TriggerSpinButtonClickEvent();
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