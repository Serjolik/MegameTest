using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{

    [SerializeField] private GameController gameController;
    [SerializeField] private GameObject playerShip;
    [SerializeField] public Button ContinueButton;
    [SerializeField] private TextMeshProUGUI ControlText;

    private PlayerMovement playerMovement;
    private Shot playerShot;

    private static Dictionary<string, GameObject> _instances = new Dictionary<string, GameObject>();
    public string ID;
    private bool isGameStarted;
    private bool controlMode;

    private void Awake()
    {
        ControlText.text = "Мышь";
        if (_instances.ContainsKey(ID))
        {
            var existing = _instances[ID];

            if (existing != null)
            {
                if (ReferenceEquals(gameObject, existing))
                    return;

                Destroy(gameObject);

                return;
            }
        }
        _instances[ID] = gameObject;
        DontDestroyOnLoad(this);
    }
    public void ContinuePressed()
    {
        gameController.inMenu = false;
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void NewGamePressed()
    {
        isGameStarted = true;
        SceneManager.LoadScene(0);
    }

    public void ChangeControlPressed()
    {
        if (controlMode)
        {
            ControlText.text = "Мышь";
        }
        else
        {
            ControlText.text = "Клавиатура";
        }
        controlMode = !controlMode;
        playerShot.controlMode = controlMode;
        playerMovement.ControlMode = controlMode;
        Debug.Log("ChangeControl");
    }

    public void ExitPressed()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void GameStarting()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerShip = GameObject.FindGameObjectWithTag("Ship");
        playerMovement = playerShip.GetComponent<PlayerMovement>();
        playerShot = playerShip.GetComponentInChildren<Shot>();
        playerShot.controlMode = controlMode;
        playerMovement.ControlMode = controlMode;
        playerMovement.ControlMode = controlMode;
        if (isGameStarted)
        {
            ContinueButton.interactable = true;
            gameController.isAlive = true;
            ContinuePressed();
            Debug.Log("PLAY");
        }
        else
        {
            gameController.inMenu = true;
            Time.timeScale = 0;
            ContinueButton.interactable = false;
        }
    }
}
