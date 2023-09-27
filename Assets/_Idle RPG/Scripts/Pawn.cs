using System;
using System.Collections;
using System.Collections.Generic;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Pawn : MonoBehaviour {
    public Character Character;
    public PawnStats Stats;
    public float TurnProgress = 0;
    
    public async UniTask TakeTurn() {
        Debug.Log($"{gameObject.name}'s turn!");
        Character.Slash();
        await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);
        return;
    }
}