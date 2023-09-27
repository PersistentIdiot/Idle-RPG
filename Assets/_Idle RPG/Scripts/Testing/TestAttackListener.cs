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
            Debug.Log($"{gameObject.name} was attacked! Adding response.");
            var response = new GameActionNode(
                owner,
                BattleManager.Instance.GetEnemiesOfPawn(owner),
                owner.TestAttack,
                (pawn, list) => {
                    Debug.Log($"{owner.gameObject.name} counterattacked!");
                });

            Debug.Log(BattleManager.Instance.AddGameActionResponse(response) ? $"Successfully added response!" : $"Failed to add response!");
        }
    }

    private void OnDisable() {
        EventBus.UnsubscribeFrom<TestAttackEvent>(AttackHandler);
    }
}