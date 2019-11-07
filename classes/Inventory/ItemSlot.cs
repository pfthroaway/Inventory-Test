using Godot;
using Sulimn.Classes.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sulimn.Classes.Inventory
{
    /// <summary>Represents a slot for an <see cref="InventoryItem"/>.</summary>
    public class ItemSlot : TextureRect
    {
        #region Properties

        /// <summary><see cref="InventoryItem"/> in the <see cref="ItemSlot"/>.</summary>
        public InventoryItem Item { get; set; }

        /// <summary><see cref="ItemTypes"/> of <see cref="Item"/>s accepted in this <see cref="ItemSlot"/>.</summary>
        public List<ItemType> ItemTypes { get; set; } = new List<ItemType>(Enum.GetValues(typeof(ItemType)).Cast<ItemType>().ToList());

        #endregion Properties

        #region Item Manipulation

        /// <summary>Sets the <see cref="InventoryItem"/> into this instance's Item.</summary>
        /// <param name="newItem"></param>
        public void SetItem(InventoryItem newItem)
        {
            AddChild(newItem);
            Item = newItem;
            Item.Slot = this;
        }

        /// <summary>Picks up an <see cref="InventoryItem"/>.</summary>
        public void PickItem()
        {
            Item.PickItem();
            RemoveChild(Item);
            GetParent().GetParent().AddChild(Item);
            Item = null;
        }

        /// <summary>Puts an <see cref="InventoryItem"/> into an <see cref="ItemSlot"/>.</summary>
        /// <param name="newItem"><see cref="InventoryItem"/> to be put into the <see cref="ItemSlot"/></param>
        public void PutItem(InventoryItem newItem)
        {
            Item = newItem;
            Item.Slot = this;
            Item.PutItem();
            GetParent().GetParent().RemoveChild(Item);
            AddChild(Item);
        }

        #endregion Item Manipulation

        /// <summary>Initializes an instance of <see cref="ItemSlot"/> by assigning the slot index.</summary>
        public ItemSlot()
        {
            Texture = (Texture)ResourceLoader.Load("res://assets/inventory/images/SlotBG.png");
            MouseFilter = MouseFilterEnum.Pass;
            MouseDefaultCursorShape = CursorShape.PointingHand;
            Theme = (Theme)ResourceLoader.Load("res://TextureRect.tres");
        }
    }
}