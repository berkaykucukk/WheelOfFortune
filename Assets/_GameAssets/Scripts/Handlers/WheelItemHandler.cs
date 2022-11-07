using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WheelItemHandler : MonoBehaviour
{
    #region PRIVATE PROPERTIES

    private GameDataManager gameDataManager;
    private RewardTypes _typeOfReward;
    private int _id = 0;
    private int _value = 1;

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

    public RewardTypes TypeOfReward => _typeOfReward;
    public int Id => _id;
    public Image Icon => icon;

    #endregion

    private void Awake()
    {
        gameDataManager = GameDataManager.instance;
    }

    public void SetValue(int value)
    {
        _value = value;
        valueText.text = _value.ToString();
    }


    public void AnimatePunch()
    {
        transform.DOPunchScale(Vector3.one * punchPower, durationAnimation)
            .SetEase(easeAnimation);
    }

    public void SetId(int id)
    {
        _id = id;
    }

    public void SetRewardType(RewardTypes rewardType)
    {
        _typeOfReward = rewardType;
    }

    public void InstantiateEffect(Transform effectParent)
    {
        StartCoroutine(InstantiateEffectCoroutine(effectParent));
    }

    private IEnumerator InstantiateEffectCoroutine(Transform effectParent)
    {
        yield return null;

        var localListIcons = new List<GameObject>();


        for (int i = 0; i < numberOfSpawnIcon; i++)
        {
            var iconGO = Instantiate(icon.gameObject, transform);
            iconGO.transform.localScale = Vector3.one * .8F;
            localListIcons.Add(iconGO);
        }

        foreach (var iconObj in localListIcons)
        {
            var rnd = Random.insideUnitCircle * radiusEffectExplode;
            //rnd.y += 5f;
            iconObj.transform.DOLocalMoveX(rnd.x, .1f).SetEase(Ease.Unset);
            iconObj.transform.DOLocalMoveY(rnd.y, .1f).SetEase(Ease.Unset);
        }

        yield return new WaitForSeconds(.15f);

        var currentArea = gameDataManager.İtemAreaCurrentEarned;
        foreach (var iconObj in localListIcons)
        {
            iconObj.transform.SetParent(effectParent);
            iconObj.transform.DOMoveX(currentArea.position.x, .5f).SetEase(Ease.InSine);
            iconObj.transform.DOMoveY(currentArea.position.y, .5f).SetEase(Ease.InSine);
        }
    }
}