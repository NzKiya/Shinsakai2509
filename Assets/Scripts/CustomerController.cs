using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    [SerializeField] float _timeLimit = 20;
    float _elapsedTime = 0f;

    CustomerManager _customerManager = default;
    SpriteManager _spriteManager = default;
    SpriteRenderer _sr = default;
    int _sprite = default;

    List<int> _ordersIndex = new List<int>();
    List<GameObject> _order = new List<GameObject>();
    OrderManager _orderManager = default;
    

    // Start is called before the first frame update
    void Awake()
    {
        _customerManager = GameObject.FindObjectOfType<CustomerManager>();
        _orderManager = GameObject.FindObjectOfType<OrderManager>();
        _spriteManager = GameObject.FindObjectOfType<SpriteManager>();
        _sr = GetComponent<SpriteRenderer>();
        _sprite = _spriteManager.ChoosePerson();
        _sr.sprite = _spriteManager.CustomerSprites[_sprite];

        for (int i = 0; i < Random.Range(1, 3); i++)
        {
            _ordersIndex.Add(Random.Range(0, 5));
            var juice = Instantiate(_spriteManager.Juice[_ordersIndex[i]], this.transform);
            _order.Add(juice);

            ShowOrder(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _timeLimit)
        {
            _customerManager.GameOver = true;
        }
        else if (_elapsedTime > _timeLimit * 0.75)
        {
            _sr.sprite = _spriteManager.CustomerSprites[_sprite - 1];
        }
        else if ( _elapsedTime > _timeLimit * 0.5)
        {
            _sr.sprite = _spriteManager.CustomerSprites[_sprite + 1];
        }
    }

    public void ShowOrder(bool show)
    {
        for (int i = 0; i < _order.Count; i++)
        {
            Vector2 pos;
            if (_order.Count == 1)
            {
                pos = new Vector2(2.25f, 0.25f);
            }
            else
            {
                pos = new Vector2(2.25f, 0.85f - 1.15f * i);
            }
            _order[i].transform.localPosition = pos;
        }

        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (var sr in renderers)
        {
            if (sr.gameObject != this.gameObject) sr.enabled = show;
        }
    }
}
