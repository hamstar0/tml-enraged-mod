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
			if( npc.boss /*&& !this._IsUpdating*/ ) {
				if( npc.HasPlayerTarget ) {
					Player player = Main.player[npc.target];
					if( player?.active == true ) {
						//this._IsUpdating = true;
						this.UpdateRageIf( npc, player );
						//this._IsUpdating = false;
					}
				}
			}
			return base.PreAI( npc );
		}
		/*public override void PostAI( NPC npc ) {
			if( npc.boss && npc.HasPlayerTarget ) {
				DebugHelpers.Print( "boss ai "+npc.FullName+" ("+npc.whoAmI+")", string.Join(", ", npc.ai) );
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
				this.ModifyHitWhileEnraged( ref damage, ref knockback );
			}
		}

		public override void ModifyHitByProjectile( NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection ) {
			if( npc.boss && npc.HasBuff( ModContent.BuffType<EnragedBuff>() ) ) {
				this.ModifyHitWhileEnraged( ref damage, ref knockback );
			}
		}
	}
}
