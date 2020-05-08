using System;
using Terraria;
using Terraria.ModLoader;


namespace Enraged {
	partial class EnragedGlobalNPC : GlobalNPC {
		private int TargetUnharmedByMe = 0;

		private int TargetDamageBuffer = 0;


		////////////////

		public override bool CloneNewInstances => false;


		////////////////

		public float RageBuildupPercent { get; private set; } = 0f;

		////

		public bool IsTargetUnharmedByMe => this.TargetUnharmedByMe >= EnragedConfig.Instance.TargetUnharmedTickThreshold;



		////////////////

		private bool _IsUpdating = false;

		public override bool PreAI( NPC npc ) {
			if( npc.boss && !this._IsUpdating ) {
				if( npc.HasPlayerTarget ) {
					Player player = Main.player[npc.target];

					if( player?.active == true ) {
						this._IsUpdating = true;
						this.UpdateRageState( npc, player );
						this._IsUpdating = false;
					}
				}
			}
			return base.PreAI( npc );
		}


		////////////////

		public void Enrage( NPC npc ) {
			npc.AddBuff( ModContent.BuffType<EnragedBuff>(), EnragedConfig.Instance.RageDurationTicks );
		}
	}
}
