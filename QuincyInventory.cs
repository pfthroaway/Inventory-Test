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
        private Hero CurrentHero = new Hero();

        /// <summary>Assign all controls.</summary>
        private void AssignControls()
        {
            GridInventory = (GridContainer)GetNode("Panel/GridInventory");
            QuincyEquipment = (QuincyEquipment)GetNode("QuincyEquipment");
        }

        public void SetUpInventory(List<Item> inventory)
        {
            for (int i = 0; i < 40; i++)
            {
                QuincySlot slot = i < inventory.Count ? new QuincySlot(new QuincyItem(inventory[i])) : new QuincySlot();
                GridInventory.AddChild(slot);
            }
        }

        public void SetUpEquipment(Equipment equipment)
        {
            QuincyEquipment = new QuincyEquipment(equipment);
        }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            OS.WindowMaximized = true;
            AssignControls();
            //SetTooltips();
            CurrentHero.Equipment.Weapon = new Item("Short Sword", "A short sword.", ItemType.MeleeWeapon, 15, 0, 10, 100, 100, 100, 0, 0, 0, 0, 0, 0, false, 1, true, true, new List<HeroClass>(), "res://assets/inventory/images/W_Sword001.png");
            CurrentHero.Equipment.Head = new Item("Helmet", "A basic metal helmet.", ItemType.HeadArmor, 0, 10, 10, 100, 40, 40, 0, 0, 0, 0, 0, 0, false, 1, true, true, new List<HeroClass>(), "res://assets/inventory/images/C_Elm03.png");
            CurrentHero.Equipment.Body = new Item("Body Armour", "A basic metal body covering.", ItemType.BodyArmor, 0, 10, 10, 100, 30, 30, 0, 0, 0, 0, 0, 0, false, 1, true, true, new List<HeroClass>(), "res://assets/inventory/images/A_Armor05.png");
            CurrentHero.Equipment.LeftRing = new Item("Ring", "A ring with +1 stats!", ItemType.Ring, 0, 5, 0, 500, 10, 10, 1, 1, 1, 1, 0, 0, false, 1, true, true, new List<HeroClass>(), "res://assets/inventory/images/Ac_Ring02.png");
            CurrentHero.Equipment.RightRing = new Item("Super Ring", "A ring with +2 stats!", ItemType.Ring, 0, 5, 0, 500, 10, 10, 2, 2, 2, 2, 0, 0, false, 1, true, true, new List<HeroClass>(), "res://assets/inventory/images/Ac_Ring05.png");

            CurrentHero.AddItem(new Item("Plate Armour", "Strong, durable metal armor.", ItemType.BodyArmor, 0, 20, 50, 200, 100, 100, 0, 0, 0, 0, 0, 0, false, 1, true, true, new List<HeroClass>(), "res://assets/inventory/images/A_Armour02.png"));
            CurrentHero.AddItem(new Item("Shoes", "Regular shoes.", ItemType.FeetArmor, 0, 5, 10, 50, 10, 10, 0, 0, 0, 0, 0, 0, false, 1, true, true, new List<HeroClass>(), "res://assets/inventory/images/A_Shoes03.png"));
            CurrentHero.AddItem(new Item("Long Sword", "A long sword.", ItemType.MeleeWeapon, 30, 0, 20, 500, 70, 70, 0, 0, 0, 0, 0, 0, false, 1, true, true, new List<HeroClass>(), "res://assets/inventory/images/W_Sword002.png"));
            CurrentHero.AddItem(new Item("Healing Potion", "A potion that heals you.", ItemType.Potion, 0, 0, 1, 100, 10, 10, 0, 0, 0, 0, 50, 0, false, 1, true, true, new List<HeroClass>(), "res://assets/inventory/images/P_Red03.png"));
            SetUpInventory(CurrentHero.Inventory);
            SetUpEquipment(CurrentHero.Equipment);
        }
    }
}