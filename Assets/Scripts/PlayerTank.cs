/*
	Title:
	
	Description:
	
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTank : Tank
{

	public enum PlayerType
	{
		P1,
		P2
	}


	public PlayerType type;
	TankDirection collisionDir = TankDirection.None;//碰撞方向
	//无敌圈
	public GameObject shield;
	//无敌状态
	bool isShield = true;

	//发射声效
	public AudioClip fireClip;



	

	protected void Awake()
	{
		base.Awake();
		Invoke("OverShield", 5);

		
	}

	void OverShield()
	{
		shield.SetActive(false);
		isShield = false;
	}

	protected override void Dead()
	{
		if (isShield)
		{
			return;
		}
		//控制游戏结束逻辑
		GameControl.Instance.lifeCount -= 1;
		GameControl.Instance.UpdateUI();
		//重生
		GameControl.Instance.PlayerReborn(0);
		base.Dead();
	}

	protected override void Move()
	{
		float h,v;
		
		if (type == PlayerType.P1)
		{
			h = Input.GetAxis("P1H");
			v = Input.GetAxis("P1V");
		}
		else
		{
			h = Input.GetAxis("P2H");
			v = Input.GetAxis("P2V");
		}
		

		//保证只要一个方向的输入处理
		if (h != 0)
		{
			v = 0;
		}
		else if(v != 0)
		{
			h = 0;
		}


		//赋值坦克方向
		if (h <0)//左
		{
			direction = TankDirection.DirLeft;
		}
		else if(h > 0)//右
		{
			direction = TankDirection.DirRight;
		}
		else if (v > 0)//上
		{
			direction = TankDirection.DirUp;
		}
		else if(v < 0)//下
		{
			direction = TankDirection.DirDown;
		}
		//设置图片 不同方向
		spriteRender.sprite = sprites[(int)direction];
		//移动
		if (direction != collisionDir)
		{
			rigidbody.velocity = new Vector3(h, v, 0) * moveSpeed;
		}
		

	}


	public KeyCode fireKey;
	protected override void Fire()
	{
		fireTime += Time.deltaTime;
		if (Input.GetKeyDown(fireKey))
		{
			if (fireTime > fireSpeed)
			{
				fireTime = 0;
				//发射子弹
				Bullet bullet = GameObject.Instantiate(tankBullet, transform.position, Quaternion.identity);
				bullet.Init(direction, Bullet.BulleType.PlayerBullet, GameControl.Instance.transform);
				//播放音效
				AudioSource.PlayClipAtPoint(fireClip,Camera.main.transform.position);
			}
			
		}
	}

	

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.tag == "Enemy")
		{
			collisionDir = direction;//设置碰撞方向 防止该方向有速度 推动坦克
			rigidbody.velocity = Vector2.zero;//速度停止
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		collisionDir = TankDirection.None;
	}


}

