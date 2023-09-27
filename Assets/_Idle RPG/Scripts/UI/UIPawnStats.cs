using System;
using System.Collections;
using System.Collections.Generic;
using DuloGames.UI;
using UnityEngine;

public class UIPawnStats : MonoBehaviour {
    public Pawn Owner;
    public UIProgressBar HealthProgressBar;
    public UIProgressBar TurnProgressBar;
    
    private void Update() {
        HealthProgressBar.fillAmount = Owner.Stats.CurrentHealth / Owner.Stats.MaxHealth;
        TurnProgressBar.fillAmount = Owner.TurnProgress / 100.0f;
    }
}
