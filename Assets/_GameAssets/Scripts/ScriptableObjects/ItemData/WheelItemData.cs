using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WheelItemData", menuName = "ItemData/WheelItemData", order = 1)]
public class WheelItemData : ScriptableObject
{
    #region INSPECTOR PROPERTIES

    [SerializeField] private RewardTypes.TypesReward typeOfReward;
    [Range(0, 100)] [SerializeField] private float rateDrop;
    [SerializeField] private int id;
    [SerializeField] private GameObject prefabImageOnWheel;

    [SerializeField] private Sprite spriteItem;
    [SerializeField] private float amountOfIncrease = 1;

    #endregion

    #region PUBLIC PROPERTIES

    public int ID => id;
    public float DropRate => rateDrop;
    public GameObject PrefabImageOnWheel => prefabImageOnWheel;
    public RewardTypes.TypesReward TypeOfReward => typeOfReward;

    #endregion
}