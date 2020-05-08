using Terraria.ModLoader;


namespace Enraged {
	public class EnragedMod : Mod {
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
	}
}