using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinButtonController : MonoBehaviour
{
    #region PRIVATE PROPERTIES

    private Button SpinButton => GetComponent<Button>();
    private GameStateManager stateManager;

    #endregion

    private void Awake()
    {
        stateManager = GameStateManager.instance;
    }

    private void OnValidate()
    {
        SpinButton.onClick.AddListener(OnClickedSpinButton);
    }

    private void OnClickedSpinButton()
    {
        stateManager.TriggerSpinButtonClickEvent();
    }
}