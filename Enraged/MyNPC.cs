using System;
using Terraria;
using Terraria.ModLoader;
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
	}
}
