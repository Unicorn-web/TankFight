/*
	Title:
	
	Description:
	
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MapElement : MonoBehaviour
{
	public enum ElementType//地图元素类型
	{
		Road,
		Wall,
		Steel,
		Grass,
		Water,
		Base,
		AirBox//空气墙
	}

	public ElementType type;//地图元素类型
	public GameObject explosion;//碰撞特效
	public AudioClip hitClip;
	protected virtual void Collision(Vector3 _bulletPos)
	{
		switch (type)
		{
			case ElementType.Wall:
				Destroy(this.gameObject);//删除自己
				break;
			case MapElement.ElementType.Steel:
				{
					//创建火花特效
					var e = GameObject.Instantiate(explosion, _bulletPos, Quaternion.identity);
					AudioSource.PlayClipAtPoint(hitClip, Camera.main.transform.position);
					GameObject.Destroy(e.gameObject, 0.25f);
				}
				break;

			case MapElement.ElementType.Base:
				{
					var e = GameObject.Instantiate(explosion, transform.position, Quaternion.identity);//创建爆炸特效
					e.transform.localScale = Vector3.one;
					GameObject.Destroy(e.gameObject, 0.25f);
					//切换精灵图
				}
				break;
			default:
				break;
		}
	}
}

