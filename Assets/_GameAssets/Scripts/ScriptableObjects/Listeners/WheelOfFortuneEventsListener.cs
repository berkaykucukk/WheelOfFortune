using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class WheelOfFortuneEventsListener : MonoBehaviour
{
    #region EVENTS

    public Action onSpinButtonClicked;
    public Action<int, float, int, AnimationCurve> onSpinReady;

    #endregion

    #region INSPECTOR PROPERTIES

    [SerializeField] private WheelOfFortuneEvents wheelOfFortuneEvents;

    #endregion

    #region UNITY METHODS

    private void OnEnable()
    {
        wheelOfFortuneEvents.OnSpinButtonClicked += OnSpinButtonClicked;
        wheelOfFortuneEvents.OnSpinReady += OnSpinReady;
    }

    private void OnDisable()
    {
        wheelOfFortuneEvents.OnSpinButtonClicked -= OnSpinButtonClicked;
        wheelOfFortuneEvents.OnSpinReady -= OnSpinReady;
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

    #endregion
}