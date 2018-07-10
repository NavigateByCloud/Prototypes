using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HexagonGridData : MonoBehaviour {
    int _col, _row;
    int _colorNum;
    Hexagon _hexagonInGrid;
    static int unlockedIndex = 0;
    
    public bool isHexagonInGrid
    {
        get
        {
            return _hexagonInGrid;
        }
    }

    public void initGrid(int row, int col)
    {
        _row = row;
        _col = col;
        _hexagonInGrid = null;
    }

    public void dettachHexagon()
    {
        _hexagonInGrid = null;
        GetComponent<SpriteRenderer>().color = Color.white;
        GridGenerator.CheckSurroundingHexagonAndUnloked(_row, _col);
        //_hexagonInGrid._data = null;
    }

    public void attachHexagonToGrid(Hexagon hexagon)
    {
        Debug.Log(_row + "," + _col);
        if (IsGridAvailable(hexagon) == false)
        {
            //back to original position
            GetComponent<SpriteRenderer>().color = Color.white;
            hexagon.transform.DOMove(hexagon._data.transform.position, 0.5f);
            return;
        }
        //place down
        GridGenerator.isFirstOne = false;
        hexagon.transform.position = transform.position;
        hexagon._lastRow = _row;
        hexagon._lastCol = _col;
        if (hexagon._data)
        {
            hexagon._data.dettachHexagon();

        }
        hexagon._data = this;
        _hexagonInGrid = hexagon;
        _colorNum = hexagon.ColorIndex;

        if (CheckSurroundingHexagonColorsAndReturnLocked(true) == false)
        {
            if(unlockedIndex < 6)
            {
                unlockedIndex++;
                Debug.Log("unlocked a new one");
                if(unlockedIndex == 6)
                {
                    Hexagon[] hs = FindObjectsOfType(typeof(Hexagon)) as Hexagon[];
                    foreach (Hexagon hex in hs)
                    {
                        hex.GetComponent<PolygonCollider2D>().enabled = true;
                    }
                }

            }
            else
            {
                hexagon.GetComponent<PolygonCollider2D>().enabled = true;
            }
            
        }
    }

    public bool IsGridAvailable(Hexagon hexagon)
    {
        //check if the same place
        if (_hexagonInGrid)
        {
            Debug.Log("Already hexagon here.");
            return false;
        }
        else
        {
            if (CheckSurroundingHexagonColorsAndReturnLocked(false) == false)
            {
                //hexagon.GetComponent<PolygonCollider2D>().enabled = true;
                //check if is adjacent to any pieces
                if (GridGenerator.isIsolated)
                {
                    if (GridGenerator.isFirstOne != true)
                    {
                        //return to the old place, nothing change
                        //hexagon.transform.position = 
                        Debug.Log("Isolated grid is not available.");
                        return false;
                    }
                    else
                    {
                        //continue
                        
                        //Debug.Log("First one is an exception");
                        return true;
                    }

                }

            }
            return true;

        }
    }

    public void EnableHexagonInGrid()
    {
        if (_hexagonInGrid && unlockedIndex>=6)
        {
            Debug.Log("enable:" + _row + "," + _col);
            _hexagonInGrid.GetComponent<PolygonCollider2D>().enabled = true;
        }
    }

    public void DisableHexagonInGrid()
    {
        if (_hexagonInGrid)
        {
            Debug.Log("disable:" + _row + "," + _col);
            _hexagonInGrid.GetComponent<PolygonCollider2D>().enabled = false;
        }
    }

    public void OnColorSwitch()
    {
        _hexagonInGrid.ColorSwitch();
        _colorNum = _hexagonInGrid.ColorIndex;
    }

    public bool CheckSurroundingHexagonColorsAndReturnLocked(bool isColorSwitch)
    {
        return GridGenerator.CheckHexagonAround(_row,_col, isColorSwitch);
    }


    // Use this for initialization
    void Start () {
        _hexagonInGrid = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
