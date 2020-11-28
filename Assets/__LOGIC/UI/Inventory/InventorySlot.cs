using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public bool Selected
    {
        get
        {
            return _selected;
        }
        set
        {
            Color newColor = _background.color;
            newColor.a = value ? 1 : .5f;
            _background.color = newColor;
            _selected = value;
        }
    }
    [SerializeField] private Image _background;
    [SerializeField] private Image _itemImage;

    private bool _selected;

    public void SetItem(Item item)
    {
        if (item == null)
        {
            _itemImage.sprite = null;
            _itemImage.color = Color.clear;
            return;
        }
        _itemImage.color = Color.white;

        _itemImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
    }

}
