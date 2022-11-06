using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WheelOfFortuneSettings", menuName = "GameSettings/WheelOfFortuneSettings", order = 1)]
public class WheelOfFortuneSettings : ScriptableObject
{
    #region INSPECTOR PROPERTIES

    [SerializeField] private int silverAreaInterval;
    [SerializeField] private int goldAreaInterval;

    #endregion

    #region PUBLIC PROPERTIES

    public int SilverAreaInterval => silverAreaInterval;
    public int GoldAreaInterval => goldAreaInterval;

    #endregion
}