using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using CursedBrambles;
using CursedBrambles.Tiles;


namespace Enraged.Buffs {
	partial class EnragedBuff : ModBuff {
		public static void ApplyExternalEffects_If_Host( NPC npc ) {
			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				return;
			}

			var mynpc = npc.GetGlobalNPC<EnragedGlobalNPC>();
			if( mynpc.BrambleBlockTimer >= 1 ) {
				return;
			}

			//

			var mymod = EnragedMod.Instance;
			string uid = NPCID.GetUniqueKey( npc.type );

			bool canBrambleTrail = true;

			if( mymod.EnragedNpcHooks.ContainsKey(uid) ) {
				(bool isBrambleTrail, bool _)? behavior = mymod
					.EnragedNpcHooks[uid]
					.Invoke( npc.whoAmI );

				canBrambleTrail = !behavior.HasValue || behavior.Value.isBrambleTrail;
			}

			//

			if( canBrambleTrail ) {
				EnragedBuff.ApplyBrambleTrail( npc );
			}
		}

		////

		public static void ApplyBrambleTrail( NPC npc ) {
			var config = EnragedConfig.Instance;
			int thickness = config.Get<int>( nameof( config.EnragedBrambleTrailWidth ) );
			float density = config.Get<float>( nameof( config.EnragedBrambleTrailDensity ) );

			if( thickness > 0 && density > 0f ) {
				int created = CursedBrambleTile.CreateBramblePatchAt_If(
					tileX: (int)npc.Center.X / 16,
					tileY: (int)npc.Center.Y / 16,
					radius: thickness,
					densityPercent: density,
					validateAt: CursedBramblesAPI.CreatePlayerAvoidingBrambleValidator( 10 ),
					sync: true
				);
			}
		}


		////////////////

		public static void ModifyHitStats_If( NPC npc, ref int damage, ref float knockback ) {
			var mymod = EnragedMod.Instance;
			string uid = NPCID.GetUniqueKey( npc.type );

			bool hasDamageResist = true;

			if( mymod.EnragedNpcHooks.ContainsKey(uid) ) {
				(bool _, bool isDamageResist)? behavior = mymod
					.EnragedNpcHooks[ uid ]
					.Invoke( npc.whoAmI );

				hasDamageResist = !behavior.HasValue || behavior.Value.isDamageResist;
			}

			//

			if( hasDamageResist ) {
				EnragedBuff.ApplyDamageResist( ref damage, ref knockback );
			}
		}

		////

		private static void ApplyDamageResist( ref int damage, ref float knockback ) {
			var config = EnragedConfig.Instance;
			float damageScale = config.Get<float>( nameof(config.EnragedDamageReceivedScale) );

			damage = Math.Max( (int)((float)damage * damageScale), 1 );
			//damage = Math.Max( (damage / 2) - 10, 1 );

			knockback = 0;
		}
	}
}
