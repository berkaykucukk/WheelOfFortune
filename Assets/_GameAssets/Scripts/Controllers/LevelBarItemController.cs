using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelBarItemController : MonoBehaviour
{
    #region INSPECTOR PROPERTIES

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject blueCard;
    [SerializeField] private GameObject greenCard;
    [SerializeField] private GameObject border;

    #endregion


    public void SetLevel(int level)
    {
        levelText.text = level.ToString();
    }

    public void SetBlueCard()
    {
        blueCard.SetActive(true);
    }

    public void SetGreenCard()
    {
        greenCard.SetActive(true);
    }

    public void Activate()
    {
        border.SetActive(true);
    }

    public void Deactivate()
    {
        border.SetActive(false);
    }
}