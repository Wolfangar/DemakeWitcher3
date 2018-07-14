using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatGrid : MonoBehaviour {

    public Vector3 derp2;
    public int myWidth;
    public int myHeight;
    private int myCurrentGridWidth;
    private int myCurrentGridHeight;
    public string[,] myTerrainGrid;

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

    public void UpdateGrid(int width, int height, bool force = false)
    {
        if (!force && width == myCurrentGridWidth && height == myCurrentGridHeight)
        {
            return;
        }

        myCurrentGridWidth = width;
        myCurrentGridHeight = height;
    
        Transform terrainLayer = transform.Find("TerrainBaseLayer");

        foreach (Transform child in terrainLayer)
        {
            GameObject.DestroyImmediate(child.gameObject);
        }

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
}
