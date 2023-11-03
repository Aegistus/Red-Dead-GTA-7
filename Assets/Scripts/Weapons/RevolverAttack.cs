using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolverAttack : RangedWeaponAttack
{
    int shootSoundID;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        shootSoundID = SoundManager.Instance.GetSoundID("Revolver_Shoot");
    }

    public override void BeginAttack()
    {
        if (weaponAmmo.CurrentLoadedAmmo > 0 && !weaponAmmo.Reloading)
        {
            SpawnProjectile();
        }
    }

    public override void DuringAttack()
    {

    }

    public override void EndAttack()
    {

    }

    void SpawnProjectile()
    {
        if (weaponAmmo.TryUseAmmo())
        {
            GameObject projectile = PoolManager.Instance.SpawnObjectWithLifetime(projectileID, projectileSpawnPoint.position, projectileSpawnPoint.rotation, 10f);
            float damage = Random.Range(damageMin, damageMax);
            projectile.GetComponent<Projectile>().SetDamage(damage, Source);
            ApplyRecoil();
            SoundManager.Instance.PlaySoundAtPosition(shootSoundID, projectileSpawnPoint.position);
        }
    }
}
