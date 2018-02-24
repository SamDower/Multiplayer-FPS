using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreboard : MonoBehaviour {

	void OnEnable()
    {
        Player[] players = GameManager.GetAllPlayers();
        foreach (Player player in players)
        {
            // Setting the UI elements equal to relative data
        }
    }

    void OnDisable()
    {
        // Clean up list of items
    }
}
