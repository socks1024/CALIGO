using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private bool isRope = false;

    [SerializeField] GameObject[] leavePoints = new GameObject[2];

    [SerializeField] GameObject[] platformsThrough;

    

    public Vector3 GetLeavePointPos(Vector3 playerPos)
    {
        Vector3 leavePointPos = PlayerManager.Instance.transform.position;

        if ((leavePoints[0].transform.position - playerPos).magnitude < (leavePoints[1].transform.position - playerPos).magnitude)
        {
            leavePointPos = leavePoints[0].transform.position;
        }

        return leavePointPos;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            WalkState w = new WalkState();
            PlayerManager.Instance.playerSM.SwitchState(w);
        }
    }

    public void ChangePlatformCollision(bool b)
    {
        foreach (GameObject platform in platformsThrough)
        {
            platform.SetActive(b);
        }
    }
}
