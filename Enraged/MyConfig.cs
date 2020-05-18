using System;
using System.Collections.Generic;
using System.ComponentModel;
using Terraria.ModLoader.Config;
using Terraria.ID;
using HamstarHelpers.Classes.UI.ModConfig;
using HamstarHelpers.Helpers.DotNET.Reflection;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Configs;


namespace Enraged {
	class MyFloatInputElement : FloatInputElement { }




	class ConfigFloat {
		[CustomModConfigItem( typeof( FloatInputElement ) )]
		[Range( 0f, 100f )]
		[DefaultValue( 1f )]
		public float Value { get; set; } = 1f;


		////

		public ConfigFloat() { }

		public ConfigFloat( float value ) {
			this.Value = value;
		}
	}




	class EnragedConfig : StackableModConfig {
		public static EnragedConfig Instance => ModConfigStack.GetMergedConfigs<EnragedConfig>();



		////////////////

		public override ConfigScope Mode => ConfigScope.ServerSide;



		////////////////

		public bool DebugModeInfo { get; set; } = false;

		////

		[Header( "Rage buildup settings" )]
		[Range( 1, 60 * 60 * 60 )]
		[DefaultValue( 60 * 15 )]
		public int TargetUnharmedTickThreshold { get; set; } = 60 * 15;

		[Range( -1f, 1f )]
		[DefaultValue( (1f / 60f) / 60f )]
		[CustomModConfigItem( typeof(MyFloatInputElement) )]
		public float RagePercentGainPerTickFromUnharmedTarget { get; set; } = ( 1f / 60f ) / 60f;

		[Range( -1f, 1f )]
		[DefaultValue( (1f / 60f) / 60f )]
		[CustomModConfigItem( typeof(MyFloatInputElement) )]
		public float RagePercentGainPerTickFromTargetTooFar { get; set; } = ( 1f / 60f ) / 60f;

		[Range( -1f, 1f )]
		[DefaultValue( 0f )]
		[CustomModConfigItem( typeof(MyFloatInputElement) )]
		public float RagePercentGainPerTickFromTargetTooClose { get; set; } = 0f;

		[Range( -1f, 1f )]
		[DefaultValue( 1f / 60f )]
		[CustomModConfigItem( typeof(MyFloatInputElement) )]
		public float RagePercentGainPerHitTaken { get; set; } = 1f / 60f;

		[Range( -1f, 1f )]
		[DefaultValue( -1f / 60f )]
		[CustomModConfigItem( typeof(MyFloatInputElement) )]
		public float RagePercentGainPerTargetHitPer10 { get; set; } = -1f / 60f;

		////

		[Range( 4, 1000 )]
		[DefaultValue( 32 )]
		public int TileDistanceUntilTargetTooFar { get; set; } = 32;

		[Range( 4, 1000 )]
		[DefaultValue( 16 )]
		public int TileDistanceUntilTargetTooClose { get; set; } = 16;    // see `RagePercentGainPerTickFromTargetTooClose`

		[Range( 0, 60 * 60 )]
		[DefaultValue( 60 )]
		public int CooldownTickDurationBetweenHits { get; set; } = 60;

		////

		[Range( 1, 60 * 60 * 60 )]
		[DefaultValue( 60 * 10 )]
		public int RageDurationTicks { get; set; } = 60 * 10;


		////////////////

		[Header( "Enraged effects settings" )]
		[Range( 1, 10 )]
		[DefaultValue( 2 )]
		public int TimesToRunAIPerTickWhileEnraged { get; set; } = 2;

		[Range( 1, 64 )]
		[DefaultValue( 5 )]
		public int EnragedBrambleTrailThickness { get; set; } = 5;

		[Range( 0f, 1f )]
		[DefaultValue( 0.006f )]
		[CustomModConfigItem( typeof(MyFloatInputElement) )]
		public float EnragedBrambleTrailDensity { get; set; } = 0.006f;


		////////////////

		[DefaultValue( true )]
		public bool TranqHasRecipe { get; set; } = true;

		[DefaultValue( true )]
		public bool TranqSoldFromArmsDealer { get; set; } = true;

		[DefaultValue( true )]
		public bool TranqIsPvP { get; set; } = true;

		[Range( -1f, 1f )]
		[DefaultValue( -20f / 60f )]
		[CustomModConfigItem( typeof(MyFloatInputElement) )]
		public float TranqRagePercentAdd { get; set; } = -20f / 60f;

		[Range( 0, 60 * 60 * 60 )]
		[DefaultValue( 60 * 12 )]
		public int TranqDebuffTickDuration { get; set; } = 60 * 12;


		////////////////

		public Dictionary<NPCDefinition, ConfigFloat> RageMeterScales { get; set; } = new Dictionary<NPCDefinition, ConfigFloat>();



		////////////////

		public void Initialize() {
			this.RageMeterScales.Clear();

			this.RageMeterScales[ new NPCDefinition(NPCID.QueenBee) ] = new ConfigFloat( 0f );
			this.RageMeterScales[ new NPCDefinition(NPCID.WallofFlesh) ] = new ConfigFloat( 0f );
			this.RageMeterScales[ new NPCDefinition(NPCID.Retinazer) ] = new ConfigFloat( 0f );
			this.RageMeterScales[ new NPCDefinition(NPCID.Spazmatism) ] = new ConfigFloat( 0f );
			this.RageMeterScales[ new NPCDefinition(NPCID.CultistBoss) ] = new ConfigFloat( 0f );
			this.RageMeterScales[ new NPCDefinition(NPCID.MoonLordCore) ] = new ConfigFloat( 0f );

			object _;
			if( !ReflectionHelpers.RunMethod( typeof(ConfigManager), null, "Save", new object[] { this }, out _ ) ) {
				LogHelpers.Alert( "Could not save config defaults." );
			}
		}
	}
}
