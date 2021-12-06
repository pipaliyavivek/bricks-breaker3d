using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public delegate void TapToStartGame();
    public static TapToStartGame TapStart;

    public GameObject TapToStart;
    public GameObject LooseScreen;
    public GameObject WinScreen;

    void Awake() => Singleton();
    void Start()
    {
        TapToStart.SetActive(true);
        var btn = TapToStart.GetComponent<Button>();
        var LooseBtn = LooseScreen.GetComponent<Button>();
        var WinBtn = WinScreen.GetComponent<Button>();
        btn.onClick.AddListener(TAP);
        LooseBtn.onClick.AddListener(YouLoose);
        WinBtn.onClick.AddListener(YouWin);
    }
    void TAP()
    {
        TapToStart.SetActive(false);
        TapStart();
    }
    void YouLoose()
    {
        LooseScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void YouWin()
    {
        WinScreen.SetActive(false);
        TapStart();
    }
    void Singleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
