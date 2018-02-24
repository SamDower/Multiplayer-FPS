using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShieldObject : MonoBehaviour {

    float duration;

    public void Setup(float _duration)
    {
        duration = _duration;
    }

	void Start () {
        Invoke("DestroyShield", duration);
	}

    void DestroyShield()
    {
        Destroy(gameObject);
    }
}
