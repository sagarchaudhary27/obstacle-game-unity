using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scorer : MonoBehaviour
{
    int hits;
    [SerializeField] float levelloaddelay = 2f;
    [SerializeField] AudioClip Success;
    [SerializeField] ParticleSystem successParticles;
    AudioSource audioSource;
    public Text ScoreText;
    bool isTransitioning = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag !="Hit")
        {
            hits++;
            Debug.Log("You've bumped into a thing this many times: " + hits);
            if(other.gameObject.tag == "Finish")
            {
                hits--;
                Debug.Log("You've bumped into a thing this many times: " + hits);
            }
            SetScoreText();
        }
        if (other.gameObject.tag == "Finish")
        {
            isTransitioning = true;
            audioSource.Stop();
            audioSource.PlayOneShot(Success);
            successParticles.Play();
            GetComponent<Mover>().enabled = false;
            Invoke("LoadNextLevel", levelloaddelay);
        }
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    void SetScoreText()
    {
        ScoreText.text = "Hits: " + hits.ToString();
    }

}
