using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    public CombatGrid myGrid;
    public float myRepeatTimeMS = 0.300f;

    private float myNextInputTime = 0;
    private int previousVertical = 0;
    private int previousHorizontal = 0;

    void Update()
    {
        float horizontalF = Input.GetAxisRaw("Horizontal");
        float verticalF = Input.GetAxisRaw("Vertical");
        int horizontal = (int)(Mathf.CeilToInt(Mathf.Abs(horizontalF)) * Mathf.Sign(horizontalF));
        int vertical = (int)(Mathf.CeilToInt(Mathf.Abs(verticalF)) * Mathf.Sign(verticalF));

        if (vertical != 0 && horizontal != 0)
        {
            horizontal = 0; // no diags
        }

        if (vertical != previousVertical || horizontal != previousHorizontal)
        {
            myNextInputTime = 0;
            previousVertical = vertical;
            previousHorizontal = horizontal;
        }
        else if (Time.time >= myNextInputTime)
        {
            Vector2Int moveVector = new Vector2Int(horizontal, vertical);
            Vector2Int newPosition = myGrid.myHighlightedCell + moveVector;
            myGrid.HighlightCell(newPosition);
            myNextInputTime = Time.time + myRepeatTimeMS;
            Debug.Log(vertical + " \t " + verticalF);
        }
    }

}