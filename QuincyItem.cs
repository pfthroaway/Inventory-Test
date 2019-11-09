using Godot;
using Sulimn.Classes.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTest
{
    public class QuincyItem : Control
    {
        public bool Drag;
        private Control orphanage;
        private Item _item = new Item();

        public Item Item
        {
            get => _item;
            set => SetItem(value);
        }

        public override void _Ready()
        {
            orphanage = (Control)GetTree().CurrentScene.FindNode("Orphanage");
            MouseDefaultCursorShape = CursorShape.PointingHand;
        }

        private void _on_TextureRect_gui_input(InputEvent @event)
        {
            if (@event is InputEventMouseButton button && button.Pressed && button.ButtonIndex == 1)
            {
                GD.Print("Clicked Item");
                if (!Drag)
                {
                    if (orphanage.GetChildCount() > 0)
                    {
                        QuincySlot slot = (QuincySlot)GetParent();
                        QuincyItem orphanItem = (QuincyItem)orphanage.GetChild(0);
                        if (slot.ItemTypes.Contains(orphanItem.Item.Type))
                        {
                            orphanItem.MouseFilter = MouseFilterEnum.Ignore;
                            orphanItem.Drag = false;
                            orphanItem.RectGlobalPosition = Vector2.Zero;
                            orphanage.RemoveChild(orphanItem);
                            slot.AddChild(orphanItem);
                            slot.RemoveChild(this);
                            orphanage.AddChild(this);

                            Drag = true;
                        }
                    }
                    else if (orphanage.GetChildCount() == 0)
                    {
                        Drag = true;
                        MouseFilter = MouseFilterEnum.Ignore;
                        GetParent().RemoveChild(this);
                        orphanage.AddChild(this);
                    }
                }
            }
        }

        public override void _Process(float delta)
        {
            if (Drag)
                RectGlobalPosition = GetViewport().GetMousePosition() + new Vector2(1, 1);
        }

        public void SetItem(Item item)
        {
            if (item != null && item != new Item())
            {
                _item = item;
                TextureRect rect = (TextureRect)GetNode("TextureRect");
                SetTooltip(item.TooltipText);
                GD.Print(item.Name);
                GD.Print(GetChildCount());
                if (GetParent() != null)
                {
                    GD.Print(GetParent().Name);
                    GD.Print(GetParent().GetChildCount());
                }
                rect.Texture = (Texture)ResourceLoader.Load(item.Texture);
            }
        }

        public QuincyItem()
        {
        }

        public QuincyItem(Item item) => SetItem(item);
    }
}