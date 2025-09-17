using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    int _moveUnit = 1;
    SpriteRenderer _sr = default;
    int _clicked = 1;

    // Start is called before the first frame update
    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCursor();

        if (Input.GetMouseButtonDown(0))
        {
            _clicked *= -1;
        }

        Color c = _clicked switch
        {
            -1 => new Color(0, 255, 0, 0.5f),
            1 or _ => new Color(255, 255, 255, 0.5f)
        };

        _sr.color = c;
    }

    void MoveCursor()
    {
        Vector3 direction = Vector3.zero;

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && transform.localPosition.y < 7)
        {
            if (_clicked == 1) direction = Vector3.up;
            else
            {
                _clicked = 1;
                //フルーツ入れ替え
            }
        }
        else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && transform.localPosition.y > 0)
        {
            if (_clicked == 1) direction = Vector3.down;
            else
            {
                _clicked = 1;
                //フルーツ入れ替え
            }
        }
        else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && transform.localPosition.x < 9)
        {
            if (_clicked == 1) direction = Vector3.right;
            else
            {
                _clicked = 1;
                //フルーツ入れ替え
            }
        }
        else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && transform.localPosition.x > 0)
        {
            if (_clicked == 1) direction = Vector3.left;
            else
            {
                _clicked = 1;
                //フルーツ入れ替え
            }
        }

        transform.position +=  direction * _moveUnit;
    }
}
