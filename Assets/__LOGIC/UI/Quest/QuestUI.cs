using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private QuestItem _questItemTemplate;
    [SerializeField] private QuestManager _questManager;
    private QuestItem[] _questSlot;
    private void Awake()
    {
        var quests = _questManager.Quests;
        _questSlot = new QuestItem[quests.Length];
        for (int i = 0; i < _questSlot.Length; i++)
        {
            GameObject slot = GameObject.Instantiate(_questItemTemplate.gameObject, transform, true);
            slot.SetActive(true);
            _questSlot[i] = slot.GetComponent<QuestItem>();
        }
    }
    private void Update()
    {
        var quests = _questManager.Quests;
        for (int i = 0; i < _questSlot.Length; i++)
        {
            _questSlot[i].UpdateUI(quests[i]);
        }
    }
}
