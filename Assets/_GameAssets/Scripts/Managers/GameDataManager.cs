using System;
using System.Collections;
using System.Collections.Generic;
using SystemPersonel;
using UnityEngine;

public class GameDataManager : Singleton<GameDataManager>
{
    #region INSPECTOR PROPERTIES

    #endregion

    #region PRIVATE PROPERTIES

    private List<CollectAreaItemVisualController> _collectedItems;
    private GameEventsListener gameEventsListener;
    private int _numberOfRotateTotal;
    private Transform _itemAreaCurrentEarned;
    private List<WheelItemData> _itemDatasCurrentlySpawned;
    private List<GameObject> _itemsGameObjectsCurrentlySpawned;
    private int _itemIndexEarned;

    #endregion

    #region PUBLIC PROPERTIES

    public List<CollectAreaItemVisualController> ItemsCollected => _collectedItems;
    public int NumberOfRotateTotal => _numberOfRotateTotal;
    public Transform İtemAreaCurrentEarned => _itemAreaCurrentEarned;
    public List<WheelItemData> ItemDatasCurrentlySpawned => _itemDatasCurrentlySpawned;
    public List<GameObject> ItemsGameObjectsCurrentlySpawned => _itemsGameObjectsCurrentlySpawned;
    public int ItemIndexEarned => _itemIndexEarned;

    #endregion

    protected override void Awake()
    {
        base.Awake();
        _itemsGameObjectsCurrentlySpawned = new List<GameObject>();
        _itemDatasCurrentlySpawned = new List<WheelItemData>();
        gameEventsListener = GetComponent<GameEventsListener>();
    }

    private void OnEnable()
    {
        gameEventsListener.onResetGame += ResetGame;
    }

    private void OnDisable()
    {
        gameEventsListener.onResetGame -= ResetGame;
    }

    public void SetNumberOfRotate(int value)
    {
        _numberOfRotateTotal = value;
    }

    public void SetItemIndexEarned(int itemIndexEarned)
    {
        _itemIndexEarned = itemIndexEarned;
    }

    public void SetCollectedItems(List<CollectAreaItemVisualController> collectedItems)
    {
        _collectedItems = new List<CollectAreaItemVisualController>();
      
        _collectedItems.AddRange(collectedItems);
        print(_collectedItems.Count);
    }

    public void SetItemDatasCurrentlySpawned(List<WheelItemData> itemDatasCurrentlySpawned)
    {
        _itemDatasCurrentlySpawned = new List<WheelItemData>();
        _itemDatasCurrentlySpawned.AddRange(itemDatasCurrentlySpawned);
    }

    public void DeleteGameObjectsCurrentlySpawned()
    {
        if (_itemsGameObjectsCurrentlySpawned.Count <= 0) return;
        var localList = new List<GameObject>();
        localList.AddRange(_itemsGameObjectsCurrentlySpawned);
        foreach (var item in _itemsGameObjectsCurrentlySpawned)
        {
            Destroy(item);
        }
    }

    public void SetItemsGameObjectsCurrentlySpawned(List<GameObject> itemsGameObjectsCurrentlySpawned)
    {
        _itemsGameObjectsCurrentlySpawned = new List<GameObject>();
        _itemsGameObjectsCurrentlySpawned.AddRange(itemsGameObjectsCurrentlySpawned);
    }

    public void SetCurrentEarnedItemAreaTransform(Transform itemArea)
    {
        _itemAreaCurrentEarned = itemArea;
    }

    private void ResetGame()
    {
        DeleteGameObjectsCurrentlySpawned();
        _numberOfRotateTotal = 0;
    }
}