using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using CursedBrambles.Tiles;


namespace Enraged.Buffs {
	partial class EnragedBuff : ModBuff {
		public static void ApplyExternalEffectsIf( NPC npc ) {
			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				return;
			}

			//

			var mymod = EnragedMod.Instance;
			string uid = NPCID.GetUniqueKey( npc.type );

			if( mymod.EnragedNpcHooks.ContainsKey(uid) ) {
				(bool isBramble, bool _)? behavior = mymod.EnragedNpcHooks[uid].Invoke( npc.whoAmI );
				if( behavior.HasValue && !behavior.Value.isBramble ) {
					return;
				}
			}

			//

			var config = EnragedConfig.Instance;
			int thickness = config.Get<int>( nameof( EnragedConfig.EnragedBrambleTrailThickness ) );
			float density = config.Get<float>( nameof( EnragedConfig.EnragedBrambleTrailDensity ) );

			if( thickness > 0 && density > 0f ) {
				int created = CursedBrambleTile.CreateBramblePatchAt(
					tileX: (int)npc.Center.X / 16,
					tileY: (int)npc.Center.Y / 16,
					radius: thickness,
					densityPercent: density,
					sync: true
				);
			}
		}


		////

		public static void ModifyHitIf( NPC npc, ref int damage, ref float knockback ) {
			var mymod = EnragedMod.Instance;
			string uid = NPCID.GetUniqueKey( npc.type );

			if( mymod.EnragedNpcHooks.ContainsKey( uid ) ) {
				(bool _, bool isDamageResist)? behavior = mymod.EnragedNpcHooks[uid].Invoke( npc.whoAmI );
				if( behavior.HasValue && !behavior.Value.isDamageResist ) {
					return;
				}
			}

			//

			damage = Math.Max( (damage / 2) - 10, 1 );
			knockback = 0;
		}
	}
}
