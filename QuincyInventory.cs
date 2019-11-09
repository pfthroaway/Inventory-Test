using Godot;
using Sulimn.Classes.Entities;
using Sulimn.Classes.HeroParts;
using Sulimn.Classes.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTest
{
    public class QuincyInventory : Control
    {
        private GridContainer GridInventory;
        private QuincyEquipment QuincyEquipment;
        private QuincySlot WeaponSlot, HeadSlot, BodySlot, HandsSlot, LegsSlot, FeetSlot, LeftRingSlot, RightRingSlot;
        private Hero CurrentHero = new Hero();

        /// <summary>Assign all controls.</summary>
        private void AssignControls()
        {
            GridInventory = (GridContainer)GetNode("Panel/GridInventory");
            QuincyEquipment = (QuincyEquipment)GetNode("QuincyEquipment");
            WeaponSlot = (QuincySlot)QuincyEquipment.GetNode("WeaponSlot");
            HeadSlot = (QuincySlot)QuincyEquipment.GetNode("HeadSlot");
            BodySlot = (QuincySlot)QuincyEquipment.GetNode("BodySlot");
            HandsSlot = (QuincySlot)QuincyEquipment.GetNode("HandsSlot");
            LegsSlot = (QuincySlot)QuincyEquipment.GetNode("LegsSlot");
            FeetSlot = (QuincySlot)QuincyEquipment.GetNode("FeetSlot");
            LeftRingSlot = (QuincySlot)QuincyEquipment.GetNode("LeftRingSlot");
            RightRingSlot = (QuincySlot)QuincyEquipment.GetNode("RightRingSlot");
            WeaponSlot.ItemTypes = new List<ItemType> { ItemType.MeleeWeapon, ItemType.RangedWeapon };
            HeadSlot.ItemTypes = new List<ItemType> { ItemType.HeadArmor };
            BodySlot.ItemTypes = new List<ItemType> { ItemType.BodyArmor };
            HandsSlot.ItemTypes = new List<ItemType> { ItemType.HandArmor };
            LegsSlot.ItemTypes = new List<ItemType> { ItemType.LegArmor };
            FeetSlot.ItemTypes = new List<ItemType> { ItemType.FeetArmor };
            LeftRingSlot.ItemTypes = new List<ItemType> { ItemType.Ring };
            RightRingSlot.ItemTypes = new List<ItemType> { ItemType.Ring };
        }

        public void SetUpInventory(List<Item> inventory)
        {
            for (int i = 0; i < 40; i++)
            {
                if (i < inventory.Count)
                {
                    QuincySlot slot = (QuincySlot)FindNode($"QuincySlot{i + 1}");
                    AddItemInstanceToSlot(slot, inventory[i]);
                }
            }
        }

        private void AddItemInstanceToSlot(QuincySlot slot, Item item)
        {
            var scene = (PackedScene)ResourceLoader.Load("res://QuincyItem.tscn");
            QuincyItem qItem = (QuincyItem)scene.Instance();
            slot.AddChild(qItem);
            slot.Item = qItem;
            slot.Item.SetItem(item);
            qItem.Theme = (Theme)ResourceLoader.Load("res://TextureRect.tres");
        }

        public void SetUpEquipment(Equipment equipment)
        {
            if (equipment.Weapon != new Item())
                AddItemInstanceToSlot(WeaponSlot, equipment.Weapon);
            if (equipment.Head != new Item())
                AddItemInstanceToSlot(HeadSlot, equipment.Head);
            if (equipment.Body != new Item())
                AddItemInstanceToSlot(BodySlot, equipment.Body);
            if (equipment.Hands != new Item())
                AddItemInstanceToSlot(HandsSlot, equipment.Hands);
            if (equipment.Legs != new Item())
                AddItemInstanceToSlot(LegsSlot, equipment.Legs);
            if (equipment.Feet != new Item())
                AddItemInstanceToSlot(FeetSlot, equipment.Feet);
            if (equipment.LeftRing != new Item())
                AddItemInstanceToSlot(LeftRingSlot, equipment.LeftRing);
            if (equipment.RightRing != new Item())
                AddItemInstanceToSlot(RightRingSlot, equipment.RightRing);
        }

        // Called when the node enters the scene tree for the first time.

        public override void _Ready()
        {
            OS.WindowMaximized = true;
            AssignControls();
            CurrentHero.Equipment.Weapon = new Item("Short Sword", "A short sword.", ItemType.MeleeWeapon, 15, 0, 10, 100, 100, 100, 0, 0, 0, 0, 0, 0, false, 1, true, true, new List<HeroClass>(), "res://assets/inventory/images/W_Sword001.png");
            CurrentHero.Equipment.Head = new Item("Helmet", "A basic metal helmet.", ItemType.HeadArmor, 0, 10, 10, 100, 40, 40, 0, 0, 0, 0, 0, 0, false, 1, true, true, new List<HeroClass>(), "res://assets/inventory/images/C_Elm03.png");
            CurrentHero.Equipment.Body = new Item("Body Armour", "A basic metal body covering.", ItemType.BodyArmor, 0, 10, 10, 100, 30, 30, 0, 0, 0, 0, 0, 0, false, 1, true, true, new List<HeroClass>(), "res://assets/inventory/images/A_Armor05.png");
            CurrentHero.Equipment.Hands = new Item("Cloth Gloves", "Basic cloth hand coverings.", ItemType.HandArmor, 0, 10, 10, 100, 30, 30, 0, 0, 0, 0, 0, 0, false, 1, true, true, new List<HeroClass>(), "res://assets/inventory/images/W_Fist001.png");
            CurrentHero.Equipment.Legs = new Item("Cloth Pants", "Basic cloth leg coverings.", ItemType.LegArmor, 0, 10, 10, 100, 30, 30, 0, 0, 0, 0, 0, 0, false, 1, true, true, new List<HeroClass>(), "res://assets/inventory/images/I_Key06.png");
            CurrentHero.Equipment.Feet = new Item("Cloth Shoes", "Basic cloth feet coverings.", ItemType.FeetArmor, 0, 10, 10, 100, 30, 30, 0, 0, 0, 0, 0, 0, false, 1, true, true, new List<HeroClass>(), "res://assets/inventory/images/A_Shoes01.png");
            CurrentHero.Equipment.LeftRing = new Item("Ring", "A ring with +1 stats!", ItemType.Ring, 0, 5, 0, 500, 10, 10, 1, 1, 1, 1, 0, 0, false, 1, true, true, new List<HeroClass>(), "res://assets/inventory/images/Ac_Ring02.png");
            CurrentHero.Equipment.RightRing = new Item("Super Ring", "A ring with +2 stats!", ItemType.Ring, 0, 5, 0, 500, 10, 10, 2, 2, 2, 2, 0, 0, false, 1, true, true, new List<HeroClass>(), "res://assets/inventory/images/Ac_Ring05.png");

            CurrentHero.AddItem(new Item("Plate Armour", "Strong, durable metal armor.", ItemType.BodyArmor, 0, 20, 50, 200, 100, 100, 0, 0, 0, 0, 0, 0, false, 1, true, true, new List<HeroClass>(), "res://assets/inventory/images/A_Armour02.png"));
            CurrentHero.AddItem(new Item("Shoes", "Regular shoes.", ItemType.FeetArmor, 0, 5, 10, 50, 10, 10, 0, 0, 0, 0, 0, 0, false, 1, true, true, new List<HeroClass>(), "res://assets/inventory/images/A_Shoes03.png"));
            CurrentHero.AddItem(new Item("Long Sword", "A long sword.", ItemType.MeleeWeapon, 30, 0, 20, 500, 70, 70, 0, 0, 0, 0, 0, 0, false, 1, true, true, new List<HeroClass>(), "res://assets/inventory/images/W_Sword002.png"));
            CurrentHero.AddItem(new Item("Healing Potion", "A potion that heals you.", ItemType.Potion, 0, 0, 1, 100, 10, 10, 0, 0, 0, 0, 50, 0, false, 1, true, true, new List<HeroClass>(), "res://assets/inventory/images/P_Red03.png"));
            SetUpInventory(CurrentHero.Inventory);
            SetUpEquipment(CurrentHero.Equipment);
        }

        public override void _Process(float delta)
        {
        }
    }
}