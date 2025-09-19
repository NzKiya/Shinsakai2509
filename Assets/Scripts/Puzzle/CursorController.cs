using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] Color _colorNormal = default;
    [SerializeField] Color _colorSelected = default;
    int _moveUnit = 1;
    SpriteRenderer _sr = default;
    bool _clicked = false;

    //GameObject _pmObj = default;
    PuzzleManager _puzzleManager = default;
    Collider2D _collider;
    int _moveX = 0;
    int _moveY = 0;

    // Start is called before the first frame update
    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _puzzleManager = FindObjectOfType<PuzzleManager>();
        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCursor();

        if (Input.GetMouseButtonDown(0))
        {
            if (!_puzzleManager.IsMoving) _clicked = !_clicked;
        }

        Color c = _clicked switch
        {
            true => _colorSelected,
            false => _colorNormal
        };

        _sr.color = c;
    }

    void MoveCursor()
    {
        Vector2 direction = Vector2.zero;

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && transform.localPosition.y < 7)
        {
            if (!_clicked) direction = Vector2.up;
            else
            {
                _clicked = !_clicked;
                //フルーツ入れ替え
                _moveY = 1;
                _collider.enabled = true;
            }
        }
        else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && transform.localPosition.y > 0)
        {
            if (!_clicked) direction = Vector2.down;
            else
            {
                _clicked = !_clicked;
                //フルーツ入れ替え
                _moveY = -1;
                _collider.enabled = true;
            }
        }
        else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && transform.localPosition.x < 9)
        {
            if (!_clicked) direction = Vector2.right;
            else
            {
                _clicked = !_clicked;
                //フルーツ入れ替え
                _moveX = 1;
                _collider.enabled = true;
            }
        }
        else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && transform.localPosition.x > 0)
        {
            if (!_clicked) direction = Vector2.left;
            else
            {
                _clicked = !_clicked;
                //フルーツ入れ替え
                _moveX = -1;
                _collider.enabled = true;
            }
        }

        var pos = (Vector2)this.transform.localPosition;
        pos += direction * _moveUnit;
        this.transform.localPosition = pos;
        Physics2D.SyncTransforms();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _puzzleManager.IsMoving = true;

        int x = (int)this.transform.localPosition.x;
        int y = (int)this.transform.localPosition.y;
        Debug.Log("Hit x = " + x + ", y = " + y + ", moveX = " + _moveX + ", moveY = " + _moveY);
        collision.GetComponent<FruitController>().MoveFruit(x, y, _moveX, _moveY);
        //Invoke("CheckMatch", 0.3f);

        var pos = (Vector2)this.transform.localPosition;
        pos.x += _moveX * _moveUnit;
        pos.y += _moveY * _moveUnit;
        this.transform.localPosition = pos;

        _moveX = 0;
        _moveY = 0;
        if(_collider) _collider.enabled = false;
    }

    //void CheckMatch()
    //{
    //    _puzzleManager.CheckMatch();
    //}
}
