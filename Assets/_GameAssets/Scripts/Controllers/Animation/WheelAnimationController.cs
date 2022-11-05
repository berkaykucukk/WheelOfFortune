using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(WheelOfFortuneEventsListener))]
public class WheelAnimationController : MonoBehaviour
{
    #region PRIVATE PROPERTIES

    private float SpinWheelAngle = 360f;
    private WheelOfFortuneEventsListener EventsListener => GetComponent<WheelOfFortuneEventsListener>();
    private float spinTimer = 0;
    private float angleSection;

    #endregion


    #region UNITY METHODS

    private void OnEnable()
    {
        EventsListener.onSpinReady += RunSpinWheel;
    }

    private void OnDisable()
    {
        EventsListener.onSpinReady -= RunSpinWheel;
    }

    #endregion

    private void RunSpinWheel(int numberOfItems, float durationRotate, int numberRotate,
        AnimationCurve curveSpin)
    {
        var spinWheel = SpinWheel(numberOfItems, durationRotate, numberRotate, curveSpin);
        StartCoroutine(spinWheel);
    }

    private IEnumerator SpinWheel(int numberOfItems, float durationRotate, int numberRotate,
        AnimationCurve curveSpin)
    {
        spinTimer = 0;
        var startAngle = transform.eulerAngles.z;

        angleSection = SpinWheelAngle / numberOfItems;
        var randomItem = Random.Range(1, numberOfItems);
        var targetAngle = (numberRotate * SpinWheelAngle) + angleSection * randomItem - startAngle;
        while (spinTimer < durationRotate)
        {
            yield return null;
            spinTimer += Time.deltaTime;
            var angleCurrent = (targetAngle) * curveSpin.Evaluate(spinTimer / durationRotate);
            transform.eulerAngles = new Vector3(0, 0, angleCurrent + startAngle);
        }
        
    }
}