using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataInserter : MonoBehaviour {

    public string inputUsername;
    public string inputPassword;

    string createUserURL = "localhost/MultiplayerFPS/InsertUser.php";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateUser(inputUsername, inputPassword);
        }
	}

    public void CreateUser(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", username);
        form.AddField("passwordPost", password);

        WWW www = new WWW(createUserURL, form);
    }
}
