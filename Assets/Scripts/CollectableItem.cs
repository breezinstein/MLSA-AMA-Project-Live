using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer),typeof(CircleCollider2D))]
public class CollectableItem : MonoBehaviour
{
    public ItemType type;
    private SpriteRenderer _spriteRenderer;
    public Sprite circleSprite;
    public Sprite squareSprite;
    public Sprite diamondSprite;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateSprite();
        ItemSpawner.ItemCount++;
    }

    public void UpdateSprite()
    {
        switch (type)
        {
            case ItemType.Diamond:
                _spriteRenderer.sprite = diamondSprite;
                break;
            case ItemType.Circle:
                _spriteRenderer.sprite = circleSprite;
                break;
            case ItemType.Square:
            default:
                _spriteRenderer.sprite = squareSprite;
                break;
        }
    }

    public Sprite GetSprite(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Diamond:
                return diamondSprite;

            case ItemType.Circle:
                return circleSprite;
            case ItemType.Square:
            default:
                return squareSprite;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        ItemSpawner.ItemCount--;
    }
}
