using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBuffAdder : MonoBehaviour {
    public Pawn TargetPawn;
    public Type BuffToAdd;

    private void Start() {
        Debug.Assert(TargetPawn != null, $"{nameof(TestBuffAdder)}.{nameof(TargetPawn)} is not assigned!");

        if (PawnBuffDatabase.instance.TryAddBuff(TargetPawn, out RetalliationBuff buff)) {
            Debug.Log($"{buff.BuffData.Name} added to {TargetPawn.name}");
        }
        else {
            Debug.Log("Failed to add buff.");
        }

        Destroy(this);
    }
}