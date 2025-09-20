using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    Queue<int> _orderQueue = new();
    public Queue<int> OrderQueue
    {
        get { return _orderQueue; }
        set { _orderQueue = value; }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
