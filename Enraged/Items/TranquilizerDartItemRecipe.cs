using System;
using Terraria.ID;
using Terraria.ModLoader;


namespace Enraged.Items {
	public class TranquilizerDartItemRecipe : ModRecipe {
		public TranquilizerDartItemRecipe( TranquilizerDartItem myitem ) : base( EnragedMod.Instance ) {
			this.AddIngredient( ItemID.PoisonDart, 10 );
			this.AddRecipeGroup( "Enraged: Strange Plants" );
			this.AddTile( TileID.ImbuingStation );
			this.SetResult( myitem, 10 );
		}


		public override bool RecipeAvailable() {
			return EnragedConfig.Instance.Get<bool>( nameof(EnragedConfig.TranqHasRecipe) );
		}
	}
}
