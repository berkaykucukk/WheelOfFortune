using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(GameEventsListener))]
public class SpinResultController : MonoBehaviour
{
    #region INSPECTOR PROPERTIES

    [SerializeField] private Transform parentIconEffects;
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
        //var dataSelectedItem = _itemsDataCurrentlySpawned[itemIndex];
        var itemWheelGO = _itemsGameObjectsCurrentlySpawned[itemIndex];
        var itemHandlerCurrentlySelected = itemWheelGO.GetComponent<WheelItemHandler>();

        itemHandlerCurrentlySelected.AnimatePunch();

        if (itemHandlerCurrentlySelected.TypeOfReward == RewardTypes.death)
        {
            Death();
            return;
        }

        CreateNewCollectAreaIfPossible(itemHandlerCurrentlySelected);
        itemHandlerCurrentlySelected.InstantiateEffect(parentIconEffects);

        IncreaseTotalRotateCount();
        CheckWheelZoneChange();

        //print("Item = " + selectedItem.ID);
        //print();
    }

    private void CreateNewCollectAreaIfPossible(WheelItemHandler itemHandlerCurrentlySelected)
    {
        gameStateManager.TriggerOnCollectAreaIconCreateEvent(itemHandlerCurrentlySelected.Id,
            itemHandlerCurrentlySelected.Icon);
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