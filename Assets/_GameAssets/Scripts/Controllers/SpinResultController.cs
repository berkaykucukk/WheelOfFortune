using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(GameEventsListener))]
public class SpinResultController : MonoBehaviour
{
    #region PRIVATE PROPERTIES

    private List<WheelItemData> _itemsDataCurrentlySpawned;
    private List<GameObject> _itemsGameObjectsCurrentlySpawned;
    private GameEventsListener gameEventsListener;

    #endregion


    #region UNITY METHODS

    private void Awake()
    {
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

    private void ReadItemsDataCurrentlySpawned(List<WheelItemData> itemDatasCurrentlySpawned,
        List<GameObject> itemsGameObjectsCurrentlySpawned)
    {
        _itemsDataCurrentlySpawned = new List<WheelItemData>();
        _itemsDataCurrentlySpawned.AddRange(itemDatasCurrentlySpawned);

        _itemsGameObjectsCurrentlySpawned = new List<GameObject>();
        _itemsGameObjectsCurrentlySpawned.AddRange(itemsGameObjectsCurrentlySpawned);
    }

    private void CheckResult(WheelItemData item)
    {
        _itemsGameObjectsCurrentlySpawned[0].transform.DOPunchScale(Vector3.one * 2, 1f);
    }
}