using Godot;
using Sulimn.Classes.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryTest
{
    public class QuincySlot : Control
    {
        private Control orphanage;

        public List<ItemType> ItemTypes { get; set; } = new List<ItemType>(Enum.GetValues(typeof(ItemType)).Cast<ItemType>().ToList());
        public QuincyItem Item { get; set; } = new QuincyItem();

        public override void _Ready()
        {
            orphanage = (Control)GetTree().CurrentScene.FindNode("Orphanage");
        }

        private void _on_TextureRect_gui_input(InputEvent @event)
        {
            if (@event is InputEventMouseButton button && button.Pressed && button.ButtonIndex == 1)
            {
                GD.Print("Clicked!");
                if (orphanage.GetChildCount() > 0)
                {
                    QuincyItem item = (QuincyItem)orphanage.GetChild(0);
                    if (ItemTypes.Contains(item.Item.Type))
                    {
                        item.MouseFilter = MouseFilterEnum.Pass;
                        item.Drag = false;
                        item.RectGlobalPosition = Vector2.Zero;
                        orphanage.RemoveChild(item);
                        AddChild(item);
                    }
                }
            }
        }

        public QuincySlot()
        {
        }

        //public QuincySlot(QuincyItem item)
        //{
        //    Item = item;
        //}

        //public QuincySlot(QuincyItem item, List<ItemType> types)
        //{
        //    Item = item;
        //    ItemTypes = types;
        //}
    }
}