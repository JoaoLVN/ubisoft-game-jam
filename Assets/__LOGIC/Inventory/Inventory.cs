using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerController))]
public class Inventory : MonoBehaviour
{
    public UnityEvent<Item> OnItemPicked = new UnityEvent<Item>();
    [SerializeField] private int _selectedSlot = 0;
    [SerializeField] private int _capacity = 5;
    [SerializeField] private Item[] _items;

    private PlayerController _controller;
    private void Awake()
    {
        _items = new Item[_capacity];
        _controller = GetComponent<PlayerController>();
    }

    private void LateUpdate()
    {

        ProcessInputs();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Item itemComponent = other.GetComponent<Item>();
        if (itemComponent == null) return;
        PickUpItem(itemComponent);
    }

    private void ProcessInputs()
    {
        if (_controller.Drop)
        {
            DropItem();
        }
        if (_controller.Use)
        {
            UseItem();
        }
    }

    private void PickUpItem(Item item)
    {
        int freeSlot = Array.FindIndex(_items, x => !x);
        if (freeSlot == -1) return;
        item.Pickup(this, _controller);
        _items[freeSlot] = item;
        OnItemPicked.Invoke(item);
    }
    private void DropItem()
    {
        var item = _items[_selectedSlot];
        if (item == null) return;
        item.Drop();
        _items[_selectedSlot] = null;
    }
    private void UseItem()
    {
        var item = _items[_selectedSlot];
        if (item == null) return;
        item.Use();
    }
}
