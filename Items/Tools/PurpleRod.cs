using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterFishing.Items.Tools
{
    public class PurpleRod : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Do you like fish sticks?");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.GoldenFishingRod);
            item.fishingPole = 100;
            item.value = Item.buyPrice(2, 0, 0, 0);
            item.rare = ItemRarityID.Purple;
            item.shoot = mod.ProjectileType("BobberPurple");
            item.shootSpeed = 20f;

            ItemID.Sets.CanFishInLava[item.type] = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GoldenFishingRod, 1);
            recipe.AddIngredient(ItemID.PurpleString, 1);
            recipe.AddIngredient(ItemID.HallowedBar, 40);
            recipe.AddIngredient(ItemID.SoulofNight, 40);
            recipe.AddTile(TileID.Anvils);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}