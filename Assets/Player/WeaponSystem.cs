using UnityEngine;
using UnityEngine.Networking;

public class WeaponSystem : NetworkBehaviour {

	private const string weaponLayerName = "Weapon";

    [SerializeField] private Camera cam;
	[SerializeField] private Transform weaponHolder;

    [Header("Weapons")]
    [SerializeField] private WeaponConfig[] weapons;
    [Header("Abilities")]
    [SerializeField] private AbilityConfig[] abilities;
    private float[] cooldowns;

    private WeaponConfig currentWeapon;
	private WeaponGraphics currentGraphics;

    public delegate void OnRightClick();
    public event OnRightClick onRightClick;

	void Start ()
	{
		EquipWeapon(0);

        cooldowns = new float[abilities.Length];
	}

    void Update()
    {
        if (!isLocalPlayer) return;
        for (int i = 0; i < abilities.Length; i++)
        {
            cooldowns[i] -= Time.deltaTime;
            cooldowns[i] = Mathf.Clamp(cooldowns[i], 0, float.MaxValue);
        }
        if (Input.GetKeyDown(KeyCode.Q) && cooldowns[0] <= 0) abilities[0].Use(this, 0);
        if (Input.GetKeyDown(KeyCode.W) && cooldowns[1] <= 0) abilities[1].Use(this, 1);
        if (Input.GetMouseButtonDown(1)) onRightClick();
    }

    public void SetCooldown(int index, float time)
    {
        cooldowns[index] = time;
    }

    public Camera GetCamera()
    {
        return cam;
    }

	public WeaponConfig GetCurrentWeapon ()
	{
		return currentWeapon;
	}

	public WeaponGraphics GetCurrentGraphics()
	{
		return currentGraphics;
	}

    public void EquipWeapon(int index)
    {
        currentWeapon = weapons[index];

        foreach (Transform child in weaponHolder)
        {
            Destroy(child.gameObject);
        }

        switch (currentWeapon.weaponType) {

            case WeaponConfig.WeaponType.Gun:
                GameObject _weaponIns = (GameObject)Instantiate(currentWeapon.gun_Graphics, weaponHolder.position, weaponHolder.rotation, weaponHolder);
                _weaponIns.transform.SetParent(weaponHolder);
                currentGraphics = _weaponIns.GetComponent<WeaponGraphics>();
                if (isLocalPlayer)
                    Util.SetLayerRecursively(_weaponIns, LayerMask.NameToLayer(weaponLayerName));
                break;
        }
	}
}
 