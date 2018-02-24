using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityConfig : ScriptableObject {

    [Header("Ability Config")]
    public int cooldown;

    public abstract void Use(WeaponSystem weaponSystem, int abilityIndex);
}
