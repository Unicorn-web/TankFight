/*
	Title:
	
	Description:
	
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
	bool isGameOver;
	public int enemyTankMaxNum;//最大数
	List<Tank> tankList;//坦克容器
	public Bore[] enemyBorePrefabs;//敌人生成点预制体

	public Transform[] randomPos;
	public GameObject gameOverUI;

	public Bore[] players;//玩家出生点预设
	public Transform[] playerPos;


	//P1玩家数据
    int killTankCount;
	public int lifeCount;

	public Text tankCountTxt;//击杀数量
	public Text lifeCountTxt;//生命数量

	//P2玩家数据


	private void Awake()
	{
		instance = this;

		StartCoroutine("CreateEnemy");

		tankCountTxt = GameObject.Find("P1TankCount").GetComponent<Text>();
		lifeCountTxt = GameObject.Find("P1LifeCount").GetComponent<Text>();

		//创建玩家
		for (int i = 0; i < players.Length; i++)
		{
			var player =Instantiate(players[i]);
			player.transform.position = playerPos[i].position;
		}

		UpdateUI();
	}

	public void PlayerReborn(int _playeType)
	{
		if (lifeCount > 0)
		{
			var player = Instantiate(players[_playeType]);
			player.transform.position = playerPos[_playeType].position;
		}
		else
		{
			GameOver();
		}
		
	}

	IEnumerator CreateEnemy()
	{
		while (true)
		{
			yield return new WaitForSeconds(1);
			if (tankList.Count < enemyTankMaxNum)
			{
				//随机生成坦克出生地
				var bore = Instantiate(enemyBorePrefabs[UnityEngine.Random.Range(0, enemyBorePrefabs.Length)]);
				//随机位置
				bore.transform.position = randomPos[UnityEngine.Random.Range(0, randomPos.Length)].position;
			}
			
		}
	}

	public void GameOver()
	{
		gameOverUI.SetActive(true);
		Invoke("ReStart", 3);
	}

	void ReStart()
	{
		SceneManager.LoadScene("StartScene");
	}

	public void UpdateUI()
	{
		lifeCountTxt.text = "x " + lifeCount;
	}

	public void AddTank(Tank tank)
	{
		tankList.Add(tank);
	}
	public void RemoveTank(Tank tank)
	{
		tankList.Remove(tank);
	}

	public void AddKillTank()
	{
		killTankCount++;
		tankCountTxt.text = "x " + killTankCount;
	}



	#region 单例
	GameControl() 
	{
		tankList = new List<Tank>();
	}
	static GameControl instance;
	public static  GameControl Instance
	{
		get
		{
			return instance;
		}
	}
	#endregion
}

