using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum WeaponOffset
{
    Idle, Running, Aiming, Reloading, Crouching
}

public class AgentEquipment : MonoBehaviour
{
    [SerializeField] Transform weaponHoldTarget;
    [SerializeField] float weaponOffsetChangeSpeed = 1f;
    [SerializeField] DamageSource damageSource;

    public event Action OnWeaponChange;

    public bool HasWeaponEquipped => CurrentWeaponGO != null;
    public bool HasTwoWeapons => PrimaryWeapon != null && SecondaryWeapon != null;
    public WeaponAttack CurrentWeaponAttack => CurrentWeapon?.attack;
    public WeaponAmmunition CurrentWeaponAmmunition => CurrentWeapon?.ammo;
    GameObject CurrentWeaponGO => CurrentWeapon?.gameObject;
    public Holdable CurrentHoldable => CurrentWeapon?.holdable;

    public Weapon CurrentWeapon { get; private set; }
    public Weapon PrimaryWeapon { get; private set; }
    public Weapon SecondaryWeapon { get; private set; }


    HumanoidIK ik;
    HumanoidAnimator humanAnim;

    Vector3 targetPosition;
    Quaternion targetRotation;

    WeaponOffset currentOffsetType;

    public class Weapon
    {
        public GameObject gameObject;
        public WeaponAnimation animation;
        public WeaponAttack attack;
        public WeaponAmmunition ammo;
        public Holdable holdable;

        public Weapon(GameObject gameObject)
        {
            this.gameObject = gameObject;
            attack = gameObject.GetComponent<WeaponAttack>();
            animation = gameObject.GetComponent<WeaponAnimation>();
            ammo = gameObject.GetComponent<WeaponAmmunition>();
            holdable = gameObject.GetComponent<Holdable>();
        }
    }

    private void Start()
    {
        ik = GetComponentInChildren<HumanoidIK>();
        humanAnim = GetComponentInChildren<HumanoidAnimator>();
        WeaponAttack[] weaponAttacks = GetComponentsInChildren<WeaponAttack>();
        if (weaponAttacks.Length > 0 && weaponAttacks[0] != null)
        {
            PrimaryWeapon = new Weapon(weaponAttacks[0].gameObject);
        }
        if (weaponAttacks.Length > 1 && weaponAttacks[1] != null)
        {
            SecondaryWeapon = new Weapon(weaponAttacks[1].gameObject);
        }
        if (PrimaryWeapon != null)
        {
            Equip(PrimaryWeapon);
        }
        if (SecondaryWeapon != null)
        {
            UnEquip(SecondaryWeapon);
        }
    }

    public void Equip(Weapon weapon)
    {
        if (weapon == null)
        {
            return;
        }
        CurrentWeapon = weapon;
        CurrentWeapon.gameObject.SetActive(true);
        weapon.gameObject.transform.SetParent(weaponHoldTarget);
        ik.SetHandTarget(Hand.Right, CurrentHoldable.RightHandPosition);
        ik.SetHandTarget(Hand.Left, CurrentHoldable.LeftHandPosition);
        humanAnim.SetAnimatorController(weapon.animation.AnimationSet);
        SetWeaponOffset(currentOffsetType);
        CurrentWeapon.attack.Source = damageSource;
        OnWeaponChange?.Invoke();
    }

    public void UnEquip(Weapon weapon)
    {
        if (weapon != null)
        {
            weapon.gameObject.SetActive(false);
        }
    }

    public void SetWeaponOffset(WeaponOffset offsetType)
    {
        currentOffsetType = offsetType;
        if (!HasWeaponEquipped)
        {
            return;
        }
        if (offsetType == WeaponOffset.Running)
        {
            targetRotation = Quaternion.Euler(CurrentHoldable.RunningRotation);
            targetPosition = CurrentHoldable.RunningOffset;
        }
        else if (offsetType == WeaponOffset.Aiming)
        {
            targetRotation = Quaternion.Euler(CurrentHoldable.AimingRotation);
            targetPosition = CurrentHoldable.AimingOffset;
        }
        else if (offsetType == WeaponOffset.Reloading)
        {
            targetRotation = Quaternion.Euler(CurrentHoldable.ReloadRotation);
            targetPosition = CurrentHoldable.ReloadOffset;
        }
        else if (offsetType == WeaponOffset.Crouching)
        {
            targetRotation = Quaternion.Euler(CurrentHoldable.CrouchingRotation);
            targetPosition = CurrentHoldable.CrouchingOffset;
        }
        else
        {
            targetRotation = Quaternion.Euler(CurrentHoldable.IdleRotation);
            targetPosition = CurrentHoldable.IdleOffset;
        }
    }

    private void Update()
    {
        if (CurrentWeaponGO)
        {
            CurrentWeaponGO.transform.localRotation = Quaternion.Lerp(CurrentWeaponGO.transform.localRotation, targetRotation, weaponOffsetChangeSpeed * Time.deltaTime);
            CurrentWeaponGO.transform.localPosition = Vector3.Lerp(CurrentWeaponGO.transform.localPosition, targetPosition, weaponOffsetChangeSpeed * Time.deltaTime);
        }
    }

    public void PickupWeapon(GameObject weaponGO)
    {
        weaponGO.transform.SetParent(weaponHoldTarget);
        Rigidbody weaponRB = weaponGO.GetComponent<Rigidbody>();
        weaponRB.isKinematic = true;
        Weapon newWeapon = new Weapon(weaponGO);
        weaponGO.GetComponent<BoxCollider>().enabled = false;
        if (PrimaryWeapon == null)
        {
            PrimaryWeapon = newWeapon;
        }
        else if (SecondaryWeapon == null)
        {
            SecondaryWeapon = newWeapon;
        }
        if (CurrentWeapon != null)
        {
            UnEquip(CurrentWeapon);
        }
        Equip(newWeapon);
    }

    public void DropWeapon()
    {
        if (CurrentWeapon != null)
        {
            if (CurrentWeapon == PrimaryWeapon)
            {
                PrimaryWeapon = null;
            }
            else
            {
                SecondaryWeapon = null;
            }
            CurrentWeaponGO.transform.SetParent(null, true);
            CurrentWeaponGO.transform.Translate(transform.forward * .5f);
            Rigidbody weaponRB = CurrentWeaponGO.GetComponent<Rigidbody>();
            weaponRB.isKinematic = false;
            CurrentWeaponGO.GetComponent<BoxCollider>().enabled = true;
            CurrentWeapon = null;
        }
    }

    public bool TrySwitchWeapon()
    {
        if (SecondaryWeapon == null)
        {
            return false;
        }
        if (CurrentWeapon == PrimaryWeapon)
        {
            UnEquip(PrimaryWeapon);
            Equip(SecondaryWeapon);
        }
        else
        {
            UnEquip(SecondaryWeapon);
            Equip(PrimaryWeapon);
        }
        return true;
    }

    public void RefillPercentAmmoAllWeapons(float percent)
    {
        if (PrimaryWeapon != null)
        {
            PrimaryWeapon.ammo.AddAmmo((int)(PrimaryWeapon.ammo.MaxCarriedAmmo * percent));
        }
        if (SecondaryWeapon != null)
        {
            SecondaryWeapon.ammo.AddAmmo((int)(SecondaryWeapon.ammo.MaxCarriedAmmo * percent));
        }
    }
}
