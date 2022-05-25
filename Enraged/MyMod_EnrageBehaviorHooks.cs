using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace Enraged {
	public delegate (bool IsMakingBrambleTrail, bool IsDamageResistant)? EnrangedBehaviorHook( int npcWho );




	public partial class EnragedMod : Mod {
		internal IDictionary<string, bool> EnragedNpcCannotBrambleBloom = new Dictionary<string, bool> {
			{ NPCID.GetUniqueKey(NPCID.QueenBee), true },
			{ NPCID.GetUniqueKey(NPCID.EaterofWorldsHead), true },
			{ NPCID.GetUniqueKey(NPCID.BrainofCthulhu), true },
			{ NPCID.GetUniqueKey(NPCID.WallofFlesh), true },
			{ NPCID.GetUniqueKey(NPCID.WallofFleshEye), true },
			{ NPCID.GetUniqueKey(NPCID.TheDestroyer), true },
			{ NPCID.GetUniqueKey(NPCID.Retinazer), true },
			{ NPCID.GetUniqueKey(NPCID.Spazmatism), true },
			{ NPCID.GetUniqueKey(NPCID.Golem), true },
			{ NPCID.GetUniqueKey(NPCID.MoonLordCore), true },
			{ NPCID.GetUniqueKey(NPCID.MoonLordHead), true },	//i forget
			//
			{ NPCID.GetUniqueKey(NPCID.DD2Betsy), true },
			{ NPCID.GetUniqueKey(NPCID.Pumpking), true },
			{ NPCID.GetUniqueKey(NPCID.MourningWood), true },
			{ NPCID.GetUniqueKey(NPCID.SantaNK1), true },
			{ NPCID.GetUniqueKey(NPCID.Everscream), true },
			{ NPCID.GetUniqueKey(NPCID.IceQueen), true }
		};


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