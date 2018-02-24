using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "WeaponConfig")]
public class WeaponConfig : ScriptableObject
{
    public enum WeaponType { Grenade, Gun, C };
    public WeaponType weaponType;

    // Grenade Settings
    public GameObject grenade_Obj;
    public int grenade_throwForce;

    // Gun Settings
    public int gun_Damage = 10;
    public int gun_Range = 100;
    public int gun_FireRate = 0;
    public int gun_KnockbackForce = 0;
    public GameObject gun_Graphics;

    void ThrowGrenade(PlayerMotor player)
    {
        GameObject grenadeInstance = Instantiate(grenade_Obj, player.transform.position + player.transform.forward, Quaternion.identity) as GameObject;
        Rigidbody rb = grenadeInstance.GetComponent<Rigidbody>();
        rb.AddForce(player.GetCamera().transform.forward * grenade_throwForce);
    }
}
