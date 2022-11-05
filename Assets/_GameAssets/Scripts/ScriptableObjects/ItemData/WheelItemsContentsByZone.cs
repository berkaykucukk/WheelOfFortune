using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WheelItemsContentsByZone", menuName = "ItemData/WheelItemsContentsByZone", order = 1)]
public class WheelItemsContentsByZone : ScriptableObject
{
    #region INSPECTOR PROPERTIES

    [SerializeField] private List<WheelItemData> itemsOnWheel;

    #endregion
}