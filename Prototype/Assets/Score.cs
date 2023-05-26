using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text ScoreText;
    public AudioClip scoreSound; // Assign the sound clip in the Unity editor
    private AudioSource audioSource;
    private static Score instance;
    public int score;

    public static Score Instance
    {
        get { return instance; }
    }

    // Awake is called before Start
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        audioSource = GetComponent<AudioSource>();
    }

    public void AddScore()
    {
        score++;
        PlayScoreSound();
    }

    public void UpdateScore()
    {
        ScoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
    }

    private void PlayScoreSound()
    {
        if (scoreSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(scoreSound);
        }
    }
}
