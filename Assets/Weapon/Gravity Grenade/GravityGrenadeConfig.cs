using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "Abilities/Gravity Grenade")]
public class GravityGrenadeConfig : AbilityConfig {

    [Header("Ability Specific")]
    public float pullRadius;
    public float pullForce;
    public GameObject grenadeObject;
    public float throwForce;

    [Command]
    public override void Use(WeaponSystem weaponSystem, int abilityIndex)
    {
        weaponSystem.SetCooldown(abilityIndex, cooldown);
        GameObject instance = Instantiate(grenadeObject, weaponSystem.transform.position + weaponSystem.GetCamera().transform.forward, Quaternion.identity) as GameObject;
        instance.GetComponent<Rigidbody>().AddForce(weaponSystem.GetCamera().transform.forward * throwForce);
        instance.GetComponent<GravityGrenadeObject>().Setup(pullForce, pullRadius, weaponSystem);
    }
}
