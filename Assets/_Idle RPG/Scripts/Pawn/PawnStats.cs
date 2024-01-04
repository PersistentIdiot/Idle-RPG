using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnStats : MonoBehaviour {
    public float CurrentHealth = 75;
    public float MaxHealth = 75;
    public float Speed = 1;
    public float AttackDamage = 5;

    private void Awake() {
        CurrentHealth = MaxHealth;
    }
}
