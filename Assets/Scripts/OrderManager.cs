using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    CustomerManager _customerManager = default;

    //List<GameObject> _orderList = new();
    //public List<GameObject> OrderList
    //{
    //    get { return _orderList; }
    //    set { _orderList = value; }
    //}
    //List<int> _orderNums = new();
    //public List<int> OrderNums
    //{
    //    get { return _orderNums; }
    //    set { _orderNums = value; }
    //}

    GameObject _puzzle = default;
    FruitsCounter _fruitsCounter = default;
    [SerializeField] int _fruitsNeed = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        _customerManager = GetComponent<CustomerManager>();
        _puzzle = GameObject.Find("Puzzle");
        _fruitsCounter = _puzzle.GetComponent<FruitsCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeJuice(int fruitIndex)
    {
        if (_fruitsCounter.Fruits[fruitIndex] >= _fruitsNeed)
        {
            //Debug.Log("right juice");
            //foreach (var customer in _customerManager.Customers)
            //{
            //    if (customer.GetComponent<CustomerController>().Ordered)
            //}
            foreach (var customer in _customerManager.Customers.Where(c => c.GetComponent<CustomerController>().Ordered))
            {
                var customerController = customer.GetComponent<CustomerController>();
                for (int i = 0; i < customerController.OrdersIndex.Count; i++)
                {
                    if (customerController.OrdersIndex[i] == fruitIndex)
                    {
                        //Debug.Log("success");
                        customerController.OrdersIndex.RemoveAt(i);
                        Destroy(customerController.Orders[i]);
                        customerController.Orders.RemoveAt(i);

                        if (customerController.Orders.Count == 0)
                        {
                            _customerManager.Customers.Remove(customer);
                            Destroy(customer);
                        }

                        _fruitsCounter.Fruits[fruitIndex] -= _fruitsNeed;
                        return;
                    }
                }
            }
        }
    }
}
