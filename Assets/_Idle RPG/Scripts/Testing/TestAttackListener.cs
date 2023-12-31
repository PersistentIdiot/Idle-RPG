using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TestAttackListener : MonoBehaviour {
    public Pawn owner;

    private void OnEnable() {
        EventBus.SubscribeTo<AttackEvent>(AttackHandler);
    }

    private void AttackHandler(ref AttackEvent eventData) {
        // Check if owner is victim of the attack
        if (eventData.Victims.Any(pawn => pawn == owner)) {
            // Debug.Log($"{nameof(TestAttackListener)} is attacking in response to an attack!");
            // If we are, post a game response with the aggressors as the target.
            List<Pawn> aggressors = new List<Pawn>();
            aggressors.Add(eventData.Instigator);
            
            // Response will be a jab attack against the aggressor
            var response = new GameActionNode(owner, aggressors, () => {
                //owner.TestAttack();
                var attackEvent = new AttackEvent {
                    Instigator = owner,
                    Victims = aggressors
                };
                EventBus.RaiseImmediately(ref attackEvent);
                owner.PawnModel.Jab();
                
                return default;
            },
                (instigator, victims) => {
                    instigator.TakeDamage(owner, owner.Stats.AttackDamage / 2);
                });
            BattleManager.Instance.AddGameActionResponse(response);
        }
    }

    private void OnDisable() {
        EventBus.UnsubscribeFrom<AttackEvent>(AttackHandler);
    }
}