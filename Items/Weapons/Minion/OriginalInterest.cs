using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;
using System.IO;

namespace HeroRegression.Items.Weapons.Minion
{
    public class OriginalInterestBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Original Fairy");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "原初精灵");
            Description.SetDefault("The original fairy is following you");
            Description.AddTranslation((int)GameCulture.CultureName.Chinese, "原初精灵帮助着你");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<OriginalInterestGenie>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }

    public class OriginalInterestItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Original Breath");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "原息");
            Tooltip.SetDefault("Summons the original fairy that releases beams towards its target");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "召唤发射粒子束的原初精灵");
            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
            ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.knockBack = 3f;
            Item.mana = 10;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.buyPrice(0, 30, 0, 0);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item44;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Summon;
            Item.buffType = ModContent.BuffType<OriginalInterestBuff>();
            Item.shoot = ModContent.ProjectileType<OriginalInterestGenie>();
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 2);
            return true;
        }
    }

    public class OriginalInterestGenie : BaseMinion
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Original Fairy");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "原初精灵");
            Main.projFrames[Projectile.type] = 4;
            MinionFeatures(Type);
        }

        public sealed override void SetDefaults()
        {
            Defaults(30, 32, 3, -1, false, 0, 0, 1, 1, false);
            MinionDefaults(1);
            Projectile.DamageType = DamageClass.Summon;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public enum AIStates
        {
            Passive,
            Attack
        };
        public int TargetEnm
        {
            get => (int)Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }
        public AIStates States
        {
            get => (AIStates)Projectile.ai[0];
            set => Projectile.ai[0] = (int)value;
        }
        public void SwitchTo(AIStates state)
        {
            States = state;
        }
        public Vector2 OwnerRelativePos;
        public float StateTimer;
        public float FollowRadius;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(StateTimer);
            writer.WriteVector2(OwnerRelativePos);
            writer.Write(FollowRadius);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            StateTimer = reader.ReadSingle();
            OwnerRelativePos = reader.ReadVector2();
            FollowRadius = reader.ReadSingle();
        }
        public override bool PreAI()
        {
            Player owner = Main.player[Projectile.owner];
            SearchTargets(owner, out int targetIndex, 500f, 1.3f, false);
            TargetEnm = targetIndex;
            if (TargetEnm == -1)
            {
                if (States == AIStates.Attack)
                {
                    StateTimer = 0;
                    SwitchTo(AIStates.Passive);
                }
            }
            else
            {
                if (States == AIStates.Passive)
                {
                    StateTimer = 0;
                    SwitchTo(AIStates.Attack);
                }
            }
            return true;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.Lerp(lightColor, Color.White, .33f);
        }
        public override void AI()
        {
            OwnerCheckMinions(ModContent.BuffType<OriginalInterestBuff>(), 3);
            MinionSort();
            Player owner = Main.player[Projectile.owner];
            switch (States)
            {
                case AIStates.Passive:
                    {
                        StateTimer++;
                        Projectile.direction = Projectile.velocity.X >= 0 ? 1 : -1;
                        Projectile.spriteDirection = Projectile.direction;
                        Projectile.rotation = 0;
                        if (StateTimer % 40 == 2)
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                OwnerRelativePos = Main.rand.NextVector2FromRectangle(new Rectangle(40 * MinionOrderNum + 20, -60, 40 * MinionOrderNum + 60, -20));
                            }
                        }
                        if (StateTimer > 10)
                        {
                            Vector2 destPos = owner.Center + OwnerRelativePos * new Vector2(-owner.direction, 1);
                            Projectile.velocity = ExtensionVec2.RestrictedVec2(Vector2.Lerp(Projectile.velocity, (destPos - Projectile.Center) / 60f, .05f), 7.5f, .25f);
                        }
                        break;
                    }
                case AIStates.Attack:
                    {
                        StateTimer++;
                        FollowRadius = 125f + 15f * (float)Math.Sin(StateTimer / 180f * MathHelper.Pi);
                        NPC target = Main.npc[TargetEnm];
                        Projectile.direction = Projectile.Center.X >= target.Center.X ? -1 : 1;
                        Projectile.spriteDirection = Projectile.direction;
                        Projectile.rotation = Projectile.direction > 0 ?
                            (target.Center - Projectile.Center).ToRotation() :
                            (target.Center - Projectile.Center).ToRotation() - MathHelper.Pi;
                        int numMinions = owner.ownedProjectileCounts[Type];
                        Vector2 destPos = target.Center + ((float)MinionOrderNum / numMinions * MathHelper.TwoPi).ToRotationVector2() * FollowRadius;
                        Projectile.velocity = ExtensionVec2.RestrictedVec2(Vector2.Lerp(Projectile.velocity, (destPos - Projectile.Center) / 30f, .5f), 24f);
                        if (Vector2.Distance(Projectile.Center, target.Center) <= 250f)
                        {
                            if (StateTimer % 60 == 0)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient) Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, ExtensionVec2.SNormalize(target.Center - Projectile.Center), ModContent.ProjectileType<OriginalBeam>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                                Projectile.velocity -= ExtensionVec2.SNormalize(target.Center - Projectile.Center) * 10f;
                            }
                        }
                        break;
                    }
            }
        }
    }
    public class OriginalBeam : FriendlyProj
    {
        public override string Texture => "HeroRegression/Textures/blank";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Original Beam");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "原息射线");
        }
        public sealed override void SetDefaults()
        {
            Defaults(2, 2, 1200, 1, true, 40);
            Projectile.DamageType = DamageClass.Summon;
        }
        public override void AI()
        {
            if (Projectile.timeLeft % 8 == 0)
            {
                Dust dust = Dust.NewDustDirect(Projectile.Center, 1, 1, DustID.GreenTorch);
                dust.velocity *= 0;
                dust.noGravity = true;
                dust.scale = 1.5f;
            }
        }
    }
}