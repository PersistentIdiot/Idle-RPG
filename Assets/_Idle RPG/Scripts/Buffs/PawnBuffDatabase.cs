using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Idle RPG/Buffs/ = Pawn Buff Database Singleton = ", fileName = "PawnBuffDatabase")]
public class PawnBuffDatabase : ScriptableSingleton<PawnBuffDatabase> {
    [SerializeField] private List<PawnBuffData> buffDatas;

    // Get methods, TryGet, etc
    // AddBuff<PawnBuff>(Pawn)

    public bool TryAddBuff<TPawnBuff, TPawnBuffData>(Pawn pawn, out TPawnBuff pawnBuff) where TPawnBuff : PawnBuff where TPawnBuffData : PawnBuffData {
        pawnBuff = null;
        
        // Try and find buffData in list which corresponds to the provided type.
        PawnBuffData pawnBuffData = buffDatas.FirstOrDefault(buffdata => buffdata is TPawnBuffData);
        
        // Return false and exit if not found
        if (pawnBuffData == null) {
            Debug.Log($"{nameof(PawnBuffDatabase)}.{nameof(TryAddBuff)}() failed to add {nameof(TPawnBuffData)}");
            return false;
        }

        pawnBuff = pawn.AddComponent<TPawnBuff>();
        pawnBuff.BuffData = pawnBuffData;

        if (pawnBuff == null) {
            Debug.Log($"Failed to add {nameof(TPawnBuff)} to {pawn.name}");
            return false;
        }

        return true;
    }
}