using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NPCBehavior : MonoBehaviour
{
    
    Animator anim;
    CharacterClass character;
    int behaviorNumber;
    bool isbehaving;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    float timer;
    private void Update() 
    {
        timer += Time.deltaTime;
        if (timer > 4)
        {
            BehaivorChange();
            timer = 0;
        }
        
    }

       
    public void BehaivorChange()
    {
        isbehaving = true;
        behaviorNumber = UnityEngine.Random.Range(0,4);
        switch(behaviorNumber)
        {
            case 0:
                anim.SetTrigger("Cheer");
                break;

            case 1:
                anim.SetTrigger("Exclaim");
                break;
            case 2:
                anim.SetTrigger("Exclaim2");
                break;
            case 3:
                anim.SetTrigger("HandWave");
                break;
        }
    }

}
