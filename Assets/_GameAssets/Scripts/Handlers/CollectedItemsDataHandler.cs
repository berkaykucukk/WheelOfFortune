using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedItemsDataHandler : MonoBehaviour
{
    #region INSPECTOR PROPERTIES

    [SerializeField] private Transform grid;

    #endregion

    #region PRIVATE PROPERTIES

    private GameEventsListener gameEventsListener;
    private GameDataManager gameDataManager;

    #endregion

    private void Awake()
    {
        gameDataManager = GameDataManager.instance;
        gameEventsListener = GetComponent<GameEventsListener>();
    }

    private void OnEnable()
    {
        gameEventsListener.onCollectItems += GetAndInstantiateEarnedPrizes;
    }

    private void GetAndInstantiateEarnedPrizes()
    {
        StartCoroutine(GetAndInstantiateEarnedPrizesCoroutine());
    }

    private IEnumerator GetAndInstantiateEarnedPrizesCoroutine()
    {
        yield return null;
        var localListPrizes = new List<CollectAreaItemVisualController>();
        localListPrizes.AddRange(gameDataManager.ItemsCollected);

        foreach (var prize in localListPrizes)
        {
            var prizeGO = Instantiate(prize.gameObject, grid);
            prizeGO.transform.localScale = Vector3.one;
        }
    }
}