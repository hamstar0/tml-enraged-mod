using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ModLibsGeneral.Libraries.XNA;
using ModLibsCamera.Classes.CameraAnimation;


namespace Enraged.Buffs {
	partial class EnragedBuff : ModBuff {
		internal static void ApplyVisualFx( NPC npc, ref Color drawColor, float lastKnownDrawScale ) {
			int npcWho = npc.whoAmI;

			float getMagnitude() {
				NPC mynpc = Main.npc[npcWho];
				if( mynpc.active != true || !mynpc.boss ) {
					CameraShaker.Current = null;
					return 0f;
				}

				float dist = (npc.Center - Main.LocalPlayer.Center).Length();
				float magnitudePercent = 1f - (dist / 768f);
				return 8f * magnitudePercent;
			}

			//

			// Add red tint
			var newColor = new Color( 255, 128, 128 );
			drawColor = XnaColorLibraries.Mul( drawColor, newColor );

			// Add NPC vibration
			npc.scale = ((Main.rand.NextFloat() * 0.2f) - 0.1f) + lastKnownDrawScale;

			//magnitude *= Main.LocalPlayer.Center.Y == 0f
			//	? magnitude
			//	: magnitude * 4f;
			CameraShaker.Current = new CameraShaker(
				name: "EnragedShake",
				peakMagnitude: getMagnitude(),
				toDuration: 0,
				lingerDuration: 30,
				froDuration: 0,
				isSmoothed: false,
				onRun: () => {
					CameraShaker.Current.SetPeakMagnitude( getMagnitude() );
				}
			);
		}
	}
}
