using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.UI;

public class SimulationController : MonoBehaviour {

    // Use this for initialization
    private List<LinkData> PlayerChain;
    private List<LinkData> EnemyChain;
    public Image PlayerHealthBar;
    public Image EnemyHealthBar;

    int indexPlayer = 0;
    int indexEnemy = 0;
    PlayerState statePlayer;
    PlayerState stateEnemy;

    void Start () {
        PlayerChain = JsonMapper.ToObject<List<LinkData>>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Player.json"));
        EnemyChain  = JsonMapper.ToObject<List<LinkData>>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Enemy.json"));


    }
	
    public PlayerState ReadChain(List<LinkData> chain, int index)
    {
        if (chain[index].Type == LinkState.Attack)
            return PlayerState.Attack;
        else if (chain[index].Type == LinkState.Dodge)
            return PlayerState.Dodge;
        else
            return PlayerState.Idle;
    }
    void Update ()
    {
        /*
        if ((PlayerChain[indexPlayer] == null))
        {
            indexPlayer = 0;
            statePlayer = ReadChain(PlayerChain, indexPlayer);
            indexPlayer++;

        }
        else
        {
            statePlayer = ReadChain(PlayerChain, indexPlayer);
            indexPlayer++;
        }

        if ((EnemyChain[indexEnemy] == null))
        {
            indexEnemy = 0;
            stateEnemy = ReadChain(EnemyChain, indexEnemy);
            indexEnemy++;

        }
        else
        {
            stateEnemy = ReadChain(EnemyChain, indexEnemy);
            indexEnemy++;
        }

        */

    }

    public void Damage(Image bar, int damage)
    {
        float amount = damage / 10;
        if (amount < bar.fillAmount)
            bar.fillAmount -= damage / 10;
        else
        {
            bar.fillAmount = 0;
        }
            
    }

    public void GameOver()
    {
        if (PlayerHealthBar.fillAmount == 0)
        {
            if (EnemyHealthBar.fillAmount == 0)
            {
                //draw
            }
            else
            {
                  //EnemyWin
            }
        }
        else
        {
            //PlayerWin
        }

    }
	// Update is called once per frame
	
}
