using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace HeroRegression.Common.BaseClasses.BaseSummon
{
    /// <summary>
    /// The class is for quickly generating summon items for bosses.
    /// </summary>
    public abstract class SummonItem : ModItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Eng">The English name of this item.</param>
        /// <param name="Chn">The Chinese name of this item.</param>
        public void ItemName(string Eng, string Chn)
        {
            DisplayName.SetDefault(Eng);
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, Chn);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Eng">The English name of this item.</param>
        /// <param name="Chn">The Chinese name of this item.</param>
        public void ItemTooltip(string Eng, string Chn)
        {
            Tooltip.SetDefault(Eng);
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, Chn);
        }
        /// <summary>
        /// Set default stats for this item.
        /// </summary>
        /// <param name="width">the width of this item</param>
        /// <param name="height">the height of this item</param>
        /// <param name="useTime">the useTime & useAnimation of this item</param>
        /// <param name="value">the sellPrice of this item</param>
        /// <param name="rare">the rarity of this item</param>
        /// <param name="consumable">whether or not the item is consumed when used</param>
        /// <param name="maxStack">the item's max stack</param>
        /// <param name="scale">the item's scale upon use</param>
        public void Defaults(int width, int height, int useTime, int value, int rare, bool consumable = false, int maxStack = 1, float scale = 1f)
        {
            Item.knockBack = 0f;
            Item.width = width;
            Item.height = height;
            Item.useTime = useTime;
            Item.useAnimation = useTime;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.value = value;
            Item.rare = rare;
            Item.noMelee = true;
            Item.autoReuse = false;
            Item.consumable = consumable;
            Item.maxStack = maxStack;
            Item.scale = scale;
        }
        /// <summary>
        /// The function for spawning boss with the specified type and position offset.
        /// </summary>
        /// <param name="player">the player instance who uses this item</param>
        /// <param name="sound">the sound played upon use</param>
        /// <param name="type">the type of boss</param>
        /// <param name="xOffset">the X offset of the boss' initial place to the player</param>
        /// <param name="yOffset">the Y offset of the boss' initial place to the player</param>
        public void SpawnBoss(Player player, SoundStyle sound, int type, int xOffset, int yOffset)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                SoundEngine.PlaySound(sound, player.Center);
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    NPC.SpawnBoss((int)player.Center.X + xOffset, (int)player.Center.Y + yOffset, type, player.whoAmI);
                }
                else
                {
                    ModPacket Boss = Mod.GetPacket();
                    Boss.Write((byte)MsgTypeSystem.SummonBoss);
                    Boss.Write(player.whoAmI);
                    Boss.Write(type);
                    Boss.WriteVector2(new Vector2(xOffset, yOffset));
                    Boss.Send();
                }
            }
        }
    }
}
