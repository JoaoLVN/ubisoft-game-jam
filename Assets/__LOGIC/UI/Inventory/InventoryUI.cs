using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private InventorySlot _slotTemplate;
    private InventorySlot[] _slots;
    private void Awake()
    {
        _slots = new InventorySlot[_inventory.Capacity];
        for (int i = 0; i < _inventory.Capacity; i++)
        {
            GameObject slot = GameObject.Instantiate(_slotTemplate.gameObject, transform, true);
            slot.SetActive(true);
            _slots[i] = slot.GetComponent<InventorySlot>();
        }
    }

    private void Update()
    {
        var inventoryItems = _inventory.Items;
        for (int i = 0; i < _inventory.Capacity; i++)
        {
            _slots[i].Selected = i == _inventory.SelectedSlot;
            _slots[i].SetItem(inventoryItems[i]);
        }
    }
}
