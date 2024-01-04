using System;
using System.Collections.Generic;
using System.Linq;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using Assets.HeroEditor.Common.Scripts.Common;
using Assets.HeroEditor.InventorySystem.Scripts.Data;
using Assets.HeroEditor.InventorySystem.Scripts.Enums;
using HeroEditor.Common.Enums;
using UnityEngine;

namespace Assets.HeroEditor.InventorySystem.Scripts.Helpers
{
    public class CharacterInventorySetup
    {
        public static void Setup(PawnModel pawnModel, List<Item> equipped)
        {
            pawnModel.ResetEquipment();
            
            foreach (var item in equipped)
            {
                try
                {
                    switch (item.Params.Type)
                    {
                        case ItemType.Helmet:
                            var helmet = pawnModel.SpriteCollection.Helmet.Single(i => i.Id == item.Params.SpriteId);

                            pawnModel.Helmet = helmet.Sprite;
                            pawnModel.FullHair = helmet.Tags.Contains("FullHair");
                            break;
                        case ItemType.Armor:
                            pawnModel.Armor = pawnModel.SpriteCollection.Armor.FindSpritesById(item.Params.SpriteId);
                            break;
                        case ItemType.Shield:
                            pawnModel.Shield = pawnModel.SpriteCollection.Shield.FindSpriteById(item.Params.SpriteId);
                            pawnModel.WeaponType = WeaponType.Melee1H;
                            break;
                        case ItemType.Weapon:

                            switch (item.Params.Class)
                            {
                                case ItemClass.Bow:
                                    pawnModel.WeaponType = WeaponType.Bow;
                                    pawnModel.Bow = pawnModel.SpriteCollection.Bow.FindSpritesById(item.Params.SpriteId);
                                    break;
                                default:
                                    if (item.IsFirearm)
                                    {
                                        throw new NotImplementedException("Firearm equipping is not implemented. Implement if needed.");
                                    }
                                    else
                                    {
                                        pawnModel.WeaponType = item.Params.Tags.Contains(ItemTag.TwoHanded) ? WeaponType.Melee2H : WeaponType.Melee1H;
                                        pawnModel.PrimaryMeleeWeapon = (pawnModel.WeaponType == WeaponType.Melee1H ? pawnModel.SpriteCollection.MeleeWeapon1H : pawnModel.SpriteCollection.MeleeWeapon2H).FindSpriteById(item.Params.SpriteId);
                                    }
                                    break;
                            }
                            break;
                    }
                }
                catch (Exception e)
                {
                    Debug.LogErrorFormat("Unable to equip {0} ({1})", item.Params.Id, e.Message);
                }
            }

            pawnModel.Initialize();
        }
    }
}