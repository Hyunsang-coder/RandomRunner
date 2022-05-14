using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CharacterClass : MonoBehaviour
{
    public string characterName;
    public InputField nameInput;
    //public Text displayedName;
    public int runSpeed = 5;
    float runMultiplier = 1;
    public float lapTime = 0;
    public bool isRunning;

    Animator animator;
    GameManager manager;
    AudioManager audioManager;
    FollowCamera camera;

    public ParticleSystem[] particles;

    void Start()
    {
        runSpeed = UnityEngine.Random.Range(2, 6);
        runMultiplier = UnityEngine.Random.Range(0, 8)*0.11f;

        manager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        characterName = gameObject.name.ToString();
        audioManager = FindObjectOfType<AudioManager>();
        camera = FindObjectOfType<FollowCamera>();

        foreach (var p in particles)
        {
            p.Stop();
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning && !manager.isGameOver)
        {
            Running();
            lapTime += Time.deltaTime;
            animator.SetFloat("Speed", runSpeed + runMultiplier);
            //Debug.Log(characterName.ToString() +": " + runSpeed);
            
        }
        
    }


    public void UpdateName() 
    {
        characterName = nameInput.text.ToString();
        //displayedName.text = inputfild.text.ToString();
    }

    public void HideInputField()
    {
        nameInput.gameObject.SetActive(false);
    }

    public void Running()
    {
        transform.Translate(0, 0, 1 * runSpeed*Time.deltaTime);
        audioManager.PlayAudio("Footstep");
    }


    public void VictoryCheck()
    {
        if (manager.finishedCharacters.Count == 1)
        {
            animator.SetTrigger("Victory");
            audioManager.PlayAudio("Complete");
            particles[0].Play(true);
        }

        else if (manager.finishedCharacters.Count > 1 && manager.finishedCharacters.Count < manager.characters.Count)
        {
            StopVFX();
            animator.SetTrigger("Sad");
            
        }

        else if (manager.finishedCharacters.Count == manager.characters.Count)
        {
            StopVFX();
            animator.SetTrigger("Sad");
            manager.isGameOver = true;

            manager.WriteOnLeaderboard();
        }

        
    }

    private void StopVFX()
    {
        foreach (var p in particles)
        {
            p.Stop();
        }
    }

    public void TripOver()
    {
        StopVFX();
        float randomNo = UnityEngine.Random.Range(0,4);
        if (randomNo == 0)
        {
            runSpeed = 0;
            animator.SetTrigger("Trip");
            audioManager.PlayAudio("Choke");
            particles[1].Play(true);    
            StartCoroutine(StandUP());
        }

        
    }

    IEnumerator StandUP()
    {
        yield return new WaitForSeconds(2.2f);
        runSpeed = UnityEngine.Random.Range(2, 4);
        yield return null;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MidLine")
        {
            runSpeed = UnityEngine.Random.Range(2, 7);
            if (runSpeed == 2)
            {
                TripOver();
                return;
            }
            else if (runSpeed >= 6)
            {
                if (particles[2] != null)
                {
                    particles[2].Play(true);   
                }
                
            }
            else if (runSpeed < 6)
            {
                StopVFX();
            }
            return;
        }

        if (other.gameObject.tag == "TripLine")
        {
            runSpeed = UnityEngine.Random.Range(7, 9);
            if (runSpeed == 7)
            {
                TripOver();
            }
            return;
        }
        
        else if (other.gameObject.tag == "FinishLine")
        {
            manager.AddFinishedCharacters(this);
            isRunning = false;
            animator.SetBool("IsRunning", false);

            VictoryCheck();
        }
        
    }

}
