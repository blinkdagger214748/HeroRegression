using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using HeroRegression.NPCs;
using HeroRegression.NPCs.Boss.PupilOfHell;

namespace HeroRegression.Items
{
    class PupilSummon : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.PocketMirror;
        public override void SetStaticDefaults()
        {
            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
            ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.knockBack = 0f;
            Item.width = 30;
            Item.height = 30;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.buyPrice(0, 0, 0, 0);
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item44;
            Item.noMelee = true;
            Item.autoReuse = false;
            Item.consumable = false;
            Item.stack = 1;
        }
        public override bool CanUseItem(Player player)
        {
            return (!NPC.AnyNPCs(ModContent.NPCType<PupilOfHell>()));
        }
        public override bool? UseItem(Player player)
        {
            NPC.SpawnOnPlayer(Main.myPlayer, ModContent.NPCType<PupilOfHell>());
            return true;
        }
    }
}
