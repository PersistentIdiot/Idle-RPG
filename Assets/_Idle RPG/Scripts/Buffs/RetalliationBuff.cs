using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RetalliationBuff : PawnBuff {
    private void OnEnable() {
        OnBuffAdded();
    }

    private void OnDisable() {
        OnBuffRemoved();
    }

    public override void OnBuffAdded() {
        EventBus.SubscribeTo<TestAttackEvent>(AttackHandler);
    }

    public override void OnBuffRemoved() {
        EventBus.UnsubscribeFrom<TestAttackEvent>(AttackHandler);
    }

    private void AttackHandler(ref TestAttackEvent eventData) {
        // Check if owner is victim of the attack
        if (eventData.Victims.Any(pawn => pawn == Owner)) {
            Debug.Log($"{nameof(TestAttackListener)} is attacking in response to an attack!");
            // If we are, post a game response with the aggressors as the target.
            List<Pawn> aggressors = new List<Pawn>();
            aggressors.Add(eventData.Instigator);

            // Response will be a jab attack against the aggressor
            var response = new GameActionNode(
                Owner,
                aggressors,
                () => {
                    //owner.TestAttack();
                    var attackEvent = new TestAttackEvent {
                        Instigator = Owner,
                        Victims = aggressors
                    };
                    EventBus.RaiseImmediately(ref attackEvent);
                    Owner.Character.Jab();

                    return default;
                },
                (instigator, victims) => {
                    instigator.Stats.CurrentHealth -= Owner.Stats.AttackDamage / 2;
                    BattleManager.Instance.TESTDamageNumber.Spawn(instigator.transform.position + new Vector3(0, 2, 0), Owner.Stats.AttackDamage / 2);
                });
            BattleManager.Instance.AddGameActionResponse(response);
        }
    }
}