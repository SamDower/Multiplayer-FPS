using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "Abilities/Gravity Grenade")]
public class EnergyShieldConfig : AbilityConfig {

    [Header("Ability Specific")]
    public GameObject shieldObject;
    public float duration;
    
    public override void Use(WeaponSystem weaponSystem, int abilityIndex)
    {
        weaponSystem.SetCooldown(abilityIndex, cooldown);
        GameObject instance = Instantiate(shieldObject, weaponSystem.transform.position + (weaponSystem.GetCamera().transform.forward / 2), weaponSystem.GetCamera().transform.rotation, weaponSystem.GetCamera().transform) as GameObject;
        instance.GetComponent<EnergyShieldObject>().Setup(duration);
    }
}
