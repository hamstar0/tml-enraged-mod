using System;
using Terraria;
using Terraria.ModLoader;
using Enraged.Buffs;
using HamstarHelpers.Tiles;

namespace Enraged {
	partial class EnragedGlobalNPC : GlobalNPC {
		public void Enrage( NPC npc ) {
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
	}
}
