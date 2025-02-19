using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BackflipTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    [SerializeField] private AudioSource interactSuccessAudio;
    [SerializeField] private AudioSource interactFailAudio;


    public string InteractionPrompt => _prompt;

    public void Update()
    {
        if (Globals.drunkenness >= 100)
        {
            _prompt = "Do a backflip!";
        }
        else
        {
            _prompt = "Get 100% drunk before doing a backflip";
        }
    }


    public bool Interact(Interactor interactor)
    {

        if (Globals.drunkenness >= 100)
        {
            interactSuccessAudio.Play();

            // Uncomment section to swap scenes

           var levelChanger = GameObject.FindObjectOfType(typeof(LevelChanger)) as LevelChanger;

            var player = GameObject.FindGameObjectWithTag("Remy");

            Globals.playerCoords = player.transform.position;

            levelChanger.FadeToLevel(5);

            // TODO handle win screen after implementing minigame
            /*if (openedWinScreen)
                WinGame();*/
            return true;
        }
        else
        {
            interactFailAudio.Play();
            Debug.Log("Not drunk enough to backflip");
        }


        return true;
    }

    /*public void WinGame()
    {
        openedWinScreen = true;
        winScreen.SetActive(true);
        int score = CalculateScore();
        scoreToDisplay.text = "Score: " + score;

        if (score > Globals.highscore)
        {
            PlayerPrefs.SetInt("Highscore", score);
            Globals.highscore = score;
        }
        highscoreDisplay.text = "Highscore: " + Globals.highscore;

        Debug.Log("Did backflip");

        Globals.isPaused = true;
        Globals.isPausedExit = true;
        Globals.freezeMovement = true;
        Globals.freezeDrunkenness = true;
        Globals.freezeInteractions = true;
    }

    public int CalculateScore()
    {
        // int score = 0;
        float multiplier = 0;
        // int totalSec;
        float timeLeft = Globals.minutesToPlay * 60 + Globals.secondsToPlay;

        if (Globals.DifficultyLevel == "easy")
        {
            multiplier = 1f;
            // totalSec = 600;
        }
        if (Globals.DifficultyLevel == "normal")
        {
            multiplier = 2f;
            // totalSec = 300;
        }
        if (Globals.DifficultyLevel == "hard")
        {
            multiplier = 3f;
            // totalSec = 180;
        }
        if (Globals.DifficultyLevel == "endless")
        {
            return 0;
        }

        return (int)(multiplier * timeLeft);

    }*/
}
