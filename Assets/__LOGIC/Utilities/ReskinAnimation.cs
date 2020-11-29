using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ReskinAnimation : MonoBehaviour
{
    [SerializeField] private string _spriteSheetPath;
    void LateUpdate()
    {
        var subSprites = Resources.LoadAll<Sprite>($"Characters/{_spriteSheetPath}");
        foreach (var renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            string spriteName = renderer.sprite.name;
            var newSprite = Array.Find(subSprites, Item => Item.name == spriteName);

            foreach (var sub in subSprites)
                Debug.Log(sub.name);
            if (newSprite)
            {
                renderer.sprite = newSprite;
            }
        }
    }
}
