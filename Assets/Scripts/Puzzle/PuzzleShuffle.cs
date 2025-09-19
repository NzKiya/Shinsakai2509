using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PuzzleShuffle : MonoBehaviour
{
    PuzzleManager _puzzleManager = default;
    CoinManager _coinManager = default;
    int _width = 10;
    int _height = 8;
    [SerializeField] int _shufflePrice = 10;

    private void Start()
    {
        _puzzleManager = GetComponent<PuzzleManager>();
        _coinManager = GetComponent<CoinManager>();
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

                _puzzleManager.PuzzleBoard[x, y] = null;
                //Debug.Log(x + ", " + y + " = " + _puzzleManager.PuzzleBoard[x, y]);
            }
        }

        //フルーツの配置をランダムで決める
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                int r = Random.Range(0, candidate.Count);
                //Debug.Log("random " + r);
                var fruit = candidate[r];
                //var fruit = _pool.GetFruit(r);
                fruit.transform.localPosition = new Vector2(x, y);
                _puzzleManager.PuzzleBoard[x, y] = fruit;
                //fruit.GetComponent<FruitController>().PreviousPos = new Vector2(x, y);
                //fruit.SetActive(false);
                //fruit.SetActive(true);
                candidate.RemoveAt(r);
            }
        }


        _puzzleManager.CheckMatch();
    }
}
