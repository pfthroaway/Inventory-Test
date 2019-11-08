using Godot;
using Sulimn.Classes;
using Sulimn.Classes.Entities;
using Sulimn.Classes.Enums;
using Sulimn.Classes.HeroParts;
using Sulimn.Classes.Inventory;
using Sulimn.Classes.Items;
using System.Collections.Generic;
using System.Linq;

public class GenericInventoryScene : Control
{
    private TextureRect WeaponRect, HeadRect, BodyRect, HandsRect, LegsRect, FeetRect, LeftRingRect, RightRingRect;
    private InventoryItem Weapon, Head, Body, Hands, Legs, Feet, LeftRing, RightRing;
    private ItemSlot WeaponSlot, HeadSlot, BodySlot, HandsSlot, LegsSlot, FeetSlot, LeftRingSlot, RightRingSlot;
    private GridContainer GridInventory;
    private List<InventoryItem> inventoryList = new List<InventoryItem>();
    private List<InventoryItem> equipmentList = new List<InventoryItem>();
    private InventoryItem holdingItem;
    private Hero CurrentHero = new Hero();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        OS.WindowMaximized = true;
        AssignControls();
        SetTooltips();
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

    //internal void Load(Character character)
    //{
    //}

    /// <summary>Assign all controls.</summary>
    private void AssignControls()
    {
        WeaponRect = (TextureRect)GetNode("Weapon");
        HeadRect = (TextureRect)GetNode("Head");
        BodyRect = (TextureRect)GetNode("Body");
        HandsRect = (TextureRect)GetNode("Hands");
        LegsRect = (TextureRect)GetNode("Legs");
        FeetRect = (TextureRect)GetNode("Feet");
        LeftRingRect = (TextureRect)GetNode("LeftRing");
        RightRingRect = (TextureRect)GetNode("RightRing");
        RightRingRect = (TextureRect)GetNode("RightRing");
        GridInventory = (GridContainer)GetNode("GridInventory");
    }

    private void ClickMe()
    {
        if (holdingItem != null)
        {
        }
    }

    private void _on_TextureRect_gui_input(InputEvent @event, ItemSlot clickedSlot)
    {
        if (@event is InputEventMouseButton button && button.ButtonIndex == 1 && button.Pressed)
        {
            // If currently holding an item
            if (holdingItem != null)
            {
                // If you clicked into a valid slot and the item isn't null
                if (clickedSlot?.Item != null)
                {
                    // Check to see if it is trying to go into an equipment slot.
                    if (clickedSlot.ItemTypes.Count <= 2)
                    {
                        // If the slot already has a valid piece of equipment in it,
                        // check if the item going into it is a valid piece of equipment.
                        // If it's not a valid piece of equipment, don't let it go in.
                        // If the currently held item is a valid piece of equipment,
                        // let it go into that slot.
                        if (clickedSlot.ItemTypes.Contains(holdingItem.Item.Type))
                        {
                            if (clickedSlot.Name != "RightRing")
                                EquipAndSwap(clickedSlot);
                            else
                                EquipAndSwap(clickedSlot, RingHand.Right);
                        }
                    }

                    // If not going into an equipment slot
                    // check to see if the item came from an equipment slot.
                    // If it came from an equipment slot, make sure the item
                    // attempting to be swapped with is a valid piece of equipment for that slot.
                    // If it's not, unequip it and drop it in the first available slot.
                    else if (holdingItem.Slot.ItemTypes.Count <= 2)
                    {
                        if (holdingItem.Slot.ItemTypes.Contains(clickedSlot.Item.Item.Type))
                            EquipAndSwap(clickedSlot);
                        else
                            UnEquipAndPut(FindFirstEmptySlot());
                    }
                    // If the item didn't come from an equipment slot, swap the items.
                    else
                        SwapItems(clickedSlot);
                }
                // If you clicked into a valid slot and there's nothing there
                else if (clickedSlot != null)
                {
                    if (clickedSlot.ItemTypes.Count <= 2)
                    {
                        // If the slot already has a valid piece of equipment in it,
                        // check if the item going into it is a valid piece of equipment.
                        // If it's not a valid piece of equipment, don't let it go in.
                        // If the currently held item is a valid piece of equipment,
                        // let it go into that slot.
                        if (clickedSlot.ItemTypes.Contains(holdingItem.Item.Type))
                        {
                            if (clickedSlot.Name != "RightRing")
                                EquipAndPut(clickedSlot);
                            else
                                EquipAndPut(clickedSlot, RingHand.Right);
                        }
                    }
                    else if (holdingItem.Slot.ItemTypes.Count <= 2)
                        UnEquipAndPut(clickedSlot);
                    else
                        PutItem(clickedSlot);
                }
            }
            else if (clickedSlot?.Item != null)
            {
                holdingItem = clickedSlot.Item;
                clickedSlot.PickItem();
                holdingItem.RectGlobalPosition = new Vector2(button.GlobalPosition.x, button.GlobalPosition.y);
            }
        }
    }

    public void SetUpInventory(List<Item> inventory)
    {
        foreach (Item item in inventory)
            inventoryList.Add(new InventoryItem(item, null));

        for (int i = 0; i < 40; i++)
        {
            ItemSlot slot = new ItemSlot();
            if (i < inventoryList.Count && inventoryList[i].Item != new Item())
                slot.SetItem(inventoryList[i]);
            GridInventory.AddChild(slot);
        }
    }

    public void SetUpEquipment(Equipment equipment)
    {
        WeaponSlot = new ItemSlot();
        WeaponSlot.ItemTypes = new List<ItemType> { ItemType.MeleeWeapon, ItemType.RangedWeapon };
        HeadSlot = new ItemSlot();
        HeadSlot.ItemTypes = new List<ItemType> { ItemType.HeadArmor };
        BodySlot = new ItemSlot();
        BodySlot.ItemTypes = new List<ItemType> { ItemType.BodyArmor };
        HandsSlot = new ItemSlot();
        HandsSlot.ItemTypes = new List<ItemType> { ItemType.HandArmor };
        LegsSlot = new ItemSlot();
        LegsSlot.ItemTypes = new List<ItemType> { ItemType.LegArmor };
        FeetSlot = new ItemSlot();
        FeetSlot.ItemTypes = new List<ItemType> { ItemType.FeetArmor };
        LeftRingSlot = new ItemSlot();
        LeftRingSlot.ItemTypes = new List<ItemType> { ItemType.Ring };
        RightRingSlot = new ItemSlot();
        RightRingSlot.Name = "RightRing";
        RightRingSlot.ItemTypes = new List<ItemType> { ItemType.Ring };
        if (equipment.Weapon != new Item())
        {
            Weapon = new InventoryItem(equipment.Weapon, WeaponSlot);
            WeaponSlot.SetItem(Weapon);
        }
        if (equipment.Head != new Item())
        {
            Head = new InventoryItem(equipment.Head, HeadSlot);
            HeadSlot.SetItem(Head);
        }
        if (equipment.Body != new Item())
        {
            Body = new InventoryItem(equipment.Body, BodySlot);
            BodySlot.SetItem(Body);
        }
        if (equipment.Hands != new Item())
        {
            Hands = new InventoryItem(equipment.Hands, HandsSlot);
            HandsSlot.SetItem(Hands);
        }
        if (equipment.Legs != new Item())
        {
            Legs = new InventoryItem(equipment.Legs, LegsSlot);
            LegsSlot.SetItem(Legs);
        }
        if (equipment.Feet != new Item())
        {
            Feet = new InventoryItem(equipment.Feet, FeetSlot);
            FeetSlot.SetItem(Feet);
        }
        if (equipment.LeftRing != new Item())
        {
            LeftRing = new InventoryItem(equipment.LeftRing, LeftRingSlot);
            LeftRingSlot.SetItem(LeftRing);
        }
        if (equipment.RightRing != new Item())
        {
            RightRing = new InventoryItem(equipment.RightRing, RightRingSlot);
            RightRingSlot.SetItem(RightRing);
        }
        List<ItemSlot> items = new List<ItemSlot> { HeadSlot, BodySlot, WeaponSlot, LegsSlot, HandsSlot, FeetSlot, LeftRingSlot, RightRingSlot };
        foreach (ItemSlot slot in items)
            slot.Connect("gui_input", this, "_on_TextureRect_gui_input", new Godot.Collections.Array { slot });
        WeaponRect.AddChild(WeaponSlot);
        HeadRect.AddChild(HeadSlot);
        BodyRect.AddChild(BodySlot);
        HandsRect.AddChild(HandsSlot);
        LegsRect.AddChild(LegsSlot);
        FeetRect.AddChild(FeetSlot);
        LeftRingRect.AddChild(LeftRingSlot);
        RightRingRect.AddChild(RightRingSlot);
    }

    private void SetTooltips()
    {
        //WeaponRect.SetTooltip(equipmentList[0].Item.TooltipText);
        //HeadRect.SetTooltip(equipmentList[1].Item.TooltipText);
        //BodyRect.SetTooltip(equipmentList[2].Item.TooltipText);
        //HandsRect.SetTooltip(equipmentList[3].Item.TooltipText);
        //LegsRect.SetTooltip(equipmentList[4].Item.TooltipText);
        //FeetRect.SetTooltip(equipmentList[5].Item.TooltipText);
        //LeftRingRect.SetTooltip(equipmentList[6].Item.TooltipText);
        //RightRingRect.SetTooltip(equipmentList[7].Item.TooltipText);
    }

    #region Item Manipulation

    /// <summary>Equips the currently held <see cref="Item"/> and puts it in the correct slot.</summary>
    /// <param name="clickedSlot"><see cref="ItemSlot"/> that was clicked on</param>
    /// <param name="hand"><see cref="RingHand"/> to place a Ring on if <see cref="Item"/> is a Ring</param>
    private void EquipAndPut(ItemSlot clickedSlot, RingHand hand = RingHand.Left)
    {
        CurrentHero.Equip(holdingItem.Item, hand);
        PutItem(clickedSlot);
    }

    /// <summary>Equips the currently held <see cref="Item"/> and swaps out the previously equipped <see cref="Item"/>.</summary>
    /// <param name="clickedSlot"><see cref="ItemSlot"/> that was clicked on</param>
    /// <param name="hand"><see cref="RingHand"/> to place a Ring on if <see cref="Item"/> is a Ring</param>
    private void EquipAndSwap(ItemSlot clickedSlot, RingHand hand = RingHand.Left)
    {
        CurrentHero.Equip(holdingItem.Item, hand);
        SwapItems(clickedSlot);
    }

    /// <summary>Finds the first empty <see cref="ItemSlot"/> in the slotList.</summary>
    /// <returns>First empty <see cref="ItemSlot"/></returns>
    private ItemSlot FindFirstEmptySlot()
    {
        List<object> children = GridInventory.GetChildren().ToList();
        foreach (ItemSlot slot in children)
        {
            if (slot?.Item == null || slot.Item.Item == null || slot.Item.Item == new Item())
            {
                return slot;
            }
        }
        return null;
    }

    private void Thing(){
    }

    /// <summary>Put an <see cref="InventoryItem"/> into an <see cref="ItemSlot"/>.</summary>
    /// <param name="clickedSlot"><see cref="ItemSlot"/> that was clicked on</param>
    private void PutItem(ItemSlot clickedSlot)
    {
        clickedSlot.PutItem(holdingItem);
        holdingItem = null;
    }

    /// <summary>Swap <see cref="InventoryItem"/>s between <see cref="ItemSlot"/>s.</summary>
    /// <param name="clickedSlot"><see cref="ItemSlot"/> that was clicked on</param>
    private void SwapItems(ItemSlot clickedSlot)
    {
        InventoryItem tempItem = clickedSlot.Item;
        ItemSlot oldSlot = holdingItem.Slot;
        clickedSlot.PickItem();
        clickedSlot.PutItem(holdingItem);
        holdingItem = null;
        oldSlot.PutItem(tempItem);
    }

    /// <summary>Unequips the currently held <see cref="Item"/> and puts it in the Inventory.</summary>
    /// <param name="clickedSlot"><see cref="ItemSlot"/> that was clicked on</param>
    /// <param name="hand"><see cref="RingHand"/> to place a Ring on if <see cref="Item"/> is a Ring</param>
    private void UnEquipAndPut(ItemSlot clickedSlot, RingHand hand = RingHand.Left)
    {
        CurrentHero.Unequip(holdingItem.Item, hand);
        PutItem(clickedSlot);
    }

    #endregion Item Manipulation

    #region Input

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseMotion && holdingItem?.Picked == true)
            holdingItem.RectGlobalPosition = new Vector2(mouseMotion.Position.x, mouseMotion.Position.y);
    }

    public override void _GuiInput(InputEvent @event)
    {
    }

    #endregion Input

    //  // Called every frame. 'delt@ga' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //
    //  }
}