using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Sprite _fullHeart;
    [SerializeField] private Sprite _emptyHeart;
    [SerializeField] private Image _heartTemplate;
    [SerializeField] private Character _character;
    private Image[] _slots;
    private void Start()
    {
        _slots = new Image[_character.TotalHealth];
        for (int i = 0; i < _character.TotalHealth; i++)
        {
            GameObject slot = GameObject.Instantiate(_heartTemplate.gameObject, transform, true);
            slot.SetActive(true);
            _slots[i] = slot.GetComponent<Image>();
        }

    }

    private void Update()
    {
        for (int i = 0; i < _character.TotalHealth; i++)
        {
            _slots[i].sprite = i < _character.Health ? _fullHeart : _emptyHeart;
        }

    }
}
