using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(GameEventsListener))]
public class WheelAnimationController : MonoBehaviour
{
    #region PRIVATE PROPERTIES

    private float SpinWheelAngle = 360f;
    private GameEventsListener EventsListener => GetComponent<GameEventsListener>();
    private float spinTimer = 0;
    private float angleSection;
    private GameStateManager gameStateManager;

    #endregion


    #region UNITY METHODS

    private void Awake()
    {
        gameStateManager = GameStateManager.instance;
    }

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
        var randomItem = Random.Range(0, numberOfItems);
        var targetAngle = (numberRotate * SpinWheelAngle) + angleSection * randomItem - startAngle;
        var angleCurrent = 0f;
        while (spinTimer < durationRotate)
        {
            yield return null;
            spinTimer += Time.deltaTime;
            angleCurrent = (targetAngle) * curveSpin.Evaluate(spinTimer / durationRotate);
            transform.eulerAngles = new Vector3(0, 0, angleCurrent + startAngle);
        }

        print("Random item index = " + angleCurrent);
        gameStateManager.TriggerOnWheelRotateDone(randomItem);
    }
}