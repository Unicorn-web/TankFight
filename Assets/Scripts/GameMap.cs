/*
	Title:
	
	Description:
	
*/

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameMap : MonoBehaviour
{
	public int row;
	public int col;
	int[,] mapArr;//二维瓦片地图数组

	public MapElement[] elements;

	private void Start()
	{
		Init();
		PrintMap();
	}

	public GameMap()
	{
		
	}

	public void PrintMap()
	{
		//遍历二维数组  创建地图元素
		for (int i = 0; i < row; i++)
		{
			for (int j = 0; j < col; j++)
			{
				//创建地图元素 
				MapElement element = null;
				switch ((MapElement.ElementType )mapArr[i,j])
				{
					case MapElement.ElementType.Road:
						element = GameObject.Instantiate(elements[0]);
						break;
					case MapElement.ElementType.Wall:
						element = GameObject.Instantiate(elements[1]);
						break;
					case MapElement.ElementType.Steel:
						element = GameObject.Instantiate(elements[2]);
						break;
					case MapElement.ElementType.Grass:
						element = GameObject.Instantiate(elements[3]);
						break;
					case MapElement.ElementType.Water:
						element = GameObject.Instantiate(elements[4]);
						break;
					case MapElement.ElementType.Base:
						element = GameObject.Instantiate(elements[5]);
						break;
				}
				element.transform.position = new Vector2(-10.15f + j, 7.5f - i);
				element.transform.parent = this.transform;
			}
		}
	}

	//读取地图配置文件
	void Init()
	{
		string url = Application.dataPath + "/Maps/MapLv1.txt";
		Debug.Log(url);
		if (!File.Exists(url))
		{
			Debug.LogError("地图配置文件出错！");
			return;
		}

		string[] rows = File.ReadAllLines(url);//读取文件的所有行

		//确定行列
		row = rows.Length;
		col = rows[0].Split('|').Length;

		//创建对应大小的二维数组
		mapArr = new int[row, col];

		//遍历所有行 分割所有行 赋值数组
		for (int i = 0; i < rows.Length; i++)
		{
			string[] cols = rows[i].Split('|');
			for (int j = 0; j < cols.Length; j++)
			{
				mapArr[i, j] = int.Parse(cols[j]);
			}
		}

	}
}

