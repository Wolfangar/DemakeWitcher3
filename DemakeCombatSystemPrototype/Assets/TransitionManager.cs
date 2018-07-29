using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour {

    public GameObject SchoolCanvas;
    public GameObject MonsterCanvas;
    public GameObject CombatCanvas;
    public GameObject LoreCanvas;
    public GameObject MenuCanvas;

    public void Start()
    {
        MenuCanvas.SetActive(true);
        SchoolCanvas.SetActive(false);
        MonsterCanvas.SetActive(false);
        LoreCanvas.SetActive(false);
        CombatCanvas.SetActive(false);
    }

    //SCHOOL

    public void OnSchoolCardClick()
    {
        SchoolCanvas.SetActive(false);
        MonsterCanvas.SetActive(true);
    }

    //MONSTER

    public void OnMonsterCombatClick()
    {
        MonsterCanvas.SetActive(false);
        CombatCanvas.SetActive(true);
    }

    //COMBAT

    public void OnWin()
    {
        CombatCanvas.SetActive(false);
        LoreCanvas.SetActive(true);
    }

    public void OnLose()
    {
        CombatCanvas.SetActive(false);
        MenuCanvas.SetActive(true);
    }

    //LORE DESCRIPTION

    public void OnContinueClick()
    {
        LoreCanvas.SetActive(false);
        MonsterCanvas.SetActive(true);
    }

    //MENU

    public void OnStartClick()
    {
        MenuCanvas.SetActive(false);
        SchoolCanvas.SetActive(true);
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }
}
