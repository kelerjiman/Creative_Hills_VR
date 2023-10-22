using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUi : MonoBehaviour
{
    public static InventoryUi Instance;
    [SerializeField] private QuestUiTick questTickPrefab;
    [SerializeField] private Transform questTickHolder;
    private List<QuestUiTick> _currentTicks;

    private void Awake()
    {
        _currentTicks = new List<QuestUiTick>();
        Instance = this;
    }

    public void InitializeUi(List<QuestItem> questItems)
    {
        foreach (var questItem in questItems)
        {
            _currentTicks.Add(Instantiate(questTickPrefab, questTickHolder));
            _currentTicks[^1].InitializeTick(questItem);
        }
    }

    public void RefreshUi()
    {
        foreach (var questUiTick in _currentTicks)
        {
            questUiTick.RefreshTick();
        }
    }
}