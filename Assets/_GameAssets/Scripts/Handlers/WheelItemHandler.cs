using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(GameEventsListener))]
public class WheelItemHandler : MonoBehaviour
{
    #region EVENTS

    public Action OnAnimationDone;

    #endregion

    #region PRIVATE PROPERTIES

    private GameEventsListener gameEventsListener;
    private GameStateManager gameStateManager;
    private GameDataManager gameDataManager;
    private RewardTypes _typeOfReward;
    private float _weight;
    private int _id = 0;
    private int _value = 1;
    private float _amountOfIncrease;
    private float _dropRate;

    #endregion

    #region INSPECTOR PROPERTIES

    [Header("Animation Values")] [SerializeField]
    private float punchPower = .5f;

    [SerializeField] private float durationAnimation = .15f;
    [SerializeField] private Ease easeAnimation;

    [Space] [Header("Effect Values")] [SerializeField]
    private int numberOfSpawnIcon = 4;

    [SerializeField] private float radiusEffectExplode;
    [Space] [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private Image icon;

    #endregion

    #region PUBLIC PROPERTIES

    public float Weight => _weight;
    public RewardTypes TypeOfReward => _typeOfReward;
    public int Id => _id;
    public float AmountOfIncrease => _amountOfIncrease;
    public float DropRate => _dropRate;
    public Image Icon => icon;

    #endregion


    #region UNITY METHODS

    private void Awake()
    {
        gameEventsListener = GetComponent<GameEventsListener>();
        gameStateManager = GameStateManager.instance;
        gameDataManager = GameDataManager.instance;
        IncreaseValue();
    }

    private void Update()
    {
        transform.eulerAngles = Vector3.zero;
    }

    private void OnEnable()
    {
        gameEventsListener.onIncreaseWheelItemValues += IncreaseValue;
    }

    private void OnDisable()
    {
        gameEventsListener.onIncreaseWheelItemValues -= IncreaseValue;
    }

    #endregion

    private void UpdateText()
    {
        valueText.text = "x" + _value;
    }


    public void AnimatePunch()
    {
        transform.DOPunchScale(Vector3.one * punchPower, durationAnimation)
            .SetEase(easeAnimation);
    }

    public void SetWeight(float weight)
    {
        _weight = weight;
    }

    public void SetId(int id)
    {
        _id = id;
    }

    public void SetDropRate(float dropRate)
    {
        _dropRate = dropRate;
    }

    public void SetIncreaseAmount(float increaseAmount)
    {
        _amountOfIncrease = increaseAmount;
    }

    public void SetRewardType(RewardTypes rewardType)
    {
        _typeOfReward = rewardType;
    }

    public void InstantiateCollectEffect(Transform effectParent)
    {
        StartCoroutine(InstantiateEffectCoroutine(effectParent));
    }

    private IEnumerator InstantiateEffectCoroutine(Transform effectParent)
    {
        yield return null;

        var localListIcons = new List<GameObject>();

        for (int i = 0; i < numberOfSpawnIcon; i++)
        {
            var rnd = Random.insideUnitCircle * radiusEffectExplode;
            rnd.y += 5f;
            var iconGO = Instantiate(icon.gameObject, transform);
            iconGO.transform.localScale = Vector3.one * .8F;
            iconGO.transform.DOLocalMoveX(rnd.x, .1f).SetEase(Ease.Unset);
            iconGO.transform.DOLocalMoveY(rnd.y, .1f).SetEase(Ease.Unset);
            localListIcons.Add(iconGO);
        }

        yield return new WaitForSeconds(.15f);

        var currentArea = gameDataManager.İtemAreaCurrentEarned;
        foreach (var iconObj in localListIcons)
        {
            iconObj.transform.SetParent(effectParent);
            iconObj.transform.DOMoveX(currentArea.position.x, .5f).SetEase(Ease.InSine);
            iconObj.transform.DOMoveY(currentArea.position.y, .5f).SetEase(Ease.InSine);
        }

        yield return new WaitForSeconds(.5f);

        var deleteList = new List<GameObject>();
        deleteList.AddRange(localListIcons);

        for (int i = 0; i < localListIcons.Count; i++)
        {
            Destroy(deleteList[i].gameObject);
        }

        TriggerUpdateCollectAreaValue();
        OnAnimationDone?.Invoke();
    }


    private void TriggerUpdateCollectAreaValue()
    {
        gameStateManager.TriggerOnCollectAreaValueUpdateEvent(_id, _value);
    }

    private void IncreaseValue()
    {
        var valueUpdated = _amountOfIncrease * gameDataManager.NumberOfRotateTotal + 1;
        _value = Mathf.CeilToInt(valueUpdated);
        UpdateText();
    }
}