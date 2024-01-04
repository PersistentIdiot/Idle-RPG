using System;
using System.Collections.Generic;
using System.Linq;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using HeroEditor.Common.Enums;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.HeroEditor.Common.Scripts.ExampleScripts
{
	/// <summary>
	/// Class for storing character data
	/// </summary>
	[Serializable]
	public class CharacterData //  Add more properties
	{
		public string Helmet;
		public string Armor;
		public List<string> Weapons;
		public WeaponType WeaponType;
	}

	/// <summary>
	/// This example will show you how to store character data. Don't mix it up with saving prefabs!
	/// </summary>
	public static class SaveLoadExample
	{
		public static void SaveToPlayerPrefs(PawnModel pawnModel)
		{
			var characterData = new CharacterData
			{
				Helmet = pawnModel.Helmet.texture.name,
				Armor = pawnModel.Armor[0].texture.name,
				WeaponType = pawnModel.WeaponType
			};

			switch (pawnModel.WeaponType)
			{
				case WeaponType.Melee1H:
				case WeaponType.Melee2H:
					characterData.Weapons = new List<string> { pawnModel.PrimaryMeleeWeapon.texture.name };
					break;
				case WeaponType.MeleePaired:
					characterData.Weapons = new List<string> { pawnModel.PrimaryMeleeWeapon.texture.name, pawnModel.SecondaryMeleeWeapon.texture.name };
					break;
				case WeaponType.Bow:
					characterData.Weapons = new List<string> { pawnModel.Bow[0].texture.name };
					break;
				case WeaponType.Firearm1H:
				case WeaponType.Firearm2H:
					characterData.Weapons = new List<string> { pawnModel.Firearms[0].texture.name };
					break;
				default: throw new NotImplementedException();
			}

			var json = JsonUtility.ToJson(characterData);

			PlayerPrefs.SetString("Character", json);
			PlayerPrefs.Save();
		}

		public static CharacterData LoadFromPlayerPrefs()
		{
			var json = PlayerPrefs.GetString("Character");

			return JsonUtility.FromJson<CharacterData>(json);
		}

		public static PawnModel CreateCharacter(GameObject prefab, CharacterData characterData)
		{
			var character = Object.Instantiate(prefab).GetComponent<PawnModel>();
            var spriteCollection = character.SpriteCollection;

			character.Helmet = spriteCollection.Helmet.Single(i => i.Name == characterData.Helmet).Sprite;
			character.Armor = spriteCollection.Armor.Single(i => i.Name == characterData.Armor).Sprites;
			character.WeaponType = characterData.WeaponType;

			switch (character.WeaponType)
			{
				case WeaponType.Melee1H:
					character.PrimaryMeleeWeapon = spriteCollection.MeleeWeapon1H.Single(i => i.Name == characterData.Weapons[0]).Sprite;
					break;
				case WeaponType.Melee2H:
					character.PrimaryMeleeWeapon = spriteCollection.MeleeWeapon2H.Single(i => i.Name == characterData.Weapons[0]).Sprite;
					break;
				case WeaponType.MeleePaired:
					character.PrimaryMeleeWeapon = spriteCollection.MeleeWeapon1H.Single(i => i.Name == characterData.Weapons[0]).Sprite;
					character.SecondaryMeleeWeapon = spriteCollection.MeleeWeapon1H.Single(i => i.Name == characterData.Weapons[1]).Sprite;
					break;
				case WeaponType.Bow:
					character.Bow = spriteCollection.Bow.Single(i => i.Name == characterData.Weapons[0]).Sprites;
					break;
				case WeaponType.Firearm1H:
					character.Bow = spriteCollection.Firearm1H.Single(i => i.Name == characterData.Weapons[0]).Sprites;
					break;
				case WeaponType.Firearm2H:
					character.Bow = spriteCollection.Firearm2H.Single(i => i.Name == characterData.Weapons[0]).Sprites;
					break;
				default: throw new NotImplementedException();
			}

			return character;
		}
	}
}