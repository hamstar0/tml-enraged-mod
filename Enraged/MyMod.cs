using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace Enraged {
	public partial class EnragedMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-enraged-mod";


		////////////////

		public static EnragedMod Instance { get; private set; }



		////////////////
		
		public EnragedMod() {
			EnragedMod.Instance = this;
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