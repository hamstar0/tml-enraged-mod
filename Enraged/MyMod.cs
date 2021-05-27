using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace Enraged {
	public partial class EnragedMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-enraged-mod";


		////////////////

		public static EnragedMod Instance => ModContent.GetInstance<EnragedMod>();



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