using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnStats : MonoBehaviour {
    public float CurrentHealth = Single.PositiveInfinity;
    public float MaxHealth = 75;
    public float Speed = 1;

    private void Awake() {
        CurrentHealth = MaxHealth;
    }
}
