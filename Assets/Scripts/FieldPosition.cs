using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldPosition : MonoBehaviour
{
    public Transform targetCharacter;
    InputField inputField;
    public Vector3 offset;
    Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = Camera.main.WorldToScreenPoint(targetCharacter.position) + offset;

        transform.position = targetPosition;

        inputField = GetComponent<InputField>();
    }

    // Update is called once per frame

    private void Update()
    {
        UpdateNameFieldPosition();
    }

    private void UpdateNameFieldPosition()
    {
        if (targetCharacter.gameObject.activeSelf == false)
        {
            this.inputField.gameObject.SetActive(false);  
        }
        else
        {
            targetPosition = Camera.main.WorldToScreenPoint(targetCharacter.position) + offset;

            transform.position = targetPosition;
        }
    }
}
