/*
	Title:
	子弹类
	Description:
	控制子弹移动
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public enum BulleType
	{
		PlayerBullet,
		EnemyBullet
	}

	public BulleType type;//子弹类型
	Rigidbody2D rigidbody2D;
	public float moveSpeed;

	public GameObject explosion;//子弹特效

	private void Awake()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
	}

	public void Init(Tank.TankDirection _dir,BulleType _type,Transform _parent = null)
	{
		rigidbody2D.velocity = Tank.GetDirection(_dir) * moveSpeed;
		type = _type;
		transform.parent = _parent;
	}

	//碰撞检测
	private void OnTriggerEnter2D(Collider2D collision)
	{

		//地图元素碰撞
		var element = collision.transform.GetComponent<MapElement>();
		if (element != null)//确认碰撞地图元素
		{
			collision.SendMessage("Collision", transform.position);//地图元素处理
			Destroy(this.gameObject);//删除子弹
			return;
		}

		//子弹与子弹碰撞
		if (collision.tag == "Bullet"  )
		{
			if (this.type != collision.GetComponent<Bullet>().type)
			{
				Destroy(this.gameObject);
				return;
			}
		}

		//坦克的碰撞
		Debug.Log("检测碰撞函数");
		//敌方子弹
		if (type == BulleType.EnemyBullet && collision.tag == "Player")
		{
			collision.SendMessage("Dead");
			Destroy(this.gameObject);//删除子弹

		}
		//我方子弹
		else if (type == BulleType.PlayerBullet && collision.tag == "Enemy")
		{
			collision.SendMessage("Dead");
			GameControl.Instance.AddKillTank();
			Destroy(this.gameObject);//删除子弹

		}


	}





}

