using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpriteManager : MonoBehaviour
{
    /// <summary>
    /// index % 3 ‚ª
    /// 0 angry
    /// 1 laugh
    /// 2 smile
    /// </summary>
    [SerializeField] Sprite[] _customerSprites;
    public Sprite[] CustomerSprites
    {
        get { return _customerSprites; }
        set { _customerSprites = value; }
    }
    [SerializeField] GameObject[] _juice;
    public GameObject[] Juice
    {
        get { return _juice; }
        set { _juice = value; }
    }

    public int ChoosePerson()
    {
        int s = Random.Range(0, _customerSprites.Length)/ 3 * 3 + 1;
        Debug.Log(s);
        return s;
    }

}
