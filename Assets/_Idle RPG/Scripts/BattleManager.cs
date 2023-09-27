using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Scripts;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class BattleManager : Singleton<BattleManager> {
    public List<Pawn> AllPawns = new List<Pawn>();
    public List<Pawn> Allies = new List<Pawn>();
    public List<Pawn> Enemies = new List<Pawn>();

    public bool ProcessingTurn = false;
    public  GameActionNode currentNode = null;

    private void Awake() {
        AllPawns = FindObjectsOfType<Pawn>().ToList();
        Allies = FindObjectsOfType<Pawn>().Where(pawn => pawn.Team == Team.Ally).ToList();
        Enemies = FindObjectsOfType<Pawn>().Where(pawn => pawn.Team == Team.Enemy).ToList();
    }

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
        //await pawn.TakeTurn();
        Debug.Log($"HandleTurn() - Start");
        currentNode = pawn.PostAction();
        await currentNode.Animation;
        currentNode.Payload(pawn, currentNode.Victims);

        if (currentNode.Reactions.Count > 0) {
            Debug.Log($"Processing reactions!");
        }

        foreach (GameActionNode actionNode in currentNode.Reactions) {
            await actionNode.Animation;
            actionNode.Payload(pawn, currentNode.Victims);
        }

        Debug.Log($"Done processing.");
        ProcessingTurn = false;
        currentNode = null;
    }

    public bool AddGameActionResponse(GameActionNode response) {
        if (currentNode == null) {
            Debug.LogError("currentNode == null");
            return false;
        }

        currentNode.Reactions.Add(response);
        Debug.Log($"Response added!");
        return true;
    }

    public List<Pawn> GetEnemiesOfPawn(Pawn pawn) {
        if (pawn.Team == Team.Ally) {
            return Enemies;
        }
        else {
            return Allies;
        }
    }
}