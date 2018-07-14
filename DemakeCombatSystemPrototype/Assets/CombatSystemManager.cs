using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystemManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        CombatActor mainChar = new CombatActor();
        mainChar.myName = "MainChar";
        mainChar.myPosition = new Vector2Int(1, 1);
        mainChar.SetAttributeValue(AttributeType.MaxHealth, 100);
        mainChar.SetAttributeValue(AttributeType.CurrentHealth, 100);

        CombatActor enemy1Char = new CombatActor();
        enemy1Char.myName = "Enemy1";
        enemy1Char.myPosition = new Vector2Int(7, 1);
        enemy1Char.SetAttributeValue(AttributeType.MaxHealth, 100);
        enemy1Char.SetAttributeValue(AttributeType.CurrentHealth, 100);

        CombatActor enemy2Char = new CombatActor();
        enemy2Char.myName = "Enemy2";
        enemy2Char.myPosition = new Vector2Int(6, 2);
        enemy2Char.SetAttributeValue(AttributeType.MaxHealth, 100);
        enemy2Char.SetAttributeValue(AttributeType.CurrentHealth, 100);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void ExecuteTurn()
    {

    }
}

enum AttributeType
{
    CurrentHealth,
    MaxHealth,
    Attack,
    Defense,
    Speed,
    MagicPower
}

class AttributeModifier
{
    int myConstantChange;
    //int myRemainingTurns;
}

class AttributeState
{
    AttributeType myType;
    internal int myBaseValue;
    List<AttributeModifier> myAttributeModifiers;

    internal int GetValue()
    {
        return myBaseValue;
    }
}

class ActiveCombatBuff
{
    private int myRemainingTurns;

    public void Init(int aRemainingTurns)
    {
        myRemainingTurns = aRemainingTurns;
    }

    public bool IsActive()
    {
        return myRemainingTurns > 0;
    }

    public void OnTurnEnded()
    {
        --myRemainingTurns;
        if (myRemainingTurns > 0)
        {
            myRemainingTurns = 0;
        }
    }
}

class DamageCombatBuff : ActiveCombatBuff
{
    int myDamagesPerTurn;
    DamageType myDamageType;

    public DamageCombatBuff(int aDamagesPerTurn, DamageType aDamageType)
    {
        this.myDamagesPerTurn = aDamagesPerTurn;
        this.myDamageType = aDamageType;
    }
}

class AttributeBonusCombatBuff : ActiveCombatBuff
{
    AttributeType myAttributeType;
    int myChange;

    public AttributeBonusCombatBuff(AttributeType anAttributeType, int aValueChange)
    {
        this.myAttributeType = anAttributeType;
        this.myChange = aValueChange;
    }
}
