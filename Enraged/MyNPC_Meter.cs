using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using Enraged.Buffs;


namespace Enraged {
	partial class EnragedGlobalNPC : GlobalNPC {
		public override bool? DrawHealthBar( NPC npc, byte hbPosition, ref float scale, ref Vector2 position ) {
			if( npc.HasBuff(ModContent.BuffType<EnragedBuff>()) ) {
				return false;
			}

			if( this.RageBuildupPercent < 0.01f ) {
				return base.DrawHealthBar( npc, hbPosition, ref scale, ref position );
			}

//DebugHelpers.Print( "rages", "built%: "+this.RageBuildupPercent.ToString("N2")
//	+", change%: "+(this.RecentRagePercentChange * 60f).ToString("N3") );
			this.DrawRageGauge( scale, position, this.RageBuildupPercent, this.RecentRagePercentChange );

			return base.DrawHealthBar( npc, hbPosition, ref scale, ref position );
		}


		private void DrawRageGauge( float scale, Vector2 position, float ragePercent, float rageChangePercent ) {
			Texture2D gauge = this.mod.GetTexture( "UI/PressureGauge" );
			Texture2D dial = this.mod.GetTexture( "UI/PressureGaugeDial" );

			float shake = Math.Min( rageChangePercent * 60f, 1f );
			float pulse = (float)Main.mouseTextColor / 255f;

			var origin = new Vector2( gauge.Width/2, gauge.Height/2 );
			float rot = MathHelper.ToRadians( ragePercent * 180f );
			position -= Main.screenPosition;
			position.Y -= 12f;
			position.X += (Main.rand.NextFloat(shake) - 0.5f) * 3f;
			position.Y += (Main.rand.NextFloat(shake) - 0.5f) * 3f;

			float opacity = 0.1f + (ragePercent * 0.9f);
			opacity += (1f - opacity) * shake;
//DebugHelpers.Print( "opac", "opac:"+opacity.ToString("N2")+", perc:"+this.RageBuildupPercent.ToString("N2")+", change%:"+(this.RecentRagePercentChange * 60f));

			//double secPerc = (double)DateTime.Now.Millisecond / 1000d;
			//secPerc = (DateTime.Now.Second & 1) == 1 ? 1f - secPerc : secPerc;
			//float dialScale = (float)Math.Sin( secPerc * (1f + (32f * this.RageBuildupPercent)) );
			//dialScale *= 0.05f;
			float gaugeScale = (scale * 0.9f) + (shake * 0.25f);
			gaugeScale = (0.65f * gaugeScale) + (0.35f * ragePercent);

			Main.spriteBatch.Draw(
				texture: gauge,
				position: position,
				sourceRectangle: null,
				color: Color.White * opacity * pulse,
				rotation: 0f,
				origin: origin,
				scale: gaugeScale,
				effects: SpriteEffects.None,
				layerDepth: 1f
			);
			Main.spriteBatch.Draw(
				texture: dial,
				position: position,
				sourceRectangle: null,
				color: Color.White * opacity * pulse * pulse,
				rotation: rot,
				origin: origin,
				scale: gaugeScale,
				effects: SpriteEffects.None,
				layerDepth: 1f
			);

			if( (Main.MouseScreen - position).LengthSquared() < 2304f ) {
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
