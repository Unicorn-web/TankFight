/*
	Title:
	
	Description:
	
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Bore : MonoBehaviour
{
	public Tank tankPrefab;//创建对象

	private void Awake()
	{
		Invoke("CreatTank", 1);//延迟调用
	}

	void CreatTank()
	{
		var tank = GameObject.Instantiate(tankPrefab);
		tank.transform.position = transform.position;
		//加入管理器
		if (tank.tag == "Enemy")
		{
			GameControl.Instance.AddTank(tank);
		}
		Destroy(this.gameObject);
	}
}

