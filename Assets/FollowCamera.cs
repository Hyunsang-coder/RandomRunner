using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Vector3 offsetPosition;
    public Vector3 offsetRotation;
    public GameObject finishLine;
    public float camSpeed = 10;
    bool isStarted;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //PositionCamera();

        if (isStarted)
        {
            PositionCamera();
        }
    }

    public void PositionCamera()
    {
        isStarted = true;
        //transform.position = finishLine.transform.position + offsetPosition;
        //transform.rotation = Quaternion.Euler(offsetRotation);

        
        Vector3 targetPositon = Vector3.Lerp(transform.position, finishLine.transform.position + offsetPosition, camSpeed * Time.deltaTime);
        Quaternion targetRotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(offsetRotation), camSpeed * Time.deltaTime);

        transform.position = targetPositon;
        transform.rotation = targetRotation;
    }

}
