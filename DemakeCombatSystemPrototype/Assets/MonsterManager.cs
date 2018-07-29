using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterManager : MonoBehaviour {

    [Serializable]
    public struct Monster
    {
        public MonsterType monsterType;
        public Sprite spriteButton;
    }
    
    //Monsters are 1 to 7
    public enum MonsterType
    {
        NONE,
        YANIFEROCE,
        JULICOPTERE,
        VOISINATOR,
        TOMISSLE,
        ALEXEUDOR,
        YUMEI,
        RAFILOU
    }

    public List<Monster> Monsters = new List<Monster>();
    public MonsterType SelectedMonsterType = MonsterType.NONE;

    public Button MonsterCard1;
    public Button MonsterCard2;
    public Button MonsterCard3;
    public Text LevelText;
    public Button SelectButton;

    private Sprite _DefaultSprite = null;

    //To change depending on the combat manager
    private int _Level = 0;

    public void Start()
    {
        _DefaultSprite = MonsterCard1.GetComponent<Image>().sprite;
    }

    public void OnEnable()
    {
        SelectButton.interactable = false;
        MonsterCard1.interactable = true;
        MonsterCard2.interactable = true;
        MonsterCard3.interactable = true;

        MonsterCard1.GetComponent<Image>().sprite = _DefaultSprite;
        MonsterCard2.GetComponent<Image>().sprite = _DefaultSprite;
        MonsterCard3.GetComponent<Image>().sprite = _DefaultSprite;
        
        SelectedMonsterType = MonsterType.NONE;

        //To change depending on the combat manager
        _Level++;
        LevelText.text = "Level " + _Level;
    }

    public void OnMonsterCardClicked(Button button)
    {
        MonsterCard1.interactable = false;
        MonsterCard2.interactable = false;
        MonsterCard3.interactable = false;
        
        SelectedMonsterType = (MonsterType)UnityEngine.Random.Range(1, 7);
        foreach (Monster m in Monsters)
        {
            if(m.monsterType == SelectedMonsterType)
            {
                button.GetComponent<Image>().sprite = m.spriteButton;
            }
        }

        SelectButton.interactable = true;
    }
}
