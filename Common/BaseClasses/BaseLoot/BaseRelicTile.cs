using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroRegression.Common.BaseClasses.BaseLoot
{
    public abstract class BaseRelicTile : ModTile
    {
        public abstract string RelicTextureName { get; }
        public abstract int AssociatedItem { get; }
        public override string Texture
        {
            get
            {
                return "HeroRegression/Tiles/RelicBase";
            }
        }
        public override void Load()
        {
            if (!Main.dedServ)
            {
                RelicTexture = ModContent.Request<Texture2D>(RelicTextureName, (AssetRequestMode)2);
            }
        }
        public override void Unload()
        {
            RelicTexture = null;
        }
        public override void SetStaticDefaults()
        {
            Main.tileShine[Type] = 400;
            Main.tileFrameImportant[Type] = true;
            TileID.Sets.InteractibleByNPCs[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.newTile.Direction = (Terraria.Enums.TileObjectDirection)1;
            TileObjectData.newTile.StyleHorizontal = false;
            TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
            TileObjectData.newAlternate.Direction = (Terraria.Enums.TileObjectDirection)2;
            TileObjectData.addAlternate(1);
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(233, 207, 94), Language.GetText("MapObject.Relic"));
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j, null), i * 16, j * 16, 32, 32, AssociatedItem, 1, false, 0, false, false);
        }
        public override bool CreateDust(int i, int j, ref int type)
        {
            return false;
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
        {
            if (drawData.tileFrameX % 54 == 0 && drawData.tileFrameY % 72 == 0)
            {
                Main.instance.TilesRenderer.AddSpecialLegacyPoint(i, j);
            }
        }
        public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Vector2 offScreen;
            offScreen = new Vector2((float)Main.offScreenRange);
            if (Main.drawToScreen)
            {
                offScreen = Vector2.Zero;
            }
            Point p = new(i, j);
            Tile tile = Main.tile[p.X, p.Y];
            if (tile == null || !tile.HasTile)
            {
                return;
            }
            Texture2D texture = RelicTexture.Value;
            int frameY = (tile.TileFrameX / 54);
            Rectangle frame = Utils.Frame(texture, 1, 1, 0, frameY, 0, 0);
            Vector2 origin = Utils.Size(frame) / 2f;
            Vector2 vector = Utils.ToWorldCoordinates(p, 24f, 64f);
            Color color = Lighting.GetColor(p.X, p.Y);
            SpriteEffects effects = (tile.TileFrameY / 72 != 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            float offset = (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * 6.2831855f / 5f));
            Vector2 drawPos = vector + offScreen - Main.screenPosition + new Vector2(0f, -40f) + new Vector2(0f, offset * 4f);
            spriteBatch.Draw(texture, drawPos, new Rectangle?(frame), color, 0f, origin, 1f, effects, 0f);
            float scale = (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * 6.2831855f / 2f)) * 0.3f + 0.7f;
            Color effectColor = color;
            effectColor.A = 0;
            effectColor = effectColor * 0.1f * scale;
            for (float num5 = 0f; num5 < 1f; num5 += 0.16666667f)
            {
                spriteBatch.Draw(texture, drawPos + Utils.ToRotationVector2(6.2831855f * num5) * (6f + offset * 2f), new Rectangle?(frame), effectColor, 0f, origin, 1f, effects, 0f);
            }
        }
        public const int FrameWidth = 54;
        public const int FrameHeight = 72;
        public const int HorizontalFrames = 1;
        public const int VerticalFrames = 1;
        public Asset<Texture2D> RelicTexture;
    }
}
