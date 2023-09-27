using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttackEvent : IEvent {
    public Pawn Instigator;
    public List<Pawn> Victims;
}
