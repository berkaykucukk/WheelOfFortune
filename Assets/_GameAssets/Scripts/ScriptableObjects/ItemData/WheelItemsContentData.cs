using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

[CreateAssetMenu(fileName = "WheelItemsContentData", menuName = "ItemData/WheelItemsContentData", order = 1)]
public class WheelItemsContentData : ScriptableObject
{
    #region INSPECTOR PROPERTIES

    [SerializeField] private WheelZoneStates statementType;
    [SerializeField] private List<WheelItemData> itemsOnWheel;

    #endregion

    #region PUBLIC PROPERTIES

    public WheelZoneStates StatementType => statementType;
    public List<WheelItemData> ItemsOnWheel => itemsOnWheel;

    #endregion
}