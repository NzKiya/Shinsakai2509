using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class FruitsPoolManager : MonoBehaviour
{
    [SerializeField] GameObject[] fruitPrefabs = new GameObject[5];
    Dictionary<int, ObjectPool<GameObject>> fruitPools = new();
    HashSet<GameObject> _releasedObj = new();

    void Awake()
    {
        for (int i = 0; i < fruitPrefabs.Length; i++)
        {
            fruitPools[i] = CreatePool(i);
        }
    }

    ObjectPool<GameObject> CreatePool(int index)
    {
        return new ObjectPool<GameObject>(
                () => Instantiate(fruitPrefabs[index], transform),
                fruit => fruit.SetActive(true),
                fruit => fruit.SetActive(false),
                fruit => Destroy(fruit),
                true, 0, 480
                );
    }

    public GameObject GetFruit(int index)
    {
        var result = fruitPools[index].Get();
        _releasedObj.Remove(result);
        return result;
    }

    public void ReleaseFruit(GameObject fruit, int index)
    {
        if (_releasedObj.Contains(fruit)) return;

        _releasedObj.Add(fruit);
        fruit.transform.position = new Vector2(0, -10);
        fruitPools[index].Release(fruit);
    }

}
