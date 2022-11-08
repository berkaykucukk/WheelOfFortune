using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class WheelOfFortuneItemSpawner : MonoBehaviour
{
    #region INSPECTOR PROPERTIES

    [Header("Wheels")] [SerializeField] private GameObject wheelBronze;
    [SerializeField] private GameObject wheelSilver;
    [SerializeField] private GameObject wheelGold;

    [Space] [Header("Item Areas")] [SerializeField]
    private Transform panelItemsAreaBronze;

    [SerializeField] private Transform panelItemsAreaSilver;

    [SerializeField] private Transform panelItemsAreaGold;
    [Space] [SerializeField] private Transform referenceCalculateRadiusWheelImage;

    #endregion

    #region PRIVATE PROPERTIES

    private GameObject wheelCurrent;
    private GameDataManager gameDataManager;
    private Transform panelItemsCurrent;
    private GameStateManager stateManager;
    private GameEventsListener gameEventsListener;
    private float accumulatedWeight = 0f;

    #endregion

    #region UNITY METHODS

    private void Awake()
    {
        wheelCurrent = wheelBronze;
        panelItemsCurrent = panelItemsAreaBronze;
        gameDataManager = GameDataManager.instance;
        stateManager = GameStateManager.instance;
        gameEventsListener = GetComponent<GameEventsListener>();
    }

    private void OnEnable()
    {
        gameEventsListener.onCreateItems += InstantiateWheelItemsCircleShape;
        gameEventsListener.onChangeWheelState += SetChangeWheelStateSettings;
        gameEventsListener.onResetGame += ResetWheelsRotations;
    }

    private void OnDisable()
    {
        gameEventsListener.onCreateItems -= InstantiateWheelItemsCircleShape;
        gameEventsListener.onChangeWheelState -= SetChangeWheelStateSettings;
        gameEventsListener.onResetGame -= ResetWheelsRotations;
    }

    #endregion

    private void SetChangeWheelStateSettings()
    {
        var currentState = stateManager.StateCurrent;
        var currentWheelLocal = wheelCurrent;

        ResetWheelsRotations();
        currentWheelLocal.SetActive(false);

        /*currentWheelLocal.transform.DOScale(Vector3.zero, .5f).SetEase(Ease.Unset).OnComplete(() =>
        {
            currentWheelLocal.SetActive(false);
        });*/
        switch (currentState)
        {
            case WheelZoneStates.bronze:
                ChangeWheelBronze();
                break;
            case WheelZoneStates.silver:
                ChangeWheelSilver();
                break;
            case WheelZoneStates.gold:
                ChangeWheelGold();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        wheelCurrent.SetActive(true);
        //wheelCurrent.transform.DOScale(Vector3.one, .5f).SetUpdate(true).SetEase(Ease.Unset);
    }

    private void ChangeWheelBronze()
    {
        wheelCurrent = wheelBronze;
        panelItemsCurrent = panelItemsAreaBronze;
    }

    private void ChangeWheelSilver()
    {
        wheelCurrent = wheelSilver;
        panelItemsCurrent = panelItemsAreaSilver;
    }

    private void ChangeWheelGold()
    {
        wheelCurrent = wheelGold;
        panelItemsCurrent = panelItemsAreaGold;
    }

    private void InstantiateWheelItemsCircleShape(WheelItemsContentData contentDataCurrent)
    {
        gameDataManager.DeleteGameObjectsCurrentlySpawned();
        accumulatedWeight = 0f;

        var itemPrefabsWillSpawn = new List<WheelItemData>();
        itemPrefabsWillSpawn.AddRange(contentDataCurrent.ItemsOnWheel);

        var numberOfItems = itemPrefabsWillSpawn.Count;
        var angle = 360f / numberOfItems;

        var radius = Vector3.Distance(referenceCalculateRadiusWheelImage.position, transform.position);

        var itemsDataCurrentlySpawned = new List<WheelItemData>();
        var itemsGameObjectsCurrentlySpawned = new List<GameObject>();

        for (int i = 0; i < numberOfItems; i++)
        {
            var currentItemData = itemPrefabsWillSpawn[i];
            var itemNextSpawn = currentItemData.PrefabImageOnWheel;
            itemsDataCurrentlySpawned.Add(itemPrefabsWillSpawn[i]);

            var rotation = Quaternion.AngleAxis(i * angle, Vector3.back);
            var direction = rotation * Vector3.up;
            var position = transform.position + (direction * radius);

            var itemGameObject = CreateAndSetTransformWheelItem(itemNextSpawn, position);
            itemsGameObjectsCurrentlySpawned.Add(itemGameObject);

            var wheelItemHandler = itemGameObject.GetComponent<WheelItemHandler>();
            SetPropertiesWheelItem(currentItemData, wheelItemHandler);
        }

        gameDataManager.SetItemDatasCurrentlySpawned(itemsDataCurrentlySpawned);
        gameDataManager.SetItemsGameObjectsCurrentlySpawned(itemsGameObjectsCurrentlySpawned);


        stateManager.TriggerOnWheelItemsCreatedEvent();
    }


    private GameObject CreateAndSetTransformWheelItem(GameObject itemPrefab, Vector3 position)
    {
        var itemGameObject = Instantiate(itemPrefab, position, Quaternion.Euler(Vector3.zero));
        itemGameObject.transform.SetParent(panelItemsCurrent);
        itemGameObject.transform.localScale = Vector3.one;
        return itemGameObject;
    }

    private void SetPropertiesWheelItem(WheelItemData currentItemData, WheelItemHandler wheelItemHandler)
    {
        wheelItemHandler.SetId(currentItemData.ID);
        wheelItemHandler.SetRewardType(currentItemData.TypeOfReward);
        wheelItemHandler.SetIncreaseAmount(currentItemData.AmountOfIncrease);
        wheelItemHandler.SetDropRate(currentItemData.DropRate);

        SetAccumulatedWeightWheelItem(wheelItemHandler);
    }

    private void SetAccumulatedWeightWheelItem(WheelItemHandler wheelItemHandler)
    {
        accumulatedWeight += (wheelItemHandler.DropRate);
        wheelItemHandler.SetWeight(accumulatedWeight);
    }

    private void ResetWheelsRotations()
    {
        var wheelsListLocal = new List<GameObject>
        {
            wheelBronze,
            wheelSilver,
            wheelGold
        };

        foreach (var wheel in wheelsListLocal)
        {
            wheel.transform.GetChild(0).transform.eulerAngles = Vector3.zero;
        }
    }
}