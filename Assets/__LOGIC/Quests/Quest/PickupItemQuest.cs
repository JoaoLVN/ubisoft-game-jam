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
    public Item Item;
    private bool _complete;

    public override void Init(Inventory inventory)
    {
        _complete = false;
        inventory.OnItemPicked.AddListener((item) =>
        {
            _complete = item.GetType() == Item.GetType();
        });
    }
}
