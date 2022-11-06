using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(GameEventsListener))]
public class SpinResultController : MonoBehaviour
{


    #region PRIVATE PROPERTIES

    private GameDataManager gameDataManager;
    private List<WheelItemData> _itemsDataCurrentlySpawned;
    private List<GameObject> _itemsGameObjectsCurrentlySpawned;
    private GameEventsListener gameEventsListener;

    #endregion


    #region UNITY METHODS

    private void Awake()
    {
        gameDataManager = GameDataManager.instance;
        gameEventsListener = GetComponent<GameEventsListener>();
    }

    private void OnEnable()
    {
        gameEventsListener.onWheelItemsSpawned += ReadItemsDataCurrentlySpawned;
        gameEventsListener.onWheelRotateDone += CheckResult;
    }

    private void OnDisable()
    {
        gameEventsListener.onWheelItemsSpawned -= ReadItemsDataCurrentlySpawned;
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
        var selectedItem = _itemsDataCurrentlySpawned[itemIndex];
        print("Item = " + selectedItem.ID);
        _itemsGameObjectsCurrentlySpawned[itemIndex].transform.DOPunchScale(Vector3.one * 2, .12f);
        //print();
    }
}