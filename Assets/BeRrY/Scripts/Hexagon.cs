using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hexagon : MonoBehaviour {
    //public const Color cRed, cBlue, cYellow;
    //Hexagon[] _adjacentGrid;
    public Color[] color;//red,yellow,blue
    SpriteRenderer _sr;
    int _colorIndex;
    public HexagonGridData _data;
    public int _lastRow, _lastCol;

	// Use this for initialization
	void Start () {
        _sr = GetComponent<SpriteRenderer>();
        _sr.color = color[0];
        _colorIndex = 0;
        _data = null;
        _lastRow = _lastRow = -1;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ColorSwitch()
    {
        _colorIndex++;
        if(_colorIndex >= 3)
        {
            _colorIndex = 0;
        }
        _sr.DOColor(color[_colorIndex], 0.5f);
        //_sr.color = color[_colorIndex];
       // void _colorIndex;
    }

    public int ColorIndex
    {
        get
        {
            return _colorIndex;
        }
    }

}
