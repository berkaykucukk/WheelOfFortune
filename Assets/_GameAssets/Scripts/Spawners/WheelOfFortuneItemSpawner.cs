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
        gameEventsListener.onCreateItems += InstantiateWheelItems;
        gameEventsListener.onChangeWheelState += SetChangeWheelStateSettings;
    }

    private void OnDisable()
    {
        gameEventsListener.onCreateItems -= InstantiateWheelItems;
        gameEventsListener.onChangeWheelState -= SetChangeWheelStateSettings;
    }

    #endregion

    private void SetChangeWheelStateSettings()
    {
        var currentState = stateManager.StateCurrent;
        var currentWheelLocal = wheelCurrent;

        currentWheelLocal.transform.GetChild(0).transform.eulerAngles = Vector3.zero;
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

    private void InstantiateWheelItems(WheelItemsContentData contentDataCurrent)
    {
        gameDataManager.DeleteGameObjectsCurrentlySpawned();

        var itemsWillSpawn = new List<WheelItemData>();
        itemsWillSpawn.AddRange(contentDataCurrent.ItemsOnWheel);

        var numberOfItems = itemsWillSpawn.Count;
        var angle = 360f / numberOfItems;
        var radius = Vector3.Distance(referenceCalculateRadiusWheelImage.position, transform.position);

        var itemsDataCurrentlySpawned = new List<WheelItemData>();
        var itemsGameObjectsCurrentlySpawned = new List<GameObject>();

        for (int i = 0; i < numberOfItems; i++)
        {
            var currentItemData = itemsWillSpawn[i];
            var itemNextSpawn = currentItemData.PrefabImageOnWheel;
            itemsDataCurrentlySpawned.Add(itemsWillSpawn[i]);

            var rotation = Quaternion.AngleAxis(i * angle, Vector3.back);
            var direction = rotation * Vector3.up;
            var position = transform.position + (direction * radius);

            var itemGameObject = Instantiate(itemNextSpawn, position, Quaternion.Euler(Vector3.zero));
            itemsGameObjectsCurrentlySpawned.Add(itemGameObject);
            itemGameObject.transform.SetParent(panelItemsCurrent);
            itemGameObject.transform.localScale = Vector3.one;
           
            var wheelItemHandler = itemGameObject.GetComponent<WheelItemHandler>();
            wheelItemHandler.SetId(currentItemData.ID);
            wheelItemHandler.SetRewardType(currentItemData.TypeOfReward);
            wheelItemHandler.SetIncreaseAmount(currentItemData.AmountOfIncrease);
            wheelItemHandler.SetDropRate(currentItemData.DropRate);
            
        }

        gameDataManager.SetItemDatasCurrentlySpawned(itemsDataCurrentlySpawned);
        gameDataManager.SetItemsGameObjectsCurrentlySpawned(itemsGameObjectsCurrentlySpawned);


        stateManager.TriggerOnWheelItemsCreatedEvent();
    }
}