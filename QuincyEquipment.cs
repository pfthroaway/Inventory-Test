using Godot;
using InventoryTest;
using Sulimn.Classes.HeroParts;
using Sulimn.Classes.Items;
using System;
using System.Collections.Generic;

public class QuincyEquipment : Panel
{
    private QuincySlot Weapon = new QuincySlot();
    private QuincySlot Head = new QuincySlot();
    private QuincySlot Body = new QuincySlot();
    private QuincySlot Hands = new QuincySlot();
    private QuincySlot Legs = new QuincySlot();
    private QuincySlot Feet = new QuincySlot();
    private QuincySlot LeftRing = new QuincySlot();
    private QuincySlot RightRing = new QuincySlot();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Weapon = (QuincySlot)GetNode("HeadSlot");
        //Head = (QuincySlot)GetNode("HeadSlot");
        //Body = (QuincySlot)GetNode("BodySlot");
        //Hands = (QuincySlot)GetNode("HandsSlot");
        //Legs = (QuincySlot)GetNode("LegsSlot");
        //Feet = (QuincySlot)GetNode("FeetSlot");
        //LeftRing = (QuincySlot)GetNode("HeadSlot");
        //RightRing = (QuincySlot)GetNode("HeadSlot");
        //Weapon.ItemTypes = new List<ItemType> { ItemType.MeleeWeapon, ItemType.RangedWeapon };
        //Head.ItemTypes = new List<ItemType> { ItemType.HeadArmor };
        //Body.ItemTypes = new List<ItemType> { ItemType.BodyArmor };
        //Hands.ItemTypes = new List<ItemType> { ItemType.HandArmor };
        //Legs.ItemTypes = new List<ItemType> { ItemType.LegArmor };
        //Feet.ItemTypes = new List<ItemType> { ItemType.FeetArmor };
        //LeftRing.ItemTypes = new List<ItemType> { ItemType.Ring };
        //RightRing.ItemTypes = new List<ItemType> { ItemType.Ring };
    }

    public QuincyEquipment()
    {
    }

    public QuincyEquipment(Equipment equipment)
    {
        //Weapon.Item.Item = equipment.Weapon;
        //Head.Item.Item = equipment.Head;
        //Body.Item.Item = equipment.Body;
        //Hands.Item.Item = equipment.Hands;
        //Legs.Item.Item = equipment.Legs;
        //Feet.Item.Item = equipment.Feet;
        //LeftRing.Item.Item = equipment.LeftRing;
        //RightRing.Item.Item = equipment.RightRing;
    }
}