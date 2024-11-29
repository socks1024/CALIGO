using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public Animator EnemyAnimatorController;
    public GameObject Enemy;
    public GameObject Player;
    public GameObject EnemyLocation;
    public bool Chasing;
    public float Speed;
    public float SuoDi_X;
    public float SuoDi_Y;
    //�ж���ҿ�������
    public bool Close()
    {
        //��Һ͹����Ƿ���ͬһƽ̨
       if(Player.transform.position.y - Enemy.transform.position.y < SuoDi_Y && Player.transform.position.y - Enemy.transform.position.y > -SuoDi_Y)
        {
            //��Һ͹���ˮƽ�����Ƿ�ӽ�
            if (Player.transform.position.x - Enemy.transform.position.x < SuoDi_X && Player.transform.position.x - Enemy.transform.position.x > -SuoDi_X)
            {
                EnemyAnimatorController.SetBool("Chase", true);
                Chasing = true;
                return true;
            }
            else
            {
                return false;
            }
        }
       //��Һ͹��ﲻ��ͬһƽ̨
       else
        {
            EnemyAnimatorController.SetBool("Chase", false);
            Chasing = false;
            if(EnemyLocation.transform.position.x - Enemy.transform.position.x != 0)
            {
                EnemyAnimatorController.SetBool("Back", true);
            }
            return false;
        }
    }
    //����׷���ƶ�����
    public void Move(GameObject a,GameObject b)
    {
        Vector3 AtoB=new Vector3((b.transform.position.x - a.transform.position.x)/System.Math.Abs(b.transform.position.x - a.transform.position.x), 0, 0);
        a.transform.position = AtoB * Speed+a.transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        //����ʵ����
        Enemy = GameObject.Find("Enemy");
        Player = GameObject.Find("Player");
        EnemyAnimatorController = GetComponent<Animator>();
        EnemyLocation = GameObject.Find("EnemyLocation");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(Chasing);
        Close();
        //�������׷���
        if (Chasing)
        {
            Move(Enemy, Player);
        }
        //�������ͻص�ԭλ
        else
        {
            if (System.Math.Abs(EnemyLocation.transform.position.x - Enemy.transform.position.x)>0.5f)
            {
                Move(Enemy, EnemyLocation);
            }
        }
    }
}
