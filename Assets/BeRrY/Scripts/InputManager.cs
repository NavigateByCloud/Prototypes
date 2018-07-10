using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InputManager : MonoBehaviour {
    Hexagon draggedHexagon, lastMouseOverHexagon;
    HexagonGridData lastHitGrid;

	// Use this for initialization
	void Start () {
        draggedHexagon = null;
        lastHitGrid = null;
        lastMouseOverHexagon = null;
	}

    // Update is called once per frame
    void Update()
    {
        Vector2 inputPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //imgCurser.transform.position = Input.mousePosition;
        //spCurser.transform.position = inputPosition;
        RaycastHit2D[] touches = Physics2D.RaycastAll(inputPosition, inputPosition, 0.5f);
        if (touches.Length > 0)
        {
            //Debug.Log()
            var hit = touches[0];
            if (hit.transform != null)
            {
                Hexagon hexagon = hit.transform.GetComponent<Hexagon>();
                HexagonGridData hexagonGrid = hit.transform.GetComponent<HexagonGridData>();

                //Debug.Log(hit.transform.name);
                if (hexagon != null)
                {
                    
                   // lastMouseOverHexagon = hexagon;
                    //Debug.Log("add dotween " + hexagon.name);
                    //hexagon.GetComponent<SpriteRenderer>().DOFade(0.5f, 0.5f);
                    if (Input.GetMouseButtonDown(0))
                    {
                        Debug.Log("mouse down!");
                        if (draggedHexagon == null)
                        {
                            draggedHexagon = hexagon;
                            if (hexagon._data)
                            {
                                hexagon._data.GetComponent<SpriteRenderer>().color = Color.red;
                                
                                //hexagon._data = null;
                            }
                            draggedHexagon.GetComponent<PolygonCollider2D>().enabled = false;
                           
                        }
                        //hexagon.ColorSwitch();
                    }


                }
                else if(draggedHexagon && hexagonGrid)
                {
                    
                    if (lastHitGrid)
                    {
                        lastHitGrid.GetComponent<SpriteRenderer>().color = Color.white;
                        //Debug.Log("move from " + lastHitGrid.name + " to " + hexagonGrid.name);
                        //if the last one is not  that one
                        if (lastHitGrid != hexagonGrid)
                        {
                            //Debug.Log("move to different place");
                            if (hexagonGrid.IsGridAvailable(draggedHexagon))
                            {
                                //if that one is available
                                lastHitGrid = hexagonGrid;
                                lastHitGrid.GetComponent<SpriteRenderer>().color = Color.green;
                            }
                        }
                    }
                    else
                    {
                        lastHitGrid = hexagonGrid;
                        lastHitGrid.GetComponent<SpriteRenderer>().color = Color.green;
                    }
                   



                }

            }
        }

        if (draggedHexagon)
        {
            draggedHexagon.transform.position = inputPosition;
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("mouse up!");

                //place down
                //draggedHexagon.transform.position = lastHitGrid.transform.position;
                //and check if locked by surrounding hexagons

                //save hexagon into grid
                lastHitGrid.attachHexagonToGrid(draggedHexagon);
                //hexagon._data.dettachHexagon();
                lastHitGrid.GetComponent<SpriteRenderer>().color = Color.white;

                draggedHexagon = null;
                
                lastHitGrid = null;
            }

            
        }
    }
}
