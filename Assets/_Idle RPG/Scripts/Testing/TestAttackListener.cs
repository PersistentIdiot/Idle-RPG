using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TestAttackListener : MonoBehaviour {
    public Pawn owner;

    private void OnEnable() {
        EventBus.SubscribeTo<TestAttackEvent>(AttackHandler);
    }

    private void AttackHandler(ref TestAttackEvent eventData) {
        // Check if owner is victim of the attack
        if (eventData.Victims.Any(pawn => pawn == owner)) {
            Debug.Log($"{nameof(TestAttackListener)} is attacking in response to an attack!");
            // If we are, post a game response with the aggressors as the target.
            List<Pawn> aggressors = new List<Pawn>();
            aggressors.Add(eventData.Instigator);
            
            // Response will be a jab attack against the aggressor
            var response = new GameActionNode(owner, aggressors, () => {
                //owner.TestAttack();
                var attackEvent = new TestAttackEvent {
                    Instigator = owner,
                    Victims = aggressors
                };
                EventBus.RaiseImmediately(ref attackEvent);
                owner.Character.Jab();
                
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