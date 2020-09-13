using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using Enraged.Buffs;
using Enraged.Items;


namespace Enraged {
	partial class EnragedGlobalNPC : GlobalNPC {
		private int TargetUnharmedByMe = 0;

		private int TargetDamageBuffer = 0;

		private float RecentRagePercentChange = 0;


		////////////////

		public float RageBuildupPercent { get; private set; } = 0f;

		////

		public bool IsTargetUnharmedByMe =>
			this.TargetUnharmedByMe >=
			EnragedConfig.Instance.Get<int>( nameof(EnragedConfig.TargetUnharmedTickThreshold) );


		////////////////

		public override bool CloneNewInstances => false;

		public override bool InstancePerEntity => true;



		////////////////

		 private bool _IsUpdating = false;

		public override bool PreAI( NPC npc ) {
			if( npc.boss && !this._IsUpdating ) {
				if( npc.HasPlayerTarget ) {
					Player player = Main.player[npc.target];
					if( player?.active == true ) {
						this._IsUpdating = true;
						this.UpdateRageBehavior( npc, player );
						this._IsUpdating = false;
					}
				}
			}
			return base.PreAI( npc );
		}

		private void UpdateRageBehavior( NPC npc, Player targetPlr ) {
			this.UpdateRageState( npc, targetPlr );

			if( npc.HasBuff(ModContent.BuffType<EnragedBuff>()) ) {
				this.UpdateEnragedEffects( npc, targetPlr );
			}

			if( this.RecentRagePercentChange > 0f ) {
				this.RecentRagePercentChange -= 1f / 7200f;
				if( this.RecentRagePercentChange < 0f ) {
					this.RecentRagePercentChange = 0f;
				} else if( this.RecentRagePercentChange > (1f / 60f) ) {
					this.RecentRagePercentChange = 1f / 60f;
				}
			}
		}


		////

		public override void SetupShop( int type, Chest shop, ref int nextSlot ) {
			if( type == NPCID.ArmsDealer ) {
				if( EnragedConfig.Instance.Get<bool>( nameof(EnragedConfig.TranqSoldFromArmsDealer) ) ) {
					var item = new Item();
					item.SetDefaults( ModContent.ItemType<TranquilizerDartItem>() );

					shop.item[ nextSlot++ ] = item;
				}
			}
		}
	}
}
