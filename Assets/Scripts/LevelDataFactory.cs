using UnityEngine;

public class LevelDataFactory
{
    public static LevelData CreateLevelData(string question, string yesButtonText, string noButtonText, bool isYesAnswerCorrect, Sprite logo, string wrongAnswerMessage, string correctAnswerMessage, string hintText, string finalText, bool isHardLevel)
    {
        LevelData newLevel = ScriptableObject.CreateInstance<LevelData>();
        newLevel.question = question;
        newLevel.yesButtonText = yesButtonText;
        newLevel.noButtonText = noButtonText;
        newLevel.isYesAnswerCorrect = isYesAnswerCorrect;
        newLevel.logo = logo;
        newLevel.wrongAnswerMessage = wrongAnswerMessage;
        newLevel.correctAnswerMessage = correctAnswerMessage;
        newLevel.hintText = hintText;
        newLevel.finalText = finalText;
        newLevel.isHardLevel = isHardLevel;

        return newLevel;
    }
}
