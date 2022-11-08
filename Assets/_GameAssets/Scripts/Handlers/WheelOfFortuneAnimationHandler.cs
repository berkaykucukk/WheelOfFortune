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
    
    private GameEventsListener gameEventsListener;

    #endregion

    private void Awake()
    {
        gameEventsListener = GetComponent<GameEventsListener>();
    }

    private void OnEnable()
    {
        gameEventsListener.onSpinButtonClicked += TriggerSpinReady;
    }

    private void OnDisable()
    {
        gameEventsListener.onSpinButtonClicked -= TriggerSpinReady;
    }


    private void TriggerSpinReady()
    {
        //stateManager.TriggerSpinReadyEvent(numberOfSpinWheelItems, durationSpin, numberRotate, easeSpin);
    }
}