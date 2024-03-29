﻿using System;
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

                    if (menu.organizeButton != null)
                    {
                        menu.FillOutStacks();
                        if (this.Config.Controls.OrganizeAfterStackToChest)
                        {
                            Organize();
                        }
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
                Chest chest = menu.context as Chest;
                
                if (chest != null && menu.organizeButton != null)
                {                   
                    StardewValley.Menus.ItemGrabMenu.organizeItemsInList(chest.items);
                }
            }
        }
    }
}