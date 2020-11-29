using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class ShoppingKart : MonoBehaviour
{
    public UnityEvent<Item> OnItemCollected = new UnityEvent<Item>();
    [SerializeField] private QuestManager _questManager;
    [SerializeField] private Animator _animator;


    private void OnTriggerEnter2D(Collider2D other)
    {
        Inventory inventory = other.gameObject.GetComponent<Inventory>();
        if (inventory == null) return;
        var items = inventory.Items.Where(item => _questManager.IsQuestItem(item));
        foreach (Item item in items)
        {
            inventory.DropItem(item);
            OnItemCollected.Invoke(item);
            GameObject.Destroy(item.gameObject);
        }

        _animator.SetInteger("CheckoutItems", _animator.GetInteger("CheckoutItems") + items.ToArray().Length);
    }

}
