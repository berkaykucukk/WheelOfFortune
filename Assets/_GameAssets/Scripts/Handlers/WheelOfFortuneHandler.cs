using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WheelOfFortuneEventsListener))]
public class WheelOfFortuneHandler : MonoBehaviour
{
    #region INSPECTOR PROPERTIES

    [SerializeField] private int numberOfSpinWheelItems = 8;
    [SerializeField] private float durationSpin = 2f;
    [SerializeField] private int numberRotate = 3;
    [SerializeField] private AnimationCurve curveSpin;

    #endregion

    #region PUBLIC PROPERTIES

    public int NumberOfSpinWheelItems => numberOfSpinWheelItems;

    #endregion

    #region PRIVATE PROPERTIES

    private WheelOfFortuneStateManager stateManager;
    private WheelOfFortuneEventsListener EventsListener => GetComponent<WheelOfFortuneEventsListener>();

    #endregion

    #region UNITY METHODS

    private void Awake()
    {
        stateManager = WheelOfFortuneStateManager.instance;
    }

    private void OnEnable()
    {
        EventsListener.onSpinButtonClicked += TriggerSpinReady;
    }

    private void OnDisable()
    {
        EventsListener.onSpinButtonClicked -= TriggerSpinReady;
    }

    #endregion


    private void TriggerSpinReady()
    {
        stateManager.TriggerSpinReadyEvent(numberOfSpinWheelItems, durationSpin, numberRotate, curveSpin);
    }
}