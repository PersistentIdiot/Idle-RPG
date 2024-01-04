using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using Assets.HeroEditor.Common.Scripts.Common;
using HeroEditor.Common.Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.HeroEditor.Common.Scripts.ExampleScripts
{
    /// <summary>
    /// An example of how to change character's equipment.
    /// </summary>
    public class EquipmentExample : MonoBehaviour
    {
        [FormerlySerializedAs("characterModel")]
        [FormerlySerializedAs("Character")]
        public PawnModel pawnModel;
        public AppearanceExample AppearanceExample;

        public void EquipRandomArmor()
        {
            var randomIndex = Random.Range(0, pawnModel.SpriteCollection.Armor.Count);
            var randomItem = pawnModel.SpriteCollection.Armor[randomIndex];

            pawnModel.Equip(randomItem, EquipmentPart.Armor);
        }

        public void RemoveArmor()
        {
            pawnModel.UnEquip(EquipmentPart.Armor);
        }

        public void EquipRandomHelmet()
        {
            pawnModel.Equip(pawnModel.SpriteCollection.Helmet.Random(), EquipmentPart.Helmet);
            AppearanceExample.Refresh();
        }

        public void RemoveHelmet()
        {
            pawnModel.UnEquip(EquipmentPart.Helmet);
            AppearanceExample.Refresh();
        }

        public void EquipRandomShield()
        {
            pawnModel.Equip(pawnModel.SpriteCollection.Shield.Random(), EquipmentPart.Shield);
        }

        public void RemoveShield()
        {
            pawnModel.UnEquip(EquipmentPart.Shield);
        }

        public void EquipRandomWeapon()
        {
            pawnModel.Equip(pawnModel.SpriteCollection.MeleeWeapon1H.Random(), EquipmentPart.MeleeWeapon1H);
        }

        public void RemoveWeapon()
        {
            pawnModel.UnEquip(EquipmentPart.MeleeWeapon1H);
        }

        public void EquipRandomBow()
        {
            pawnModel.Equip(pawnModel.SpriteCollection.Bow.Random(), EquipmentPart.Bow);
        }

        public void RemoveBow()
        {
            pawnModel.UnEquip(EquipmentPart.Bow);
        }

        public void Reset()
        {
            pawnModel.ResetEquipment();
            AppearanceExample.Appearance = new CharacterAppearance();
            AppearanceExample.Refresh();
        }
    }
}