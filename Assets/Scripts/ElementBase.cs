/*
	Title:
	
	Description:
	
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ElementBase : MapElement
{
	public Sprite sprite;//销毁替代图
	SpriteRenderer sr;
	bool isGameOver;
	public AudioClip explosionClip;
	private void Start()
	{
		sr = GetComponent<SpriteRenderer>();
	}
	protected override void Collision(Vector3 _bulletPos)
	{
		if (isGameOver)
		{
			return;
		}

		var e = GameObject.Instantiate(explosion, transform.position, Quaternion.identity);//创建爆炸特效
		e.transform.localScale = Vector3.one;
		AudioSource.PlayClipAtPoint(explosionClip, Camera.main.transform.position);
		GameObject.Destroy(e.gameObject, 0.25f);
		//切换精灵图
		sr.sprite = sprite;
		isGameOver = true;
		//游戏结束
		GameControl.Instance.GameOver();
	}
}

