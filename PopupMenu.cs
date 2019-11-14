using Godot;
using Sulimn.Classes;
using Sulimn.Classes.Items;
using System;

namespace InventoryTest
{
    public class PopupMenu : Godot.PopupMenu
    {
        public QuincySlot CurrentSlot { get; set; }
        private Button BtnConsume, BtnDrop;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            BtnConsume = (Button)GetNode("BtnConsume");
            BtnDrop = (Button)GetNode("BtnDrop");
        }

        public void LoadSlot(QuincySlot slot)
        {
            CurrentSlot = slot;
            GD.Print(CurrentSlot.Item.Item.Type);
            switch (slot.Item.Item.Type)
            {
                case ItemType.Food:
                case ItemType.Drink:
                case ItemType.Potion:
                    BtnConsume.Disabled = false;
                    break;
            }
        }

        private void Drop()
        {
            CurrentSlot.RemoveChild(CurrentSlot.Item);
            CurrentSlot.Item = null;
        }

        private void _on_BtnConsume_pressed()
        {
            GameState.CurrentHero.ConsumeItem(CurrentSlot.Item.Item);
            Drop();
        }

        private void _on_BtnDrop_pressed() => Drop();
    }
}