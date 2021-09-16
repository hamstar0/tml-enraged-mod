using System;
using Terraria.ID;


namespace Enraged {
	public static class EnragedAPI {
		public static EnrangedBehaviorHook GetEnragedNpcBehaviorHook( int npcType ) {
			EnragedMod.Instance.EnragedNpcHooks.TryGetValue(
				NPCID.GetUniqueKey(npcType),
				out EnrangedBehaviorHook callback
			);
			return callback;
		}

		public static void SetEnragedNpcBehaviorHook( int npcType, EnrangedBehaviorHook callback ) {
			EnragedMod.Instance.EnragedNpcHooks[ NPCID.GetUniqueKey(npcType) ] = callback;
		}
	}
}
