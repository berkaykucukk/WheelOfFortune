using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LevelBarHandler : MonoBehaviour
{
    #region INSPECTOR PROPERTIES

    [SerializeField] private float amountContentSlide = 300f;
    [SerializeField] private int numberOfStartSpawn = 30;
    [SerializeField] private int intervalIncreaseCardList = 10;
    [SerializeField] private GameObject prefabLevelItem;
    [SerializeField] private RectTransform contentArea;
    [SerializeField] private WheelOfFortuneSettings wheelOfFortuneSettings;
    [SerializeField] private Transform activeLevelItemParent;

    #endregion

    #region PRIVATE PROPERTIES

    private List<LevelBarItemController> levelBarItemControllers;
    private GameDataManager gameDataManager;
    private GameStateManager gameStateManager;
    private LevelBarItemController levelBarItemCurrent;
    private GameEventsListener gameEventsListener;
    private GameObject activeLevelItemGameObject;

    #endregion

    private void Awake()
    {
        gameDataManager = GameDataManager.instance;
        gameStateManager = GameStateManager.instance;
        gameEventsListener = GetComponent<GameEventsListener>();
        levelBarItemControllers = new List<LevelBarItemController>();
        SpawnCards();
        UpdateCards();
    }

    private void OnEnable()
    {
        gameEventsListener.onCheckNextSpin += UpdateCards;
    }

    private void OnDisable()
    {
        gameEventsListener.onCheckNextSpin -= UpdateCards;
    }

    private void SpawnCards()
    {
        for (int i = 0; i < numberOfStartSpawn; i++)
        {
            var levelBarItem = Instantiate(prefabLevelItem, contentArea);
            var levelBarItemController = levelBarItem.GetComponent<LevelBarItemController>();
            levelBarItemController.SetLevel(i + 1);
            levelBarItemControllers.Add(levelBarItemController);
        }
    }

    private void UpdateCards()
    {
        var numberOfRotate = gameDataManager.NumberOfRotateTotal;
        //var contentNext = numberOfRotate * amountContentSlide;

        if (numberOfRotate != 0)
            SlideContentArea();

        if (levelBarItemCurrent != null)
        {
            levelBarItemCurrent.Deactivate();
            Destroy(activeLevelItemGameObject);
        }
        
        levelBarItemCurrent = levelBarItemControllers[numberOfRotate];
        levelBarItemCurrent.Activate();
        
        for (int i = 0; i < levelBarItemControllers.Count; i++)
        {
            var isSilver = (i + 1) % wheelOfFortuneSettings.SilverAreaInterval == 0;
            var isGold = (i + 1) % wheelOfFortuneSettings.GoldAreaInterval == 0;

            if (isSilver || isGold)
                levelBarItemControllers[i].SetGreenCard();
            else
                levelBarItemControllers[i].SetBlueCard();
        }
        
        activeLevelItemGameObject = Instantiate(levelBarItemCurrent.gameObject, activeLevelItemParent);
        activeLevelItemGameObject.transform.localScale = Vector3.one * 1.3f;
        activeLevelItemGameObject.transform.localPosition = Vector3.zero;
    }

    private void SlideContentArea()
    {
        var current = contentArea.rect.width;

        var target = current + amountContentSlide;
        print("Width = " + current);
        DOTween.To(() => current, x => current = x, target, .4f).OnUpdate(() =>
        {
            var size = contentArea.sizeDelta;
            size.x = current;
            contentArea.sizeDelta = size;
        });
    }
}