using UnityEngine;
using System.Collections;

public class MainBehaviour : MonoBehaviour {

	public GameObject[] playerPlanes = new GameObject[5];
	public GameObject[] enemyPlanes = new GameObject[5];
	public GameObject[] players = new GameObject[5];
	[Tooltip("0 / 3 / 6 / 9 / 12")]
	public int[] scores = new int[5];

	void Start () {
		for(int i = 1; i<players.Length; i++)
			{
			//players[i] = enemyPlanes[0];
			}
	}
	

	void Update () {
	
	
		}

	public void StartCor()
		{
		StartCoroutine(Instcor());
		}

	IEnumerator Instcor()
		{

		yield return new WaitForSeconds(3f);
	
		for(int i = 0; i < players.Length; i++)
			{

			if(players[i] == null)
				{
				if(i == 0)
					{
					switch(scores[i])
						{
						case 0 : 
						case 1 :
						case 2 :
							players[i] = (GameObject)Instantiate(playerPlanes[0]); break; 

						case 3 : 
						case 4 :
						case 5 :
							players[i] = (GameObject)Instantiate(playerPlanes[1]); break; 
						case 6 : 
						case 7 :
						case 8 :
							players[i] = (GameObject)Instantiate(playerPlanes[2]); break;
						case 9 : 
						case 10 :
						case 11 :
							players[i] = (GameObject)Instantiate(playerPlanes[3]); break;
						case 12 : 
						case 13 :
						case 14 :
							players[i] = (GameObject)Instantiate(playerPlanes[4]); break;
					}
				}
				else
					{
					switch(scores[i])
						{
						case 0 : 
						case 1 :
						case 2 :
							players[i] = (GameObject)Instantiate(enemyPlanes[0]); 
							players[i].name += i.ToString();
							break; 
						case 3 : 
						case 4 :
						case 5 :
							players[i] = (GameObject)Instantiate(enemyPlanes[1]); 
							players[i].name += i.ToString();
							break; 
						case 6 : 
						case 7 :
						case 8 :
							players[i] = (GameObject)Instantiate(enemyPlanes[2]); 
							players[i].name += i.ToString();
							break;
						case 9 : 
						case 10 :
						case 11 :
							players[i] = (GameObject)Instantiate(enemyPlanes[3]); 
							players[i].name += i.ToString();
							break;
						case 12 : 
						case 13 :
						case 14 :
							players[i] = (GameObject)Instantiate(enemyPlanes[4]); 
							players[i].name += i.ToString();
							break;
						}
					}
				}
			}
		}
		

	public void Scores(string name)
		{
		for(int i = 0; i < players.Length; i++)
			{
			if(players[i] != null)
				{
				if(players[i].name == name)
					{
					scores[i]++;
					}
				}
			}
		}

	}
