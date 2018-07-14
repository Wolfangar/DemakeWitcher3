using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ActionType
{
    Move,
    BasicAttack,
    SpellAar
}

public enum DamageType
{
    PHYSICAL,
    FIRE,
    AIR,
    ICE,
    POISON
}

public abstract class CombatAction
{
    ActionType myActionType;

    public abstract List<CombatEffect> GenerateCombatEffects();
}

public class TriggerCellSkillAction : CombatAction
{
    CombatActor mySourceActor;
    SkillDefinition mySkillDefinition;
    Vector2Int myTargetCell;

    public override List<CombatEffect> GenerateCombatEffects()
    {
        return mySkillDefinition.GenerateCombatEffects(mySourceActor, myTargetCell);
    }
}


public abstract class CombatEffect
{
    protected CombatActor mySourceActor;

    public abstract void ApplyToActor(CombatActor aTargetActor);

}

class DamageCombatEffect : CombatEffect
{
    int myDamageAmount;
    DamageType myDamageType;
    internal List<Vector2Int> myAffectedCells;

    public DamageCombatEffect(int aDamageAmount, DamageType aDamageType)
    {
        myDamageAmount = aDamageAmount;
        myDamageType = aDamageType;
    }

    public override void ApplyToActor(CombatActor aTargetActor)
    {
        int defense = aTargetActor.GetAttributeValue(AttributeType.Defense);
        int finalDamages = (int)Mathf.Ceil(myDamageAmount - defense / 2);
        // TODO: damage type
        aTargetActor.TakeDamage(mySourceActor, finalDamages, myDamageType);
    }
}

class BuffCombatEffect : CombatEffect
{
    ActiveCombatBuff myBuffDefinition;

    public override void ApplyToActor(CombatActor aTargetActor)
    {
        throw new NotImplementedException();
    }
}

enum MoveType
{
    Walk,
    Push,
    Jump,
    Dash
}

class PushActorCombatEffect : CombatEffect
{
    Vector2Int myDisplacement;

    public PushActorCombatEffect(Vector2Int aDisplacement)
    {
        myDisplacement = aDisplacement;
    }

    public override void ApplyToActor(CombatActor aTargetActor)
    {
        aTargetActor.MoveRelative(myDisplacement, MoveType.Push);
    }
}
