using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Tiles;
using HamstarHelpers.Helpers.XNA;
using Enraged.Buffs;


namespace Enraged {
	partial class EnragedGlobalNPC : GlobalNPC {
		public void Enrage( NPC npc ) {
			this.RageBuildupPercent = 0f;

			npc.AddBuff( ModContent.BuffType<EnragedBuff>(), EnragedConfig.Instance.RageDurationTicks );
		}


		////////////////
		
		private void UpdateEnragedEffects( NPC npc, Player targetPlr ) {
			var config = EnragedConfig.Instance;

			if( config.EnragedBrambleTrailThickness > 0 && config.EnragedBrambleTrailDensity > 0f ) {
				CursedBrambleTile.CreateBramblePatchAt(
					(int)( npc.Center.X / 16f ),
					(int)( npc.Center.Y / 16f ),
					config.EnragedBrambleTrailThickness,
					config.EnragedBrambleTrailDensity
				);
			}
		}


		////////////////

		public override void ModifyHitByItem( NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit ) {
			if( npc.boss && npc.HasBuff(ModContent.BuffType<EnragedBuff>()) ) {
				damage = Math.Max( (damage / 2) - 10, 1 );
				knockback = 0;
			}
		}

		public override void ModifyHitByProjectile( NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection ) {
			if( npc.boss && npc.HasBuff( ModContent.BuffType<EnragedBuff>() ) ) {
				damage = Math.Max( (damage / 2) - 10, 1 );
				knockback = 0;
			}
		}


		////////////////

		 private float BaseScale = 1;

		public override void DrawEffects( NPC npc, ref Color drawColor ) {
			if( npc.boss ) {
				if( npc.HasBuff(ModContent.BuffType<EnragedBuff>()) ) {
					// Add red tint
					var newColor = new Color( 255, 128, 128 );
					drawColor = XNAColorHelpers.Mul( drawColor, newColor );

					// Add shakes
					npc.scale = ((Main.rand.NextFloat() * 0.1f) - 0.05f) + this.BaseScale;
				} else {
					this.BaseScale = npc.scale;
				}
			}
		}
	}
}
