using UnityEngine;

public class ShopItem : MonoBehaviour, IPickable, ISelectable
{
    [SerializeField] private Outline outLine;

    public void Select()
    {
        outLine.enabled = true;
    }

    public void Deselect()
    {
        outLine.enabled = false;
    }

    public void PickUp()
    {
        outLine.enabled = false;
    }

    public void Drop()
    {
    }
}