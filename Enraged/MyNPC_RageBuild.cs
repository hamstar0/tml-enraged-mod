using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader.Config;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Timers;
using HamstarHelpers.Helpers.DotNET.Extensions;
using Enraged.Buffs;


namespace Enraged {
	partial class EnragedGlobalNPC : GlobalNPC {
		private void UpdateRageState( NPC npc, Player targetPlr ) {
			var config = EnragedConfig.Instance;

			this.TargetUnharmedByMe++;

			if( this.IsTargetUnharmedByMe ) {
				string entryName = nameof( EnragedConfig.RagePercentGainPerTickFromUnharmedTarget );
				this.AddRage( "unharmed", npc, config.Get<float>(entryName) );
			}

			int distSqr = (int)Vector2.DistanceSquared( npc.Center, targetPlr.Center );
			int minSafeDistSqr = config.Get<int>( nameof(EnragedConfig.TileDistanceUntilTargetTooFar) ) * 16;
			minSafeDistSqr *= minSafeDistSqr;
			int maxSafeDistSqr = config.Get<int>( nameof(EnragedConfig.TileDistanceUntilTargetTooClose) ) * 16;
			maxSafeDistSqr *= maxSafeDistSqr;
			
			if( distSqr > minSafeDistSqr ) {
				string entryName = nameof( EnragedConfig.RagePercentGainPerTickFromTargetTooFar );
				this.AddRage( "too far", npc, config.Get<float>(entryName) );
			}
			if( distSqr < maxSafeDistSqr ) {
				string entryName = nameof( EnragedConfig.RagePercentGainPerTickFromTargetTooClose );
				this.AddRage( "too near", npc, config.Get<float>( entryName ) );
			}
		}


		////////////////

		public override void HitEffect( NPC npc, int hitDirection, double damage ) {
			if( npc.boss ) {
				string timerName = "EnragedNpcHitCooldown_"+npc.whoAmI;
				var config = EnragedConfig.Instance;

				if( Timers.GetTimerTickDuration(timerName) <= 0 ) {
					int cooldownTicks = config.Get<int>( nameof(EnragedConfig.CooldownTickDurationBetweenHits) );
					float ragePerc = config.Get<float>( nameof(EnragedConfig.RagePercentGainPerHitTaken) );

					Timers.SetTimer( timerName, cooldownTicks, false, () => false );
					this.AddRage( "on hit", npc, ragePerc );
				}
			}
		}

		public override void OnHitPlayer( NPC npc, Player target, int damage, bool crit ) {
			if( npc.boss && npc.HasPlayerTarget && npc.target == target.whoAmI ) {
				var config = EnragedConfig.Instance;

				this.TargetUnharmedByMe = 0;
				this.TargetDamageBuffer += crit ? damage * 2 : damage;

				while( this.TargetDamageBuffer > 10 ) {
					float ragePerc = config.Get<float>( nameof(EnragedConfig.RagePercentGainPerHitTaken) );

					this.TargetDamageBuffer -= 10;
					this.AddRage( "target hit", npc, ragePerc );
				}
			}
		}


		////////////////

		public void AddRage( string context, NPC npc, float addedPercent ) {
			var config = EnragedConfig.Instance;
			var rageScale = config.Get<Dictionary<NPCDefinition, ConfigFloat>>( nameof(EnragedConfig.RageMeterScales) );
			float scale = rageScale.GetOrDefault( new NPCDefinition(npc.type) )?.Value ?? 1f;

			if( scale == 0f || addedPercent == 0f ) {
				return;
			}
			if( npc.HasBuff( ModContent.BuffType<EnragedBuff>() ) ) {
				return;
			}

			addedPercent *= scale;

			this.RageBuildupPercent += addedPercent;
			this.RecentRagePercentChange += addedPercent;

			if( this.RageBuildupPercent < 0 ) {
				this.RageBuildupPercent = 0;
			} else if( this.RageBuildupPercent >= 1f ) {
				this.RageBuildupPercent = 1f;
				this.Enrage( npc );
			}

			if( config.DebugModeInfo ) {
				DebugHelpers.Print(
					context+npc.whoAmI,
					"Boss "+npc.FullName+" enraged from "+context+" by "+addedPercent+"; is now "+this.RageBuildupPercent
				);
			}
		}
	}
}
