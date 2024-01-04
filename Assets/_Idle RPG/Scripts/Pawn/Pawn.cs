using System;
using System.Collections;
using System.Collections.Generic;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using Cysharp.Threading.Tasks;
using DamageNumbersPro;
using UnityEngine;

[Serializable]
public enum Team {
    Ally,
    Enemy
}

public class Pawn : MonoBehaviour {
    public Character Character;
    public PawnStats Stats;


    public Team Team;
    public float TurnProgress = 0;
    public float AttackDuration = 0.25f;

    public GameActionNode PostAction() {
        var test = new GameActionNode(
            this,
            BattleManager.Instance.GetEnemiesOfPawn(this),
            TestAttack,
            (instigator, victims) => {
                victims.ForEach(
                    victim => {
                        victim.Stats.CurrentHealth -= Stats.AttackDamage;
                        BattleManager.Instance.TESTDamageNumber.Spawn(victim.transform.position + new Vector3(0, 2, 0), Stats.AttackDamage);
                    });
            });
        return test;
    }

    public async UniTask TestAttack() {
        var attackEvent = new TestAttackEvent {
            Instigator = this,
            Victims = BattleManager.Instance.GetEnemiesOfPawn(this)
        };
        EventBus.RaiseImmediately(ref attackEvent);
        Character.Slash();
        // Character.Jab();
        await UniTask.Delay(TimeSpan.FromSeconds(AttackDuration), ignoreTimeScale: false);
    }
}