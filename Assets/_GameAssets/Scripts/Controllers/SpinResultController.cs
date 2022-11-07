using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(GameEventsListener))]
public class SpinResultController : MonoBehaviour
{
    #region EVENTS

    #endregion

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
    private WheelItemHandler _itemHandlerCurrentlySelected;

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
        //gameEventsListener.onCheckNextSpin += CheckWheelZoneChange;
        gameEventsListener.onWheelRotateDone += CheckResult;
    }

    private void OnDisable()
    {
        gameEventsListener.onWheelItemsCreated -= ReadItemsDataCurrentlySpawned;
        //gameEventsListener.onCheckNextSpin -= CheckWheelZoneChange;
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
        var itemWheelGO = _itemsGameObjectsCurrentlySpawned[itemIndex];
        _itemHandlerCurrentlySelected = itemWheelGO.GetComponent<WheelItemHandler>();

        _itemHandlerCurrentlySelected.AnimatePunch();

        if (_itemHandlerCurrentlySelected.TypeOfReward == RewardTypes.death)
        {
            Death();
            return;
        }

        IncreaseTotalRotateCount();
        CreateNewCollectAreaIfPossible();

        _itemHandlerCurrentlySelected.InstantiateCollectEffect(parentIconEffects);
        _itemHandlerCurrentlySelected.OnAnimationDone += CheckWheelZoneChange;
    }

    private void CreateNewCollectAreaIfPossible()
    {
        gameStateManager.TriggerOnCollectAreaIconCreateEvent(_itemHandlerCurrentlySelected.Id,
            _itemHandlerCurrentlySelected.Icon);
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
        print("Check Wheel Zone Change ");
        _itemHandlerCurrentlySelected.OnAnimationDone -= CheckWheelZoneChange;
        
        var currentState = gameStateManager.StateCurrent;
        var state = currentState;

        if (_numberOfTotalRotate % wheelOfFortuneSettings.GoldAreaInterval == 0)
            state = WheelZoneStates.gold;

        else if (_numberOfTotalRotate % wheelOfFortuneSettings.SilverAreaInterval == 0)
            state = WheelZoneStates.silver;

        else if (gameStateManager.StateCurrent != WheelZoneStates.bronze)
            state = WheelZoneStates.bronze;

        if (currentState != state)
        { 
            gameStateManager.SetWheelState(state);
            gameStateManager.TriggerOnChangeWheelStateEvent();
        }

        gameStateManager.TriggerOnIncreaseWheelItemValuesEvent();
    }
}