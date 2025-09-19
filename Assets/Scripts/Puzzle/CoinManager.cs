using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    int _coin = 0;
    public int Coin
    {
        set {  _coin = value; }
        get { return _coin; }
    }

    [SerializeField] GameObject _coinTextObj;
    Text _coinText;


    private void Start()
    {
        _coinText = _coinTextObj.GetComponent<Text>();
    }

    private void Update()
    {
        _coinText.text = "Coin : " + _coin.ToString("D4");
    }
}
