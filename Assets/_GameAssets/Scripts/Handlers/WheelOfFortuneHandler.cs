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
    private WheelOfFortuneEventsListener eventsListener;

    #endregion

    #region UNITY METHODS

    private void Awake()
    {
        eventsListener = GetComponent<WheelOfFortuneEventsListener>();
        stateManager = WheelOfFortuneStateManager.instance;
    }

    private void OnEnable()
    {
        eventsListener.onSpinButtonClicked += TriggerSpinReady;
    }

    private void OnDisable()
    {
        eventsListener.onSpinButtonClicked -= TriggerSpinReady;
    }

    private void Start()
    {
        stateManager.TriggerCreateItemsEvent(numberOfSpinWheelItems);
    }

    #endregion

    private void TriggerSpinReady()
    {
        stateManager.TriggerSpinReadyEvent(numberOfSpinWheelItems, durationSpin, numberRotate, curveSpin);
    }
}