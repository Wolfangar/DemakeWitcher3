using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour {

    public CombatGrid myGrid;
    public float myRepeatTimeMS = 0.300f;
    private float myNextInputTime = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float horizontalF = Input.GetAxisRaw("Horizontal");
        float verticalF = Input.GetAxisRaw("Vertical");
        int horizontal = (int)(Mathf.CeilToInt(Mathf.Abs(horizontalF)) * Mathf.Sign(horizontalF));
        int vertical = (int)(Mathf.CeilToInt(Mathf.Abs(verticalF)) * Mathf.Sign(verticalF));

        if (horizontal == 0 && vertical == 0)
        {
            myNextInputTime = 0;
        }

        if (Time.time >= myNextInputTime && (horizontal != 0 || vertical != 0))
        {
            Vector2Int moveVector = new Vector2Int(horizontal, vertical);
            Vector2Int newPosition = myGrid.myHighlightedCell + moveVector;
            myGrid.HighlightCell(newPosition);
            myNextInputTime = Time.time + myRepeatTimeMS;
            Debug.Log(vertical + " \t " + verticalF);
        }
    }
        
}
