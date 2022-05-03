using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    // 리워드 설정 시
    public Vector3 iniPosition;
    public Vector3 iniRotation;

    // 달리기 시작 전 
    public Vector3 basePosition;
    public Vector3 baseRotation;

    // 달리기 시작 후 위치 
    public Vector3 offsetPosition;
    public Vector3 offsetRotation;
    public GameObject finishLine;
    public GameObject startingLine;
    public float camSpeed = 10;


    bool isStarted;
    // Start is called before the first frame update
    void Start()
    {

        PositionAtStart();
        //PositionBeforeRun();
    }

    public void PositionAtStart()
    {
        transform.position = iniPosition;
        transform.rotation = Quaternion.Euler(iniRotation);
    }

    public void ZoomChange(int playerCount)
    {
        offsetPosition.z -= (playerCount*0.5f); 
    }

    public void StartPosition()
    {
        transform.position = startingLine.transform.position + offsetPosition;
        transform.rotation = Quaternion.Euler(baseRotation);
    }

    // Update is called once per frame
    void Update()
    {
        //PositionCamera();

        if (isStarted)
        {
            PositionAfterRun();
        }
    }

    public void PositionAfterRun()
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
