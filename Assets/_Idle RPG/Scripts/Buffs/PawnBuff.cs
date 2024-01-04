using UnityEngine;

public class PawnBuff : MonoBehaviour {
    public Pawn Owner;
    public PawnBuffData BuffData;

    /// Subscribe to events, increase stats, etc.
    public virtual void OnBuffAdded() {}

    /// Unsubscribe to events, remove increased stats, etc.
    public virtual void OnBuffRemoved() {}
}