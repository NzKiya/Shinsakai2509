using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitsCounter : MonoBehaviour
{
    [SerializeField] Text[] _texts = new Text[5];
    int[] _fruits = new int[5];
    public int[] Fruits
    {
        set { _fruits = value; }
        get { return _fruits; }
    }

    Text _scoreText = default;


    // Start is called before the first frame update
    void Start()
    {
        //var obj = GameObject.FindGameObjectWithTag("Score");
        //_scoreText = obj.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < _fruits.Length; i++)
        {
            _texts[i].text = Fruits[i].ToString();
        }
        //string score = "Apple : " + _fruits[0].ToString("D4")
        //    + "\nBanana : " + _fruits[1].ToString("D4")
        //    + "\nGrape : " + _fruits[2].ToString("D4")
        //    + "\nOrange : " + _fruits[3].ToString("D4")
        //    + "\nPeach : " + _fruits[4].ToString("D4");
        //_scoreText.text = score;
    }
}
