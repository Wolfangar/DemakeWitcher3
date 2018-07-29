using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New TerrainGridData", menuName = "DemakeWitcher/Terrain Grid Data", order = 1)]
public class TerrainGridData : ScriptableObject
{
    public int myWidth;
    public int myHeight;
    //private string[] myTerrainGrid;
    [SerializeField]
    public List<string> myTerrainGrid;

    public delegate void OnChangedCallback();
    public event OnChangedCallback myChangeCallback;

    public void OnEnable()
    {
        EnforceSize();
        Debug.Log("ENABLE: " + this.name + " [" + myWidth + "," + myHeight + "][" + myTerrainGrid.Count + "]");
    }

    /*public void Awake()
    {
        EnforceSize();
        Debug.Log("AWAKE " + this.name + " [" + myWidth + "," + myHeight + "][" + myTerrainGrid.Count + "]");
    }*/

    private void EnforceSize()
    {
        int expectedSize = myWidth * myHeight;
        //if (myTerrainGrid == null || myTerrainGrid.Length != expectedSize)
        if (myTerrainGrid == null || myTerrainGrid.Count != expectedSize)
        {
            Debug.LogWarning("TerrainGridData " + this.name + " was corrupted: raw data length mismatched weight/height data.");
            //myTerrainGrid = new List<string>(expectedSize);
            myTerrainGrid = Enumerable.Repeat("", expectedSize).ToList();
        }
    }

    public void LoadFrom2DGrid(string[,] aGrid, int aWidth, int anHeight)
    {
        Debug.Log("Loading from 2D Grid [" + aWidth + "," + anHeight + "]");

        myWidth = aWidth;
        myHeight = anHeight;
        //myTerrainGrid = new string[myWidth * myHeight];
        myTerrainGrid = Enumerable.Repeat("", myWidth * myHeight).ToList();

        for (int j = 0; j < myHeight; ++j)
        {
            for (int i = 0; i < myWidth; ++i)
            {
                int index = j * myWidth + i;
                myTerrainGrid[index] = aGrid[j, i];       
            }
        }

        OnGridChange();
    }

    public string[,] Make2DGrid()
    {
        string[,] grid = new string[myHeight, myWidth];

        for (int j = 0; j < myHeight; ++j)
        {
            for (int i = 0; i < myWidth; ++i)
            {
                int index = j * myWidth + i;
                grid[j, i] = myTerrainGrid[index];
            }
        }

        return grid;
    }

    public string GetCellTypeAt(int x, int y)
    {
        int index = y * myWidth + x;
        return myTerrainGrid[index];
    }

    public void SetGridChangeListener(OnChangedCallback action)
    {
        myChangeCallback = action;
    }

    public void OnGridChange()
    {
        if (myChangeCallback != null)
            myChangeCallback();
    }
}
