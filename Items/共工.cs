using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using HeroRegression.Projectiles;

namespace HeroRegression.Items
{
    //文件名
    public class 共工 : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("共工");

        }

        public override void SetDefaults()
        {
            //伤害
            Item.damage = 25;
            //击退
            Item.knockBack = 4f;
            //暴击几率
            Item.crit = 9;

            //物品稀有度
            Item.rare = 4;
            // 攻击速度和攻击动画持续时间！
            Item.useTime = 16;
            Item.useAnimation = 16;
            // 使用方式，这个值决定了武器使用时到底是按什么样的动画播放
            // 1 代表挥动，也就是剑类武器！
            // 2 代表像药水一样喝下去，emmmm这个放在剑上会不会很奇怪（吞
            // 3 代表像同志短剑一样刺x 出去
            // 4 唔，这个一般不是用在武器上的，想象一下生命水晶使用的时候的动作
            // 5 手持，枪、弓、法杖类武器的动作，用途最广
            Item.useStyle = 5;
            // 决定了这个武器鼠标按住不放能不能一直攻击， true代表可以, false代表不行
            Item.autoReuse = true;
            // 决定了这个武器的伤害属性，
            // melee 代表近战
            // ranged 代表远程
            // magic 代表膜法，不，魔法
            // summon 代表召唤
            // thrown 代表投掷
            Item.DamageType = DamageClass.Ranged;
          //  Item.noUseGraphic = true;
            Item.useAmmo = AmmoID.Arrow;
            Item.value = Item.sellPrice(0, 0, 80, 0);
            // 设置这个物品使用时发出的声音，以后会讲到怎么调出其他声音
            // 在这里我用的是普通的挥剑声音
            Item.UseSound = SoundID.Item1;
            // 物品使用的时候变大的倍数，这里是1.2倍，也就是比贴图大1.2倍（emm
            Item.scale = 0.9f;
            // 物品的碰撞体积大小，可以与贴图无关，但是建议设为跟贴图一样的大小
            // 不然鬼知道会不会发生奇怪的事情
            Item.width =45;
            Item.height = 80;
            // 最大堆叠数量，唔，对于武器来说，即使你堆了99个，使用的时候还是只有一个的效果
            Item.maxStack = 1;
            //弹幕
            Item.shoot = ModContent.ProjectileType<激流箭>();
            Item.shootSpeed = 5;
           
            //正反手挥剑
           
        }
        // 物品合成表的设置部分
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 vel = velocity;
            Projectile.NewProjectile(source,position, vel, ModContent.ProjectileType<激流箭>(), damage, 5f, player.whoAmI);
            return false;
        }
    }
}

