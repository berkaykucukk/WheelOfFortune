using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class WheelOfFortuneAnimationHandler : MonoBehaviour
{
    #region SPIN VALUES

    [BoxGroup("Spin Values")] [SerializeField]
    private int numberOfSpinWheelItems = 8;

    [BoxGroup("Spin Values")] [SerializeField]
    private float durationSpin = 2f;

    [BoxGroup("Spin Values")] [SerializeField]
    private int numberRotate = 3;

   
    #endregion

    #region PRIVATE PROPERTIES

    private GameStateManager stateManager;
    private GameEventsListener eventsListener;

    #endregion

    private void Awake()
    {
        eventsListener = GetComponent<GameEventsListener>();
        stateManager = GameStateManager.instance;
    }

    private void OnEnable()
    {
        eventsListener.onSpinButtonClicked += TriggerSpinReady;
    }

    private void OnDisable()
    {
        eventsListener.onSpinButtonClicked -= TriggerSpinReady;
    }


    private void TriggerSpinReady()
    {
        //stateManager.TriggerSpinReadyEvent(numberOfSpinWheelItems, durationSpin, numberRotate, easeSpin);
    }
}