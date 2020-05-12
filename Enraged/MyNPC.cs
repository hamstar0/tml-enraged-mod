using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Draw;
using Enraged.Buffs;


namespace Enraged {
	partial class EnragedGlobalNPC : GlobalNPC {
		private int TargetUnharmedByMe = 0;

		private int TargetDamageBuffer = 0;


		////////////////

		public float RageBuildupPercent { get; private set; } = 0f;

		////

		public bool IsTargetUnharmedByMe => this.TargetUnharmedByMe >= EnragedConfig.Instance.TargetUnharmedTickThreshold;


		////////////////

		public override bool CloneNewInstances => false;

		public override bool InstancePerEntity => true;



		////////////////

		 private bool _IsUpdating = false;

		public override bool PreAI( NPC npc ) {
			if( npc.boss && !this._IsUpdating ) {
				if( npc.HasPlayerTarget ) {
					Player player = Main.player[npc.target];
					if( player?.active == true ) {
						this._IsUpdating = true;
						this.UpdateRageBehavior( npc, player );
						this._IsUpdating = false;
					}
				}
			}
			return base.PreAI( npc );
		}

		private void UpdateRageBehavior( NPC npc, Player targetPlr ) {
			this.UpdateRageState( npc, targetPlr );

			if( npc.HasBuff(ModContent.BuffType<EnragedBuff>()) ) {
				this.UpdateEnragedEffects( npc, targetPlr );
			}
		}


		////

		public override bool? DrawHealthBar( NPC npc, byte hbPosition, ref float scale, ref Vector2 position ) {
			if( npc.HasBuff(ModContent.BuffType<EnragedBuff>()) ) {
				return false;
			}

			float alpha = Lighting.Brightness(
				(int)(npc.Center.X / 16f),
				(int)(npc.gfxOffY + (npc.Center.Y/16f))
			);

			position = new Vector2(
				npc.position.X + (npc.width / 2),
				npc.position.Y + npc.gfxOffY );
			if( hbPosition == 1 ) {
				position.Y += npc.height + 10f + Main.NPCAddHeight( npc.whoAmI );
			} else if( hbPosition == 2 ) {
				position.Y -= 24f + (Main.NPCAddHeight(npc.whoAmI) / 2f);
			}

			Main.instance.DrawHealthBar( position.X, position.Y, npc.life, npc.lifeMax, alpha, scale );

			var rect = new Rectangle(
				(int)(position.X - Main.screenPosition.X - (15.5f * scale)),	//16f
				(int)(position.Y - Main.screenPosition.Y + (8.5f * scale)),	//6f
				(int)(32f * scale),
				(int)(2f * scale)  //3f
			);

			DrawHelpers.DrawBorderedRect( Main.spriteBatch, Color.Lerp(Color.Red, Color.Black, 0.8f), null, rect, 1 );

			rect.Width = (int)( 30f * scale * this.RageBuildupPercent );
			//Main.spriteBatch.Draw( Main.magicPixel, rect, Color.Red );
			DrawHelpers.DrawBorderedRect( Main.spriteBatch, Color.Red, null, rect, 1 );

			return false;
		}
	}
}
