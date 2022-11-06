using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class WheelOfFortuneItemSpawner : MonoBehaviour
{
    #region INSPECTOR PROPERTIES

    
    [Header("Wheels")]
    [SerializeField] private GameObject wheelBronze;
    [SerializeField] private GameObject wheelSilver;
    [SerializeField] private GameObject wheelGold;

    [Space] 
    [Header("Item Areas")]
    [SerializeField] private Transform panelItemsAreaBronze;

    [SerializeField] private Transform panelItemsAreaSilver;

    [SerializeField] private Transform panelItemsAreaGold;
    [Space] [SerializeField] private Transform referenceCalculateRadiusWheelImage;

    #endregion

    #region PRIVATE PROPERTIES

    private GameObject goWheelCurrent;
    private GameDataManager gameDataManager;
    private Transform panelItemsCurrent;
    private GameStateManager stateManager;
    private GameEventsListener gameEventsListener;

    #endregion

    #region UNITY METHODS

    private void Awake()
    {
        gameDataManager = GameDataManager.instance;
        stateManager = GameStateManager.instance;
        gameEventsListener = GetComponent<GameEventsListener>();
    }

    private void OnEnable()
    {
        gameEventsListener.onCreateItems += InstantiateWheelItems;
    }

    private void OnDisable()
    {
        gameEventsListener.onCreateItems -= InstantiateWheelItems;
    }

    #endregion


    private void InstantiateWheelItems(WheelItemsContentData contentDataCurrent)
    {
        var itemsWillSpawn = new List<WheelItemData>();
        itemsWillSpawn.AddRange(contentDataCurrent.ItemsOnWheel);

        /*var haveBomb = stateManager.StateCurrent == WheelZoneStates.bronze;
        //print("HAve Bomb = " + haveBomb);

        if (haveBomb)
            itemsWillSpawn.Add(itemBombPrefab);*/


        var numberOfItems = itemsWillSpawn.Count;
        //print("number of items = " + numberOfItems);
        var angle = 360f / numberOfItems;
        var radius = Vector3.Distance(referenceCalculateRadiusWheelImage.position, transform.position);

        var itemsDataCurrentlySpawned = new List<WheelItemData>();
        var itemsGameObjectsCurrentlySpawned = new List<GameObject>();

        for (int i = 0; i < numberOfItems; i++)
        {
            var itemNextSpawn = itemsWillSpawn[i].PrefabImageOnWheel;
            itemsDataCurrentlySpawned.Add(itemsWillSpawn[i]);

            var rotation = Quaternion.AngleAxis(i * angle, Vector3.back);
            var direction = rotation * Vector3.up;
            var position = transform.position + (direction * radius);

            var itemGameObject = Instantiate(itemNextSpawn, position, Quaternion.Euler(Vector3.zero));
            itemsGameObjectsCurrentlySpawned.Add(itemGameObject);

            itemGameObject.transform.SetParent(panelItemsAreaBronze);
            itemGameObject.transform.localScale = Vector3.one;
        }

        gameDataManager.SetItemDatasCurrentlySpawned(itemsDataCurrentlySpawned);
        gameDataManager.SetItemsGameObjectsCurrentlySpawned(itemsGameObjectsCurrentlySpawned);

        stateManager.TriggerOnWheelItemsSpawned();
    }
}