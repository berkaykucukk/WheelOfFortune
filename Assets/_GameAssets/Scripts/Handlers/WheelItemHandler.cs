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

    public Image Icon => icon;

    #endregion

    public void SetValue(int value)
    {
        _value = value;
        valueText.text = _value.ToString();
    }

    private void Update()
    {
    }

    public void AnimatePunch()
    {
        transform.DOPunchScale(Vector3.one * punchPower, durationAnimation)
            .SetEase(easeAnimation);
    }

    public void InstantiateEffect()
    {
        StartCoroutine(InstantiateEffectCoroutine());
    }

    private IEnumerator InstantiateEffectCoroutine()
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
            iconObj.transform.DOLocalMoveX(rnd.x, .1f).SetEase(Ease.Unset);
            iconObj.transform.DOLocalMoveY(rnd.y, .1f).SetEase(Ease.Unset);
        }
    }
}