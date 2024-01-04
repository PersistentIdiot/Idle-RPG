using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Assets.HeroEditor.Common.Scripts.EditorScripts
{
    /// <summary>
    /// Plays animation from character editor. Just for example.
    /// </summary>
    public class AnimationManager : MonoBehaviour
    {
        [FormerlySerializedAs("characterModel")]
        [FormerlySerializedAs("Character")]
        public PawnModel pawnModel;
        public Text UpperClipName;
        public Text LowerClipName;

        /// <summary>
        /// Called automatically on app start.
        /// </summary>
        public void Start()
        {
            pawnModel.UpdateAnimation();
            Refresh();
        }

        public void Refresh()
        {
            UpperClipName.text = "Relax / Ready";
            LowerClipName.text = pawnModel.GetState().ToString();
        }

        /// <summary>
        /// Change upper body animation and play it.
        /// </summary>
        public void PlayUpperBodyAnimation(int direction)
        {
            if (pawnModel.IsReady())
            {
                pawnModel.Relax();
            }
            else
            {
                pawnModel.GetReady();
            }

            Refresh();
        }

        /// <summary>
        /// Change lower body animation and play it.
        /// </summary>
        public void PlayLowerBodyAnimation(int direction)
        {
            var state = pawnModel.GetState();

            state += direction;

            if (state < 0)
            {
                state = CharacterState.DeathF;
            }
            else if (state > CharacterState.DeathF)
            {
                state = CharacterState.Idle;
            }

            pawnModel.SetState(state);

            Refresh(); 
        }
    }
}