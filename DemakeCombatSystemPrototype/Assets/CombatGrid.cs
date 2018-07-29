using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatGrid : MonoBehaviour
{
    public TerrainGridSpriteLibrary myTerrainSpriteLibrary;
    public TerrainGridData myTerrainGridData;
    public Transform myCellTemplate;

    public Vector2Int myHighlightedCell;

    public void UpdateGridRepresentation()
    {
        Debug.Log("REPRESENTATION UPDATE");
    
        Transform terrainLayer = transform.Find("TerrainBaseLayer");

        List<Transform> children = new List<Transform>();
        foreach (Transform child in terrainLayer)
        {
            children.Add(child);
        }
        foreach (Transform child in children)
        {
            GameObject.DestroyImmediate(child.gameObject);
        }

        if (myTerrainGridData == null)
            return;
        
        int width = myTerrainGridData.myWidth;
        int height = myTerrainGridData.myHeight;

        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                Transform cell = GameObject.Instantiate(myCellTemplate, terrainLayer);
                cell.gameObject.SetActive(true);
                cell.name = "Cell_" + x + "_" + y;
                Vector3 position = new Vector3();
                position.x = x;
                position.z = y;
                cell.localPosition = position;

                string terrainType = myTerrainGridData.GetCellTypeAt(x, y);
                SpriteRenderer spriteRenderer = cell.GetComponent<SpriteRenderer>();
                Sprite sprite = myTerrainSpriteLibrary.GetTerrainSprite(terrainType);
                if (sprite != null)
                {
                    spriteRenderer.sprite = sprite;
                }
            }
        }
    }

    public Transform GetCellAt(int x, int y)
    {
        Transform terrainLayer = transform.Find("TerrainBaseLayer");
        return terrainLayer.Find("Cell_" + x + "_" + y);
    }

    public void HighlightCell(Vector2Int position)
    {
        if (position.x < 0 || position.x >= myTerrainGridData.myWidth || position.y < 0 || position.y >= myTerrainGridData.myHeight)
            return;

        if (myHighlightedCell != null)
        {
            BlurHighlightedCell();
        }

        myHighlightedCell = position;
        Transform cell = GetCellAt(myHighlightedCell.x, myHighlightedCell.y);

        float hue, saturation, value;
        Color.RGBToHSV(Color.blue, out hue, out saturation, out value);
        saturation *= 0.5f;
        Color c = Color.HSVToRGB(hue, saturation, value);
        cell.GetComponent<SpriteRenderer>().color = c;
    }

    public void BlurHighlightedCell()
    {
        Transform cell = GetCellAt(myHighlightedCell.x, myHighlightedCell.y);
        cell.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
