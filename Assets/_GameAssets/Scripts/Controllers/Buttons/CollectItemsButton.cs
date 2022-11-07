using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectItemsButton : MonoBehaviour
{
    #region PRIVATE PROPERTIES

    private GameStateManager gameStateManager;
    private Button buttonCollect;

    #endregion


    private void Awake()
    {
        gameStateManager = GameStateManager.instance;
    }

    private void OnValidate()
    {
        buttonCollect = GetComponent<Button>();
        buttonCollect.onClick.AddListener(CollectItems);
    }

    private void CollectItems()
    {
        gameStateManager.TriggerCollectItemsEvent();
        gameStateManager.TriggerGameResetEvent();
    }
}