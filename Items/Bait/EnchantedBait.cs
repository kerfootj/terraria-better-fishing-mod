using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterFishing.Items.Bait
{
    public class EnchantedBait : ModItem
    {
        
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 4;
            item.bait = 100;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.EnchantedNightcrawler, 4);
            recipe.AddIngredient(ItemID.SoulofLight, 1);
            recipe.AddTile(TileID.Campfire);

            recipe.SetResult(this, 4);
            recipe.AddRecipe();
        }
    }
}