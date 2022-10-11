using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;
using HeroRegression.Items;
using static Terraria.ModLoader.ModContent;

namespace HeroRegression.Tiles.矿物
{
    public class 古木物块 : ModTile
    {

        public override void SetStaticDefaults()
        {
            Main.tileSpelunker[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = false;  //true for block to emit light
            Main.tileLighted[Type] = false;
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("古木");
            AddMapEntry(new Color(35, 17, 17), name);
            HitSound = SoundID.Tink;
            ItemDrop = ItemType<Items.Placeable.Block.古木>();
            MinPick = 70;

        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r = 0;
            g = 0.06f;
            b = 0;
        }
    }
}