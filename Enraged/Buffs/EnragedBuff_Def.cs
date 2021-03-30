using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;


namespace Enraged.Buffs {
	partial class EnragedBuff : ModBuff {
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
