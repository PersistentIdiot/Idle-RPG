using System;
using System.Collections;
using System.Collections.Generic;
using DuloGames.UI;
using UnityEngine;

public class UIPawnStats : MonoBehaviour {
    public Pawn Owner;
    public UIProgressBar TurnProgressBar;
    
    private void Update() {
        TurnProgressBar.fillAmount = Owner.TurnProgress / 100.0f;
    }
}
