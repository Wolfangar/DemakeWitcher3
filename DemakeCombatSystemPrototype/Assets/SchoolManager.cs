using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SchoolManager : MonoBehaviour {

    public enum SchoolType
    {
        NONE,
        WOLF,
        CAT,
        BEAR
    }

    public SchoolType SelectedSchoolType = SchoolType.NONE;

    public GameObject WolfTextDescription;
    public GameObject CatTextDescription;
    public GameObject BearTextDescription;
    public GameObject DefaultTextDescription;
    public Button WolfButton;
    public Button CatButton;
    public Button BearButton;
    public Button SelectButton;

    private GameObject _LastTextDescription = null;

    public void OnEnable()
    {
        _LastTextDescription = DefaultTextDescription;
        SelectButton.interactable = false;
        DefaultTextDescription.SetActive(true);
        WolfTextDescription.SetActive(false);
        CatTextDescription.SetActive(false);
        BearTextDescription.SetActive(false);

        SelectedSchoolType = SchoolType.NONE;
    }

    public void OnClicked(Button button)
    {
        SelectButton.interactable = true;
        _LastTextDescription.SetActive(false);

        if (button == WolfButton)
        {
            SelectedSchoolType = SchoolType.WOLF;
            WolfTextDescription.SetActive(true);
            _LastTextDescription = WolfTextDescription;
        }
        else if(button == CatButton)
        {
            SelectedSchoolType = SchoolType.CAT;
            CatTextDescription.SetActive(true);
            _LastTextDescription = CatTextDescription;
        }
        else// if(button == BearButton)
        {
            SelectedSchoolType = SchoolType.BEAR;
            BearTextDescription.SetActive(true);
            _LastTextDescription = BearTextDescription;
        }
    }
}
