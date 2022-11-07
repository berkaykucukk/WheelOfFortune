using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameEventsListener))]
public class UIController : MonoBehaviour
{
    #region INSPECTOR PROPERTIES

    [SerializeField] private float durationGameOverPanelOpen;
    [SerializeField] private Ease easeGameOverPanelOpen;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button playAgainBtn;
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
        gameEventsListener.onGameOver += OpenGameOverPanel;
    }

    private void OnDisable()
    {
        gameEventsListener.onGameOver -= OpenGameOverPanel;
    }

    private void OpenGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        gameOverPanel.transform.DOScale(Vector3.one, durationGameOverPanelOpen).SetEase(easeGameOverPanelOpen);
        playAgainBtn.transform.DOScale(Vector3.one, durationGameOverPanelOpen).SetEase(easeGameOverPanelOpen);
    }
}