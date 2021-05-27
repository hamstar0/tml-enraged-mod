using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using ModLibsCore.Libraries.Debug;
using Enraged.Buffs;


namespace Enraged {
	partial class EnragedGlobalNPC : GlobalNPC {
		public static bool CanEnrage( NPC npc ) {
			var config = EnragedConfig.Instance;
			var wl = config.Get<HashSet<NPCDefinition>>( nameof(config.NpcWhitelist) );
			var def = new NPCDefinition( npc.type );

			return wl.Contains( def );
		}


		////////////////

		public void BeginEnragedState( NPC npc ) {
			this.RagePercent = 0f;
			this.RecentRagePercentChangeChaser = 0f;

			int ticks = EnragedConfig.Instance.Get<int>( nameof( EnragedConfig.RageDurationTicks ) );

			npc.AddBuff( ModContent.BuffType<EnragedBuff>(), ticks );

			Main.PlaySound( SoundID.NPCHit57, npc.Center );
		}
	}
}
