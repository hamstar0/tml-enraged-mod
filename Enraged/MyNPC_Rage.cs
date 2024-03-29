﻿using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.DotNET.Extensions;
using Enraged.Buffs;


namespace Enraged {
	partial class EnragedGlobalNPC : GlobalNPC {
		public float AddRage_If( string context, NPC npc, float addedPercent ) {
			var config = EnragedConfig.Instance;

			var rageScale = config.Get<Dictionary<NPCDefinition, ConfigFloat>>( nameof(config.RageRateScales) );
			float scale = rageScale.GetOrDefault( new NPCDefinition( npc.type ) )?.Value
				?? 1f;

			//

			if( scale == 0f || addedPercent == 0f ) {
				return 0f;
			}
			if( npc.HasBuff( ModContent.BuffType<EnragedBuff>() ) ) {
				return 0f;
			}

			//

			addedPercent *= scale;

			this.RagePercent += addedPercent;

			if( this.RagePercent < 0 ) {
				this.RagePercent = 0;
			} else if( this.RagePercent >= 1f ) {
				this.RagePercent = 1f;

				this.BeginEnragedState( npc );
			}

			//

			if( config.DebugModeInfo ) {
				DebugLibraries.Print(
					context + npc.whoAmI,
					$"Boss {npc.FullName} enraged from {context} by {addedPercent}; is now {this.RagePercent}"
				);
			}

			return addedPercent;
		}
	}
}
