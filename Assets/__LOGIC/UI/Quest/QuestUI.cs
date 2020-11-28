using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private QuestItem _questItemTemplate;
    [SerializeField] private QuestManager _questManager;
    private QuestItem[] _slots;
    private void Awake()
    {
        var quests = _questManager.Quests;
        _slots = new QuestItem[quests.Length];
        for (int i = 0; i < _slots.Length; i++)
        {
            GameObject slot = GameObject.Instantiate(_questItemTemplate.gameObject, transform, true);
            slot.SetActive(true);
            _slots[i] = slot.GetComponent<QuestItem>();
        }
    }
    private void Update()
    {

    }
}
