/*
	Title:
	
	Description:
	
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseGame : MonoBehaviour
{
	public Transform p1;
	public Transform p2;

	private void Start()
	{
		transform.position = p1.position;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
		{
			transform.position = transform.position == p1.position ? p2.position : p1.position;
		}

		if (Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			//开始游戏
			if (transform.position == p1.position)
			{
				Debug.Log("选择一人游戏");
				SceneManager.LoadScene("Main");
			}
			else
			{
				Debug.Log("选择二人游戏待开发");
				SceneManager.LoadScene("Main");
			}	
		}
	}
}

