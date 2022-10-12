using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;

namespace HeroRegression.Common.BaseClasses.BaseLoot
{
    public abstract class BaseBossBag : ModItem
    {
        /// <summary>
        /// This List contains itemLoot infos for common loots (for example, materials).
        /// The 4 parameters in the Vector4 represents: item ID, drop chance, minimum drop, maximum drop.
        /// </summary>
        public List<Vector4> CommonLoots = new();
        /// <summary>
        /// This int[] array contains item IDs that will be select by 1 among all of them as the loot.
        /// </summary>
        public int[] OneFromOptionsLoots = new int[0] { };
        /// <summary>
        /// This int[] array contains item IDs that will be select multiple ones among them as loots.
        /// </summary>
        public int[] MultipleFromMultipleLoots = new int[0] {};
        public void BagStatics(string bossName, bool preHardmode = true)
        {
            DisplayName.SetDefault(ChnTrans("Treasure Bag (" + bossName + ")", "宝藏袋（" + bossName + "）"));
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
            ItemID.Sets.BossBag[Type] = true;
            if (preHardmode)
            {
                ItemID.Sets.PreHardmodeLikeBossBag[Type] = true;
            }
            else
            {
                ItemID.Sets.BossBag[Type] = true;
            }
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        }
        public void BagDefaults(int width = 32, int height = 32, int rarity = ItemRarityID.Blue)
        {
            Item.maxStack = 999;
            Item.consumable = true;
            Item.width = width;
            Item.height = height;
            Item.rare = rarity;
            Item.expert = true;
        }
        public override bool CanRightClick()
        {
            return true;
        }
        /// <summary>
        /// For deciding the loot of this treasure bag.
        /// </summary>
        /// <param name="itemLoot">the ItemLoot passed in by ModifyItemLoot.</param>
        /// <param name="npcType">the type of NPC this bag refers to.</param>
        /// <param name="expertAcc">the type of expert-exclusive accessory.</param>
        /// <param name="maskID">the type of boss mask.</param>
        /// <param name="optionsLootChance"></param>
        /// <param name="numFromMultipleLoots">how many loots will be selected from the multiple loots array.</param>
        public void BagLoot(ItemLoot itemLoot, int npcType, int expertAcc = -1, int maskID = -1, int optionsLootChance = 1, int numFromMultipleLoots=-1)
        {
            itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(npcType));
            if (expertAcc != -1) itemLoot.Add(ItemDropRule.Common(expertAcc));
            if (maskID != -1) itemLoot.Add(ItemDropRule.NotScalingWithLuck(maskID, 7, 1, 1));
            if (OneFromOptionsLoots.Length >= 1) itemLoot.Add(ItemDropRule.OneFromOptions(optionsLootChance, OneFromOptionsLoots));
            if (CommonLoots.Count >= 1)
            {
                for (int i = 0; i < CommonLoots.Count; i++)
                {
                    Vector4 lootInfo = CommonLoots[i];
                    itemLoot.Add(ItemDropRule.Common((int)lootInfo.X, (int)lootInfo.Y, (int)lootInfo.Z, (int)lootInfo.W));
                }
            }
            if (MultipleFromMultipleLoots.Length > 0)
            {
                if (numFromMultipleLoots > 0)
                {
                    itemLoot.Add(ItemDropRule.FewFromOptions(numFromMultipleLoots, 1, MultipleFromMultipleLoots));
                }
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color?(Color.Lerp(lightColor, Color.White, 0.4f));
        }
        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.White.ToVector3() * 0.4f);
            bool flag = Item.timeSinceItemSpawned % 12 == 0;
            if (flag)
            {
                Vector2 center = Item.Center + new Vector2(0f, Item.height * -0.1f);
                Vector2 direction = Utils.NextVector2CircularEdge(Main.rand, Item.width * 0.6f, Item.height * 0.6f);
                float distance = 0.3f + Utils.NextFloat(Main.rand) * 0.5f;
                Vector2 velocity = new Vector2(0f, -Utils.NextFloat(Main.rand) * 0.3f - 1.5f);
                Dust dust = Dust.NewDustPerfect(center + direction * distance, 279, new Vector2?(velocity), 0, default(Color), 1f);
                dust.scale = 0.5f;
                dust.fadeIn = 1.1f;
                dust.noGravity = true;
                dust.noLight = true;
                dust.alpha = 0;
            }
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = TextureAssets.Item[Item.type].Value;
            bool flag = Main.itemAnimations[Item.type] != null;
            Rectangle frame;
            if (flag)
            {
                frame = Main.itemAnimations[Item.type].GetFrame(texture, Main.itemFrameCounter[whoAmI]);
            }
            else
            {
                frame = Utils.Frame(texture, 1, 1, 0, 0, 0, 0);
            }
            Vector2 frameOrigin = Utils.Size(frame) / 2f;
            Vector2 offset;
            offset = new Vector2((Item.width / 2) - frameOrigin.X, (Item.height - frame.Height));
            Vector2 drawPos = Item.position - Main.screenPosition + frameOrigin + offset;
            float time = Main.GlobalTimeWrappedHourly;
            float timer = Item.timeSinceItemSpawned / 240f + time * 0.04f;
            time %= 4f;
            time /= 2f;
            bool flag2 = time >= 1f;
            if (flag2)
            {
                time = 2f - time;
            }
            time = time * 0.5f + 0.5f;
            for (float i = 0f; i < 1f; i += 0.25f)
            {
                float radians = (i + timer) * 6.2831855f;
                spriteBatch.Draw(texture, drawPos + Utils.RotatedBy(new Vector2(0f, 8f), (double)radians, default(Vector2)) * time, new Rectangle?(frame), new Color(90, 70, 255, 50), rotation, frameOrigin, scale, 0, 0f);
            }
            for (float j = 0f; j < 1f; j += 0.34f)
            {
                float radians2 = (j + timer) * 6.2831855f;
                spriteBatch.Draw(texture, drawPos + Utils.RotatedBy(new Vector2(0f, 4f), (double)radians2, default(Vector2)) * time, new Rectangle?(frame), new Color(140, 120, 255, 77), rotation, frameOrigin, scale, 0, 0f);
            }
            return true;
        }
    }
}
