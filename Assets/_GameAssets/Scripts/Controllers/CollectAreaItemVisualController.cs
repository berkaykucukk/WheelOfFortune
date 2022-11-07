using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectAreaItemVisualController : MonoBehaviour
{
    
    
    #region PRIVATE PROPERTIES

    private int _id;
    private float value;

    #endregion

    #region INSPECTOR PROPERTIES

    [SerializeField] private Image imageIcon;
    [SerializeField] private TextMeshProUGUI numberText;

    #endregion

    #region PUBLIC PROPERTIES

    public int ID => _id;

    #endregion
    public void SetId(int id)
    {
        _id = id;
    }
    public void SetIcon(Image icon)
    {
        imageIcon.rectTransform.sizeDelta = new Vector2(icon.rectTransform.rect.width, icon.rectTransform.rect.height);
        imageIcon.sprite = icon.sprite;
    }

    public void UpdateValue(int numberOfTotal)
    {
        value += numberOfTotal;
        numberText.text = value.ToString();
    }
}