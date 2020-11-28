using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private Inventory _invetory;
    [SerializeField] private Quest[] _quests;

    private void Awake()
    {
        foreach (Quest quest in _quests)
        {
            quest.Init(_invetory);
        }
    }

    private void Update()
    {
    }
}
