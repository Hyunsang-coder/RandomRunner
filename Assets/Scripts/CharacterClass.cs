using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterClass : MonoBehaviour
{
    public string characterName;
    public InputField nameInput;
    //public Text displayedName;
    public int runSpeed = 5;
    public float lapTime = 0;
    public bool isRunning;
    Animator animator;
    GameManager manager;
    AudioManager audioManager;

    public ParticleSystem[] particles;

    void Start()
    {
        
        runSpeed = Random.Range(1, 6);

        manager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        characterName = gameObject.name.ToString();
        audioManager = FindObjectOfType<AudioManager>();

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
            animator.SetFloat("Speed", runSpeed);
            Debug.Log(characterName.ToString() +": " + runSpeed);
            
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
            animator.SetTrigger("Sad");
        }

        else if (manager.finishedCharacters.Count == manager.characters.Count)
        {
            animator.SetTrigger("Trip");
            audioManager.PlayAudio("Choke");
            particles[1].Play(true);
            manager.isGameOver = true;

            manager.WriteOnLeaderboard();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MidLine")
        {
            runSpeed = Random.Range(3, 8);
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
