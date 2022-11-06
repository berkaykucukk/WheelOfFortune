using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GamePlayData", menuName = "GameData/GamePlayData", order = 1)]
public class GamePlayData : ScriptableObject
{
    #region PRIVATE PROPERTIES

    private List<WheelItemData> _itemDatasCurrentlySpawned;
    private List<GameObject> _itemsGameObjectsCurrentlySpawned;
    private int _itemIndexEarned;

    #endregion

    #region PUBLIC PROPERTIES

    public List<WheelItemData> ItemDatasCurrentlySpawned => _itemDatasCurrentlySpawned;
    public List<GameObject> ItemsGameObjectsCurrentlySpawned => _itemsGameObjectsCurrentlySpawned;
    public int ItemIndexEarned => _itemIndexEarned;

    #endregion


    public void SetItemIndexEarned(int itemIndexEarned)
    {
        _itemIndexEarned = itemIndexEarned;
    }

    public void SetItemDatasCurrentlySpawned(List<WheelItemData> itemDatasCurrentlySpawned)
    {
        _itemDatasCurrentlySpawned = new List<WheelItemData>();
        _itemDatasCurrentlySpawned.AddRange(itemDatasCurrentlySpawned);
    }

    public void SetItemsGameObjectsCurrentlySpawned(List<GameObject> itemsGameObjectsCurrentlySpawned)
    {
        _itemsGameObjectsCurrentlySpawned = new List<GameObject>();
        _itemsGameObjectsCurrentlySpawned.AddRange(itemsGameObjectsCurrentlySpawned);
    }
}