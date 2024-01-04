using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestAttackListener : MonoBehaviour {
    public Pawn owner;

    private void OnEnable() {
        EventBus.SubscribeTo<TestAttackEvent>(AttackHandler);
    }

    private void AttackHandler(ref TestAttackEvent eventData) {
        if (eventData.Victims.Any(pawn => pawn == owner)) {
            var response = new GameActionNode(owner, BattleManager.Instance.GetEnemiesOfPawn(owner), () => {
                owner.TestAttack();
                return default;
            },
                (instigator, victims) => {
                    instigator.Stats.CurrentHealth -= owner.Stats.AttackDamage / 2;
                    BattleManager.Instance.TESTDamageNumber.Spawn(instigator.transform.position + new Vector3(0, 2, 0), owner.Stats.AttackDamage / 2);
                });
            BattleManager.Instance.AddGameActionResponse(response);
        }
    }

    private void OnDisable() {
        EventBus.UnsubscribeFrom<TestAttackEvent>(AttackHandler);
    }
}