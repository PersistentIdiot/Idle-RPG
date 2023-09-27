using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class GameActionNode {
    public Pawn Instigator;
    public List<Pawn> Victims;
    public Func<UniTask> Animation;
    public Action<Pawn, List<Pawn>> Payload;
    public List<GameActionNode> Reactions = new List<GameActionNode>();

    public GameActionNode(Pawn instigator, List<Pawn> victims, Func<UniTask> animation, Action<Pawn, List<Pawn>> payload) {
        Instigator = instigator;
        Victims = victims;
        Animation = animation;
        Payload = payload;
    }
}