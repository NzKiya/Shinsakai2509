using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.Progress;
using System.Linq;
using Random = UnityEngine.Random;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] GameObject[] Fruits = new GameObject[5];
    int _width = 10;
    int _height = 8;
    //FruitsPoolManager _pool = default;

    GameObject[,] _puzzleBoard = new GameObject[10, 8];
    public GameObject[,] PuzzleBoard
    {
        set { _puzzleBoard = value; }
        get { return _puzzleBoard; }
    }

    List<GameObject> _deleteList = new List<GameObject>();
    [SerializeField] float _deleteTime = 0.2f;
    [SerializeField] float _spawnTime = 1.2f;

    ScoreManager _scoreManager;

    [SerializeField] GameObject _cursorPrefab;
    GameObject _cursor = default;
    bool _isMoving = false;
    public bool IsMoving
    {
        set { _isMoving = value; }
        get { return _isMoving; }
    }

    CoinManager _coinManager;

    // Start is called before the first frame update
    void Start()
    {
        //_pool = GetComponent<FruitsPoolManager>();
        _scoreManager = GetComponent<ScoreManager>();
        _coinManager = GetComponent<CoinManager>();
        ResetPuzzle();
        _cursor = Instantiate(_cursorPrefab, this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetPuzzle()
    {
        //フルーツの配置をランダムで決める
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                //候補をリスト化
                List<int> candidate = new List<int>();
                for (int i = 0; i < Fruits.Length; i++)
                {
                    if (!IsMatch(x, y, i, true)) candidate.Add(i);
                }

                int r = candidate.Count > 0
                    ? candidate[Random.Range(0, candidate.Count)]
                    : Random.Range(0, Fruits.Length);

                var fruit = Instantiate(Fruits[r], this.transform);
                //var fruit = _pool.GetFruit(r);
                fruit.transform.localPosition = new Vector2(x, y);
                fruit.GetComponent<FruitController>().PreviousPos = new Vector2(x, y);
                _puzzleBoard[x, y] = fruit;
            }
        }
    }

    bool IsMatch(int x, int y, int fruitIndex, bool reset)
    {
        bool match = false;

        if (x > 1)
        {
            int left1 = GetFruitIndex(_puzzleBoard[x - 1, y]);
            int left2 = GetFruitIndex(_puzzleBoard[x - 2, y]);
            if (left1 == fruitIndex && left2 == fruitIndex)
            {
                if (!reset)
                {
                    _puzzleBoard[x, y].GetComponent<FruitController>().IsMatch = true;
                    _puzzleBoard[x - 1, y].GetComponent<FruitController>().IsMatch = true;
                    _puzzleBoard[x - 2, y].GetComponent<FruitController>().IsMatch = true;
                }
                match = true;
            }
        }

        if (y > 1)
        {
            int down1 = GetFruitIndex(_puzzleBoard[x, y - 1]);
            int down2 = GetFruitIndex(_puzzleBoard[x, y - 2]);
            if (down1 == fruitIndex && down2 == fruitIndex)
            {
                if (!reset)
                {
                    _puzzleBoard[x, y].GetComponent<FruitController>().IsMatch = true;
                    _puzzleBoard[x, y - 1].GetComponent<FruitController>().IsMatch = true;
                    _puzzleBoard[x, y - 2].GetComponent<FruitController>().IsMatch = true;
                }
                match = true;
            }
        }

        return match;
    }

    int GetFruitIndex(GameObject fruit)
    {
        if (!fruit) return -1;

        for (int i = 0; i < Fruits.Length; i++)
        {
            if (fruit.tag == Fruits[i].tag) return i;
        }

        return -1;
    }

    public void CheckMatch()
    {
        StartCoroutine(MatchSequence());
    }

    IEnumerator MatchSequence()
    {
        for (int x = 0; x < _width; x++) 
        {
            for (int y = 0; y < _height; y++)
            {
                int fruitIndex = GetFruitIndex(_puzzleBoard[x, y]);
                IsMatch(x, y, fruitIndex, false);
            }
        }

        foreach (var item in _puzzleBoard)
        {
            if (item.GetComponent<FruitController>().IsMatch)
            {
                //3つそろった時のフルーツの処理追加（アニメーション？）
                item.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, -0.5f);
                _deleteList.Add(item);
            }
        }

        if (_deleteList.Count > 0)
        {
            _coinManager.Coin += CountMatchGroups();
            //yield return new WaitForSeconds(_deleteTime); 
            DeleteFruits();
            yield return new WaitForSeconds(_spawnTime);
            SpawnNewFruit();
            yield return new WaitForSeconds(_deleteTime);
            StartCoroutine(MatchSequence());
        }
        else
        {
            foreach (var item in _puzzleBoard)
            {
                item.GetComponent<FruitController>().BackToPreviousPos();
            }

            yield return new WaitForSeconds(_deleteTime);
            CanMoveFruits();
        }
    }

    void DeleteFruits()
    {
        foreach (var item in _deleteList)
        {
            int fruitIndex = GetFruitIndex(item);
            Destroy(item);
            _puzzleBoard[(int)item.transform.localPosition.x, (int)item.transform.localPosition.y] = null;
            _scoreManager.Score[fruitIndex] += 1;
        }

        _deleteList.Clear();
        //yield return SpawnNewFruit();
    }

    void SpawnNewFruit()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                if (_puzzleBoard[x, y] == null)
                {
                    int r = Random.Range(0, Fruits.Length);
                    var fruit = Instantiate(Fruits[r], this.transform);
                    fruit.transform.localPosition = new Vector2(x, y + 0.3f);
                    _puzzleBoard[x, y] = fruit;
                }
            }
        }

        foreach (var item in _puzzleBoard)
        {
            //int c = (int)item.transform.localPosition.x;
            //int r = (int)item.transform.localPosition.y;

            item.GetComponent<FruitController>().PreviousPos = (Vector2)item.transform.localPosition;
        }
    }

    void CanMoveFruits()
    {
        _isMoving = false;
    }

    int CountMatchGroups()
    {
        bool[,] visited = new bool[_width, _height];
        int groupCount = 0;

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var fruit = _puzzleBoard[x, y];
                if (fruit == null || visited[x, y]) continue;

                if (fruit.GetComponent<FruitController>().IsMatch)
                {
                    ExploreGroup(x, y, visited);
                    groupCount++;
                }
            }
        }

        return groupCount;
    }

    void ExploreGroup(int startX, int startY, bool[,] visited)
    {
        Queue<Vector2Int> queue = new();
        queue.Enqueue(new Vector2Int(startX, startY));
        visited[startX, startY] = true;

        while (queue.Count > 0)
        {
            var pos = queue.Dequeue();
            int x = pos.x;
            int y = pos.y;

            foreach (var dir in new[] {
            new Vector2Int(1, 0), new Vector2Int(-1, 0),
            new Vector2Int(0, 1), new Vector2Int(0, -1)
        })
            {
                int nx = x + dir.x;
                int ny = y + dir.y;

                if (nx >= 0 && nx < _width && ny >= 0 && ny < _height && !visited[nx, ny])
                {
                    var neighbor = _puzzleBoard[nx, ny];
                    if (neighbor != null && neighbor.GetComponent<FruitController>().IsMatch)
                    {
                        visited[nx, ny] = true;
                        queue.Enqueue(new Vector2Int(nx, ny));
                    }
                }
            }
        }
    }

}
