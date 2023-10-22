using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Properties", menuName = "Inventory/ItemPorperties", order = 0)]
public class ItemProperties : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private int itemId;
    [SerializeField] private int itemPrice;

    [Header("Expire date")] [SerializeField]
    private int day, month, year;

    public string Name => name;

    public int ItemId => itemId;

    public int ItemPrice => itemPrice;
    public List<int> ExpireDate => new List<int> {day, month, year};
}