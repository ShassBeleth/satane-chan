using RpgAtsumaruApiForUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class SampleComponent : MonoBehaviour
{
    public PlusButtonComponent plusButton;
    public MinusButtonComponent minusButton;
    public RankingButtonComponent rankingButton;
    public Text resultText;
    private int Point { set; get; } = 0;
    public void Awake()
    {
        if (!RpgAtsumaruApi.Initialized)
        {
            RpgAtsumaruApi.Initialize();
        }
        plusButton.OnClickHandler = () => { 
            Point++; 
            resultText.text = Point.ToString();
        };
        minusButton.OnClickHandler = () => { 
            Point--;
            resultText.text = Point.ToString();
        };
        rankingButton.OnClickHandler = async () => {
            await RpgAtsumaruApi.ScoreboardApi.SendScoreAsync(1, Point);
        };
        resultText.text = string.Empty;
    }
}
