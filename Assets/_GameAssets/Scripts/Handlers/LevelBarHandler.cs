using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBarHandler : MonoBehaviour
{
    #region INSPECTOR PROPERTIES

    [SerializeField] private float amountContentSlide = 300f;
    [SerializeField] private int numberOfStartSpawn = 30;
    [SerializeField] private int intervalUpdateCardList = 10;
    [SerializeField] private GameObject prefabLevelItem;
    [SerializeField] private Transform contentArea;
    #endregion

    #region PRIVATE PROPERTIES

    private GameDataManager gameDataManager;
    private GameStateManager gameStateManager;

    #endregion
    
    private void Awake()
    {
        gameDataManager = GameDataManager.instance;
        gameStateManager = GameStateManager.instance;
        SpawnCards();
    }

    private void SpawnCards()
    {
        
    }
}
