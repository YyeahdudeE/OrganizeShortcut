using Newtonsoft.Json;

using StardewModdingAPI;


    public class ModConfig
    {
        public ModConfigControls Controls { get; set; } = new ModConfigControls();

        public class ModConfigControls
        {
            public SButton[] Organize { get; set; } = { SButton.OemTilde };
            public SButton[] StackToChest { get; set; } = { SButton.E };
    }
    }

