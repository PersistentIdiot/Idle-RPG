using System.Linq;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using HeroEditor.Common.Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.HeroEditor.Common.Scripts.ExampleScripts
{
    /// <summary>
    /// An example of how to change character's appearance.
    /// </summary>
    public class AppearanceExample : MonoBehaviour
    {
        public CharacterAppearance Appearance = new CharacterAppearance();
        [FormerlySerializedAs("characterModel")]
        [FormerlySerializedAs("Character")]
        public PawnModel pawnModel;
        public AvatarSetup AvatarSetup;
        
        public void Start()
        {
            Refresh();
        }

        public void Refresh()
        {
            Appearance.Setup(pawnModel);

            var helmetId = pawnModel.SpriteCollection.Helmet.SingleOrDefault(i => i.Sprite == pawnModel.Helmet)?.Id;

            AvatarSetup.Initialize(Appearance, helmetId);
        }

        public void SetRandomAppearance()
        {
            Appearance.Hair = Random.Range(0, 3) == 0 ? null : pawnModel.SpriteCollection.Hair[Random.Range(0, pawnModel.SpriteCollection.Hair.Count)].Id;
            Appearance.HairColor = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
            Appearance.Eyebrows = pawnModel.SpriteCollection.Eyebrows[Random.Range(0, pawnModel.SpriteCollection.Eyebrows.Count)].Id;
            Appearance.Eyes = pawnModel.SpriteCollection.Eyes[Random.Range(0, pawnModel.SpriteCollection.Eyes.Count)].Id;
            Appearance.EyesColor = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
            Appearance.Mouth = pawnModel.SpriteCollection.Mouth[Random.Range(0, pawnModel.SpriteCollection.Mouth.Count)].Id;
            Appearance.Beard = Random.Range(0, 3) == 0 ? pawnModel.SpriteCollection.Beard[Random.Range(0, pawnModel.SpriteCollection.Beard.Count)].Id : null;

            Refresh();
        }

        public void ResetAppearance()
        {
            Appearance = new CharacterAppearance();
            Refresh();
        }

        public void SetRandomHair()
        {
            var randomIndex = Random.Range(0, pawnModel.SpriteCollection.Hair.Count);
            var randomItem = pawnModel.SpriteCollection.Hair[randomIndex];
            var randomColor = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));

            pawnModel.SetBody(randomItem, BodyPart.Hair, randomColor);
        }

        public void SetRandomEyebrows()
        {
            var randomIndex = Random.Range(0, pawnModel.SpriteCollection.Eyebrows.Count);
            var randomItem = pawnModel.SpriteCollection.Eyebrows[randomIndex];

            pawnModel.SetBody(randomItem, BodyPart.Eyebrows);
        }

        public void SetRandomEyes()
        {
            var randomIndex = Random.Range(0, pawnModel.SpriteCollection.Eyes.Count);
            var randomItem = pawnModel.SpriteCollection.Eyes[randomIndex];
            var randomColor = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));

            pawnModel.SetBody(randomItem, BodyPart.Eyes, randomColor);
        }

        public void SetRandomMouth()
        {
            var randomIndex = Random.Range(0, pawnModel.SpriteCollection.Mouth.Count);
            var randomItem = pawnModel.SpriteCollection.Mouth[randomIndex];

            pawnModel.SetBody(randomItem, BodyPart.Mouth);
        }
    }
}