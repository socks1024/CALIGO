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
    //判断玩家靠近怪物
    public bool Close()
    {
        //玩家和怪物是否在同一平台
       if(Player.transform.position.y - Enemy.transform.position.y < SuoDi_Y && Player.transform.position.y - Enemy.transform.position.y > -SuoDi_Y)
        {
            //玩家和怪物水平方向是否接近
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
       //玩家和怪物不在同一平台
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
    //怪物追逐移动函数
    public void Move(GameObject a,GameObject b)
    {
        Vector3 AtoB=new Vector3((b.transform.position.x - a.transform.position.x)/System.Math.Abs(b.transform.position.x - a.transform.position.x), 0, 0);
        a.transform.position = AtoB * Speed+a.transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        //变量实例化
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
        //如果靠近追玩家
        if (Chasing)
        {
            Move(Enemy, Player);
        }
        //不靠近就回到原位
        else
        {
            if (System.Math.Abs(EnemyLocation.transform.position.x - Enemy.transform.position.x)>0.5f)
            {
                Move(Enemy, EnemyLocation);
            }
        }
    }
}
