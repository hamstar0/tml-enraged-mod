using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;
using HamstarHelpers.Services.Timers;
using Enraged.Buffs;


namespace Enraged {
	partial class EnragedGlobalNPC : GlobalNPC {
		private void UpdateRageAmount( NPC npc, Player targetPlr ) {
			var config = EnragedConfig.Instance;
			float oldRageValue = this.RagePercent;

			this.TargetUnharmedByMe++;

			if( this.IsTargetUnharmedByMe ) {
				string entryName = nameof( EnragedConfig.RagePercentGainPerTickFromUnharmedTarget );
				this.AddRageIf( "unharmed", npc, config.Get<float>( entryName ) );
			}

			int distSqr = (int)Vector2.DistanceSquared( npc.Center, targetPlr.Center );
			int minSafeDistSqr = config.Get<int>( nameof( EnragedConfig.TileDistanceUntilTargetTooFar ) ) * 16;
			minSafeDistSqr *= minSafeDistSqr;
			int maxSafeDistSqr = config.Get<int>( nameof( EnragedConfig.TileDistanceUntilTargetTooClose ) ) * 16;
			maxSafeDistSqr *= maxSafeDistSqr;

			if( distSqr > minSafeDistSqr ) {
				string entryName = nameof( EnragedConfig.RagePercentGainPerTickFromTargetTooFar );
				this.AddRageIf( "too far", npc, config.Get<float>( entryName ) );
			}
			if( distSqr < maxSafeDistSqr ) {
				string entryName = nameof( EnragedConfig.RagePercentGainPerTickFromTargetTooClose );
				this.AddRageIf( "too near", npc, config.Get<float>( entryName ) );
			}

			//

			if( this.RecentRagePercentChange > 0f ) {
				this.RecentRagePercentChange -= 1f / 7200f;
				if( this.RecentRagePercentChange < 0f ) {
					this.RecentRagePercentChange = 0f;
				} else if( this.RecentRagePercentChange > ( 1f / 60f ) ) {
					this.RecentRagePercentChange = 1f / 60f;
				}
			}

			//

			var mymod = EnragedMod.Instance;
			string uid = NPCID.GetUniqueKey( npc.type );

			if( mymod.RageOverrides.ContainsKey( uid ) ) {
				this.RagePercent = mymod.RageOverrides[uid].Invoke( npc.whoAmI, oldRageValue, this.RagePercent );
			}
		}


		////////////////

		private void AdjustRageOnHitIf( NPC npc ) {
			if( !EnragedGlobalNPC.CanEnrage(npc) ) {
				return;
			}

			string timerName = "EnragedNpcHitCooldown_" + npc.whoAmI;
			if( Timers.GetTimerTickDuration(timerName) > 0 ) {
				return;
			}

			var config = EnragedConfig.Instance;
			int cooldownTicks = config.Get<int>( nameof( EnragedConfig.CooldownTickDurationBetweenHits ) );
			float ragePerc = config.Get<float>( nameof( EnragedConfig.RagePercentGainPerHitTaken ) );

			Timers.SetTimer( timerName, cooldownTicks, false, () => false );
			this.AddRageIf( "on hit", npc, ragePerc );
		}


		private void AdjustRageOnHitToPlayerIf( NPC npc, Player target, int damage, bool crit ) {
			if( !EnragedGlobalNPC.CanEnrage(npc) ) {
				return;
			}

			if( !npc.HasPlayerTarget || npc.target != target.whoAmI ) {
				return;
			}

			var config = EnragedConfig.Instance;

			this.TargetUnharmedByMe = 0;
			this.TargetDamageBuffer += crit ? damage * 2 : damage;

			while( this.TargetDamageBuffer > 10 ) {
				float ragePerc = config.Get<float>( nameof(EnragedConfig.RagePercentGainPerHitTaken) );

				this.TargetDamageBuffer -= 10;
				this.AddRageIf( "target hit", npc, ragePerc );
			}
		}


		////////////////

		public void AddRageIf( string context, NPC npc, float addedPercent ) {
			var config = EnragedConfig.Instance;
			var rageScale = config.Get<Dictionary<NPCDefinition, ConfigFloat>>( nameof(EnragedConfig.RageRateScales) );
			float scale = rageScale.GetOrDefault( new NPCDefinition(npc.type) )?.Value
				?? 1f;

			if( scale == 0f || addedPercent == 0f ) {
				return;
			}
			if( npc.HasBuff( ModContent.BuffType<EnragedBuff>() ) ) {
				return;
			}

			addedPercent *= scale;

			this.RagePercent += addedPercent;
			this.RecentRagePercentChange += addedPercent;

			if( this.RagePercent < 0 ) {
				this.RagePercent = 0;
			} else if( this.RagePercent >= 1f ) {
				this.RagePercent = 1f;
				this.BeginEnragedState( npc );
			}

			if( config.DebugModeInfo ) {
				DebugHelpers.Print(
					context+npc.whoAmI,
					"Boss "+npc.FullName+" enraged from "+context+" by "+addedPercent+"; is now "+this.RagePercent
				);
			}
		}
	}
}
