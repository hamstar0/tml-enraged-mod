using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using CursedBrambles.Tiles;
using Enraged.Buffs;


namespace Enraged {
	partial class EnragedGlobalNPC : GlobalNPC {
		public void Enrage( NPC npc ) {
			this.RageBuildupPercent = 0f;
			this.RecentRagePercentChange = 0f;

			int ticks = EnragedConfig.Instance.Get<int>( nameof(EnragedConfig.RageDurationTicks) );

			npc.AddBuff( ModContent.BuffType<EnragedBuff>(), ticks );

			Main.PlaySound( SoundID.NPCHit57, npc.Center );
		}


		////////////////
		
		private void UpdateEnragedEffects( NPC npc, Player targetPlr ) {
			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				return;
			}

			var config = EnragedConfig.Instance;
			int thickness = config.Get<int>( nameof(EnragedConfig.EnragedBrambleTrailThickness) );
			float density = config.Get<float>( nameof(EnragedConfig.EnragedBrambleTrailDensity) );

			if( thickness > 0 && density > 0f ) {
				int created = CursedBrambleTile.CreateBramblePatchAt(
					tileX: (int)npc.Center.X / 16,
					tileY: (int)npc.Center.Y / 16,
					radius: thickness,
					densityPercent: density,
					sync: true
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
