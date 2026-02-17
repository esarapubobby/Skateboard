using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreCalculation : MonoBehaviour
{
     public TextMeshProUGUI Coins;
     public TextMeshProUGUI Score;
     int coinsCollected=0;
    float timer=0;
  
    public static int FinalScore;
    public static int FinalCoins;
    void Start()
    {
       
    }

    
    void Update()
    {
         timer+=Time.deltaTime*10;
         FinalScore=(int)timer;
         
         Score.text=FinalScore.ToString();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            coinsCollected++;
            FinalCoins=coinsCollected;
            Coins.text=coinsCollected.ToString();
    
        }
    }

}
