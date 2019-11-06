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
    private List<ItemSlot> slotList = new List<ItemSlot>();
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
        Item helmet = new Item("Helmet", "", ItemType.HeadArmor, 0, 10, 10, 100, 10, 10, 0, 0, 0, 0, 0, 0, false, 1, true, true, new List<HeroClass>(), "res://assets/inventory/images/C_Elm03.png");
        Item body1 = new Item("Body Armour", "", ItemType.BodyArmor, 0, 10, 10, 100, 10, 10, 0, 0, 0, 0, 0, 0, false, 1, true, true, new List<HeroClass>(), "res://assets/inventory/images/A_Armor05.png");
        Item body2 = new Item("Plate Armour", "", ItemType.BodyArmor, 0, 20, 10, 200, 10, 10, 0, 0, 0, 0, 0, 0, false, 1, true, true, new List<HeroClass>(), "res://assets/inventory/images/A_Armour02.png");
        Item shoes = new Item("Shoes", "", ItemType.FeetArmor, 0, 5, 10, 50, 10, 10, 0, 0, 0, 0, 0, 0, false, 1, true, true, new List<HeroClass>(), "res://assets/inventory/images/A_Shoes03.png");
        CurrentHero.Equipment.Head = helmet;
        CurrentHero.Equipment.Body = body1;

        CurrentHero.AddItem(shoes);
        CurrentHero.AddItem(body2);
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
            Vector2 clickedPosition = GetGlobalMousePosition();
            //if ()
        }
    }

    public void SetUpInventory(List<Item> inventory)
    {
        foreach (Item item in inventory)
            inventoryList.Add(new InventoryItem(item, null));

        for (int i = 0; i < 40; i++)
        {
            ItemSlot slot = new ItemSlot(i);
            slot.Connect("gui_input", slot, "_on_Weapon2_gui_input", new Godot.Collections.Array { slot });
            slotList.Add(slot);
            GridInventory.AddChild(slot);
        }

        if (inventoryList.Count <= 40)
        {
            for (int i = 0; i < inventoryList.Count; i++)
            {
                slotList[i].SetItem(inventoryList[i]);
                slotList[i].Name = inventoryList[i].Item.Name;
            }
        }
        else
        {
            for (int i = 0; i < 40; i++)
                slotList[i].SetItem(inventoryList[i]);
        }
    }

    public void SetUpEquipment(Equipment equipment)
    {
        WeaponSlot = new ItemSlot(40);
        WeaponSlot.ItemTypes = new List<ItemType> { ItemType.MeleeWeapon, ItemType.RangedWeapon };
        HeadSlot = new ItemSlot(41);
        HeadSlot.ItemTypes = new List<ItemType> { ItemType.HeadArmor };
        BodySlot = new ItemSlot(42);
        BodySlot.ItemTypes = new List<ItemType> { ItemType.BodyArmor };
        HandsSlot = new ItemSlot(43);
        HandsSlot.ItemTypes = new List<ItemType> { ItemType.HandArmor };
        LegsSlot = new ItemSlot(44);
        LegsSlot.ItemTypes = new List<ItemType> { ItemType.LegArmor };
        FeetSlot = new ItemSlot(45);
        FeetSlot.ItemTypes = new List<ItemType> { ItemType.FeetArmor };
        LeftRingSlot = new ItemSlot(46);
        LeftRingSlot.ItemTypes = new List<ItemType> { ItemType.Ring };
        RightRingSlot = new ItemSlot(47);
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

        slotList.Add(WeaponSlot);
        slotList.Add(HeadSlot);
        slotList.Add(BodySlot);
        slotList.Add(HandsSlot);
        slotList.Add(LegsSlot);
        slotList.Add(FeetSlot);
        slotList.Add(LeftRingSlot);
        slotList.Add(RightRingSlot);

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
    private ItemSlot FindFirstEmptySlot() => slotList.First(slot => slot.Item == null);

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
        ItemSlot oldSlot = slotList[holdingItem.Slot.SlotIndex];
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
        if (clickedSlot.SlotIndex <= 40)
        {
            CurrentHero.Unequip(holdingItem.Item, hand);
            PutItem(clickedSlot);
        }
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
        if (@event is InputEventMouseButton button && button.ButtonIndex == 1 && button.Pressed)
        {
            ItemSlot clickedSlot = null;
            // Check if the slot being clicked is in the slotList
            foreach (ItemSlot slot in slotList)
            {
                Vector2 slotMousePos = slot.GetLocalMousePosition();
                Texture slotTexture = slot.Texture;
                bool isClicked = slotMousePos.x >= 0 && slotMousePos.x <= slotTexture.GetWidth() && slotMousePos.y >= 0 && slotMousePos.y <= slotTexture.GetHeight();
                if (isClicked)
                {
                    clickedSlot = slot;
                    break;
                }
            }

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
                            if (clickedSlot.SlotIndex != 47)
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
                        UnEquipAndPut(FindFirstEmptySlot());
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
                            if (clickedSlot.SlotIndex != 47)
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
                holdingItem.RectGlobalPosition = new Vector2(button.Position.x, button.Position.y);
            }
        }
    }

    #endregion Input

    private void _on_Weapon2_gui_input(InputEvent @event, ItemSlot slot)
    {
        if (@event is InputEventMouseButton button && button.ButtonIndex == 1 && button.Pressed)
        {
            if (slot == null)
                GD.Print("Null slot");
            else
                GD.Print(slot.Item.Item.Name);
            GD.Print(@event.AsText());
        }
    }

    //  // Called every frame. 'delt@ga' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //
    //  }
}