using UnityEngine;

public class PlayerUI : MonoBehaviour {

    [SerializeField] RectTransform healthFill;

    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    GameObject scoreboard;

    private Player player;

    public void SetPlayer(Player _player)
    {
        player = _player;
    }

    void Start()
    {
        PauseMenu.IsOn = false;
    }

	void Update ()
	{
        SetHealthAmount(player.GetHealthAsPercentage());

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            scoreboard.SetActive(true);
        } else if (Input.GetKeyUp(KeyCode.Tab))
        {
            scoreboard.SetActive(false);
        }
    }

    void SetHealthAmount(float _amount)
    {
        healthFill.localScale = new Vector3(1f, _amount, 1f);
    }

    void TogglePauseMenu ()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        PauseMenu.IsOn = pauseMenu.activeSelf;
    }
}
