using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Misc;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace Enraged {
	class EnragedModData {
		public bool IsInitialized = false;
	}




	public partial class EnragedMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-enraged-mod";


		////////////////

		public static EnragedMod Instance { get; private set; }



		////////////////

		public EnragedMod() {
			EnragedMod.Instance = this;
		}

		public override void Load() {
			var data = ModCustomDataFileHelpers.LoadJson<EnragedModData>( this, "data" );
			if( data == null ) {
				data = new EnragedModData();
			}

			if( !data.IsInitialized ) {
				data.IsInitialized = true;
				EnragedConfig.Instance.Initialize();
			}

			if( !ModCustomDataFileHelpers.SaveAsJson<EnragedModData>( this, "data", true, data ) ) {
				LogHelpers.Warn( "Could not save Enraged mod data." );
			}
		}

		public override void Unload() {
			EnragedMod.Instance = null;
		}


		////////////////

		public override void AddRecipeGroups() {
			RecipeGroup.RegisterGroup(
				"Enraged: Strange Plants",
				new RecipeGroup(
					() => Language.GetTextValue("LegacyMisc.37")+" Strange Plant",
					ItemID.StrangePlant1,
					ItemID.StrangePlant2,
					ItemID.StrangePlant3,
					ItemID.StrangePlant4
				)
			);
		}
	}
}