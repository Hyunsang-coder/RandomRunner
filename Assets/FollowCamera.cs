using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Vector3 offsetPosition;
    public Vector3 offsetRotation;
    public GameObject finishLine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PositionCamera()
    {
        transform.position = finishLine.transform.position + offsetPosition;
        transform.rotation = Quaternion.Euler(offsetRotation);
    }

}
