using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;


namespace Enraged {
	public delegate float RageValueOverride( int npcWho, float oldRagePercent, float newRagePercent );




	public partial class EnragedMod : Mod {
		internal IDictionary<string, RageValueOverride> RageOverrides = new Dictionary<string, RageValueOverride> {
			{ NPCID.GetUniqueKey(NPCID.EyeofCthulhu), (npcWho, oldRagePerc, newRagePerc) => {
				NPC npc = Main.npc[npcWho];
				if( npc?.active == true && npc.ai[1] > 0f ) {
					return oldRagePerc >= 1f ? 0.999999f : oldRagePerc;
				}
				return newRagePerc;
			} },
			{ NPCID.GetUniqueKey(NPCID.QueenBee), (npcWho, oldRagePerc, newRagePerc) => {
				NPC npc = Main.npc[npcWho];
				if( npc?.active == true && npc.ai[0] == 0f ) {
					return oldRagePerc >= 1f ? 0.999999f : oldRagePerc;
				}
				return newRagePerc;
			} },
			{ NPCID.GetUniqueKey(NPCID.BrainofCthulhu), (npcWho, oldRagePerc, newRagePerc) => {
				NPC npc = Main.npc[npcWho];
				if( npc?.active == true && npc.ai[0] < 0f ) {
					return oldRagePerc >= 1f ? 0.999999f : oldRagePerc;
				}
				return newRagePerc;
			} },
			{ NPCID.GetUniqueKey(NPCID.SkeletronHead), (npcWho, oldRagePerc, newRagePerc) => {
				NPC npc = Main.npc[npcWho];
				if( npc?.active == true && npc.ai[1] > 0f ) {
					return oldRagePerc >= 1f ? 0.999999f : oldRagePerc;
				}
				return newRagePerc;
			} },
			{ NPCID.GetUniqueKey(NPCID.SkeletronPrime), (npcWho, oldRagePerc, newRagePerc) => {
				NPC npc = Main.npc[npcWho];
				if( npc?.active == true && npc.ai[1] > 0f ) {
					return oldRagePerc >= 1f ? 0.999999f : oldRagePerc;
				}
				return newRagePerc;
			} },
			{ NPCID.GetUniqueKey(NPCID.Retinazer), (npcWho, oldRagePerc, newRagePerc) => {
				NPC npc = Main.npc[npcWho];
				if( npc?.active == true && npc.ai[1] > 0f ) {
					return oldRagePerc >= 1f ? 0.999999f : oldRagePerc;
				}
				return newRagePerc;
			} },
			{ NPCID.GetUniqueKey(NPCID.Spazmatism), (npcWho, oldRagePerc, newRagePerc) => {
				NPC npc = Main.npc[npcWho];
				if( npc?.active == true && npc.ai[1] > 0f ) {
					return oldRagePerc >= 1f ? 0.999999f : oldRagePerc;
				}
				return newRagePerc;
			} },
			{ NPCID.GetUniqueKey(NPCID.DukeFishron), (npcWho, oldRagePerc, newRagePerc) => {
				NPC npc = Main.npc[npcWho];
				if( npc?.active == true && npc.ai[0] == 1f ) {
					return oldRagePerc >= 1f ? 0.999999f : oldRagePerc;
				}
				return newRagePerc;
			} }
		};
	}
}