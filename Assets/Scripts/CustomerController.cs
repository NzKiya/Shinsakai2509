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
    public List<int> OrdersIndex
    {
        get { return _ordersIndex; }
        set { _ordersIndex = value; }
    }
    List<GameObject> _orders = new List<GameObject>();
    public List<GameObject> Orders
    {
        get { return _orders; }
        set { _orders = value; }
    }
    OrderManager _orderManager = default;
    bool _ordered = false;
    public bool Ordered
    {
        get { return _ordered; }
        set { _ordered = value; }
    }


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
            _orders.Add(juice);
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

        if (_orders.Count == 0)
        {
            _customerManager.Customers.Remove(this.gameObject);
            Destroy(this.gameObject);
            
        }
    }

    public void ShowOrder(bool show)
    {
        if (show && !_ordered)
        {
            //for (int i = 0; i < _orders.Count; i++)
            //{
            //    _orderManager.OrderList.Add(_orders[i]);
            //    _orderManager.OrderNums.Add(_ordersIndex[i]);
            //}
            _ordered = true;
        }
        //ジュース表示
        for (int i = 0; i < _orders.Count; i++)
        {
            Vector2 pos;
            if (_orders.Count == 1)
            {
                pos = new Vector2(2.25f, 0.25f);
            }
            else
            {
                pos = new Vector2(2.25f, 0.85f - 1.15f * i);
            }
            _orders[i].transform.localPosition = pos;
        }

        //注文中
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (var sr in renderers)
        {
            if (sr.gameObject != this.gameObject) sr.enabled = show;
        }
    }
}
