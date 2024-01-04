using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBuffAdder : MonoBehaviour {
    public Pawn TargetPawn;
    private void Start() {
        Debug.Assert(TargetPawn != null, $"{nameof(TestBuffAdder)}.{nameof(TargetPawn)} is not assigned!");
        PawnBuffDatabase.instance.TryAddBuff<RetalliationBuff, RetalliationBuffData>(TargetPawn, out RetalliationBuff buff);
        Debug.Log($"{buff.BuffData.Name} added to {TargetPawn.name}");
        Destroy(this);
    }
}