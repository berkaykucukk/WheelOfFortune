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
    private GameEventsListener gameEventsListener;
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
        gameEventsListener = GetComponent<GameEventsListener>();
        gameStateManager = GameStateManager.instance;
    }

    private void OnEnable()
    {
        gameEventsListener.onSpinReady += RunSpinWheel;
    }

    private void OnDisable()
    {
        gameEventsListener.onSpinReady -= RunSpinWheel;
    }

    #endregion


    private void RunSpinWheel(float durationRotate, int numberRotate, Ease easeSpin)
    {
        var itemsGameObjectsCurrentlySpawned = new List<GameObject>();
        itemsGameObjectsCurrentlySpawned.AddRange(gameDataManager.ItemsGameObjectsCurrentlySpawned);
        var itemsHandlersCurrentlySpawned = itemsGameObjectsCurrentlySpawned
            .Select(wheelItem => wheelItem.GetComponent<WheelItemHandler>()).ToList();

        var startAngle = transform.eulerAngles.z;
        anglePerSection = (SpinWheelAngle / itemsGameObjectsCurrentlySpawned.Count);
        var randomItemIndex = GetRandomWheelItem(itemsHandlersCurrentlySpawned);
        var targetAngle = (numberRotate * SpinWheelAngle) + anglePerSection * randomItemIndex - startAngle;
        var targetAngleVector = Vector3.forward * targetAngle;

        gameDataManager.SetItemIndexEarned(randomItemIndex);
        tweenWheelSpin = transform.DORotate(targetAngleVector, durationRotate, RotateMode.LocalAxisAdd);

        tweenWheelSpin.OnComplete(TriggerOnWheelRotateDone);
    }

    private int GetRandomWheelItem(List<WheelItemHandler> wheelItemHandlers)
    {
        var totalWeight = wheelItemHandlers.Sum(wheelItem => wheelItem.DropRate);
        wheelItemHandlers = Sort(wheelItemHandlers);

        var rnd = Random.Range(0, totalWeight);

        for (int i = 0; i < wheelItemHandlers.Count; i++)
        {
            if (wheelItemHandlers[i].Weight >= rnd)
                return i;

            //rnd -= wheelItemHandlers[i].Weight;
        }

        return 0;
    }

    private List<WheelItemHandler> Sort(List<WheelItemHandler> wheelItemHandlers)
    {
        return wheelItemHandlers.OrderBy(item => item.Weight).ToList();
    }

    private void TriggerOnWheelRotateDone()
    {
        gameStateManager.TriggerOnWheelRotateDone();
    }
}