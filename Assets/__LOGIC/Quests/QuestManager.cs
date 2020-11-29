using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Quest[] Quests
    {
        get
        {
            return (Quest[])_quests.Clone();
        }
    }
    [SerializeField] private ShoppingKart _shoppingKart;
    [SerializeField] private Quest[] _quests;

    private void Awake()
    {
        foreach (Quest quest in _quests)
        {
            quest.Init(_shoppingKart);
        }
    }

    public bool IsQuestItem(Item item)
    {
        if (item == null) return false;
        var pickupItemQuests = _quests.Where(x => (x.GetType() == typeof(PickupItemQuest)));
        foreach (PickupItemQuest quest in pickupItemQuests)
        {
            if (quest.Item.GetComponent<SpriteRenderer>().sprite == item.GetComponent<SpriteRenderer>().sprite) return true;
        }
        return false;
    }

}
