using UnityEngine;
using UnityEngine.UI;

public class QuestItem : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private Text _progressText;

    public void UpdateUI(Quest quest)
    {
        if (quest.GetType() == typeof(PickupItemQuest))
        {
            PickupItemQuest itemQuest = (PickupItemQuest)quest;
            _itemImage.sprite = itemQuest.Item.GetComponent<SpriteRenderer>().sprite;
            _progressText.text = $"{itemQuest.CurrentQuantity}/{itemQuest.Quantity}";
        }
    }
}
