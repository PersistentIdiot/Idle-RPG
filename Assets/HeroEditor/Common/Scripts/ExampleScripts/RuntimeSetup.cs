using System.Linq;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using Assets.HeroEditor.Common.Scripts.EditorScripts;
using Assets.HeroEditor.Common.Scripts.CharacterScripts.Firearms;
using Assets.HeroEditor.Common.Scripts.Collections;
using HeroEditor.Common.Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.HeroEditor.Common.Scripts.ExampleScripts
{
	/// <summary>
	/// Changing equipment at runtime examples.
	/// </summary>
	public class RuntimeSetup : MonoBehaviour // Extend this class to fit your needs!
	{
		[FormerlySerializedAs("characterModel")]
		[FormerlySerializedAs("Character")]
		public PawnModel pawnModel;

		/// <summary>
		/// Example call: SetBody("HeadScar", "Basic", "Human", "Basic");
		/// </summary>
		public void SetBody(string headName, string headCollection, string bodyName, string bodyCollection)
		{
			var head = pawnModel.SpriteCollection.Head.SingleOrDefault(i => i.Name == headName && i.Collection == headCollection);
			var body = pawnModel.SpriteCollection.Body.SingleOrDefault(i => i.Name == bodyName && i.Collection == bodyCollection);

            pawnModel.SetBody(head, BodyPart.Head);
            pawnModel.SetBody(body, BodyPart.Body);
		}

		public void EquipMeleeWeapon1H(string spriteName, string collectionName)
		{
			var entry = pawnModel.SpriteCollection.MeleeWeapon1H.SingleOrDefault(i => i.Name == spriteName && i.Collection == collectionName);
			
			pawnModel.Equip(entry, EquipmentPart.MeleeWeapon1H);
		}

		public void EquipMeleeWeapon2H(string spriteName, string collectionName)
		{
			var entry = pawnModel.SpriteCollection.MeleeWeapon2H.SingleOrDefault(i => i.Name == spriteName && i.Collection == collectionName);

			pawnModel.Equip(entry, EquipmentPart.MeleeWeapon2H);
		}

		public void EquipBow(string spriteName, string collectionName)
		{
			var entry = pawnModel.SpriteCollection.Bow.SingleOrDefault(i => i.Name == spriteName && i.Collection == collectionName);

			pawnModel.Equip(entry, EquipmentPart.Bow);
		}

		public void EquipFirearm1H(string spriteName, string collectionName)
		{
			var entry = pawnModel.SpriteCollection.Firearm1H.SingleOrDefault(i => i.Name == spriteName && i.Collection == collectionName);
			
			pawnModel.Equip(entry, EquipmentPart.Firearm1H);
            pawnModel.Firearm.Params = FirearmCollection.Instances[pawnModel.SpriteCollection.Id].Firearms.SingleOrDefault(i => i.Name == spriteName);
		}

		public void EquipShield(string spriteName, string collectionName)
		{
			var entry = pawnModel.SpriteCollection.Shield.SingleOrDefault(i => i.Name == spriteName && i.Collection == collectionName);

			pawnModel.Equip(entry, EquipmentPart.Shield);
		}

		public void EquipArmor(string spriteName, string collectionName)
		{
			var entry = pawnModel.SpriteCollection.Armor.SingleOrDefault(i => i.Name == spriteName && i.Collection == collectionName);

			pawnModel.Equip(entry, EquipmentPart.Armor);
		}

		public void EquipHelmet(string spriteName, string collectionName)
		{
			var entry = pawnModel.SpriteCollection.Helmet.SingleOrDefault(i => i.Name == spriteName && i.Collection == collectionName);

            pawnModel.Equip(entry, EquipmentPart.Helmet);
		}

		public void RemoveHelmet()
		{
            pawnModel.UnEquip(EquipmentPart.Helmet); // Simply put null to remove equipment.
		}

        public void RemoveHair()
        {
            pawnModel.Hair = null; // Alternatively, you can work with properties directly (or write your own wrappers).
            pawnModel.Initialize();
		}
	}
}