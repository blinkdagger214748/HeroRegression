using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.ItemDropRules;

namespace HeroRegression.Common.BaseClasses.BaseLoot
{
    public class BaseBossLoot:GlobalNPC
    {
        public override bool InstancePerEntity => true;
        /// <summary>
        /// item ID, chance, min drop, max drop
        /// </summary>
        public List<Vector4> NormalLoots = new();
        public int[] OptionsLoots = new int[0] { };
        /// <summary>
        /// This function is for adding loots to bosses' ModifyLoot part.
        /// </summary>
        /// <param name="npcloot"></param>
        /// <param name="bagID"></param>
        /// <param name="trophyID"></param>
        /// <param name="relicID"></param>
        /// <param name="petID"></param>
        /// <param name="maskID"></param>
        /// <param name="optionsChance"></param>
        public void ModifyLoots(NPCLoot npcloot, int bagID = -1, int trophyID = -1, int relicID = -1, int petID = -1, int maskID = -1, int optionsChance = 1)
        {
            if (bagID != -1) npcloot.Add(ItemDropRule.BossBag(bagID));
            if (trophyID != -1) npcloot.Add(ItemDropRule.Common(trophyID, 10, 1, 1));
            if (relicID != -1) npcloot.Add(ItemDropRule.MasterModeCommonDrop(relicID));
            if (petID != -1) npcloot.Add(ItemDropRule.MasterModeDropOnAllPlayers(petID, 4));
            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
            if (maskID != -1) Chains.OnSuccess(notExpertRule, ItemDropRule.Common(maskID, 7, 1, 1), false);
            if (OptionsLoots.Length >= 1) Chains.OnSuccess(notExpertRule, ItemDropRule.OneFromOptions(optionsChance, OptionsLoots), false);
            if (NormalLoots.Count > 0)
            {
                for (int i = 0; i < NormalLoots.Count; i++)
                {
                    Vector4 lootInfo = NormalLoots[i];
                    Chains.OnSuccess(notExpertRule, ItemDropRule.Common((int)lootInfo.X, (int)lootInfo.Y, (int)lootInfo.Z, (int)lootInfo.W), false);
                }
            }
            npcloot.Add(notExpertRule);
        }
    }
}
