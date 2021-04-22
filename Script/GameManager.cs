using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private Button StartLvl;
    [SerializeField] private Button RestartGame;
    [SerializeField] private GameObject PlayBtton;
    [SerializeField] private Button PauseBtton;

    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject ExitGaME;
    public bool StartGame = false;

    public GameObject PanalTutur;
    void Awake()
    {
        instance = this;
    }
    public void StartLVL()
    {
        PanalTutur.gameObject.SetActive(true);
        AnimationFinger.instance.AnimationTutur();
        PauseBtton.gameObject.SetActive(true);
        StartLvl.gameObject.SetActive(false);
        ExitGaME.gameObject.SetActive(false);
        Time.timeScale = 1f;
        StartGame = true;
        ObjectPlacer.instance.StartScriptObjectPlacer();
        Destroy(PanalTutur, 3f);
    }
    public void RestartGme()
    {
        PauseBtton.gameObject.SetActive(false);
        StartLvl.gameObject.SetActive(true);
        SceneManager.LoadScene(0);
        StartGame = false;
    }
    public void PlayButton()
    {
        Time.timeScale = 1f;
        PauseBtton.gameObject.SetActive(true);
        PlayBtton.gameObject.SetActive(false);
        StartGame = true;
    }
    public void PauseButton()
    {
        Time.timeScale = 0f;
        PauseBtton.gameObject.SetActive(false);
        PlayBtton.gameObject.SetActive(true);
        StartGame = false;
    }
    public void DeleteScore()
    {
        MovePlayer.instance.SCORE = 0;
        PlayerPrefs.DeleteKey("SCORE");
    }
    public void EXIT_Game()
    {
        Application.Quit();
    }
}
