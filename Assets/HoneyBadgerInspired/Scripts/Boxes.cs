using UnityEngine;
using System.Collections;

public class Boxes : MonoBehaviour {

	float fall = 0;
	public static float fallGap = 0.5f;
	public static int gridWeight = 20;
	public static int gridHeight = 40;
	public static float startX = 0f, startY = 0f;
	public static Transform[,] grid = new Transform[gridWeight, gridHeight];//x = 18 , 0 ymax = 12
	

	void Start () {
		
		if (!isValidPosition()) {
			Debug.Log("Tetrix player loses");
			CharacterDying.tetrixLose ();
		}
	}

	public static void reset(){
		
		grid = new Transform[gridWeight, gridHeight];
	}

	void Update() {
		if (CharacterDying.isPause)
			return;
		
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			transform.position += new Vector3(1, 0, 0);

			if (isValidPosition())
				GridUpdate();
			else
				transform.position += new Vector3(-1, 0, 0);
		}

		else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			transform.position += new Vector3(-1, 0, 0);
			if (isValidPosition())
				GridUpdate();
			else
				transform.position += new Vector3(1, 0, 0);
		}

		else if (Input.GetKeyDown(KeyCode.UpArrow)) {
			transform.Rotate(0, 0, -90);
			if (isValidPosition())
				GridUpdate();
			else
				transform.Rotate(0, 0, 90);
		}

		else if (Input.GetKeyDown(KeyCode.DownArrow) ||
			Time.time - fall >= fallGap) {
			transform.position += new Vector3(0, -1, 0);
			if (isValidPosition()) {
				GridUpdate();
			} else {
				transform.position += new Vector3(0, 1, 0);
				DeleteRow();
				FindObjectOfType<SpawnBox>().SpawnNewBox();
				enabled = false;
			}
			
			fall = Time.time;
		}
	}

	bool isValidPosition() {        
		foreach (Transform child in transform) {
			Vector2 v = round(child.position);

			if (!isInsideGrid(v)){
				
//				Debug.Log("is not inside grid:"+v);
				return false;
			}
			
			if (grid[(int)v.x, (int)v.y] != null &&
				grid[(int)v.x, (int)v.y].parent != transform){
				
//				Debug.Log("blocked at the position:"+v);
				return false;
			}
		}
		return true;
	}

	void GridUpdate() {
		for (int y = 0; y < gridHeight; ++y)
			for (int x = 0; x < gridWeight; ++x)
				if (grid[x, y] != null)
					if (grid[x, y].parent == transform)
						grid[x, y] = null;

//		Debug.Log("///////////////////");
		foreach (Transform child in transform) {
			Vector2 v = round(child.position);
			grid[(int)v.x, (int)v.y] = child;
//			Debug.Log(child.position+" saved as "+ v);
		}        
	}
	public static Vector2 round(Vector2 v) {
		return new Vector2(Mathf.Round(v.x+0.5f), Mathf.Round(v.y+0.5f));
	}

	public static bool isInsideGrid(Vector2 pos) {
		return ((int)pos.x >= startX && (int)pos.x < gridWeight+startX && (int)pos.y >= startY);
	}
	
	public static void Delete(int y) {
		for (int x = 0; x < gridWeight; ++x) {
			Destroy(grid[x, y].gameObject);
			grid[x, y] = null;
		}
	}

	public static bool isFull(int y) {
		for (int x = 0; x < gridWeight; ++x)
			if (grid[x, y] == null)
				return false;
		return true;
	}

	public static void DeleteRow() {
		for (int y = 0; y < gridHeight; ++y) {
			if (isFull(y)) {
				Delete(y);
				RowDownAll(y+1);
				--y;
			}
		}
	}
	
	public static void RowDown(int y) {
		for (int x = 0; x < gridWeight; ++x) {
			if (grid[x, y] != null) {
				grid[x, y-1] = grid[x, y];
				grid[x, y] = null;
				grid[x, y-1].position += new Vector3(0, -1, 0);
			}
		}
	}
	
	public static void RowDownAll(int y) {
		for (int i = y; i < gridHeight; ++i)
			RowDown(i);
	}
}
