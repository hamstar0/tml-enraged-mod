using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;
using HamstarHelpers.Services.Configs;


namespace Enraged {
	//class MyFloatInputElement : FloatInputElement { }
	//[CustomModConfigItem( typeof( MyFloatInputElement ) )]




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
		[DefaultValue( 1f / 60f )]
		public float RagePercentGainPerTickFromUnharmedTarget { get; set; } = 1f / 60f;

		[Range( -1f, 1f )]
		[DefaultValue( 1f / 60f )]
		public float RagePercentGainPerTickFromTargetFleeing { get; set; } = 1f / 60f;

		[Range( -1f, 1f )]
		[DefaultValue( 0f )]
		public float RagePercentGainPerTickFromTargetTooClose { get; set; } = 0f;

		[Range( -1f, 1f )]
		[DefaultValue( 0f )]
		public float RagePercentGainPerHitTaken { get; set; } = 0f;

		[Range( -1f, 1f )]
		[DefaultValue( 0f )]
		public float RagePercentGainPerTargetHitPer10 { get; set; } = -1f / 60f;

		////

		[Range( 4, 1000 )]
		[DefaultValue( 16 )]
		public int MinimumTileDistanceBeforeRageGain { get; set; } = 16;

		[Range( 4, 1000 )]
		[DefaultValue( 16 )]
		public int MaximumTileDistanceBeforeRageGain { get; set; } = 16;

		[Range( 0, 60 * 60 )]
		[DefaultValue( 60 )]
		public int CooldownTickDurationBetweenHits { get; set; } = 60;

		////

		[Range( 1, 60 * 60 * 60 )]
		[DefaultValue( 60 * 20 )]
		public int RageDurationTicks { get; set; } = 60 * 20;


		////////////////

		[Header( "Enraged effects settings" )]
		[Range( 1, 10 )]
		[DefaultValue( 2 )]
		public int TimesToRunAIPerTickWhileEnraged { get; set; } = 2;

		[Range( 1, 64 )]
		[DefaultValue( 4 )]
		public int EnragedBrambleTrailThickness { get; set; } = 4;

		[Range( 0f, 32f )]
		[DefaultValue( 1f )]
		public float EnragedBrambleTrailDensity { get; set; } = 1f;
	}
}
