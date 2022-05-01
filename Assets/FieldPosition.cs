using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldPosition : MonoBehaviour
{
    public Transform targetCharacter;
    public Vector3 offset;
    Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = Camera.main.WorldToScreenPoint(targetCharacter.position) + offset;

        transform.position = targetPosition;
    }

    // Update is called once per frame
    
}
