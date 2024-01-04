using UnityEngine;

public class PawnBuff : MonoBehaviour  {
    public PawnBuffData BuffData;
    
    /// Subscribe to events, increase stats, etc.
    public virtual void OnBuffAdded() {
        TryGetComponent(out PawnBuff pawnBuff);
    }

    /// Unsubscribe to events, remove increased stats, etc.
    public virtual void OnBuffRemoved() {

        Pawn pawn = GetComponent<Pawn>();
        PawnBuffDatabase.instance.TryAddBuff<RetalliationBuff, RetalliationBuffData>(pawn, out RetalliationBuff pawnBuff);
    }
}