using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public static int ItemCount = 0;
    private static int MaxItemCount = 15;
    private Bounds bounds;
    public float xOffset;
    public CollectableItem collectableItemPrefab;
    public float interval = 2f;
    public float decentRate = 1f;
    private float _timer = 0f;
    Vector2 RandomXPosition
    {
        get
        {
            Vector2 temp = new Vector2(Random.Range(bounds.min.x, bounds.max.x), transform.position.y);
            return temp;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        bounds.center = Camera.main.transform.position;
        bounds.SetMinMax(Camera.main.ViewportToWorldPoint(new Vector3(0, 0)), Camera.main.ViewportToWorldPoint(new Vector3(1, 1)));
        bounds.extents -= new Vector3(xOffset, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer < 0f)
        {
            _timer = Random.Range(interval, interval * 1.5f);
            SpawnItem();
        }
        _timer -= Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y - (decentRate * Time.deltaTime));
    }

    void SpawnItem()
    {
        if (collectableItemPrefab != null && ItemCount < MaxItemCount)
        {
            CollectableItem item = Instantiate(collectableItemPrefab, RandomXPosition, Quaternion.identity);
            item.type = (ItemType)Random.Range(0, 3);
        }
    }
}
