using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGrenadeObject : MonoBehaviour {

    float movementExplodeThreshold = 1f;
    Rigidbody rb;
    WeaponSystem weaponSystemFiredFrom;

    float pullForce;
    float pullRadius;

    public void Setup(float _pullForce, float _pullRadius, WeaponSystem weaponSystem)
    {
        pullForce = _pullForce;
        pullRadius = _pullRadius;
        weaponSystemFiredFrom = weaponSystem;
        weaponSystemFiredFrom.onRightClick += Explode;
    }
    
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
    
    void Update()
    {
        if (rb.velocity.magnitude <= movementExplodeThreshold)
            Explode();
    }

    void Explode()
    {
        DragInPlayers();
        UnsubscribeFromRightClickEvent();
        Destroy(gameObject);
    }

    void DragInPlayers()
    {
        Player[] players = FindObjectsOfType<Player>();
        foreach (Player player in players)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 5)
            {
                Vector3 forceDir = (transform.position - player.transform.position).normalized;
                player.GetComponent<Rigidbody>().AddForce(forceDir * pullForce);
            }
        }
    }

    void UnsubscribeFromRightClickEvent()
    {
        weaponSystemFiredFrom.onRightClick -= Explode;
    }
}
