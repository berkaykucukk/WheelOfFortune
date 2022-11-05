using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class WheelOfFortuneItemSpawner : MonoBehaviour
{
    #region INSPECTOR PROPERTIES

    [SerializeField] private WheelItemData itemBombPrefab;

    [BoxGroup("Panels by Zones")] [SerializeField]
    private Transform panelItemsAreaBronze;

    [BoxGroup("Panels by Zones")] [SerializeField]
    private Transform panelItemsAreaSilver;

    [BoxGroup("Panels by Zones")] [SerializeField]
    private Transform panelItemsAreaGold;

    [SerializeField] private Transform referenceCalculateRadiusWheelImage;

    #endregion

    #region PRIVATE PROPERTIES

    private GameStateManager stateManager;
    private GameEventsListener gameEventsListener;

    #endregion

    #region UNITY METHODS

    private void Awake()
    {
        stateManager = GameStateManager.instance;
        gameEventsListener = GetComponent<GameEventsListener>();
    }

    private void OnEnable()
    {
        gameEventsListener.onCreateItems += InstantiateItems;
    }

    private void OnDisable()
    {
        gameEventsListener.onCreateItems -= InstantiateItems;
    }

    #endregion


    private void InstantiateItems(WheelItemsContentData contentDataCurrent)
    {
        var itemsWillSpawn = new List<WheelItemData>();
        itemsWillSpawn.AddRange(contentDataCurrent.ItemsOnWheel);

        var haveBomb = stateManager.StateCurrent == WheelZoneStates.bronze;
        //print("HAve Bomb = " + haveBomb);

        if (haveBomb)
            itemsWillSpawn.Add(itemBombPrefab);


        var numberOfItems = itemsWillSpawn.Count;
        //print("number of items = " + numberOfItems);
        var angle = 360f / numberOfItems;

        var radius = Vector3.Distance(referenceCalculateRadiusWheelImage.position, transform.position);

        var itemsCurrentlySpawned = new List<WheelItemData>();
        for (int i = 0; i < numberOfItems; i++)
        {
            var itemNextSpawn = itemsWillSpawn[i].PrefabImageOnWheel;
            itemsCurrentlySpawned.Add(itemsWillSpawn[i]);
            var rotation = Quaternion.AngleAxis(i * angle, Vector3.forward);
            var direction = rotation * Vector3.up;

            var position = transform.position + (direction * radius);
            var go = Instantiate(itemNextSpawn, position, Quaternion.Euler(Vector3.zero));

            go.transform.SetParent(panelItemsAreaBronze);
            go.transform.localScale = Vector3.one;
        }

        stateManager.TriggerOnWheelItemsSpawned(itemsCurrentlySpawned);
    }
}