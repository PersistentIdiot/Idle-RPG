using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBuffAdder : MonoBehaviour {
    public Pawn TargetPawn;

    private void Start() {
        Debug.Assert(TargetPawn != null, $"{nameof(TestBuffAdder)}.{nameof(TargetPawn)} is not assigned!");
        
        if (!PawnBuffDatabase.instance.TryAddBuff(TargetPawn, out RetalliationBuff _)) {
            Debug.Log("Failed to add buff.");
        }

        Destroy(this);
    }
}