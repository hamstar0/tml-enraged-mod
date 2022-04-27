using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using Enraged.Buffs;


namespace Enraged {
	partial class EnragedGlobalNPC : GlobalNPC {
		private Color LitColor = Color.White;



		////////////////

		public override void PostDraw( NPC npc, SpriteBatch spriteBatch, Color drawColor ) {
			this.LitColor = drawColor;
		}

		////////////////

		public override bool? DrawHealthBar( NPC npc, byte hbPosition, ref float scale, ref Vector2 position ) {
			if( npc.HasBuff(ModContent.BuffType<EnragedBuff>()) ) {
				return false;
			}

			if( this.RagePercent < 0.01f ) {
				return true;
			}
			
//DebugLibraries.Print( "rages", "built%: "+this.RageBuildupPercent.ToString("N2")
//	+", change%: "+(this.RecentRagePercentChange * 60f).ToString("N3") );
			this.DrawRageGauge( scale, position, this.RagePercent, this.RecentRagePercentChangeChaser );

			return true;
		}


		private void DrawRageGauge( float scale, Vector2 worldPos, float ragePercent, float rageChangePercent ) {
			Texture2D gauge = this.mod.GetTexture( "UI/PressureGauge" );
			Texture2D dial = this.mod.GetTexture( "UI/PressureGaugeDial" );

			//

			float shake = Math.Min( rageChangePercent * 60f, 1f );
			float pulse = (float)Main.mouseTextColor / 255f;

			float rot = MathHelper.ToRadians( ragePercent * 180f );

			//

			Vector2 gaugeMidPos = worldPos - Main.screenPosition;
			gaugeMidPos.Y -= 12f;
			gaugeMidPos.X += (Main.rand.NextFloat(shake) - 0.5f) * 4f;
			gaugeMidPos.Y += (Main.rand.NextFloat(shake) - 0.5f) * 4f;

			//

			float opacity = 0.1f + (ragePercent * 0.9f);
			opacity += (1f - opacity) * shake;
//DebugLibraries.Print( "opac", "opac:"+opacity.ToString("N2")+", perc:"+this.RageBuildupPercent.ToString("N2")+", change%:"+(this.RecentRagePercentChange * 60f));

			//double secPerc = (double)DateTime.Now.Millisecond / 1000d;
			//secPerc = (DateTime.Now.Second & 1) == 1 ? 1f - secPerc : secPerc;
			//float dialScale = (float)Math.Sin( secPerc * (1f + (32f * this.RageBuildupPercent)) );
			//dialScale *= 0.05f;
			float gaugeScale = (scale * 0.9f) + (shake * 0.25f);
			gaugeScale = (0.65f * gaugeScale) + (0.35f * ragePercent);

			float gaugeLerp = (ragePercent * 0.25f) + (ragePercent * 0.75f * Main.rand.NextFloat());
			Color gaugeColor = Color.Lerp( this.LitColor, Color.White, gaugeLerp );
			gaugeColor *= opacity * pulse * 0.85f;
			Color dialColor = Color.White * opacity * pulse * pulse;

			//

			Main.spriteBatch.Draw(
				texture: gauge,
				position: gaugeMidPos,
				sourceRectangle: null,
				color: gaugeColor,
				rotation: 0f,
				origin: new Vector2( gauge.Width, gauge.Height ) * 0.5f,
				scale: gaugeScale,
				effects: SpriteEffects.None,
				layerDepth: 1f
			);
			Main.spriteBatch.Draw(
				texture: dial,
				position: gaugeMidPos,
				sourceRectangle: null,
				color: dialColor,
				rotation: rot,
				origin: new Vector2( dial.Width, dial.Height ) * 0.5f,
				scale: gaugeScale,
				effects: SpriteEffects.None,
				layerDepth: 1f
			);

			if( (Main.MouseScreen - gaugeMidPos).LengthSquared() < 2304f ) {
				string percStr = (ragePercent * 100f).ToString("N0") + "%";
				var percColor = Color.Lerp( Color.Lime, Color.Red, ragePercent );

				Utils.DrawBorderStringFourWay(
					sb: Main.spriteBatch,
					font: Main.fontMouseText,
					text: percStr,
					x: Main.MouseScreen.X + 16f,
					y: Main.MouseScreen.Y - 16f,
					textColor: percColor * pulse,
					borderColor: Color.Black,
					origin: Vector2.Zero,
					scale: 1.25f
				);
			}
		}
	}
}
