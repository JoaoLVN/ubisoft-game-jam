using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PickupItemQuest", order = 1)]
public class PickupItemQuest : Quest
{
    public override bool Complete
    {
        get
        {
            return _complete;
        }
    }
    public int CurrentQuantity
    {
        get
        {
            return _currentQuantity;
        }
    }
    public Item Item;
    public int Quantity;
    private int _currentQuantity;
    private bool _complete;

    public override void Init(Inventory inventory)
    {
        _complete = false;
        _currentQuantity = 0;
        inventory.OnItemPicked.AddListener((item) =>
        {
            if (item.GetType() != Item.GetType()) return;
            _complete = ++_currentQuantity >= Quantity;
        });
    }
}
