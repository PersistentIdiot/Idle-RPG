using System;
using System.Collections;
using System.Collections.Generic;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using Cysharp.Threading.Tasks;
using DamageNumbersPro;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public enum Team {
    Ally,
    Enemy
}

public class Pawn : MonoBehaviour {
    public PawnModel PawnModel;
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
                        //victim.Stats.CurrentHealth -= Stats.AttackDamage;
                        //victim.PawnModel.Animator.SetTrigger("Hit");
                        victim.TakeDamage(this, Stats.AttackDamage);
                    });
            });
        return test;
    }

    public void TakeDamage(Pawn instigator, float damage) {
        // Subtract health and show damage text
        Stats.CurrentHealth -= damage;
        BattleManager.Instance.TESTDamageNumber.Spawn(transform.position + new Vector3(0, 2, 0), damage);

        // If damage is fatal, set health to zero and play death animations. Raise events?
        if (Stats.CurrentHealth <= 0) {
            Stats.CurrentHealth = 0;
            var response = new GameActionNode(
                instigator,
                new List<Pawn>() {
                    this
                },
                DieRevive,
                (pawn, list) => {
                    Stats.CurrentHealth = Stats.MaxHealth;
                });
            BattleManager.Instance.AddGameActionResponse(response);
        }
        // Else play hit animation. Raise events?
        else {
            PawnModel.Animator.SetTrigger("Hit");
        }
    }

    public async UniTask TestAttack() {
        var attackEvent = new AttackEvent {
            Instigator = this,
            Victims = BattleManager.Instance.GetEnemiesOfPawn(this)
        };
        EventBus.RaiseImmediately(ref attackEvent);
        PawnModel.Slash();
        // Character.Jab();
        await UniTask.Delay(TimeSpan.FromSeconds(AttackDuration), ignoreTimeScale: false);
    }

    public async UniTask DieRevive() {
        PawnModel.Animator.SetInteger("State", 6);
        await UniTask.Delay(TimeSpan.FromSeconds(4), ignoreTimeScale: false);
        PawnModel.Animator.SetInteger("State", 0);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5), ignoreTimeScale: false);
    }
}