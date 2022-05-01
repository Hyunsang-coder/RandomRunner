using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] inputfields;
    public CharacterClass[] characters = new CharacterClass[] { };
    public List<CharacterClass> rankers = new List<CharacterClass> { };
    public Text result;
    public GameObject Leaderboard;
    void Start()
    {
        Leaderboard.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideAllInputFields()
    {
        foreach (var field in inputfields)
        {
            field.gameObject.SetActive(false);
        }
    }

    public void RunCharacters()
    {
        foreach(var character in characters)
        {
            character.isRunning = true;
            character.GetComponent<Animator>().SetBool("IsRunning", true);
        }
    }

    public void RecordRank(CharacterClass character)
    {
        rankers.Add(character);
    }

    public void ShowRanking()
    {
        if (rankers.Count < characters.Length)
        {
            return;
            Debug.Log("Not finished yet.");
        }
        //result.text = "No.1 " + rankers[0].characterName + " recorded " + rankers[0].lapTime.ToString() +"\nNo.2 " + rankers[1].characterName + " recorded " + rankers[1].lapTime.ToString()
        //    + "\nNo.3 " + rankers[2].characterName + " recorded " + rankers[2].lapTime.ToString() + "\nNo.4 " + rankers[3].characterName + " recorded " + rankers[3].lapTime.ToString()
        //    +"\nNo.5 " + rankers[4].characterName + " recorded " + rankers[4].lapTime.ToString() + "\nNo.6 " + rankers[5].characterName + " recorded " + rankers[5].lapTime.ToString();

        result.text = "\n     No.1  " + rankers[0].characterName + "\n     No.2  " + rankers[1].characterName 
            + "\n     No.3  " + rankers[2].characterName  + "\n     No.4  " + rankers[3].characterName 
            + "\n     No.5  " + rankers[4].characterName + "\n     No.6  " + rankers[5].characterName;

        if (Leaderboard.activeSelf == false)
        {
            Leaderboard.SetActive(true);
        }
        else
        {
            Leaderboard.SetActive(false);
        }

    }
}
