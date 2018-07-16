using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatGrid : MonoBehaviour {
    private int myCurrentGridWidth;
    private int myCurrentGridHeight;
    private string[,] myTerrainGrid = new string[0, 0];

    [Serializable]
    public struct SpriteEntry
    {
        public string name;
        public Sprite sprite;
    }
    public List<SpriteEntry> myTerrainSpriteLibrary = new List<SpriteEntry>();
  
    public Transform myCellTemplate;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public string GetTerrainCellAt(int x, int y)
    {
        return myTerrainGrid[y, x];
    }

    public void SetTerrainGrid(string[,] grid, int w, int h)
    {
        myTerrainGrid = grid;
        myCurrentGridWidth = w;
        myCurrentGridHeight = h;
    }

    public int GetTerrainWidth()
    {
        return myTerrainGrid.GetLength(1);
    }
    public int GetTerrainHeight()
    {
        return myTerrainGrid.GetLength(0);
    }
    public bool IsValidCoord(int x, int y)
    {
        return x < GetTerrainWidth() && y < GetTerrainHeight();
    }

    public void UpdateGrid(bool force = false)
    {
        /*myCurrentGridWidth = myTerrainGrid.GetLength(1);
        myCurrentGridHeight = myTerrainGrid.GetLength(0);*/
    
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

        for (int y = 0; y < myCurrentGridHeight; ++y)
        {
            for (int x = 0; x < myCurrentGridWidth; ++x)
            {
                Transform cell = GameObject.Instantiate(myCellTemplate, terrainLayer);
                cell.gameObject.SetActive(true);
                cell.name = "Cell_" + x + "_" + y;
                Vector3 position = new Vector3();
                position.x = x;
                position.z = y;
                cell.localPosition = position;

                string terrainType = myTerrainGrid[y, x];
                SpriteRenderer spriteRenderer = cell.GetComponent<SpriteRenderer>();
                Sprite sprite = GetTerrainSprite(terrainType);
                if (sprite != null)
                {
                    spriteRenderer.sprite = sprite;
                }
            }
        }
    }

    Sprite GetTerrainSprite(string text)
    {
        foreach (SpriteEntry entry in myTerrainSpriteLibrary)
        {
            if (entry.name == text)
            {
                return entry.sprite;
            }
        }
        return null;
    }

    public string ValidateValue(string input)
    {
        foreach (SpriteEntry entry in myTerrainSpriteLibrary)
        {
            if (entry.name == input)
                return entry.name;
        }

        if (myTerrainSpriteLibrary.Count > 0)
            return myTerrainSpriteLibrary[0].name;

        return "!";
    }
}
