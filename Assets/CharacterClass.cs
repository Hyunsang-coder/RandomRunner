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

    void Start()
    {
        StartCoroutine(UpdateRunSpeed());
        //runSpeed2 = Random.Range(2, 6);

        manager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        characterName = gameObject.name.ToString();
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

    IEnumerator UpdateRunSpeed()
    {
        runSpeed = Random.Range(1, 3);
        
        yield return new WaitForSeconds(0.8f);
        runSpeed = Random.Range(2, 6);

        yield return new WaitForSeconds(0.8f);
        runSpeed = Random.Range(3, 7);

        yield return new WaitForSeconds(0.8f);
        runSpeed = Random.Range(4, 9);
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
        transform.Translate(0, 0, 1 * runSpeed*Time.deltaTime);
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
            manager.RecordRank(this);
            isRunning = false;
            animator.SetBool("IsRunning", false);

            VictoryCheck();
        }

    }


}
