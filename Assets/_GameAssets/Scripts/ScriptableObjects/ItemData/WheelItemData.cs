using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WheelItemData", menuName = "ItemData/WheelItemData", order = 1)]
public class WheelItemData : ScriptableObject
{
    #region INSPECTOR PROPERTIES

    [Range(0, 100)] [SerializeField] private float rateDrop;
    [SerializeField] private GameObject prefabImageOnWheel;
    //[SerializeField] private GameObject prefabItemCard;
    [SerializeField] private Sprite spriteItem;
    [SerializeField] private float amountOfIncrease = 1;

    #endregion

    #region PUBLIC PROPERTIES

    public float DropRate => rateDrop;
    public GameObject PrefabImageOnWheel => prefabImageOnWheel;
    //public GameObject PrefabItemCard => prefabItemCard;

    #endregion
}