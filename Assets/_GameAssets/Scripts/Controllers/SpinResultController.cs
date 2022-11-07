using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(GameEventsListener))]
public class SpinResultController : MonoBehaviour
{
    #region INSPECTOR PROPERTIES

    [SerializeField] private WheelOfFortuneSettings wheelOfFortuneSettings;

    #endregion

    #region PRIVATE PROPERTIES

    private int _numberOfTotalRotate;
    private GameDataManager gameDataManager;
    private List<WheelItemData> _itemsDataCurrentlySpawned;
    private List<GameObject> _itemsGameObjectsCurrentlySpawned;
    private GameEventsListener gameEventsListener;
    private GameStateManager gameStateManager;

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
        gameEventsListener.onWheelItemsCreated += ReadItemsDataCurrentlySpawned;
        gameEventsListener.onWheelRotateDone += CheckResult;
    }

    private void OnDisable()
    {
        gameEventsListener.onWheelItemsCreated -= ReadItemsDataCurrentlySpawned;
        gameEventsListener.onWheelRotateDone -= CheckResult;
    }

    #endregion

    private void ReadItemsDataCurrentlySpawned()
    {
        _itemsDataCurrentlySpawned = new List<WheelItemData>();
        _itemsDataCurrentlySpawned.AddRange(gameDataManager.ItemDatasCurrentlySpawned);

        _itemsGameObjectsCurrentlySpawned = new List<GameObject>();
        _itemsGameObjectsCurrentlySpawned.AddRange(gameDataManager.ItemsGameObjectsCurrentlySpawned);
    }

    private void CheckResult()
    {
        var itemIndex = gameDataManager.ItemIndexEarned;
        var dataSelectedItem = _itemsDataCurrentlySpawned[itemIndex];
        var itemWheelGO = _itemsGameObjectsCurrentlySpawned[itemIndex];
        var itemWheelHandler = itemWheelGO.GetComponent<WheelItemHandler>();

        itemWheelHandler.AnimatePunch();

        if (dataSelectedItem.TypeOfReward == RewardTypes.death)
        {
            Death();
            return;
        }

        SetCollectArea(dataSelectedItem, itemWheelHandler);
        IncreaseTotalRotateCount();
        CheckWheelZoneChange();

        //print("Item = " + selectedItem.ID);
        //print();
    }

    private void SetCollectArea(WheelItemData dataCurrent, WheelItemHandler itemWheelHandlerCurrent)
    {
        itemWheelHandlerCurrent.InstantiateEffect();
        gameStateManager.TriggerOnCollectAreaIconUpdateEvent(dataCurrent.ID, itemWheelHandlerCurrent.Icon);
    }

    private void Death()
    {
    }

    private void IncreaseTotalRotateCount()
    {
        _numberOfTotalRotate++;
    }

    private void CheckWheelZoneChange()
    {
    }
}