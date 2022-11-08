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
    private List<GameObject> collectAreaItemVisualControllers;

    #endregion

    private void Awake()
    {
        gameDataManager = GameDataManager.instance;
        gameEventsListener = GetComponent<GameEventsListener>();
       
    }

    private void OnEnable()
    {
        collectAreaItemVisualControllers = new List<GameObject>();
        gameEventsListener.onCollectItems += GetAndInstantiateEarnedPrizes;
        gameEventsListener.onPlayAgain += ResetDatas;
    }

    private void OnDisable()
    {
        gameEventsListener.onCollectItems -= GetAndInstantiateEarnedPrizes;
        gameEventsListener.onPlayAgain -= ResetDatas;
    }

    private void GetAndInstantiateEarnedPrizes()
    {
        StartCoroutine(GetAndInstantiateEarnedPrizesCoroutine());
    }

    private IEnumerator GetAndInstantiateEarnedPrizesCoroutine()
    {
        yield return null;
        var localListCollectedItems = new List<CollectAreaItemVisualController>();
        collectAreaItemVisualControllers = new List<GameObject>();

        localListCollectedItems.AddRange(gameDataManager.ItemsCollected);

        foreach (var prize in localListCollectedItems)
        {
            var prizeGO = Instantiate(prize.gameObject, grid);
            prizeGO.transform.localScale = Vector3.one;
            collectAreaItemVisualControllers.Add(prizeGO);
        }
    }

    private void ResetDatas()
    {
        if (collectAreaItemVisualControllers.Count <= 0)
        {
            return;
        }

        var collectedItemCount = collectAreaItemVisualControllers.Count;
        if (collectedItemCount > 0)
        {
            for (int i = 0; i < collectedItemCount; i++)
            {
                Destroy(collectAreaItemVisualControllers[i].gameObject);
            }

            collectAreaItemVisualControllers.Clear();
        }
    }
}