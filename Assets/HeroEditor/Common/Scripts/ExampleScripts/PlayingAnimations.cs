using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.HeroEditor.Common.Scripts.ExampleScripts
{
	/// <summary>
	/// Playing different animations example. For full list of animation params and states please open Animator window and select Human.controller.
	/// </summary>
	public class PlayingAnimations : MonoBehaviour
	{
		[FormerlySerializedAs("characterModel")]
		[FormerlySerializedAs("Character")]
		public PawnModel pawnModel;

		public void Reset()
		{
			pawnModel.ResetAnimation();
		}

        public void GetReady()
        {
            pawnModel.GetReady();
        }

        public void Relax()
        {
            pawnModel.Relax();
        }

		public void Idle()
		{
			pawnModel.SetState(CharacterState.Idle);
		}

		public void Walk()
		{
            pawnModel.SetState(CharacterState.Walk);
		}

		public void Run()
		{
            pawnModel.SetState(CharacterState.Run);
		}

		public void Jump()
		{
            pawnModel.SetState(CharacterState.Jump);
		}

		public void Slash()
		{
			pawnModel.Slash();
		}

		public void Jab()
		{
			pawnModel.Jab();
		}

        public void Die()
        {
			pawnModel.SetState(CharacterState.DeathB);
		}
	}
}