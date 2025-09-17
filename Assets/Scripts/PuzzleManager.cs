using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] GameObject[] Fruits = new GameObject[5];
    int _width = 10;
    int _height = 8;
    GameObject[,] _puzzleBoard = new GameObject[10, 8];

    [SerializeField] GameObject _cursor;


    void ResetPuzzle()
    {
        //フルーツの配置をランダムで決める
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                int num = Random.Range(0, Fruits.Length);
                var fruit = Instantiate(Fruits[num], this.transform);
                fruit.transform.localPosition = new Vector2(i, j);
                _puzzleBoard[i, j] = fruit;
            }
        }

        ////配置をConsoleに表示
        //for (int i = 0; i < _puzzleBoard.GetLength(0); i++)
        //{
        //    string line = null;
        //    for (int j = 0; j < _puzzleBoard.GetLength(1); j++)
        //    {
        //        line += _puzzleBoard[i, j].ToString() + ", ";
        //    }
        //    Debug.Log(line);
        //}
    }

        // Start is called before the first frame update
        void Start()
    {
        ResetPuzzle();
        Instantiate(_cursor, this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
