using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(WheelOfFortuneEventsListener))]
public class UIElementAnimationController : MonoBehaviour
{
    #region PRIVATE PROPERTIES

    private float spinWheelAngle = 360f;
    private int _numberOfSpinWheelItems;
    private float _durationSpin;
    private int _numberRotate;
    private AnimationCurve _curveSpin;
    private WheelOfFortuneEventsListener EventsListener => GetComponent<WheelOfFortuneEventsListener>();

    #endregion


    #region UNITY METHODS

    private void OnEnable()
    {
        EventsListener.onSpinReady += SpinWheel;
    }

    #endregion

    private void SpinWheel(int numberOfItems, float duration, int numberRotate,
        AnimationCurve curveSpin)
    {
        _numberOfSpinWheelItems = numberOfItems;
        _durationSpin = duration;
        _numberRotate = numberRotate;
        _curveSpin = curveSpin;
        
        StartCoroutine(SpinWheelIEnumerator());
    }

    private IEnumerator SpinWheelIEnumerator()
    {
        print("Çevirr");
        yield return null;
    }
}