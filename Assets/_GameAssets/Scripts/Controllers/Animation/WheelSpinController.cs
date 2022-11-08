using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private System.Random rand = new System.Random();

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
        var itemsGameObjectsCurrentlySpawned = new List<GameObject>();
        itemsGameObjectsCurrentlySpawned.AddRange(gameDataManager.ItemsGameObjectsCurrentlySpawned);
        var itemsHandlersCurrentlySpawned = itemsGameObjectsCurrentlySpawned
            .Select(wheelItem => wheelItem.GetComponent<WheelItemHandler>()).ToList();

        var startAngle = transform.eulerAngles.z;
        anglePerSection = (SpinWheelAngle / itemsGameObjectsCurrentlySpawned.Count);
        var randomItem = GetRandomWheelItem(itemsHandlersCurrentlySpawned);
        var targetAngle = (numberRotate * SpinWheelAngle) + anglePerSection * randomItem - startAngle;
        var targetAngleVector = Vector3.forward * targetAngle;

        gameDataManager.SetItemIndexEarned(randomItem);
        tweenWheelSpin = transform.DORotate(targetAngleVector, durationRotate, RotateMode.LocalAxisAdd);

        tweenWheelSpin.OnComplete(TriggerOnWheelRotateDone);
    }

    private int GetRandomWheelItem(List<WheelItemHandler> wheelItemHandlers)
    {
        var accumulatedWeight = wheelItemHandlers.Sum(wheelItem => wheelItem.DropRate);

        //print("Toplam = " + accumulatedWeight);

        var rnd = Random.Range(0, accumulatedWeight);

        for (int i = 0; i < wheelItemHandlers.Count; i++)
        {
            //print("item Weight= " + wheelItemHandlers[i].Weight);
            if (wheelItemHandlers[i].Weight >= rnd)
                return i;

            accumulatedWeight -= wheelItemHandlers[i].Weight;
        }


        return 0;
    }

    private void TriggerOnWheelRotateDone()
    {
        gameStateManager.TriggerOnWheelRotateDone();
    }
}