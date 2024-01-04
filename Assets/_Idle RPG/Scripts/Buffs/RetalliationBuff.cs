using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class RetalliationBuff : PawnBuff {
    private void OnEnable() {
        base.OnBuffAdded();
        OnBuffAdded();
    }

    private void OnDisable() {
        base.OnBuffRemoved();
        OnBuffRemoved();
    }

    public override void OnBuffAdded() {
        EventBus.SubscribeTo<AttackEvent>(RetalliateHandler);
    }

    public override void OnBuffRemoved() {
        EventBus.UnsubscribeFrom<AttackEvent>(RetalliateHandler);
    }

    private void RetalliateHandler(ref AttackEvent eventData) {
        // Check if owner is victim of the attack
        if (eventData.Victims.Any(pawn => pawn == Owner)) {
            Debug.Log($"{nameof(TestAttackListener)} - {Owner.gameObject.name} is attacking {eventData.Instigator.gameObject.name} in response to an attack!");
            // If we are, post a game response with the aggressors as the target.
            List<Pawn> aggressors = new List<Pawn>();
            aggressors.Add(eventData.Instigator);

            // Response will be a jab attack against the aggressor
            var response = new GameActionNode(
                Owner,
                aggressors,
                async () => {
                    var attackEvent = new AttackEvent {
                        Instigator = Owner,
                        Victims = aggressors
                    };
                    EventBus.RaiseImmediately(ref attackEvent);
                    Owner.PawnModel.Jab();
                    await UniTask.Delay(TimeSpan.FromSeconds(Owner.AttackDuration), ignoreTimeScale: false);
                },
                (instigator, victims) => {
                    instigator.TakeDamage(Owner, Owner.Stats.AttackDamage / 2);
                });
            BattleManager.Instance.AddGameActionResponse(response);
        }
    }
}