using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace HeroRegression.Common.Systems
{
    public class RecipeSystem:ModSystem
    {
        public static RecipeGroup AnyCopperBar;
        public static RecipeGroup AnySilverBar;
        public static RecipeGroup AnyIronBar;
        public static RecipeGroup AnyGoldBar;
        public static RecipeGroup AnyEvilBar;
        public static RecipeGroup AnyCobaltBar;
        public static RecipeGroup AnyMythrilBar;
        public static RecipeGroup AnyAdamantiteBar;
        public static RecipeGroup AnyVertebra;
        public static RecipeGroup AnyViciousMushroom;
        public static RecipeGroup AnyEvilBossMaterial;
        public static RecipeGroup AnyBloodMoonLoot;
        public static RecipeGroup AnyBird;
        public override void Unload()
        {
            AnyCopperBar = null;
            AnySilverBar = null;
            AnyIronBar = null;
            AnyGoldBar = null;
            AnyEvilBar = null;
            AnyCobaltBar = null;
            AnyMythrilBar = null;
            AnyAdamantiteBar = null;
            AnyVertebra = null;
            AnyViciousMushroom = null;
            AnyEvilBossMaterial = null;
            AnyBloodMoonLoot = null;
            AnyBird = null;
        }
        public override void AddRecipeGroups()
        {
            AnyCopperBar = new RecipeGroup(() =>
            ChnTrans("Any Copper Bar", "任意铜锭"),
            new int[]
            {
                ItemID.CopperBar,
                ItemID.TinBar
            }
            );
            RecipeGroup.RegisterGroup("HeroRegression:AnyCopperBar", AnyCopperBar);
            AnySilverBar = new RecipeGroup(() =>
            ChnTrans("Any Silver Bar", "任意银锭"),
            new int[]
            {
                ItemID.SilverBar,
                ItemID.TungstenBar
            }
            );
            RecipeGroup.RegisterGroup("HeroRegression:AnySilverBar", AnySilverBar);
            AnyIronBar = new RecipeGroup(() =>
            ChnTrans("Any Iron Bar", "任意铁锭"),
            new int[]
            {
                ItemID.IronBar,
                ItemID.LeadBar
            }
            );
            RecipeGroup.RegisterGroup("HeroRegression:AnyIronBar", AnyIronBar);
            AnyGoldBar = new RecipeGroup(() =>
            ChnTrans("Any Gold Bar", "任意金锭"),
            new int[]
            {
                ItemID.GoldBar,
                ItemID.PlatinumBar
            }
            );
            RecipeGroup.RegisterGroup("HeroRegression:AnyGoldBar", AnyGoldBar);
            AnyEvilBar = new RecipeGroup(() =>
            ChnTrans("Any Evil Bar", "任意邪恶锭"),
            new int[]
            {
                ItemID.CrimtaneBar,
                ItemID.DemoniteBar
            }
            );
            RecipeGroup.RegisterGroup("HeroRegression:AnyEvilBar", AnyEvilBar);
            AnyCobaltBar = new RecipeGroup(() =>
            ChnTrans("Any Cobalt Bar", "任意钴锭"),
            new int[]
            {
                ItemID.CobaltBar,
                ItemID.PalladiumBar
            }
            );
            RecipeGroup.RegisterGroup("HeroRegression:AnyCobaltBar", AnyCobaltBar);
            AnyMythrilBar = new RecipeGroup(() =>
            ChnTrans("Any Mythril Bar", "任意秘银锭"),
            new int[]
            {
                ItemID.MythrilBar,
                ItemID.OrichalcumBar
            }
            );
            RecipeGroup.RegisterGroup("HeroRegression:AnyMythrilBar", AnyMythrilBar);
            AnyAdamantiteBar = new RecipeGroup(() =>
            ChnTrans("Any Adamantite Bar", "任意精金锭"),
            new int[]
            {
                ItemID.AdamantiteBar,
                ItemID.TitaniumBar
            }
            );
            RecipeGroup.RegisterGroup("HeroRegression:AnyAdamantiteBar", AnyAdamantiteBar);
            AnyVertebra = new RecipeGroup(() =>
            ChnTrans("Any Vertebra", "任意椎骨"),
            new int[]
            {
                ItemID.Vertebrae,
                ItemID.RottenChunk
            }
            );
            RecipeGroup.RegisterGroup("HeroRegression:AnyVertebra", AnyVertebra);
            AnyViciousMushroom = new RecipeGroup(() =>
            ChnTrans("Any Vicious Mushroom", "任意毒蘑菇"),
            new int[]
            {
                ItemID.ViciousMushroom,
                ItemID.VileMushroom
            }
            );
            RecipeGroup.RegisterGroup("HeroRegression:AnyViciousMushroom", AnyViciousMushroom);
            AnyEvilBossMaterial = new RecipeGroup(() =>
            ChnTrans("Any Evil Boss Material", "任意邪恶boss材料"),
            new int[]
            {
                ItemID.ShadowScale,
                ItemID.TissueSample
            }
            );
            RecipeGroup.RegisterGroup("HeroRegression:AnyEvilBossMaterial", AnyEvilBossMaterial);
            AnyBloodMoonLoot = new RecipeGroup(() =>
            ChnTrans("Any Early-Game Blood Moon Loot", "任意早期血月掉落物"),
            new int[]
            {
                ItemID.MoneyTrough,
                ItemID.SharkToothNecklace,
                ItemID.BloodRainBow,
                ItemID.VampireFrogStaff,
                4325
            }
            );
            RecipeGroup.RegisterGroup("HeroRegression:AnyBloodMoonLoot", AnyBloodMoonLoot);
            AnyBird = new RecipeGroup(() =>
            ChnTrans("Any Bird", "任意鸟"),
            new int[]
            {
            ItemID.Bird,
            ItemID.GoldBird,
            ItemID.BlueJay,
            ItemID.Cardinal,
            ItemID.Duck,
            ItemID.MallardDuck,
            ItemID.Penguin,
            ItemID.Grebe,
            ItemID.Seagull
            }
            );
            RecipeGroup.RegisterGroup("HeroRegression:AnyBird", AnyBird);

        }
    }
}
