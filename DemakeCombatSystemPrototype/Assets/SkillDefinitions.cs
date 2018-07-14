using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class SkillDefinition
{
    public abstract List<Vector2Int> GetTargetableCells();
    public abstract List<Vector2Int> GetAffectedCellsPattern();
    public abstract List<CombatEffect> GenerateCombatEffects(CombatActor aSourceCombatActor, Vector2Int aTargetPosition);
}

class BaseAttackSkill : SkillDefinition
{

    public override List<Vector2Int> GetAffectedCellsPattern()
    {
        return new List<Vector2Int>()
        {
            new Vector2Int(1, 0)
        };
    }

    public override List<CombatEffect> GenerateCombatEffects(CombatActor aSourceCombatActor, Vector2Int aTargetPosition)
    {
        int attack = aSourceCombatActor.GetAttributeValue(AttributeType.Attack);
        DamageCombatEffect damageEffect = new DamageCombatEffect(attack, DamageType.PHYSICAL);
        damageEffect.myAffectedCells = GetAffectedCellsPattern();

        return new List<CombatEffect>() { damageEffect };
    }

    public override List<Vector2Int> GetTargetableCells()
    {
        return new List<Vector2Int>
        {
            new Vector2Int(1, 0),
            new Vector2Int(1, -1),
            new Vector2Int(1, 1)
        };
    }
}

class SpellIgniSkill : SkillDefinition
{
    public override List<Vector2Int> GetTargetableCells()
    {
        return new List<Vector2Int>()
        {
            new Vector2Int(1, 0),
            new Vector2Int(2, 0),
        };
    }

    public override List<Vector2Int> GetAffectedCellsPattern()
    {
        return new List<Vector2Int>()
        {
            new Vector2Int(1, 0),
            new Vector2Int(2, -1),
            new Vector2Int(2, 0),
            new Vector2Int(2, 1)
        };
    }

    public override List<CombatEffect> GenerateCombatEffects(CombatActor aSourceCombatActor, Vector2Int aTargetPosition)
    {
        int magic = aSourceCombatActor.GetAttributeValue(AttributeType.MagicPower);
        float magicModifier = 1.0f + magic / 10.0f;
        int damages = (int)Mathf.Floor(10 * magicModifier);

        DamageCombatEffect damageCombatEffect = new DamageCombatEffect(damages, DamageType.FIRE);
        BuffCombatEffect burningEffect = new BuffCombatEffect(); // TODO: burning as buff

        return new List<CombatEffect>()
        {
            damageCombatEffect,
            burningEffect
        };
    }
}

class SpellAardSkill : SkillDefinition
{
    public override List<Vector2Int> GetTargetableCells()
    {
        return new List<Vector2Int>()
        {
            new Vector2Int(1, 0)
        };
    }

    public override List<Vector2Int> GetAffectedCellsPattern()
    {
        return new List<Vector2Int>()
        {
            new Vector2Int(1, -1),
            new Vector2Int(1, 0),
            new Vector2Int(1, 1)
        };
    }

    public override List<CombatEffect> GenerateCombatEffects(CombatActor aSourceCombatActor, Vector2Int aTargetPosition)
    {
        int magic = aSourceCombatActor.GetAttributeValue(AttributeType.MagicPower);
        float magicModifier = 1.0f + magic / 10.0f;
        int damages = (int)Mathf.Floor(10 * magicModifier);
        DamageCombatEffect damageEffect = new DamageCombatEffect(damages, DamageType.AIR);
        PushActorCombatEffect pushEffect = new PushActorCombatEffect(new Vector2Int(2, 0));
        return new List<CombatEffect>() { damageEffect, pushEffect };
    }
}