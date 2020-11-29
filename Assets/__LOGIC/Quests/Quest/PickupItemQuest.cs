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

    public override void Init(ShoppingKart shoppingKart)
    {
        _complete = false;
        _currentQuantity = 0;
        shoppingKart.OnItemCollected.AddListener((item) =>
        {
            if (Item.GetComponent<SpriteRenderer>().sprite != item.GetComponent<SpriteRenderer>().sprite) return;
            _complete = ++_currentQuantity >= Quantity;
        });
    }
}
