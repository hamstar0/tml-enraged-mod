using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Timers;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;


namespace Enraged {
	partial class EnragedGlobalNPC : GlobalNPC {
		private void UpdateRageState( NPC npc, Player targetPlr ) {
			var config = EnragedConfig.Instance;

			this.TargetUnharmedByMe++;

			if( this.IsTargetUnharmedByMe ) {
				this.AddRage( "unharmed", npc, config.RagePercentGainPerTickFromUnharmedTarget );
			}

			int distSqr = (int)Vector2.DistanceSquared( npc.Center, targetPlr.Center );
			int minSafeDistSqr = config.MinimumTileDistanceBeforeRageGain * 16;
			minSafeDistSqr *= minSafeDistSqr;
			int maxSafeDistSqr = config.MaximumTileDistanceBeforeRageGain * 16;
			maxSafeDistSqr *= maxSafeDistSqr;
			
			if( distSqr > minSafeDistSqr ) {
				this.AddRage( "too far", npc, config.RagePercentGainPerTickFromTargetFleeing );
			}
			if( distSqr < maxSafeDistSqr ) {
				this.AddRage( "too near", npc, config.RagePercentGainPerTickFromTargetTooClose );
			}
		}


		////////////////

		public override void HitEffect( NPC npc, int hitDirection, double damage ) {
			if( npc.boss ) {
				string timerName = "EnragedNpcHitCooldown_"+npc.whoAmI;
				var config = EnragedConfig.Instance;

				if( Timers.GetTimerTickDuration(timerName) <= 0 ) {
					Timers.SetTimer( timerName, config.CooldownTickDurationBetweenHits, false, () => false );
					this.AddRage( "on hit", npc, config.RagePercentGainPerHitTaken );
				}
			}
		}

		public override void OnHitPlayer( NPC npc, Player target, int damage, bool crit ) {
			if( npc.boss && npc.HasPlayerTarget && npc.target == target.whoAmI ) {
				var config = EnragedConfig.Instance;

				this.TargetUnharmedByMe = 0;
				this.TargetDamageBuffer += crit ? damage * 2 : damage;

				while( this.TargetDamageBuffer > 10 ) {
					this.TargetDamageBuffer -= 10;
					this.AddRage( "target hit", npc, config.RagePercentGainPerTargetHitPer10 );
				}
			}
		}


		////////////////

		public void AddRage( string context, NPC npc, float addedPercent ) {
			if( addedPercent == 0f ) {
				return;
			}

			this.RageBuildupPercent += addedPercent;

			if( this.RageBuildupPercent < 0 ) {
				this.RageBuildupPercent = 0;
			} else if( this.RageBuildupPercent >= 1f ) {
				this.RageBuildupPercent = 1f;
				this.Enrage( npc );
			}

			if( EnragedConfig.Instance.DebugModeInfo ) {
				DebugHelpers.Print(
					context+npc.whoAmI,
					"Boss "+npc.FullName+" enraged from "+context+" by "+addedPercent+"; is now "+this.RageBuildupPercent
				);
			}
		}
	}
}
