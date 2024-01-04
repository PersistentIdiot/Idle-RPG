using UnityEngine;

public class PawnBuff : MonoBehaviour {
    public Pawn Owner;
    public PawnBuffData BuffData;

    /// Fires a BuffAddedEvent. Used to subscribe to events, increase stats, etc.
    public virtual void OnBuffAdded() {
        BuffAddedEvent buffAddedEvent = new BuffAddedEvent() {
            Owner = Owner,
            Buff = this
        };

        EventBus.RaiseImmediately(ref buffAddedEvent);
    }

    /// Fires a BuffRemovedEvent. Used to remove increased stats, etc.
    public virtual void OnBuffRemoved() {
        BuffRemovedEvent buffRemovedEvent = new BuffRemovedEvent() {
            Owner = Owner,
            Buff = this
        };
        EventBus.RaiseImmediately(ref buffRemovedEvent);
    }
}