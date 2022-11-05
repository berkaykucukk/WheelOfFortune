using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameEventsListener))]
public class SpinResultController : MonoBehaviour
{
    #region PRIVATE PROPERTIES

    private List<WheelItemData> _itemsCurrentlySpawned;
    private GameEventsListener gameEventsListener;

    #endregion


    #region UNITY METHODS

    private void Awake()
    {
        gameEventsListener = GetComponent<GameEventsListener>();
    }

    private void OnEnable()
    {
        gameEventsListener.onWheelItemsSpawned += ReadItemsCurrentlySpawned;
        gameEventsListener.onWheelRotateDone += CheckResult;
    }

    private void OnDisable()
    {
        gameEventsListener.onWheelItemsSpawned -= ReadItemsCurrentlySpawned;
        gameEventsListener.onWheelRotateDone -= CheckResult;
    }

    #endregion

    private void ReadItemsCurrentlySpawned(List<WheelItemData> itemsCurrentlySpawned)
    {
        _itemsCurrentlySpawned = new List<WheelItemData>();
        _itemsCurrentlySpawned.AddRange(itemsCurrentlySpawned);
    }

    private void CheckResult(int itemIndex)
    {
        print("id ver = " + _itemsCurrentlySpawned[itemIndex].ID);
    }
}