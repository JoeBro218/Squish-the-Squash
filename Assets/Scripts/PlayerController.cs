using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Text score;
    int scoreValue = 0;

    public Text timer;
    public int seconds = 10;
    int introSeconds;
    bool countDown = false;

    public Text general;
   
    bool gameOver = true;

    public AudioSource musicSource;
    public AudioClip background;
    public AudioClip winClip;
    public AudioClip loseClip;

    void Start()
    {
        introSeconds = seconds;
        seconds = seconds + 2;
        score.text = "Score: " + scoreValue;
        timer.text = "" + seconds;

        musicSource.clip = background;
        musicSource.Play();
        musicSource.loop = true;
    }

    void Update()
    {
        if (seconds == introSeconds)
        {
            gameOver = false;
        }

        if(gameOver == false)
        {
            float horizontal = Input.GetAxis("Horizontal");
            Vector2 position = transform.position;
            position.x = position.x + 0.1f * horizontal;
            transform.position = position;
        }

        if(countDown == false && seconds>0)
        {
            StartCoroutine(Count());
        }

        if(gameOver == false && seconds == 0)
        {
            if(scoreValue == 4)
            {
                musicSource.loop = false;
                musicSource.Stop();
                musicSource.clip = winClip;
                musicSource.Play();

                general.text = "YOU WIN! Game by Joseph Demeritt.";
                gameOver = true;
            }
            if(scoreValue != 4)
            {
                musicSource.loop = false;
                musicSource.Stop();
                musicSource.clip = loseClip;
                musicSource.Play();

                general.text = "You lose, press R to restart.";
                gameOver = true;
            }
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.R))
        {
            if (gameOver == true)
            {
              SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
            }
        }
    }

    public void ChangeScore(int amount)
    {
        scoreValue = scoreValue + amount;
        score.text = "Score: " + scoreValue;
    }

    IEnumerator Count()
    {
        countDown = true;
        yield return new WaitForSeconds(1);
        seconds -= 1;
        timer.text = "" + seconds;
        countDown = false;
    }
}
