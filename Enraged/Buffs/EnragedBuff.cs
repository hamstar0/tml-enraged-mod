using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Services.Timers;
using HamstarHelpers.Helpers.Debug;


namespace Enraged.Buffs {
	class EnragedBuff : ModBuff {
		public override void SetDefaults() {
			this.DisplayName.SetDefault( "Enraged" );
			this.Description.SetDefault( "Run away!" );
			//Main.debuff[this.Type] = true;
			//Main.pvpBuff[this.Type] = true;
			Main.buffNoSave[this.Type] = true;
			//this.longerExpertDebuff = true;
		}


		public override void Update( NPC npc, ref int buffIndex ) {
			if( EnragedConfig.Instance.DebugModeInfo ) {
				DebugHelpers.Print( "BossEnrageDuration_"+npc.whoAmI, ""+npc.buffTime[buffIndex] );
			}

			Timers.RunNow( () => {
				int times = EnragedConfig.Instance.Get<int>( nameof(EnragedConfig.TimesToRunAIPerTickWhileEnraged) );

				for( int i = 0; i < times; i++ ) {
					npc.AI();
				}
			} );
		}
	}
}
