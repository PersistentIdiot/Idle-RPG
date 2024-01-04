using System;

public class RetalliationBuff: PawnBuff {
    private void OnEnable() {
        OnBuffAdded();
    }

    private void OnDisable() {
        OnBuffRemoved();
    }
}