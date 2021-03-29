using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;


namespace Enraged {
	public delegate float RageValueOverride( int npcWho, float oldRagePercent, float addedRagePercent );




	public partial class EnragedMod : Mod {
		internal IDictionary<string, RageValueOverride> RageOverrides = new Dictionary<string, RageValueOverride> {
			{ NPCID.GetUniqueKey(NPCID.EyeofCthulhu), (npcWho, oldRagePerc, addRagePerc) => {
				NPC npc = Main.npc[npcWho];
				if( addRagePerc > 0f ) {
					if( npc?.active == true && npc.ai[1] > 0f ) {	// Dash
						return 0f;
					}
				}
				return addRagePerc;
			} },
			{ NPCID.GetUniqueKey(NPCID.QueenBee), (npcWho, oldRagePerc, addRagePerc) => {
				NPC npc = Main.npc[npcWho];
				if( addRagePerc > 0f ) {
					if( npc?.active == true && npc.ai[0] == 0f ) {	// Dash
						return 0f;
					}
				}
				return addRagePerc;
			} },
			{ NPCID.GetUniqueKey(NPCID.BrainofCthulhu), (npcWho, oldRagePerc, addRagePerc) => {
				NPC npc = Main.npc[npcWho];
				if( addRagePerc > 0f ) {
					if( npc?.active == true && npc.ai[0] < 0f ) {	// Tricky charges
						return 0f;
					}
				}
				return addRagePerc;
			} },
			{ NPCID.GetUniqueKey(NPCID.SkeletronHead), (npcWho, oldRagePerc, addRagePerc) => {
				NPC npc = Main.npc[npcWho];
				if( addRagePerc > 0f ) {
					if( npc?.active == true && npc.ai[1] > 0f ) {	// Chase
						return 0f;
					}
				}
				return addRagePerc;
			} },
			{ NPCID.GetUniqueKey(NPCID.SkeletronPrime), (npcWho, oldRagePerc, addRagePerc) => {
				NPC npc = Main.npc[npcWho];
				if( addRagePerc > 0f ) {
					if( npc?.active == true && npc.ai[1] > 0f ) {	// Chase
						return 0f;
					}
				}
				return addRagePerc;
			} },
			{ NPCID.GetUniqueKey(NPCID.Retinazer), (npcWho, oldRagePerc, addRagePerc) => {
				NPC npc = Main.npc[npcWho];
				if( addRagePerc > 0f ) {
					if( npc?.active == true && npc.ai[1] > 0f ) {	// Dash
						return 0f;
					}
				}
				return addRagePerc;
			} },
			{ NPCID.GetUniqueKey(NPCID.Spazmatism), (npcWho, oldRagePerc, addRagePerc) => {
				NPC npc = Main.npc[npcWho];
				if( addRagePerc > 0f ) {
					if( npc?.active == true && npc.ai[1] > 0f ) {	// Dash
						return 0f;
					}
				}
				return addRagePerc;
			} },
			{ NPCID.GetUniqueKey(NPCID.DukeFishron), (npcWho, oldRagePerc, addRagePerc) => {
				NPC npc = Main.npc[npcWho];
				if( addRagePerc > 0f ) {
					if( npc?.active == true && npc.ai[0] == 1f ) {	// Dash
						return 0f;
					}
				}
				return addRagePerc;
			} }
		};
	}
}