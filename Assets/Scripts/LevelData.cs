using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public string question;
    public string yesButtonText;
    public string noButtonText;
    public bool isYesAnswerCorrect;
    public Sprite logo;
    public string wrongAnswerMessage;
    public string correctAnswerMessage;
    public string hintText;
    public string finalText;
    public bool isHardLevel;


}
