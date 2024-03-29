﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using ModLibsCore.Classes.UI.ModConfig;
using ModLibsCore.Libraries.Debug;


namespace Enraged {
	public partial class EnragedConfig : ModConfig {
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

		////

		[Range( 0, 512 )]
		[DefaultValue( 96 )]
		public int EnrageBrambleBloomSize { get; set; } = 96;


		////////////////

		//[Header( "Enraged effects settings" )]
		//[Range( 1, 10 )]
		//[DefaultValue( 2 )]
		//public int TimesToRunAIPerTickWhileEnraged { get; set; } = 2;

		[Range( 1, 64 )]
		[DefaultValue( 5 )]
		public int EnragedBrambleTrailWidth { get; set; } = 5;

		[Range( 0f, 1f )]
		[DefaultValue( 0.003f )]	// was 0.006
		[CustomModConfigItem( typeof(MyFloatInputElement) )]
		public float EnragedBrambleTrailDensity { get; set; } = 0.003f;


		////////////////
		

		[Range( 0f, 10f )]
		[DefaultValue( 0.25f )]
		[CustomModConfigItem( typeof(MyFloatInputElement) )]
		public float EnragedDamageReceivedScale { get; set; } = 0.25f;


		////////////////

		[DefaultValue( true )]
		public bool TranqHasRecipe { get; set; } = true;

		[DefaultValue( false )]
		public bool TranqSoldFromArmsDealer { get; set; } = false;

		[DefaultValue( true )]
		public bool TranqSoldFromWitchDoctor { get; set; } = true;

		[DefaultValue( true )]
		public bool TranqIsPvP { get; set; } = true;

		[Range( -1f, 1f )]
		[DefaultValue( -20f / 60f )]
		[CustomModConfigItem( typeof(MyFloatInputElement) )]
		public float TranqRagePercentAdd { get; set; } = -20f / 60f;

		[Range( 0, 60 * 60 * 60 )]
		[DefaultValue( 60 * 12 )]
		public int TranqDebuffTickDuration { get; set; } = 60 * 12;

		[DefaultValue( false )]
		public bool TranqCausesConfuseToNonBossEnemies { get; set; } = false;


		////////////////

		public HashSet<NPCDefinition> NpcWhitelist { get; set; } = new HashSet<NPCDefinition> {
			new NPCDefinition( NPCID.EyeofCthulhu ),
			new NPCDefinition( NPCID.KingSlime ),
			new NPCDefinition( NPCID.QueenBee ),
			//new NPCDefinition( NPCID.BrainofCthulhu ),
			//new NPCDefinition( NPCID.EaterofWorldsHead ),
			new NPCDefinition( NPCID.SkeletronHead ),
			//new NPCDefinition( NPCID.WallofFlesh ),
			//new NPCDefinition( NPCID.TheDestroyer ),
			new NPCDefinition( NPCID.Retinazer ),
			new NPCDefinition( NPCID.Spazmatism ),
			new NPCDefinition( NPCID.SkeletronPrime ),
			new NPCDefinition( NPCID.Plantera ),
			new NPCDefinition( NPCID.Golem ),
			new NPCDefinition( NPCID.DukeFishron ),
			//new NPCDefinition( NPCID.DD2Betsy ),
			//new NPCDefinition( NPCID.MoonLordCore ),
		};

		public Dictionary<NPCDefinition, ConfigFloat> RageRateScales { get; set; } = new Dictionary<NPCDefinition, ConfigFloat> {
			{ new NPCDefinition(NPCID.SkeletronHead), new ConfigFloat( 0.5f ) },
			{ new NPCDefinition(NPCID.Retinazer), new ConfigFloat( 0.5f ) },
			{ new NPCDefinition(NPCID.Spazmatism), new ConfigFloat( 0.5f ) },
			{ new NPCDefinition(NPCID.DukeFishron), new ConfigFloat( 0.35f ) }
		};
	}
}
