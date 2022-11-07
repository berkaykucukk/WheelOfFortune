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

    private GameStateManager gameStateManager;
    private GameEventsListener gameEventsListener;

    #endregion

    private void OnValidate()
    {
        playAgainBtn.onClick.AddListener(PlayAgain);
    }

    private void Awake()
    {
        gameEventsListener = GetComponent<GameEventsListener>();
        gameStateManager = GameStateManager.instance;
    }

    private void OnEnable()
    {
        gameEventsListener.onGameOver += OpenGameOverPanel;
    }

    private void OnDisable()
    {
        gameEventsListener.onGameOver -= OpenGameOverPanel;
    }

    private void PlayAgain()
    {
        gameStateManager.TriggerPlayAgainEvent();

        gameOverPanel.SetActive(false);
        playAgainBtn.gameObject.SetActive(false);
        gameOverPanel.transform.localScale = Vector3.zero;
        playAgainBtn.transform.localScale = Vector3.zero;
    }

    private void OpenGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        playAgainBtn.gameObject.SetActive(true);
        gameOverPanel.transform.DOScale(Vector3.one, durationGameOverPanelOpen).SetEase(easeGameOverPanelOpen);
        playAgainBtn.transform.DOScale(Vector3.one, durationGameOverPanelOpen).SetEase(easeGameOverPanelOpen);
    }
}