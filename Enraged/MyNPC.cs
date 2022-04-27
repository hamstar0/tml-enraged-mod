using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using Enraged.Buffs;
using Enraged.Items;


namespace Enraged {
	partial class EnragedGlobalNPC : GlobalNPC {
		private int TargetUnharmedByMe = 0;

		private int TargetDamageBuffer = 0;

		private float RecentRagePercentChangeChaser = 0;

		private float LastKnownDrawScale = 1f;

		////

		private float HurtAnimationPercent = 0f;

		////

		internal int BrambleBlockTimer = 0;


		////////////////

		public float RagePercent { get; private set; } = 0f;

		////

		public bool IsTargetUnharmedByMe =>
			this.TargetUnharmedByMe >=
				EnragedConfig.Instance.Get<int>( nameof(EnragedConfig.TargetUnharmedTickThreshold) );



		////////////////

		public override bool CloneNewInstances => false;

		public override bool InstancePerEntity => true;



		////////////////

		// private bool _IsUpdating = false;

		public override bool PreAI( NPC npc ) {
			/*!this._IsUpdating*/
			if( npc.HasPlayerTarget ) {
				Player player = Main.player[npc.target];
				if( player?.active == true ) {
					//this._IsUpdating = true;
					this.UpdateRageGeneralSourcesIf( npc, player );
					//this._IsUpdating = false;
				}
			}

			//

			if( npc.HasBuff(ModContent.BuffType<EnragedBuff>()) ) {
				EnragedBuff.ApplyExternalEffects_If_Host( npc );
			}

			//

			if( this.HurtAnimationPercent > 0f ) {
				this.HurtAnimationPercent -= 1f / 10f;
				if( this.HurtAnimationPercent < 0f ) {
					this.HurtAnimationPercent = 0f;
				}
			}

			if( npc.velocity.LengthSquared() >= 25f ) {	// 5mph
				this.BrambleBlockTimer = 60 * 2;
			}

			if( this.BrambleBlockTimer >= 1 ) {
				this.BrambleBlockTimer--;
			}

			//

			return base.PreAI( npc );
		}
		/*public override void PostAI( NPC npc ) {
			if( npc.boss && npc.HasPlayerTarget ) {
				DebugLibraries.Print( "boss ai "+npc.FullName+" ("+npc.whoAmI+")", string.Join(", ", npc.ai) );
			}
		}*/


		////

		public override void SetupShop( int type, Chest shop, ref int nextSlot ) {
			var config = EnragedConfig.Instance;

			switch( type ) {
			case NPCID.WitchDoctor:
				if( config.Get<bool>( nameof(config.TranqSoldFromWitchDoctor) ) ) {
					var item = new Item();
					item.SetDefaults( ModContent.ItemType<TranquilizerDartItem>() );

					shop.item[ nextSlot++ ] = item;
				}
				break;
			case NPCID.ArmsDealer:
				if( config.Get<bool>( nameof(config.TranqSoldFromArmsDealer) ) ) {
					var item = new Item();
					item.SetDefaults( ModContent.ItemType<TranquilizerDartItem>() );

					shop.item[ nextSlot++ ] = item;
				}
				break;
			}
		}


		////////////////

		public override void ModifyHitByItem( NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit ) {
			if( npc.boss && npc.HasBuff( ModContent.BuffType<EnragedBuff>() ) ) {
				EnragedBuff.ModifyHitStats_If( npc, ref damage, ref knockback );
			}
		}

		public override void ModifyHitByProjectile( NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection ) {
			if( npc.boss && npc.HasBuff( ModContent.BuffType<EnragedBuff>() ) ) {
				EnragedBuff.ModifyHitStats_If( npc, ref damage, ref knockback );
			}
		}


		////////////////

		public override void HitEffect( NPC npc, int hitDirection, double damage ) {
			if( this.AdjustRageOnHitIf(npc) ) {
				this.HurtAnimationPercent = damage > 3d
					? 1f
					: this.HurtAnimationPercent;
			}
		}

		public override void OnHitPlayer( NPC npc, Player target, int damage, bool crit ) {
			this.AdjustRageOnHitToPlayerIf( npc, target, damage, crit );
		}


		////////////////

		public override void DrawEffects( NPC npc, ref Color drawColor ) {
			if( npc.boss ) {
				if( npc.HasBuff( ModContent.BuffType<EnragedBuff>() ) ) {
					EnragedBuff.ApplyVisualFx( npc, ref drawColor, this.LastKnownDrawScale, this.HurtAnimationPercent );
				} else {
					this.LastKnownDrawScale = npc.scale;
				}
			}
		}
	}
}
