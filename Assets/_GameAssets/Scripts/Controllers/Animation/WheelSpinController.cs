using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(GameEventsListener))]
public class WheelSpinController : MonoBehaviour
{
    #region PRIVATE PROPERTIES

    private Tween tweenWheelSpin;
    private float SpinWheelAngle = 360f;
    private GameEventsListener eventsListener;
    private float spinTimer = 0;
    private float anglePerSection;
    private GameStateManager gameStateManager;
    private float halfPieceAngle;
    private GameDataManager gameDataManager;

    #endregion


    #region UNITY METHODS

    private void Awake()
    {
        gameDataManager = GameDataManager.instance;
        eventsListener = GetComponent<GameEventsListener>();
        gameStateManager = GameStateManager.instance;
    }

    private void OnEnable()
    {
        eventsListener.onSpinReady += RunSpinWheel;
    }

    private void OnDisable()
    {
        eventsListener.onSpinReady -= RunSpinWheel;
    }

    #endregion


    private void RunSpinWheel(float durationRotate, int numberRotate,
        Ease easeSpin)
    {
        var itemsSpawned = new List<GameObject>();
        itemsSpawned.AddRange(gameDataManager.ItemsGameObjectsCurrentlySpawned);
        var startAngle = transform.eulerAngles.z;
        anglePerSection = (SpinWheelAngle / itemsSpawned.Count);
        var randomItem = Random.Range(0, itemsSpawned.Count);
        var targetAngle = (numberRotate * SpinWheelAngle) + anglePerSection * randomItem - startAngle;
        var targetAngleVector = Vector3.forward * targetAngle;

        gameDataManager.SetItemIndexEarned(randomItem);
        tweenWheelSpin = transform.DORotate(targetAngleVector, durationRotate, RotateMode.LocalAxisAdd);

        tweenWheelSpin.OnComplete(TriggerOnWheelRotateDone);
    }

    private void TriggerOnWheelRotateDone()
    {
        gameStateManager.TriggerOnWheelRotateDone();
    }
}