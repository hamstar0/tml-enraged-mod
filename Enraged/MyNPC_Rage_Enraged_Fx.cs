﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Classes.CameraAnimation;
using HamstarHelpers.Helpers.XNA;


namespace Enraged {
	partial class EnragedGlobalNPC : GlobalNPC {
		private float _BaseScale = 1;



		////////////////

		private void RevertVisualFx( NPC npc ) {
			this._BaseScale = npc.scale;
		}


		private void ApplyVisualFx( NPC npc, ref Color drawColor ) {
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
			drawColor = XNAColorHelpers.Mul( drawColor, newColor );

			// Add NPC vibration
			npc.scale = ((Main.rand.NextFloat() * 0.2f) - 0.1f) + this._BaseScale;

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
