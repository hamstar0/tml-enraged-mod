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
			this.item.value = 10000;
			this.item.rare = 2;
			this.item.shoot = ModContent.ProjectileType<TranquilizerDartProjectile>();
			this.item.shootSpeed = 2f;
		}

		////

		public override void AddRecipes() {
			if( !EnragedConfig.Instance.TranqHasRecipe ) {
				return;
			}

			ModRecipe recipe = new ModRecipe( mod );
			recipe.AddIngredient( ItemID.PoisonDart, 10 );
			recipe.AddRecipeGroup( "Enraged: Strange Plants" );
			recipe.AddTile( TileID.WorkBenches );
			recipe.SetResult( this, 10 );
			recipe.AddRecipe();
		}
	}
}
