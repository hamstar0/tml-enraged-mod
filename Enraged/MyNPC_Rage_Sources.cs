using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Timers;


namespace Enraged {
	partial class EnragedGlobalNPC : GlobalNPC {
		private void UpdateRageGeneralSourcesIf( NPC npc, Player targetPlr ) {
			if( !EnragedGlobalNPC.CanEnrage(npc) ) {
				return;
			}

			var config = EnragedConfig.Instance;

			this.TargetUnharmedByMe++;

			if( this.IsTargetUnharmedByMe ) {
				string entryName = nameof( EnragedConfig.RagePercentGainPerTickFromUnharmedTarget );
				this.RecentRagePercentChangeChaser += this.AddRageIf( "unharmed", npc, config.Get<float>( entryName ) );
			}

			int distSqr = (int)Vector2.DistanceSquared( npc.Center, targetPlr.Center );
			int minSafeDistSqr = config.Get<int>( nameof( EnragedConfig.TileDistanceUntilTargetTooFar ) ) * 16;
			minSafeDistSqr *= minSafeDistSqr;
			int maxSafeDistSqr = config.Get<int>( nameof( EnragedConfig.TileDistanceUntilTargetTooClose ) ) * 16;
			maxSafeDistSqr *= maxSafeDistSqr;

			if( distSqr > minSafeDistSqr ) {
				string entryName = nameof( EnragedConfig.RagePercentGainPerTickFromTargetTooFar );
				this.RecentRagePercentChangeChaser += this.AddRageIf( "too far", npc, config.Get<float>( entryName ) );
			}
			if( distSqr < maxSafeDistSqr ) {
				string entryName = nameof( EnragedConfig.RagePercentGainPerTickFromTargetTooClose );
				this.RecentRagePercentChangeChaser += this.AddRageIf( "too near", npc, config.Get<float>( entryName ) );
			}

			//

			if( this.RecentRagePercentChangeChaser > 0f ) {
				this.RecentRagePercentChangeChaser -= 1f / 7200f;

				if( this.RecentRagePercentChangeChaser < 0f ) {
					this.RecentRagePercentChangeChaser = 0f;
				} else if( this.RecentRagePercentChangeChaser > (1f / 60f) ) {
					this.RecentRagePercentChangeChaser = 1f / 60f;
				}
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
			this.RecentRagePercentChangeChaser += this.AddRageIf( "on hit", npc, ragePerc );
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
				this.RecentRagePercentChangeChaser += this.AddRageIf( "target hit", npc, ragePerc );
			}
		}
	}
}
