using System;
using System.Collections.Generic;
using System.ComponentModel;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using ModLibsCore.Classes.UI.ModConfig;
using ModLibsCore.Libraries.Debug;


namespace Enraged {
	class MyFloatInputElement : FloatInputElement { }




	public class ConfigFloat {
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		[Range( 0f, 100f )]
		[DefaultValue( 1f )]
		public float Value { get; set; } = 1f;


		////

		public ConfigFloat() { }

		public ConfigFloat( float value ) {
			this.Value = value;
		}
	}




	public partial class EnragedConfig : ModConfig {
		public static EnragedConfig Instance => ModContent.GetInstance<EnragedConfig>();



		////////////////

		public override ConfigScope Mode => ConfigScope.ServerSide;



		////////////////

		public override ModConfig Clone() {
			var clone = base.Clone() as EnragedConfig;
			if( clone == null ) {
				return clone;
			}

			this.NpcWhitelist = new HashSet<NPCDefinition>( clone.NpcWhitelist );
			this.RageRateScales = new Dictionary<NPCDefinition, ConfigFloat>( clone.RageRateScales );

			return clone;
		}
	}
}
