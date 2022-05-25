using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using ModLibsCore.Libraries.Debug;
using Enraged.Buffs;
using CursedBrambles;
using CursedBrambles.Generators.Samples;


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

			var config = EnragedConfig.Instance;
			int ticks = config.Get<int>( nameof(config.RageDurationTicks) );

			//

			int bloomSize = config.Get<int>( nameof(config.EnrageBrambleBloomSize) );

			if( bloomSize > 0 ) {
				var mymod = EnragedMod.Instance;
				string uid = NPCID.GetUniqueKey( npc.netID );

				if( !mymod.EnragedNpcCannotBrambleBloom.ContainsKey(uid) ) {
					var gen = new BloomBrambleGen(
						size: bloomSize,
						minTickRate: 8,
						addedTickRateVariation: 8,
						tileX: (int)npc.Center.X / 16,
						tileY: (int)npc.Center.Y / 16
					);

					CursedBramblesAPI.AddBrambleGenerator( gen );
				}
			}

			//

			npc.AddBuff( ModContent.BuffType<EnragedBuff>(), ticks );

			//

			Main.PlaySound( SoundID.NPCHit57, npc.Center );
		}
	}
}
