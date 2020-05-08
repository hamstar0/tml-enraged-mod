using System;
using Terraria;
using Terraria.ModLoader;


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
	}
}
