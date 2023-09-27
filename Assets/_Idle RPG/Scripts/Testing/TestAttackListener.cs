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
            var response = new GameActionNode(owner, BattleManager.Instance.GetEnemiesOfPawn(owner), owner.TestAttack, (pawn, list) => {});
            BattleManager.Instance.AddGameActionResponse(response);
        }
    }

    private void OnDisable() {
        EventBus.UnsubscribeFrom<TestAttackEvent>(AttackHandler);
    }
}