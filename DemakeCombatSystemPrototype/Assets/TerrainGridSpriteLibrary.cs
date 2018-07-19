using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New TerrainGridSpriteLibrary", menuName = "DemakeWitcher/Terrain Grid Sprite Library", order = 1)]
public class TerrainGridSpriteLibrary : ScriptableObject {

    [Serializable]
    public struct SpriteEntry
    {
        public string name;
        public Sprite sprite;
    }
    public List<SpriteEntry> mySprites = new List<SpriteEntry>();

    public Sprite GetTerrainSprite(string text)
    {
        foreach (SpriteEntry entry in mySprites)
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
        foreach (SpriteEntry entry in mySprites)
        {
            if (entry.name == input)
                return entry.name;
        }

        return "!";
    }

}
