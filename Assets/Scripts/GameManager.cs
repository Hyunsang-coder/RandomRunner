using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public InputField[] nameInputFields;
    public InputField[] rewardInputFields;

    [Range(2, 8)]
    public int playerCount = 5;
    [SerializeField] Text playerCountTxt;

    [SerializeField] CharacterClass[] allCharacters;
    public List<CharacterClass> characters;
    public List<CharacterClass> finishedCharacters;
    public Text result;
    public GameObject leaderboard;
    public GameObject rewardboard;
    public GameObject counterBoard;
    public bool isGameOver = false;

    public Button[] buttons;
    AudioManager audioManager;
    ParticleSystem fireworks;
    
    void Start()
    {
        counterBoard.SetActive(true);
        leaderboard.SetActive(false);
        rewardboard.SetActive(false);

        HideNameInputFields();
        HideButtons();
        audioManager = FindObjectOfType<AudioManager>();
        fireworks = GameObject.FindWithTag("Fireworks").GetComponent<ParticleSystem>();
        playerCountTxt.text = playerCount.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SpawnCharacter()
    {
        for (int i = 0; i < playerCount; i++)
        {
            allCharacters[i].gameObject.SetActive(true);
            allCharacters[i].nameInput.gameObject.SetActive(true);
            characters.Add(allCharacters[i]);
        }

    }
    public void ShowButtons()
    {
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(true);
        }
    }

    public void HideButtons()
    {
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }


    public void HideNameInputFields()
    {
        foreach (var field in nameInputFields)
        {
            field.gameObject.SetActive(false);
        }
    }

    public void ShowNameInputFields()
    {
        foreach (var field in nameInputFields)
        {
            field.gameObject.SetActive(true);
        }
    }

    public void HideRewardBoard()
    {
        rewardboard.SetActive(false);
    }

    public void ShowRewardBoard()
    {
        rewardboard.SetActive(true);
    }

    public void ReadyUpRunner()
    {
        FollowCamera camera = FindObjectOfType<FollowCamera>();
        HideRewardBoard();
        camera.ZoomChange(playerCount);
        camera.StartPosition();
    }

    public void ReStartGame()
    {
        if (isGameOver)
        {
            SceneManager.LoadScene(0);
            isGameOver = false;
            Debug.Log("Reload");
        }

    }

    public void RunCharacters()
    {
        HideNameInputFields();

        if (!isGameOver)
        {
            audioManager.PlayAudio("Gong");
            fireworks.Play();
        }
        
        foreach(var character in characters)
        {
            character.isRunning = true;
            character.GetComponent<Animator>().SetBool("IsRunning", true);
        }
    }

    public void AddFinishedCharacters(CharacterClass character)
    {
        finishedCharacters.Add(character);
    }

    public void WriteOnLeaderboard()
    {

        for (int i=0; i < playerCount; i++)
        {
            result.text += string.Format("\nNo.{0} {1} = {2}", i + 1, finishedCharacters[i].characterName, rewardInputFields[i].text.ToString());
        }

        ShowLeaderboard();
    }

    public void ShowLeaderboard()
    {
        if (leaderboard.activeSelf == false)
        {
            leaderboard.SetActive(true);
        }
        else
        {
            leaderboard.SetActive(false);
        }
    }

    public void ClickSound()
    {
        audioManager.PlayAudio("Click");
    }

    public void CompleteSound()
    {
        audioManager.PlayAudio("Complete");
    }

    public void IncreasePlayerCount()
    {
        if (playerCount > 7)
        {
            return;
        }
        else
        {
            playerCount++;
            playerCountTxt.text = playerCount.ToString();
        }
    }

    public void DecreasePlayerCount()
    {
        if (playerCount < 3)
        {
            return;
        }
        else
        {
            playerCount--;
            playerCountTxt.text = playerCount.ToString();
        }
    }

    public void CloseCounterBoard()
    {
        if (counterBoard.activeSelf == false)
        {
            counterBoard.SetActive(true);
        }
        else
        {
            counterBoard.SetActive(false);
        }
    }

}
