using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] int ammoClip;
    [SerializeField] int maxAmmoClip;
    [SerializeField] int ammo;
    [SerializeField] int maxAmmo;

    void Update()
    {
        if (Input.GetKey(KeyCode.R)) Reload();
        //if (Input.GetKeyDown(KeyCode.Mouse0)) ammoClip--;
    }
    void AmmoCheck()
    {
        if (ammo > maxAmmo)
            ammo = maxAmmo;
        return;
    }
    void Reload()
    {
        if (ammoClip == maxAmmoClip || ammo == 0)
            return;
        else
        {
            int inputAmmo = maxAmmoClip - ammoClip;
            ammoClip = maxAmmoClip;
            ammo -= inputAmmo;
            return;
        }
    }
}