/*
	Title:
	
	Description:
	
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : Tank
{
	public float randomDirectionSpeed;//随机方向的频率
	protected float directionTime;//控制随机方向频率时间


	protected override void Fire()
	{
		fireTime += Time.deltaTime;

		if (fireTime > fireSpeed)
		{
			fireTime = 0;
			//发射子弹
			var bullet = Instantiate(tankBullet,transform.position,Quaternion.identity);
			bullet.Init(this.direction,Bullet.BulleType.EnemyBullet,GameControl.Instance.transform);//设置子弹方向
		}
	}



	protected override void Move()
	{
		directionTime += Time.deltaTime;
		if (directionTime > randomDirectionSpeed)
		{
			directionTime = 0;
			//随机方向
			this.direction = (TankDirection)Random.Range(0, 4);

			//根据朝向设置精灵图
			spriteRender.sprite = sprites[(int)direction];

		}
		//实时移动
		rigidbody.velocity = Tank.GetDirection(direction) * moveSpeed;


	}

	private void OnCollisionEnter2D(Collision2D collision)
	{

		int dir = ((int)direction + Random.Range(1,4))%4;
		direction = (TankDirection)dir;
		
		//根据朝向设置精灵图
		spriteRender.sprite = sprites[(int)direction];
	}



	private void OnCollisionExit2D(Collision2D collision)
	{

	}







}

