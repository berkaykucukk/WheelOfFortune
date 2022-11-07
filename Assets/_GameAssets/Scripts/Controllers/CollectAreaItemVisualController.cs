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


    public void SetIcon(Image icon)
    {
        imageIcon.rectTransform.sizeDelta = new Vector2(icon.rectTransform.rect.width, icon.rectTransform.rect.height);

        imageIcon.sprite = icon.sprite;
    }

    public void SetValue(int numberOfTotal)
    {
        numberText.text = numberOfTotal.ToString();
    }
}