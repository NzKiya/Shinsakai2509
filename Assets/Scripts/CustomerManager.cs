using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] GameObject _customerObj = default;
    [SerializeField] float _interval = 10;
    List<GameObject> _customers = new List<GameObject>();
    public List<GameObject> Customers
    {
        get { return _customers; }
        set { _customers = value; }
    }
    bool _gameOver = false;
    public bool GameOver
    {
        get { return _gameOver; }
        set { _gameOver = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameOver)
        {
            //Debug.Log("GameOver");
        }

        for (int i = 0; i < _customers.Count; i++)
        {
            var pos = i switch
            {
                0 => new Vector2(-4.5f, 5),
                1 => new Vector2(0, 5),
                2 => new Vector2(4.5f, 5),
                3 => new Vector2(9, 5),
                4 => new Vector2(11.25f, 5),
                _ => new Vector2(13.5f, 5)
            };
            _customers[i].transform.position = pos;

            bool showOrder = i switch
            {
                0 or 1 or 2 => true,
                _ => false
            };
            _customers[i].GetComponent<CustomerController>().ShowOrder(showOrder);

        }
    }

    IEnumerator SpawnLoop()
    {
        var wait = new WaitForSeconds(_interval);
        while (true)
        {
            _customers.Add(Instantiate(_customerObj));

            yield return wait;
        }
    }
}
