using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D),typeof(SpriteRenderer),typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    public float moveSpeed = 1f;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private ItemType type = ItemType.Circle;
    private int score;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _rigidbody2D.MovePosition(transform.position - new Vector3(moveSpeed * Time.deltaTime, 0f));
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _rigidbody2D.MovePosition(transform.position + new Vector3(moveSpeed * Time.deltaTime, 0f));
        }
    }
    void UpdateType(ItemType itemType)
    {
        type = (ItemType)Mathf.Repeat((int)itemType + 1, 3);

    }

    void UpdateSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }

    void UpdateScore(int amount)
    {
        score += amount;
        score = Mathf.Clamp(score, 0, 999);
        scoreText.text = score.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<CollectableItem>())
        {
            CollectableItem collectableItem = collision.gameObject.GetComponent<CollectableItem>();
            if (collectableItem.type == type)
            {
                UpdateScore(2);
                UpdateType(collectableItem.type);
                UpdateSprite(collectableItem.GetSprite(type));
            }
            else
            {
                UpdateScore(-1);
            }
            Destroy(collectableItem.gameObject);
        }
    }

}

public enum ItemType { Diamond, Circle, Square }
