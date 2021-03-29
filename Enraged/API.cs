using System;
using Terraria.ID;


namespace Enraged {
	public static class EnragedAPI {
		public static RageValueOverride GetBossEnrageOverrides( int npcType ) {
			EnragedMod.Instance.RageOverrides.TryGetValue( NPCID.GetUniqueKey(npcType), out RageValueOverride callback );
			return callback;
		}

		public static void SetBossEnrageOverrides( int npcType, RageValueOverride callback ) {
			EnragedMod.Instance.RageOverrides[ NPCID.GetUniqueKey(npcType) ] = callback;
		}
	}
}
