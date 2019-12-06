using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using System.Linq;
using StardewValley.Menus;
using StardewValley.Objects;



namespace OrganizeShortcut
{
    public class ModEntry : Mod
    {

        private ModConfig Config;

        public override void Entry(IModHelper helper)
        {
            this.Config = helper.ReadConfig<ModConfig>();
            Helper.Events.Input.ButtonPressed += Input_ButtonPressed;
           
        }

        private void Input_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (Context.IsWorldReady)
            {
                if (this.Config.Controls.Organize.Contains(e.Button))
                {
                    Organize();
                }

                if (this.Config.Controls.StackToChest.Contains(e.Button) && Game1.activeClickableMenu is ItemGrabMenu)
                {
                    ItemGrabMenu menu = (ItemGrabMenu)Game1.activeClickableMenu;
                    
                    if (menu.chestColorPicker != null)
                    {
                        menu.FillOutStacks();
                        Organize();
                    }
                }
            }
        }

        private void Organize()
        {
            StardewValley.Menus.ItemGrabMenu.organizeItemsInList(Game1.player.Items);
            Game1.playSound("Ship");

            if (Game1.activeClickableMenu is ItemGrabMenu)
            {
                ItemGrabMenu menu = (ItemGrabMenu)Game1.activeClickableMenu;

                if (menu.chestColorPicker != null)
                {
                    Chest chest = menu.behaviorOnItemGrab?.Target as Chest;
                    StardewValley.Menus.ItemGrabMenu.organizeItemsInList(chest.items);
                }
            }
        }
    }
}