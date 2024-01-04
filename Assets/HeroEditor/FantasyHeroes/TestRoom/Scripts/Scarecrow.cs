using Assets.HeroEditor.FantasyHeroes.TestRoom.Scripts;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.HeroEditor.FantasyHeroes.TestRoom.Scarecrow
{
    /// <summary>
    /// Enemy example behaviour.
    /// </summary>
    public class Scarecrow : MonoBehaviour
    {
        [FormerlySerializedAs("characterModel")]
        [FormerlySerializedAs("Character")]
        public PawnModel pawnModel;

        public void Start()
        {
            pawnModel = FindObjectOfType<PawnModel>();

            if (pawnModel != null)
            {
                pawnModel.Animator.GetComponent<AnimationEvents>().OnCustomEvent += OnAnimationEvent;
            }
        }

        public void OnDestroy()
        {
            if (pawnModel != null)
            {
                pawnModel.Animator.GetComponent<AnimationEvents>().OnCustomEvent -= OnAnimationEvent;
            }
        }

        private void OnAnimationEvent(string eventName)
        {
            if (eventName == "Hit" && Vector2.Distance(pawnModel.MeleeWeapon.Edge.position, transform.position) < 1.5)
            {
                GetComponent<Monster>().Spring();
            }
        }
    }
}