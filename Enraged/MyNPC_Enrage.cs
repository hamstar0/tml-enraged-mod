using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using CursedBrambles.Tiles;
using Enraged.Buffs;


namespace Enraged {
	partial class EnragedGlobalNPC : GlobalNPC {
		private void UpdateRageIf( NPC npc, Player targetPlr ) {
			if( !this.CanEnrage( npc, targetPlr ) ) {
				return;
			}

			this.UpdateRageAmount( npc, targetPlr );

			//

			if( npc.HasBuff( ModContent.BuffType<EnragedBuff>() ) ) {
				this.UpdateEnragedExternalEffects( npc, targetPlr );
			}
		}


		////////////////

		public void BeginEnragedState( NPC npc ) {
			this.RagePercent = 0f;
			this.RecentRagePercentChange = 0f;

			int ticks = EnragedConfig.Instance.Get<int>( nameof(EnragedConfig.RageDurationTicks) );

			npc.AddBuff( ModContent.BuffType<EnragedBuff>(), ticks );

			Main.PlaySound( SoundID.NPCHit57, npc.Center );
		}


		////////////////
		
		private void UpdateEnragedExternalEffects( NPC npc, Player targetPlr ) {
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

		private void ModifyHitWhileEnraged( ref int damage, ref float knockback ) {
			damage = Math.Max( (damage / 2) - 10, 1 );
			knockback = 0;
		}
	}
}
