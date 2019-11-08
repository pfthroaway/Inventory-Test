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

        public Item Item { get; set; } = new Item();

        public override void _Ready()
        {
            orphanage = (Control)GetTree().CurrentScene.FindNode("Orphanage");
        }

        private void _on_TextureRect_gui_input(InputEvent @event)
        {
            if (@event is InputEventMouseButton button && button.Pressed && button.ButtonIndex == 1)
            {
                if (!Drag)
                {
                    if (orphanage.GetChildCount() > 0)
                    {
                        QuincyItem orphanItem = (QuincyItem)orphanage.GetChild(0);
                        orphanItem.MouseFilter = MouseFilterEnum.Pass;
                        orphanItem.Drag = false;
                        orphanItem.RectGlobalPosition = Vector2.Zero;
                        orphanage.RemoveChild(orphanItem);
                        GetParent().AddChild(orphanItem);
                        GetParent().RemoveChild(this);
                        orphanage.AddChild(this);
                        Drag = true;
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
                Item = item;
                TextureRect rect = (TextureRect)FindNode("TextureRect");
                rect.Texture = (Texture)ResourceLoader.Load(Item.Texture);
            }
        }

        public QuincyItem()
        {
        }

        public QuincyItem(Item item) => SetItem(item);
    }
}