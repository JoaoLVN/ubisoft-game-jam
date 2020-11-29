using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Inventory : MonoBehaviour
{
    public int SelectedSlot
    {
        get
        {
            return _selectedSlot;
        }
        set
        {
            _selectedSlot = (value % _capacity + _capacity) % _capacity;
        }
    }
    public Item[] Items
    {
        get
        {
            return (Item[])_items.Clone();
        }
    }

    public int Capacity
    {
        get
        {
            return _capacity;
        }
    }

    private int _selectedSlot = 0;
    [SerializeField] private int _capacity = 5;
    [SerializeField] private Item _defaultItem;
    [SerializeField] private Item[] _items;

    private PlayerController _controller;
    private List<Collider2D> _ignoredColliders = new List<Collider2D>();
    private Item _defaultItemInstance;
    private void Awake()
    {
        _items = new Item[_capacity];
        _controller = GetComponent<PlayerController>();
        _defaultItemInstance = GameObject.Instantiate(_defaultItem.gameObject).GetComponent<Item>();
        _defaultItemInstance.Pickup(this, _controller);
    }

    private void LateUpdate()
    {

        ProcessInputs();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_ignoredColliders.Contains(other)) return;
        _ignoredColliders.Add(other);
        Item itemComponent = other.GetComponent<Item>();
        if (itemComponent == null) return;
        PickUpItem(itemComponent);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_ignoredColliders.Contains(other))
        {
            Item itemComponent = other.GetComponent<Item>();
            if (itemComponent != null && _items.Contains(itemComponent)) return;
            _ignoredColliders.Remove(other);
        }
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
        int freeSlot = !_items[_selectedSlot] ? _selectedSlot : Array.FindIndex(_items, x => !x);
        if (freeSlot == -1) return;
        item.Pickup(this, _controller);
        _items[freeSlot] = item;
    }
    private void DropItem()
    {
        var item = _items[_selectedSlot];
        if (item == null) return;
        item.Drop();
        _items[_selectedSlot] = null;
    }

    public void DropItem(Item item)
    {
        var index = Array.FindIndex(_items, (x) => x == item);
        if (index == -1) return;
        item.Drop();
        _items[index] = null;
    }
    private void UseItem()
    {
        var item = _items[_selectedSlot];
        if (item == null)
        {
            _defaultItemInstance.Use();
            return;
        }
        item.Use();
    }
}
