using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitsCounter : MonoBehaviour
{
    int[] _score = new int[5];
    public int[] Score
    {
        set { _score = value; }
        get { return _score; }
    }

    Text _scoreText = default;


    // Start is called before the first frame update
    void Start()
    {
        var obj = GameObject.FindGameObjectWithTag("Score");
        _scoreText = obj.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        string score = "Apple : " + _score[0].ToString("D4")
            + "\nBanana : " + _score[1].ToString("D4")
            + "\nGrape : " + _score[2].ToString("D4")
            + "\nOrange : " + _score[3].ToString("D4")
            + "\nPeach : " + _score[4].ToString("D4");
        _scoreText.text = score;
    }
}
