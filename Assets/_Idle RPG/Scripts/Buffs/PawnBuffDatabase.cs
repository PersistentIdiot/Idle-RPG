using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Idle RPG/Buffs/ = Pawn Buff Database Singleton = ", fileName = "PawnBuffDatabase")]
public class PawnBuffDatabase : ScriptableSingleton<PawnBuffDatabase> {
    [SerializeField] private List<PawnBuffData> buffDatas;

    public bool TryAddBuff<TPawnBuff>(Pawn pawn, out TPawnBuff pawnBuff) where TPawnBuff : PawnBuff {
        pawnBuff = pawn.AddComponent<TPawnBuff>();

        // Try and find buffData in list which corresponds to the provided type.
        PawnBuffData pawnBuffData = buffDatas.FirstOrDefault(buffData => buffData.PawnBuffScript.GetClass() == typeof(TPawnBuff));

        // Return false and exit if not found
        if (pawnBuffData == null) {
            Debug.Log($"{nameof(PawnBuffDatabase)}.{nameof(TryAddBuff)}() failed to add {nameof(TPawnBuff)}");
            return false;
        }

        // Return false and exit if we failed to add TPawnBuff to pawn
        if (pawnBuff == null) {
            Debug.Log($"Failed to add {nameof(TPawnBuff)} to {pawn.name}");
            return false;
        }

        // Finally, assign BuffData and return success
        pawnBuff.Owner = pawn;
        pawnBuff.BuffData = pawnBuffData;
        return true;
    }
}