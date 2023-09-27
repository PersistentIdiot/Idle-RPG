using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class BattleManager : Singleton<BattleManager> {
    public List<Pawn> AllPawns = new List<Pawn>();
    public List<Pawn> Allies = new List<Pawn>();
    public List<Pawn> Enemies = new List<Pawn>();

    public GameActionNode currentNode = null;
    private CancellationTokenSource _cts;

    private void Awake() {
        AllPawns = FindObjectsOfType<Pawn>().ToList();
        Allies = FindObjectsOfType<Pawn>().Where(pawn => pawn.Team == Team.Ally).ToList();
        Enemies = FindObjectsOfType<Pawn>().Where(pawn => pawn.Team == Team.Enemy).ToList();
    }

    private void Start() {
        _cts = new CancellationTokenSource();
        BattleLoop().Forget();
    }

    private async UniTask BattleLoop() {
        while (!_cts.IsCancellationRequested) {
            await UniTask.Yield(cancellationToken: _cts.Token);

            AllPawns.ForEach(pawn => pawn.TurnProgress += pawn.Stats.Speed * Time.deltaTime);
            var pawnTurn = AllPawns.FirstOrDefault(pawn => pawn.TurnProgress >= 100);

            if (pawnTurn == null) continue;
            pawnTurn.TurnProgress = 0;
            await HandleTurn(pawnTurn).AttachExternalCancellation(_cts.Token);
        }
    }

    private void OnDestroy() {
        _cts.Cancel();
    }

    private async UniTask HandleTurn(Pawn pawn) {
        currentNode = pawn.PostAction();
        await currentNode.Animation();
        currentNode.Payload(pawn, currentNode.Victims);

        foreach (GameActionNode actionNode in currentNode.Reactions) {
            await actionNode.Animation();
            actionNode.Payload(pawn, currentNode.Victims);
        }

        currentNode = null;
    }

    public bool AddGameActionResponse(GameActionNode response) {
        if (currentNode == null) {
            Debug.LogError("currentNode == null");
            return false;
        }

        currentNode.Reactions.Add(response);
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