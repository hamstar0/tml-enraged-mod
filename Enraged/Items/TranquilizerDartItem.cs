using System;
using Terraria.ID;
using Terraria.ModLoader;
using Enraged.Projectiles;


namespace Enraged.Items {
	public class TranquilizerDartItem : ModItem {
		public override void SetStaticDefaults() {
			this.Tooltip.SetDefault( "Slows enemies. Reduces boss rage." );
		}

		public override void SetDefaults() {
			this.item.damage = 2;
			this.item.ranged = true;
			this.item.ammo = AmmoID.Dart;
			this.item.width = 8;
			this.item.height = 8;
			this.item.maxStack = 999;
			this.item.ranged = true;
			this.item.consumable = true;
			this.item.knockBack = 2f;
			this.item.value = 1000;
			this.item.rare = ItemRarityID.Green;
			this.item.shoot = ModContent.ProjectileType<TranquilizerDartProjectile>();
			this.item.shootSpeed = 2f;
		}


		////////////////

		public override void AddRecipes() {
			var recipe = new TranquilizerDartItemRecipe( this );
			recipe.AddRecipe();
		}
	}
}
