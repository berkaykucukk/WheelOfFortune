using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class WheelOfFortuneItemSpawner : MonoBehaviour
{
    #region INSPECTOR PROPERTIES

    [SerializeField] private GameObject itemBombPrefab;
    [SerializeField] private GameObject itemPrefab;

    [BoxGroup("Panels by Zones")] [SerializeField]
    private Transform panelItemsAreaBronze;

    [BoxGroup("Panels by Zones")] [SerializeField]
    private Transform panelItemsAreaSilver;

    [BoxGroup("Panels by Zones")] [SerializeField]
    private Transform panelItemsAreaGold;

    [SerializeField] private Transform referenceCalculateRadius;

    #endregion

    #region PRIVATE PROPERTIES

    private WheelOfFortuneEventsListener wheelOfFortuneEventsListener;

    #endregion

    #region UNITY METHODS

    private void Awake()
    {
        wheelOfFortuneEventsListener = GetComponent<WheelOfFortuneEventsListener>();
    }

    private void OnEnable()
    {
        wheelOfFortuneEventsListener.onCreateItems += InstantiateItems;
    }

    private void OnDisable()
    {
        wheelOfFortuneEventsListener.onCreateItems -= InstantiateItems;
    }

    #endregion


    private void InstantiateItems(int numberOfItems, WheelItemsContentData contentDataCurrent)
    {
        var angle = 360f / numberOfItems;
        
        var radius = Vector3.Distance(referenceCalculateRadius.position, transform.position);

        for (int i = 0; i < numberOfItems; i++)
        {
            var rotation = Quaternion.AngleAxis(i * angle, Vector3.forward);
            var direction = rotation * Vector3.up;

            var position = transform.position + (direction * radius);
            var go = Instantiate(itemPrefab, position, Quaternion.Euler(Vector3.zero));

            go.transform.SetParent(panelItemsAreaBronze);
            go.transform.localScale = Vector3.one;
        }
    }
}