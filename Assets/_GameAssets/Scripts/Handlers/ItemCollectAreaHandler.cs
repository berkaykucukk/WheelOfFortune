using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameEventsListener))]
public class ItemCollectAreaHandler : MonoBehaviour
{
    #region INSPECTOR PROPERTIES

    [SerializeField] private GameObject prefabCollectAreaItem;
    [SerializeField] private Transform panelCollectArea;

    #endregion

    #region PRIVATE PROPERTIES

    private List<int> itemsIdCollectArea;
    private GameDataManager gameDataManager;
    private GameEventsListener gameEventsListener;

    #endregion

    #region UNITY METHODS

    private void Awake()
    {
        itemsIdCollectArea = new List<int>();
        gameDataManager = GameDataManager.instance;
        gameEventsListener = GetComponent<GameEventsListener>();
    }

    private void OnEnable()
    {
        gameEventsListener.onCollectAreaIconUpdate += UpdateCollectAreaIcon;
    }

    private void OnDisable()
    {
        gameEventsListener.onCollectAreaIconUpdate -= UpdateCollectAreaIcon;
    }

    #endregion

    private void UpdateCollectAreaIcon(int id, Image icon)
    {
        if (!CheckIsNewItem(id))
            return;

        itemsIdCollectArea.Add(id);
        var collectAreaItem = Instantiate(prefabCollectAreaItem, panelCollectArea);
        collectAreaItem.transform.localScale = Vector3.one;

        var collectAreaItemController = collectAreaItem.GetComponent<CollectAreaItemVisualController>();
        collectAreaItemController.SetIcon(icon);
    }

    private bool CheckIsNewItem(int id)
    {
        return itemsIdCollectArea.Count <= 0 || itemsIdCollectArea.All(itemId => itemId != id);
    }
}