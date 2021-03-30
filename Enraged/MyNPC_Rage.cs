using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;
using Enraged.Buffs;


namespace Enraged {
	partial class EnragedGlobalNPC : GlobalNPC {
		public float AddRageIf( string context, NPC npc, float addedPercent ) {
			var config = EnragedConfig.Instance;

			float oldRageValue = this.RagePercent;
			var rageScale = config.Get<Dictionary<NPCDefinition, ConfigFloat>>( nameof( EnragedConfig.RageRateScales ) );
			float scale = rageScale.GetOrDefault( new NPCDefinition( npc.type ) )?.Value
				?? 1f;

			if( scale == 0f || addedPercent == 0f ) {
				return 0f;
			}
			if( npc.HasBuff( ModContent.BuffType<EnragedBuff>() ) ) {
				return 0f;
			}

			addedPercent *= scale;

			//

			var mymod = EnragedMod.Instance;
			string uid = NPCID.GetUniqueKey( npc.type );

			if( mymod.RageOverrides.ContainsKey( uid ) ) {
				addedPercent = mymod.RageOverrides[uid].Invoke( npc.whoAmI, oldRageValue, addedPercent );
			}

			//

			this.RagePercent += addedPercent;

			if( this.RagePercent < 0 ) {
				this.RagePercent = 0;
			} else if( this.RagePercent >= 1f ) {
				this.RagePercent = 1f;
				this.BeginEnragedState( npc );
			}

			//

			if( config.DebugModeInfo ) {
				DebugHelpers.Print(
					context + npc.whoAmI,
					"Boss " + npc.FullName + " enraged from " + context + " by " + addedPercent + "; is now " + this.RagePercent
				);
			}

			return addedPercent;
		}
	}
}
