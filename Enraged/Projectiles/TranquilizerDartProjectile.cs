using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Enraged.Projectiles {
	public class TranquilizerDartProjectile : ModProjectile {
		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Tranquilizer Dart" );
			ProjectileID.Sets.TrailCacheLength[ this.projectile.type ] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[ this.projectile.type ] = 0;        //The recording mode
		}

		public override void SetDefaults() {
			this.projectile.width = 14;
			this.projectile.height = 14;
			this.projectile.aiStyle = 1;
			this.projectile.friendly = true;
			this.projectile.hostile = true;//false;
			this.projectile.ranged = true;
			//this.projectile.penetrate = 0;
			//this.projectile.timeLeft = 600;
			//this.projectile.ignoreWater = true;
			//this.projectile.tileCollide = true;
			//this.projectile.extraUpdates = 1;
			this.aiType = ProjectileID.PoisonDart;
		}

		public override void Kill( int timeLeft ) {
			Collision.HitTiles( projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height );
			Main.PlaySound( SoundID.Item10, projectile.position );
		}


		////

		public override void OnHitNPC( NPC target, int damage, float knockback, bool crit ) {
			if( target.boss ) {
				var mynpc = target.GetGlobalNPC<EnragedGlobalNPC>();
				mynpc.AddRage( "tranq", target, EnragedConfig.Instance.TranqRagePercentAdd );
			} else {
				target.AddBuff( BuffID.Weak, EnragedConfig.Instance.TranqDebuffTickDuration );
				target.AddBuff( BuffID.Slow, EnragedConfig.Instance.TranqDebuffTickDuration );
				target.AddBuff( BuffID.Confused, EnragedConfig.Instance.TranqDebuffTickDuration );
			}
		}

		public override bool CanHitPvp( Player target ) {
			return EnragedConfig.Instance.TranqIsPvP;
		}

		public override void OnHitPlayer( Player target, int damage, bool crit ) {
			target.AddBuff( BuffID.Weak, EnragedConfig.Instance.TranqDebuffTickDuration );
			target.AddBuff( BuffID.Slow, EnragedConfig.Instance.TranqDebuffTickDuration );
			target.AddBuff( BuffID.Confused, EnragedConfig.Instance.TranqDebuffTickDuration );
		}
	}
}
