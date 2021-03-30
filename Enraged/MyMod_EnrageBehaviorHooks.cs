using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;


namespace Enraged {
	public delegate (bool IsMakingBrambleTrail, bool IsDamageResistant)? EnrangedBehaviorHook( int npcWho );




	public partial class EnragedMod : Mod {
		internal IDictionary<string, EnrangedBehaviorHook> EnragedNpcHooks = new Dictionary<string, EnrangedBehaviorHook> {
			{ NPCID.GetUniqueKey(NPCID.EyeofCthulhu), (npcWho) => {
				NPC npc = Main.npc[npcWho];
				if( npc?.active == true && npc.ai[1] > 0f ) {	// Dash
					return (false, false);
				}
				return null;
			} },
			{ NPCID.GetUniqueKey(NPCID.QueenBee), (npcWho) => {
				NPC npc = Main.npc[npcWho];
				if( npc?.active == true && npc.ai[0] == 0f ) {	// Dash
					return (false, false);
				}
				return null;
			} },
			{ NPCID.GetUniqueKey(NPCID.BrainofCthulhu), (npcWho) => {
				NPC npc = Main.npc[npcWho];
				if( npc?.active == true && npc.ai[0] < 0f ) {	// Tricky charges
					return (false, false);
				}
				return null;
			} },
			{ NPCID.GetUniqueKey(NPCID.SkeletronHead), (npcWho) => {
				NPC npc = Main.npc[npcWho];
				if( npc?.active == true && npc.ai[1] > 0f ) {	// Chase
					return (false, false);
				}
				return null;
			} },
			{ NPCID.GetUniqueKey(NPCID.SkeletronPrime), (npcWho) => {
				NPC npc = Main.npc[npcWho];
				if( npc?.active == true && npc.ai[1] > 0f ) {	// Chase
					return (false, false);
				}
				return null;
			} },
			{ NPCID.GetUniqueKey(NPCID.Retinazer), (npcWho) => {
				NPC npc = Main.npc[npcWho];
				if( npc?.active == true && npc.ai[1] > 0f ) {	// Dash
					return (false, false);
				}
				return null;
			} },
			{ NPCID.GetUniqueKey(NPCID.Spazmatism), (npcWho) => {
				NPC npc = Main.npc[npcWho];
				if( npc?.active == true && npc.ai[1] > 0f ) {	// Dash
					return (false, false);
				}
				return null;
			} },
			{ NPCID.GetUniqueKey(NPCID.DukeFishron), (npcWho) => {
				NPC npc = Main.npc[npcWho];
				if( npc?.active == true && npc.ai[0] == 1f ) {	// Dash
					return (false, false);
				}
				return null;
			} }
		};
	}
}