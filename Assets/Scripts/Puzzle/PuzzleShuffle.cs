using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PuzzleShuffle : MonoBehaviour
{
    PuzzleManager _puzzleManager = default;
    CoinManager _coinManager = default;
    int _width = 10;
    int _height = 8;
    [SerializeField] int _shufflePrice = 10;
    Button _button = default;

    private void Start()
    {
        _puzzleManager = GetComponent<PuzzleManager>();
        _coinManager = GetComponent<CoinManager>();

        var obj = GameObject.FindWithTag("Shuffle");
        _button = obj.GetComponent<Button>();
    }

    private void Update()
    {
        if (_coinManager.Coin < _shufflePrice)
        {
            _button.enabled = false;
        }
        else
        {
            _button.enabled = true;
        }
    }

    public void ShufflePuzzle()
    {
        if (_puzzleManager.IsMoving || _coinManager.Coin < _shufflePrice) return;

        _coinManager.Coin -= _shufflePrice;
        Debug.Log(_coinManager.Coin + "coins left");
        _puzzleManager.IsMoving = true;
        List<GameObject> candidate = new List<GameObject>();

        // フルーツの参照を削除
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                candidate.Add(_puzzleManager.PuzzleBoard[x, y]);
                //_puzzleManager.PuzzleBoard[x, y].SetActive(false);
                _puzzleManager.PuzzleBoard[x, y] = null;
            }
        }

        //フルーツの配置をランダムで決める
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                int r = Random.Range(0, candidate.Count);
                var fruit = candidate[r];
                //fruit.GetComponent<FruitController>().PreviousPos = new Vector2(x, y);
                //fruit.GetComponent<FruitController>().IsMatch = false;
                fruit.transform.localPosition = new Vector2(x, y);
                _puzzleManager.PuzzleBoard[x, y] = fruit;

                var fruitController = _puzzleManager.PuzzleBoard[x, y].GetComponent<FruitController>();
                fruitController.PreviousPos = new Vector2(x, y);
                fruitController.IsMatch = false;
                fruitController.pbColumn = x;
                fruitController.pbRow = y;
                //_puzzleManager.PuzzleBoard[x, y].SetActive(true);
                candidate.RemoveAt(r);
            }
        }
        //Physics2D.SyncTransforms();

        _puzzleManager.Invoke("CheckMatch", 0.3f);
    }
}
