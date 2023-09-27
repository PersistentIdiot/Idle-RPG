using System;
using System.Collections;
using System.Collections.Generic;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using Cysharp.Threading.Tasks;
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

    public GameActionNode PostAction() {
        var test = new GameActionNode(
            this,
            BattleManager.Instance.GetEnemiesOfPawn(this),
            TestAttack,
            (instigator, victims) => {
                victims.ForEach(pawn => pawn.Stats.CurrentHealth -= 1);
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
        await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);
    }
}