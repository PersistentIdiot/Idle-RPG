using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class BattleManager : MonoBehaviour {
    public List<Pawn> AllPawns = new List<Pawn>();
    public List<Pawn> Allies = new List<Pawn>();
    public List<Pawn> Enemies = new List<Pawn>();

    public bool ProcessingTurn = false;

    private void Update() {
        if (ProcessingTurn) return;

        AllPawns.ForEach(pawn => pawn.TurnProgress += pawn.Stats.Speed * Time.deltaTime);

        var pawnTurn = AllPawns.FirstOrDefault(pawn => pawn.TurnProgress >= 100);

        if (pawnTurn != null) {
            ProcessingTurn = true;
            pawnTurn.TurnProgress = 0;
            HandleTurn(pawnTurn);
        }
    }

    private async UniTask HandleTurn(Pawn pawn) {
        await pawn.TakeTurn();
        ProcessingTurn = false;
    }
}