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

    private int idCurrentEarnedItem;
    private List<int> itemsIdCollectArea;
    private List<CollectAreaItemVisualController> itemsControllersCollectArea;
    private GameDataManager gameDataManager;
    private GameEventsListener gameEventsListener;

    #endregion

    #region UNITY METHODS

    private void Awake()
    {
        itemsIdCollectArea = new List<int>();
        itemsControllersCollectArea = new List<CollectAreaItemVisualController>();
        gameDataManager = GameDataManager.instance;
        gameEventsListener = GetComponent<GameEventsListener>();
    }

    private void OnEnable()
    {
        gameEventsListener.onCollectAreaIconCreate += CreateCollectArea;
        gameEventsListener.onCollectAreaValueUpdate += UpdateCollectAreaTotalText;
    }

    private void OnDisable()
    {
        gameEventsListener.onCollectAreaIconCreate -= CreateCollectArea;
        gameEventsListener.onCollectAreaValueUpdate -= UpdateCollectAreaTotalText;
    }

    #endregion

    private void CreateCollectArea(int id, Image icon)
    {
        if (CheckIsNewItem(id))
        {
            itemsIdCollectArea.Add(id);

            var collectAreaItem = Instantiate(prefabCollectAreaItem, panelCollectArea);
            var collectAreaController = collectAreaItem.GetComponent<CollectAreaItemVisualController>();
            itemsControllersCollectArea.Add(collectAreaController);
            collectAreaItem.transform.localScale = Vector3.one;

            var collectAreaItemController = collectAreaItem.GetComponent<CollectAreaItemVisualController>();
            collectAreaItemController.SetIcon(icon);
            collectAreaController.SetId(id);
        }
        
        UpdateCurrentEarnedItemArea(id);
    }

    private void UpdateCurrentEarnedItemArea(int id)
    {
        foreach (var collectAreaItem in itemsControllersCollectArea.Where(collectAreaItem => collectAreaItem.ID == id))
        {
            gameDataManager.SetCurrentEarnedItemArea(collectAreaItem.transform);
        }
    }

    private void UpdateCollectAreaTotalText(int id, int value)
    {
        foreach (var collectAreaItem in itemsControllersCollectArea.Where(collectAreaItem => collectAreaItem.ID == id))
        {
            //print("update et");
            collectAreaItem.UpdateValue(value);
            break;
        }
    }

    private bool CheckIsNewItem(int id)
    {
        return itemsIdCollectArea.Count <= 0 || itemsIdCollectArea.All(itemId => itemId != id);
    }
}