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

			Texture2D gauge = this.mod.GetTexture( "UI/PressureGauge" );
			Texture2D dial = this.mod.GetTexture( "UI/PressureGaugeDial" );

			var origin = new Vector2( gauge.Width/2, gauge.Height/2 );
			float rot = MathHelper.ToRadians( this.RageBuildupPercent * 180f );
			position -= Main.screenPosition;
			position.Y -= 12f;

			float opacity = 0.05f + (this.RageBuildupPercent * 0.95f);
			opacity += (1f - opacity) * Math.Min( this.RecentRagePercentChange * 60f, 1f );
//DebugHelpers.Print( "opac", "opac:"+opacity.ToString("N2")+", perc:"+this.RageBuildupPercent.ToString("N2")+", change%:"+(this.RecentRagePercentChange * 60f));

			//double secPerc = (double)DateTime.Now.Millisecond / 1000d;
			//secPerc = (DateTime.Now.Second & 1) == 1 ? 1f - secPerc : secPerc;
			//float dialScale = (float)Math.Sin( secPerc * (1f + (32f * this.RageBuildupPercent)) );
			//dialScale *= 0.05f;
			float dialScale = 1.1f;

			Main.spriteBatch.Draw( gauge, position, null, Color.White * opacity, 0f, origin, dialScale, SpriteEffects.None, 1f );
			Main.spriteBatch.Draw( dial, position, null, Color.White * opacity, rot, origin, dialScale, SpriteEffects.None, 1f );

			if( (Main.MouseScreen - position).LengthSquared() < 2304f ) {
				string percStr = (this.RageBuildupPercent * 100f).ToString("N0") + "%";
				var percColor = Color.Lerp( Color.Lime, Color.Red, this.RageBuildupPercent );

				Utils.DrawBorderStringFourWay(
					sb: Main.spriteBatch,
					font: Main.fontMouseText,
					text: percStr,
					x: Main.MouseScreen.X + 16f,
					y: Main.MouseScreen.Y - 16f,
					textColor: percColor,
					borderColor: Color.Black,
					origin: Vector2.Zero,
					scale: 1.25f
				);
			}

			return base.DrawHealthBar( npc, hbPosition, ref scale, ref position );
		}
	}
}
