
using System.Collections;
using TMPro;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]GameObject PausePannel;
    [SerializeField]GameObject MainMenuPannel;
    [SerializeField]GameObject CountdownPannel;
    [SerializeField]TMPro.TMP_Text CountdownText;
    [SerializeField]GameObject SettingsPannel;
    [SerializeField]TextMeshProUGUI GameOver_Score;
    [SerializeField]TextMeshProUGUI GameOver_Coins;
    public GameObject LevelsPannel;
    public Transform Player;

    public PlayerMovement player;
    
    static bool SkipMenu=false;
    static bool SkipLevelPanel = false;
    Rigidbody Rb;
   

    
    
    
    
    void Start()
    {
    Time.timeScale = 0f;
    Rb=Player.GetComponent<Rigidbody>();

    
    if (!SkipMenu)
    {
        MainMenuPannel.SetActive(true);
        return;
        
    }

    MainMenuPannel.SetActive(false);
    SkipMenu = false;

    
    if (SkipLevelPanel)
    {
        LevelsPannel.SetActive(false);
        SkipLevelPanel = false;
        Time.timeScale = 1f;
    }
    else
    {
        LevelsPannel.SetActive(true);
    }
    }


    
    void Update()
    {
        
    }

    public void Beginner()
    {
        PlayerPrefs.SetInt("Level",0);
        LevelsPannel.SetActive(false);
        Time.timeScale=1f;
    }
    public void Intermediate()
    {
        PlayerPrefs.SetInt("Level",1);
        LevelsPannel.SetActive(false);
        Time.timeScale=1f;
    }
    public void Difficult()
    {
        PlayerPrefs.SetInt("Level",2);
        LevelsPannel.SetActive(false);
        Time.timeScale=1f;
    }

    public void StartButton()
    {
        SkipMenu=true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        
     

    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void ShowPause()
    {
        PausePannel.SetActive(true);
        Time.timeScale=0f;
    }
    public void ResumeButton()
    {
        StartCoroutine(ResumeCountdown());
        
    }
    public void HomeButton()
    {   
        PausePannel.SetActive(false);
        MainMenuPannel.SetActive(true);
    }
    public void BackButton()
    {
        SettingsPannel.SetActive(false);
    }
    public void ShowSettings()
    {
        SettingsPannel.SetActive(true);
    }
    public void Restart()
    
    {
        SkipMenu = true;  
        SkipLevelPanel = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    

    }

    

    IEnumerator ResumeCountdown()
    {
        PausePannel.SetActive(false);
        CountdownPannel.SetActive(true);
        player.enabled=false;
        Rb.constraints=RigidbodyConstraints.FreezePosition;
        Time.timeScale=1f;
        CountdownText.text="3";
        yield return new WaitForSeconds(1f);
        CountdownText.text="2";
        yield return new WaitForSeconds(1f);
        CountdownText.text="1";
        yield return new WaitForSeconds(1f);
        Rb.constraints=RigidbodyConstraints.None;
        Rb.freezeRotation=true;
        CountdownPannel.SetActive(false);
        player.enabled=true;
        
        
    }
    public void FinalGameUpdation()
    {
        GameOver_Coins.text = "Coins: " + ScoreCalculation.FinalCoins;
        GameOver_Score.text = "Score: " + ScoreCalculation.FinalScore;

    }
    
}
