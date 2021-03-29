﻿using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Helpers.Debug;
using Enraged.Buffs;


namespace Enraged {
	partial class EnragedGlobalNPC : GlobalNPC {
		private bool CanEnrage( NPC npc, Player targetPlr ) {
			var config = EnragedConfig.Instance;
			var wl = config.Get<HashSet<NPCDefinition>>( nameof( config.NpcWhitelist ) );
			var def = new NPCDefinition( npc.type );

			return wl.Contains( def );
		}


		////////////////

		private void UpdateRageIf( NPC npc, Player targetPlr ) {
			if( !this.CanEnrage( npc, targetPlr ) ) {
				return;
			}

			this.UpdateRageAmount( npc, targetPlr );

			//

			if( npc.HasBuff( ModContent.BuffType<EnragedBuff>() ) ) {
				this.UpdateEnragedExternalEffects( npc, targetPlr );
			}
		}
	}
}
