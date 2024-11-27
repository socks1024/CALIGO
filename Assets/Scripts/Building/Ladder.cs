using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] GameObject[] leavePoints = new GameObject[2];

    public Vector3 GetLeavePointPos(Vector3 playerPos)
    {
        Vector3 leavePointPos = Vector3.zero;

        if ((leavePoints[0].transform.position - playerPos).magnitude < (leavePoints[1].transform.position - playerPos).magnitude)
        {
            leavePointPos = leavePoints[0].transform.position;
        }
        else
        {
            leavePointPos = leavePoints[1].transform.position;
        }

        return leavePointPos;
    }
}
