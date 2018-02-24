using UnityEngine;
using UnityEngine.Networking;

[RequireComponent (typeof (WeaponSystem))]
public class ShootSystem : NetworkBehaviour {

	private const string PLAYER_TAG = "Player";

	[SerializeField]
	private Camera cam;

	[SerializeField]
	private LayerMask mask;

	private WeaponConfig currentWeapon;

	private WeaponSystem weaponSystem;

    int currentWeaponIndex = 0;

    void Start ()
	{
		if (cam == null)
		{
			Debug.LogError("PlayerShoot: No camera referenced!");
			this.enabled = false;
		}

		weaponSystem = GetComponent<WeaponSystem>();
	}

	void Update ()
	{
		currentWeapon = weaponSystem.GetCurrentWeapon();
        if (PauseMenu.IsOn)
        {
            return;
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0)
        {
            currentWeaponIndex += (int)(Input.GetAxisRaw("Mouse ScrollWheel") * 10);
            if (currentWeaponIndex < 0) currentWeaponIndex = 1; // TODO Remove hard coded value
            if (currentWeaponIndex > 1)  currentWeaponIndex = 0;

            Debug.Log(currentWeaponIndex);
            weaponSystem.EquipWeapon(currentWeaponIndex);
        }

        // Look for weapon input
        switch (currentWeapon.weaponType)
        {
            case WeaponConfig.WeaponType.Gun:
                if (currentWeapon.gun_FireRate <= 0f)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        ShootGun();
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        InvokeRepeating("ShootGun", 0f, 1f / currentWeapon.gun_FireRate);
                    } 
                    else if (Input.GetButtonUp("Fire1"))
                    {
                        CancelInvoke("ShootGun");
                    }
                }
                break;
        }
	}

    #region Gun
    //Is called on the server when a player shoots
    [Command]
	void CmdOnShootGun ()
	{
		RpcDoShootGunEffect();
    }

	// Is called on all clients when we need to to
	// a shoot effect
	[ClientRpc]
	void RpcDoShootGunEffect ()
	{
		weaponSystem.GetCurrentGraphics().muzzleFlash.Play();
	}

	//Is called on the server when we hit something
	//Takes in the hit point and the normal of the surface
	[Command]
	void CmdOnGunHit (Vector3 _pos, Vector3 _normal)
	{
		RpcDoHitGunEffect(_pos, _normal);
        GetComponent<PlayerMotor>().ApplyKnockback(currentWeapon.gun_KnockbackForce);
    }

	//Is called on all clients
	//Here we can spawn in cool effects
	[ClientRpc]
	void RpcDoHitGunEffect(Vector3 _pos, Vector3 _normal)
	{
		GameObject _hitEffect = (GameObject)Instantiate(weaponSystem.GetCurrentGraphics().hitEffectPrefab, _pos, Quaternion.LookRotation(_normal));
		Destroy(_hitEffect, 2f);
	}

	[Client]
	void ShootGun ()
	{
		if (!isLocalPlayer)
		{
			return;
		}

		//We are shooting, call the OnShoot method on the server
		CmdOnShootGun();

		RaycastHit _hit;
		if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, currentWeapon.gun_Range, mask) )
		{
			if (_hit.collider.tag == PLAYER_TAG)
			{
				CmdPlayerWasShot(_hit.collider.name, currentWeapon.gun_Damage);
			}

			// We hit something, call the OnHit method on the server
			CmdOnGunHit(_hit.point, _hit.normal);
		}
	}

	[Command]
	void CmdPlayerWasShot (string _playerID, int _damage)
	{
		Player _player = GameManager.GetPlayer(_playerID);
        _player.RpcTakeDamage(_damage);
	}
    #endregion
}
