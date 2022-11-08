using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameEventsListener))]
public class UIControllerGameEnd : MonoBehaviour
{
    #region INSPECTOR PROPERTIES

    [Header("Panel Animation Values")] [SerializeField]
    private float durationPanelOpen;

    [SerializeField] private Ease easePanelOpen;
    [SerializeField] private GameObject gameOverPanel;

    [Space] [Header("Buttons")] [SerializeField]
    private Button playAgainBtn;

    [Space] [SerializeField] private GameObject collectItemsPanel;

    #endregion

    #region PRIVATE PROPERTIES

    private GameStateManager gameStateManager;

    private GameEventsListener gameEventsListener;

    #endregion


    private void Awake()
    {
        gameEventsListener = GetComponent<GameEventsListener>();
        gameStateManager = GameStateManager.instance;
    }

    private void Start()
    {
        playAgainBtn.onClick.AddListener(PlayAgain);
    }

    private void OnEnable()
    {
        gameEventsListener.onGameOver += OpenGameOverPanel;
        gameEventsListener.onCollectItems += OpenCollectItemsPanel;
    }

    private void OnDisable()
    {
        gameEventsListener.onGameOver -= OpenGameOverPanel;
        gameEventsListener.onCollectItems -= OpenCollectItemsPanel;
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
        gameOverPanel.transform.DOScale(Vector3.one, durationPanelOpen).SetEase(easePanelOpen);
        playAgainBtn.transform.DOScale(Vector3.one, durationPanelOpen).SetEase(easePanelOpen);
    }

    private void OpenCollectItemsPanel()
    {
        collectItemsPanel.SetActive(true);
        playAgainBtn.gameObject.SetActive(true);
        collectItemsPanel.transform.DOScale(Vector3.one, durationPanelOpen).SetEase(easePanelOpen);
        playAgainBtn.transform.DOScale(Vector3.one, durationPanelOpen).SetEase(easePanelOpen);
    }
}