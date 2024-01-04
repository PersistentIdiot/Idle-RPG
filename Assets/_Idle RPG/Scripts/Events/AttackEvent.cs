using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEvent : IEvent {
    public Pawn Instigator;
    public List<Pawn> Victims;
}
