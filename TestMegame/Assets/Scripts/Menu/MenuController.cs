using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;   

public class MenuController : MonoBehaviour
{

    [SerializeField] private GameController gameController;
    [SerializeField] Button ContinueButton;

    private static Dictionary<string, GameObject> _instances = new Dictionary<string, GameObject>();
    public string ID;
    private bool isGameStarted;

    private void Awake()
    {
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
        Debug.Log("ChangeControl");
    }

    public void ExitPressed()
    {
        Application.Quit();
    }

    public void GameStarting()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
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
