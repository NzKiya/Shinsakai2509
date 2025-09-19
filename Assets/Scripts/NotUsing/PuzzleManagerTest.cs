using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class PuzzleManagerTest : MonoBehaviour
{
    [SerializeField] Tilemap _puzzleTile = default;
    [SerializeField] Tile[] _fruitsTile = new Tile[5];
    enum Fruits
    {
        Apple, 
        Orange, 
        Grape, 
        Peach, 
        Banana
    }
    Fruits[,] _puzzleBoard = new Fruits[10, 8];
    


    void ResetPuzzle()
    {
        //フルーツの配置をランダムで決める
        for (int i = 0; i < _puzzleBoard.GetLength(0); i++)
        {
            for (int j = 0; j < _puzzleBoard.GetLength(1); j++)
            {
                int num = Random.Range(0, Enum.GetValues(typeof(Fruits)).Length);
                _puzzleBoard[i, j] = (Fruits)num;
                //Tileのスプライト変更
                Vector3Int cellPosition = new Vector3Int(i, j, 0);
                _puzzleTile.SetTile(cellPosition, _fruitsTile[num]);
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

    void horizontalCheck()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        ResetPuzzle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
