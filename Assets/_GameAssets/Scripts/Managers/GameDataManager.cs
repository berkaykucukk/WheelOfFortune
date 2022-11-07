using System.Collections;
using System.Collections.Generic;
using SystemPersonel;
using UnityEngine;

public class GameDataManager : Singleton<GameDataManager>
{
    #region INSPECTOR PROPERTIES

    #endregion

    #region PRIVATE PROPERTIES

    private Transform _itemAreaCurrentEarned;
    private List<WheelItemData> _itemDatasCurrentlySpawned;
    private List<GameObject> _itemsGameObjectsCurrentlySpawned;
    private int _itemIndexEarned;

    #endregion

    #region PUBLIC PROPERTIES

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
    }

    public void SetItemIndexEarned(int itemIndexEarned)
    {
        _itemIndexEarned = itemIndexEarned;
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

    public void SetCurrentEarnedItemArea(Transform itemArea)
    {
        _itemAreaCurrentEarned = itemArea;
    }
}