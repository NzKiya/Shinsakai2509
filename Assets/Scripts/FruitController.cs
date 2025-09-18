using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FruitController : MonoBehaviour
{
    PuzzleManager _puzzleManager;
    //自身の配列の座標
    int _column = default;
    int pbColumn
    {
        set { _column = value; }
        get { return _column; }
    }
    int _row = default;
    int pbRow
    {
        set { _row = value; }
        get { return _row; }
    }
    GameObject _neighborFruit;
    [SerializeField] float _speed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        _puzzleManager = FindObjectOfType<PuzzleManager>();
        _column = (int)this.transform.localPosition.x;
        _row = (int)this.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        //PuzzleBoardとtransform.localPositionの座標が違ったらPuzzleBoardの座標に移動する
        if (transform.localPosition.x != _column || transform.localPosition.y != _row)
        {
            transform.localPosition = Vector2.Lerp(transform.localPosition, new Vector2(_column, _row), _speed);
            Vector2 dif = (Vector2)transform.localPosition - new Vector2(_column, _row);

            if (Mathf.Abs(dif.magnitude) < 0.1f)
            {
                transform.localPosition = new Vector2(_column, _row);
                SetFruitToArray();
            }
        }
        else if (_row > 0 && _puzzleManager.PuzzleBoard[_column, _row - 1] == null)
        {
            FallFruit();
        }
    }

    public void MoveFruit(int x, int y, int moveX, int moveY)
    {
        if (moveY == 1)
        {
            if (y < 7)
            {
                _neighborFruit = _puzzleManager.PuzzleBoard[_column, _row + 1];
                _neighborFruit.GetComponent<FruitController>().pbRow -= 1;
                _row += 1;
            }
        }
        else if (moveY == -1)
        {
            if (y > 0)
            {
                _neighborFruit = _puzzleManager.PuzzleBoard[_column, _row - 1];
                _neighborFruit.GetComponent<FruitController>().pbRow += 1;
                _row -= 1;
            }
        }
        else if (moveX == 1)
        {
            if (x < 9)
            {
                _neighborFruit = _puzzleManager.PuzzleBoard[_column + 1, _row];
                _neighborFruit.GetComponent<FruitController>().pbColumn -= 1;
                _column += 1;
            }
        }
        else if (moveX == -1)
        {
            if (x > 0)
            {
                _neighborFruit = _puzzleManager.PuzzleBoard[_column - 1, _row];
                _neighborFruit.GetComponent<FruitController>().pbColumn += 1;
                _column -= 1;
            }
        }
    }

    void SetFruitToArray()
    {
        _puzzleManager.PuzzleBoard[_column, _row] = gameObject;
    }

    void FallFruit()
    {
        _puzzleManager.PuzzleBoard[_column, _row] = null;
        _row -= 1;
    }
}
