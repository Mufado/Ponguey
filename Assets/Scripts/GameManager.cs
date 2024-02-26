using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameManager() { }

    [SerializeField] private Text leftScoreboardText;
    [SerializeField] private Text rightScoreboardText;

    private int playerOnePoints = 0;
    private int playerTwoPoints = 0;

    public static GameManager Instance { get; private set; }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void UpdateScore(Enums.Player pToScore)
    {
        switch (pToScore)
        {
            case Enums.Player.One:
                playerOnePoints++;
                leftScoreboardText.text = playerOnePoints.ToString();
                break;
            case Enums.Player.Two:
                playerTwoPoints++;
                rightScoreboardText.text = playerTwoPoints.ToString();
                break;
        }
    }

    public void ResetScoreboard()
    {
        playerOnePoints = 0;
        playerTwoPoints = 0;

        leftScoreboardText.text = "0";
        rightScoreboardText.text = "0";
    }
}
