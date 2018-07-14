using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatActor : MonoBehaviour
{
    public String myName;
    public Vector2Int myPosition;
    private Dictionary<AttributeType, AttributeState> myAttributeStates;
    private int myCurrentHealthPoint;

    public List<CombatAction> myQueuedActions;

    private List<ActiveCombatBuff> myCombatBuffs;

    public void TakeDamage(CombatActor aSource, int aDamageAmount, DamageType aDamageType)
    {
        GetAttributeState(AttributeType.CurrentHealth).myBaseValue -= aDamageAmount;
        Debug.Log("Actor " + myName + " took " + aDamageAmount + " " + aDamageType + " damages from " + aSource.myName);
    }

    public void OnCombatTurnEnded()
    {
        foreach (ActiveCombatBuff buff in myCombatBuffs)
        {
            buff.OnTurnEnded();
        }
    }

    internal AttributeState GetAttributeState(AttributeType anAttributeType)
    {
        return myAttributeStates[anAttributeType];
    }

    internal int GetAttributeValue(AttributeType anAttributeType)
    {
        return myAttributeStates[anAttributeType].GetValue();
    }

    internal void SetAttributeValue(AttributeType anAttributeType, int aNewValue)
    {
        GetAttributeState(anAttributeType).myBaseValue = aNewValue;
    }

    internal void ChangeAttributeValue(AttributeType anAttributeType, int aValueChange)
    {
        GetAttributeState(anAttributeType).myBaseValue += aValueChange;
    }

    internal void MoveRelative(Vector2Int myDisplacement, MoveType aMoveType)
    {
        // TODO: animation
        myPosition += myDisplacement;
        Debug.Log("Actor " + myName + " moved to " + myPosition + " (" + aMoveType + ")");
    }
}
