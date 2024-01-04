using System.Linq;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.HeroEditor.Common.Scripts.ExampleScripts
{
    /// <summary>
    /// Bow shooting behaviour (charge/release bow, create arrow). It's just an example!
    /// </summary>
    public class BowExample : MonoBehaviour
    {
        [FormerlySerializedAs("characterModel")]
        [FormerlySerializedAs("Character")]
        public PawnModel pawnModel;
        public AnimationClip ClipCharge;
	    public Transform FireTransform;
	    public GameObject ArrowPrefab;
        public bool CreateArrows;

        /// <summary>
        /// Should be set outside (by input manager or AI).
        /// </summary>
        [HideInInspector] public bool ChargeButtonDown;
        [HideInInspector] public bool ChargeButtonUp;

        private float _chargeTime;

        public void Update()
        {
            if (ChargeButtonDown)
            {
                _chargeTime = Time.time;
                pawnModel.Animator.SetInteger("Charge", 1);
            }

            if (ChargeButtonUp)
            {
                var charged = Time.time - _chargeTime > ClipCharge.length;

                pawnModel.Animator.SetInteger("Charge", charged ? 2 : 3);

                if (charged && CreateArrows)
                {
	                CreateArrow();
                }
            }
        }

		private void CreateArrow()
		{
			var arrow = Instantiate(ArrowPrefab, FireTransform);
			var sr = arrow.GetComponent<SpriteRenderer>();
			var rb = arrow.GetComponent<Rigidbody>();
			const float speed = 18.75f; // Change this!
			
			arrow.transform.localPosition = Vector3.zero;
			arrow.transform.localRotation = Quaternion.identity;
			arrow.transform.SetParent(null);
			sr.sprite = pawnModel.Bow.Single(j => j.name == "Arrow");
			rb.velocity = speed * FireTransform.right * Mathf.Sign(pawnModel.transform.lossyScale.x) * Random.Range(0.85f, 1.15f);

			var characterCollider = pawnModel.GetComponent<Collider>();

			if (characterCollider != null)
			{
				Physics.IgnoreCollision(arrow.GetComponent<Collider>(), characterCollider);
			}

			arrow.gameObject.layer = 31; // Create layer in your project and disable collision for it (in physics settings)
			Physics.IgnoreLayerCollision(31, 31, true); // Disable collision with other projectiles.
		}
	}
}