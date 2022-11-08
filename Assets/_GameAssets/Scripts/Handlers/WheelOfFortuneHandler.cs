using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameEventsListener))]
public class WheelOfFortuneHandler : MonoBehaviour
{
    #region SPIN VALUES

    /*[BoxGroup("Spin Values")] [SerializeField]
    private int numberOfSpinWheelItems = 8;*/

    [BoxGroup("Spin Values")] [SerializeField]
    private float durationSpin = 2f;

    [BoxGroup("Spin Values")] [SerializeField]
    private int numberRotate = 3;

    [BoxGroup("Spin Values")] [SerializeField]
    private Ease easeSpin;

    #endregion

    #region INSPECTOR PROPERTIES

    [BoxGroup("Wheel Content Data By Zone")] [SerializeField]
    private WheelItemsContentData contentDataBronze;

    [BoxGroup("Wheel Content Data By Zone")] [SerializeField]
    private WheelItemsContentData contentDataSilver;

    [BoxGroup("Wheel Content Data By Zone")] [SerializeField]
    private WheelItemsContentData contentDataGold;

    #endregion

    #region PUBLIC PROPERTIES

    // public int NumberOfSpinWheelItems => numberOfSpinWheelItems;

    #endregion

    #region PRIVATE PROPERTIES

    private GameStateManager gameStateManager;
    private GameEventsListener gameEventsListener;
    private WheelItemsContentData contentDataCurrent;

    #endregion

    #region UNITY METHODS

    private void Awake()
    {
        gameEventsListener = GetComponent<GameEventsListener>();
        gameStateManager = GameStateManager.instance;
    }

    private void Start()
    {
        SetCurrentWheelItemContentData();
        TriggerCreateItemsEvent();
    }

    private void OnEnable()
    {
        gameEventsListener.onSpinButtonClicked += TriggerSpinReady;
        gameEventsListener.onChangeWheelState += ChangeWheelState;
        gameEventsListener.onPlayAgain += ChangeWheelState;
    }

    private void OnDisable()
    {
        gameEventsListener.onSpinButtonClicked -= TriggerSpinReady;
        gameEventsListener.onChangeWheelState -= ChangeWheelState;
        gameEventsListener.onPlayAgain += ChangeWheelState;
    }

    #endregion

    #region PRIVATE METHODS

    private void SetCurrentWheelItemContentData()
    {
        var contentsListLocal = new List<WheelItemsContentData>
        {
            contentDataBronze,
            contentDataSilver,
            contentDataGold
        };

        var stateCurrent = gameStateManager.StateCurrent;

        foreach (var content in contentsListLocal.Where(content => content.StatementType == stateCurrent))
            contentDataCurrent = content;
    }

    private void TriggerCreateItemsEvent()
    {
        print("Create Items");
        gameStateManager.TriggerCreateItemsEvent(contentDataCurrent);
    }

    private void TriggerSpinReady()
    {
        gameStateManager.TriggerSpinReadyEvent(durationSpin, numberRotate, easeSpin);
    }

    private void ChangeWheelState()
    {
        SetCurrentWheelItemContentData();
        TriggerCreateItemsEvent();
    }

    #endregion
}