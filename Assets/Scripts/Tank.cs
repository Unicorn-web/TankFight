/*
	Title:
	坦克基类
	
	Description:
	属性及控制
	
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Tank : MonoBehaviour
{
	public enum TankDirection//方向枚举
	{
		DirUp,
		DirDown,
		DirLeft,
		DirRight,
		None
	}

	public TankDirection direction;//坦克方向
	public float moveSpeed;//移动速度
	public Sprite[] sprites;//坦克图集
	protected SpriteRenderer spriteRender;//精灵渲染器
	protected Rigidbody2D rigidbody;//刚体
	public int hp;

	//子弹预制体
	public Bullet tankBullet;
	public float fireSpeed;
	protected float fireTime;//控制发射频率的 时间值

	//特效预设
	public GameObject explosion;//爆炸特效

	//爆炸音效
	public AudioClip explosionClip;//爆炸音效

	protected void Awake()
	{
		spriteRender = GetComponent<SpriteRenderer>();
		rigidbody = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		Move();//移动更新
		Fire();//发射子弹更新
	}

	protected virtual void Dead()
	{

		//创建特效
		var e = Instantiate(explosion, transform.position, transform.rotation);
		e.transform.localScale = Vector2.one;//重置缩放
		AudioSource.PlayClipAtPoint(explosionClip, Camera.main.transform.position);
		Destroy(e, 0.25f);
		//移除坦克管理器
		GameControl.Instance.RemoveTank(this);
		//删除坦克
		Destroy(this.gameObject);
		
	}

	protected virtual void Move()
	{
		//...由派生类自行实现
	}

	protected virtual void Fire()
	{
		//开火函数  敌人自动发射  我方 控制发射
	}

	public static Vector2 GetDirection(TankDirection dir)
	{
		switch (dir)
		{
			case TankDirection.DirUp:
				return Vector2.up;
				break;
			case TankDirection.DirDown:
				return Vector2.down;
				break;
			case TankDirection.DirLeft:
				return Vector2.left;
				break;
			case TankDirection.DirRight:
				return Vector2.right;
				break;
			default:
				break;
		}
		return  Vector2.zero;
	}

}

