using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(WheelOfFortuneEventsListener))]
public class WheelOfFortuneHandler : MonoBehaviour
{
    #region SPIN VALUES

    [BoxGroup("Spin Values")] [SerializeField]
    private int numberOfSpinWheelItems = 8;

    [BoxGroup("Spin Values")] [SerializeField]
    private float durationSpin = 2f;

    [BoxGroup("Spin Values")] [SerializeField]
    private int numberRotate = 3;

    [BoxGroup("Spin Values")] [SerializeField]
    private AnimationCurve curveSpin;

    #endregion

    #region INSPECTOR PROPERTIES

    [BoxGroup("Wheel Content Data By Zone")] [SerializeField]
    private bool useDifferentContentsAtZones;

    [HideIf("useDifferentContentsAtZones")] [BoxGroup("Wheel Content Data By Zone")] [SerializeField]
    private WheelItemsContentData contentDefault;

    [ShowIf("useDifferentContentsAtZones")] [BoxGroup("Wheel Content Data By Zone")] [SerializeField]
    private WheelItemsContentData contentsBronze;

    [ShowIf("useDifferentContentsAtZones")] [BoxGroup("Wheel Content Data By Zone")] [SerializeField]
    private WheelItemsContentData contentsSilver;

    [ShowIf("useDifferentContentsAtZones")] [BoxGroup("Wheel Content Data By Zone")] [SerializeField]
    private WheelItemsContentData contentsGold;

    #endregion

    #region PUBLIC PROPERTIES

    public int NumberOfSpinWheelItems => numberOfSpinWheelItems;

    #endregion

    #region PRIVATE PROPERTIES

    private WheelOfFortuneStateManager stateManager;
    private WheelOfFortuneEventsListener eventsListener;
    private WheelItemsContentData contentDataCurrent;

    #endregion

    #region UNITY METHODS

    private void Awake()
    {
        eventsListener = GetComponent<WheelOfFortuneEventsListener>();
        stateManager = WheelOfFortuneStateManager.instance;
    }

    private void Start()
    {
        SetCurrentWheelItemContentData();
        TriggerCreateItemsEvent();
    }

    private void OnEnable()
    {
        eventsListener.onSpinButtonClicked += TriggerSpinReady;
    }

    private void OnDisable()
    {
        eventsListener.onSpinButtonClicked -= TriggerSpinReady;
    }

    #endregion

    private void SetCurrentWheelItemContentData()
    {
        if (!useDifferentContentsAtZones)
        {
            contentDataCurrent = contentDefault;
            return;
        }

        var contentsListLocal = new List<WheelItemsContentData>
        {
            contentsBronze,
            contentsSilver,
            contentsGold
        };

        var stateCurrent = stateManager.StateCurrent;

        foreach (var content in contentsListLocal.Where(content => content.StatementType == stateCurrent))
            contentDataCurrent = content;
    }

    private void TriggerCreateItemsEvent()
    {
        stateManager.TriggerCreateItemsEvent(contentDataCurrent);
    }

    private void TriggerSpinReady()
    {
        stateManager.TriggerSpinReadyEvent(numberOfSpinWheelItems, durationSpin, numberRotate, curveSpin);
    }
}