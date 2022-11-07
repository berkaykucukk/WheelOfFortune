using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectAreaItemVisualController : MonoBehaviour
{
    #region INSPECTOR PROPERTIES

    [SerializeField] private Image imageIcon;
    [SerializeField] private TextMeshProUGUI numberText;

    #endregion


    public void SetIcon(float width, float height, Sprite sprite)
    {
        var imgRect = imageIcon.rectTransform.rect;
        imgRect.height = height;
        imgRect.width = width;

        imageIcon.sprite = sprite;
    }

    public void SetValue(int numberOfTotal)
    {
        numberText.text = numberOfTotal.ToString();
    }
}