using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour {
    public GameObject grid, hexagon;
    //public GameObject pre_tile;
    static int row = 9, col = 12;
    static HexagonGridData[,] tiles;
    static List<Hexagon> lockedHexagon;
    public static bool isIsolated;
    public static bool isFirstOne;
    //public Hexagon[] sixHexagons;

    // Use this for initialization
    void Start() {
        tiles = new HexagonGridData[row, col];
        isIsolated = true;
        isFirstOne = true;

        createGrids();
    }

    public void disableCollision()
    {

    }

    public void createGrids()
    {
        float width = 1.5f;
        float height = 1.35f;
        float startX = 0;//
        float startY = 5;

        for (int i = 0; i < row; i++)
        {

            for (int j = 0; j < col; j++)
            {

                Vector3 pos = new Vector3(startX + (i % 2) * width / 2f + width * (j - col / 2), startY + (i - row / 2f) * height, 0);
                GameObject go = Instantiate(grid, pos, Quaternion.identity);
                go.transform.SetParent(transform, true);
                go.name = "Grid" + " " + (i).ToString() + "," + (j).ToString();
                //go.GetComponent<TileSimple>().init(i, j);
                tiles[i, j] = go.GetComponent<HexagonGridData>();
                tiles[i, j].initGrid(i, j);
            }
        }
    }

    //return locked or not
    public static void CheckSurroundingHexagonAndUnloked(int _row, int _col)
    {
        int x = _row % 2 == 0 ? 1 : 0;
        int[,] hexagonIndexs = { { -1, 0 - x }, { -1, 1 - x }, { 0, -1 }, { 0, 1 }, { 1, 0 - x }, { 1, 1 - x } };//[6,2]
        List<int> ihexagonList = new List<int>();

        int i;
        for (i = 0; i < 6; i++)
        {
            int thisrow = _row + hexagonIndexs[i, 0];
            int thiscol = _col + hexagonIndexs[i, 1];
            if (thisrow >= 0 && thisrow < row && thiscol >= 0 && thiscol <= col)
            {
                if (tiles[thisrow, thiscol].isHexagonInGrid)
                {
                    CheckThisHexagonAndUnlocked(thisrow, thiscol);
                }
            }
        }

    }

    static void CheckThisHexagonAndUnlocked(int _row, int _col)
    {
        if(CheckHexagonAround(_row,_col,false) == false)
        {
            Debug.Log("try unlok:" + _row + "," + _col);
            tiles[_row, _col].EnableHexagonInGrid();
        }
        else
        {
            Debug.Log("try lock:" + _row + "," + _col);
            tiles[_row, _col].DisableHexagonInGrid();
        }
    }
    //return true if locked
    public static bool CheckHexagonAround(int _row, int _col, bool isColorSwitch = true)
    {
        int x = _row%2 == 0? 1:0;
        int[,] hexagonIndexs = { { -1,0 -x }, { -1, 1-x }, { 0, -1 }, { 0, 1 }, { 1, 0-x }, { 1, 1-x } };//[6,2]
        List<int> ihexagonList = new List<int>();

        int i;
        for (i = 0; i < 6; i++)
        {
            int thisrow = _row + hexagonIndexs[i,0];
            int thiscol = _col + hexagonIndexs[i,1];
            if (thisrow >= 0 && thisrow < row && thiscol >= 0 && thiscol< col)
            {
               // Debug.Log("Check:"+thisrow + "," + thiscol);
                if (tiles[thisrow, thiscol].isHexagonInGrid){
                    if (isColorSwitch)
                    {
                        tiles[thisrow, thiscol].OnColorSwitch();
                        CheckThisHexagonAndUnlocked(thisrow, thiscol);

                    }
                    ihexagonList.Add(i);
                }
            }
        }

        if (ihexagonList.Count > 0)
        {
            isIsolated = false;

            if (ihexagonList.Count >= 2)
            {
                //check if locked
                for (i = 0; i < ihexagonList.Count; i++)
                {
                    for (int j = i + 1; j < ihexagonList.Count; j++)
                    {
                        if (ihexagonList[i] + ihexagonList[j] == 5)
                        {
                            //lockedHexagon.Add(tiles)
                            Debug.Log("lock:" + _row + "," + _col);
                            return true;
                        }
                    }
                }

                //check if should switch color itself
                if (isColorSwitch && ihexagonList.Count >= 3)
                {
                    tiles[_row, _col].OnColorSwitch();
                }
            }
        }
        else
        {
            isIsolated = true;
        }
        
        

        return false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Instantiate(hexagon);
        }
	}
}
