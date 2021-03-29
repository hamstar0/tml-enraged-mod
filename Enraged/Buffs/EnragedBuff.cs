using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using CursedBrambles.Tiles;


namespace Enraged.Buffs {
	partial class EnragedBuff : ModBuff {
		public static void ApplyExternalEffects( NPC npc ) {
			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				return;
			}

			var config = EnragedConfig.Instance;
			int thickness = config.Get<int>( nameof( EnragedConfig.EnragedBrambleTrailThickness ) );
			float density = config.Get<float>( nameof( EnragedConfig.EnragedBrambleTrailDensity ) );

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

		public static void ModifyHit( ref int damage, ref float knockback ) {
			damage = Math.Max( (damage / 2) - 10, 1 );
			knockback = 0;
		}



		////////////////

		public override void SetDefaults() {
			this.DisplayName.SetDefault( "Enraged" );
			this.Description.SetDefault( "Run away!" );
			//Main.debuff[this.Type] = true;
			//Main.pvpBuff[this.Type] = true;
			Main.buffNoSave[this.Type] = true;
			//this.longerExpertDebuff = true;
		}


		public override void Update( NPC npc, ref int buffIndex ) {
			var config = EnragedConfig.Instance;

			if( config.DebugModeInfo ) {
				DebugHelpers.Print( "BossEnrageDuration_"+npc.whoAmI, ""+npc.buffTime[buffIndex] );
			}

			/*Timers.RunNow( () => {
				int times = config.Get<int>( nameof(config.TimesToRunAIPerTickWhileEnraged) );

				for( int i = 0; i < times; i++ ) {
					npc.AI();
				}
			} );*/
		}
	}
}
