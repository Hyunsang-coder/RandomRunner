using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterClass : MonoBehaviour
{
    public string characterName;
    public InputField inputfield;
    //public Text displayedName;
    public int runSpeed = 5;
    int runSpeed2;
    public float lapTime = 0;
    public bool isRunning;
    Animator animator;
    GameManager manager;
    bool isGameOver;

    void Start()
    {
        runSpeed = Random.Range(10, 15);
        runSpeed2 = Random.Range(2, 6);
        manager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        characterName = gameObject.name.ToString();
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning && !isGameOver)
        {
            Running();
            lapTime += Time.deltaTime;
            Debug.Log("RunSpeed: " + runSpeed);
        }
        
    }

    public void UpdateName() 
    {
        characterName = inputfield.text.ToString();
        //displayedName.text = inputfild.text.ToString();
    }

    public void HideInputField()
    {
        inputfield.gameObject.SetActive(false);
    }

    public void Running()
    {
        transform.Translate(0, 0, 1 * runSpeed*Time.deltaTime /runSpeed2);
    }


    public void VictoryCheck()
    {
        if (manager.rankers.Count == 1)
        {
            animator.SetTrigger("Victory");
        }

        else if (manager.rankers.Count > 1 && manager.rankers.Count < manager.characters.Length)
        {
            animator.SetTrigger("Sad");
        }

        else if (manager.rankers.Count == manager.characters.Length)
        {
            animator.SetTrigger("Trip");

            manager.ShowRanking();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FinishLine")
        {
            isGameOver = true;
            manager.RecordRank(this);
            isRunning = false;
            animator.SetBool("IsRunning", false);

            VictoryCheck();
        }

    }


}
