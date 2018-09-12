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
    public bool playerReturn { get; set; }
    public bool enemyReturn { get; set; }
    int indexPlayer = 0;
    int indexEnemy = 0;
    PlayerState statePlayer;
    PlayerState stateEnemy;

    void Start () {
        PlayerChain = JsonMapper.ToObject<List<LinkData>>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Player.json"));
        EnemyChain  = JsonMapper.ToObject<List<LinkData>>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Enemy.json"));
        playerReturn = false;
        enemyReturn = false;


    }
	
    public PlayerState ReadChain(List<LinkData> chain, int index)
    {
        if (chain[index].Type == LinkState.Attack)
            return PlayerState.Attack;
        else if (chain[index].Type == LinkState.Dodge)
            return PlayerState.Dodge;
        else if (chain[index].Type == LinkState.Watch)
        {
            //random
            ChainSwapper(chain, index);

            return PlayerState.Idle;
        }


        ChainSwapper(chain, index);
       
        return PlayerState.Idle;
    }
    void ChainSwapper (List<LinkData> chain , int index)
    {
        int swapIndex=0,propertyIndex=0;
        bool increment = true;
        if (index == chain.Count - 1)
            propertyIndex = 0;
        else
            propertyIndex = index+1;

            switch (chain[propertyIndex].Type)
                {
                    case LinkState.Attack:
                        swapIndex = chain[0].Attack;
                        break;
                    case LinkState.Dodge:
                        swapIndex = chain[0].Dodge;
                        break;
                    case LinkState.Think:
                        swapIndex = chain[0].Idle;
                        break;
                    case LinkState.Watch:
                        swapIndex = chain[0].Idle;
                        break;

                }

        if (swapIndex < 0)
        {
            swapIndex -= 2 * swapIndex;
            increment = false;
        }
        for (int i=0; i<swapIndex;  i++)
        {
            if (increment)
            {
                if (propertyIndex == chain.Count - 1)
                    propertyIndex = 0;
                else
                    propertyIndex++;
            }
            else
            {
                if (propertyIndex == 0)
                    propertyIndex = chain.Count-1;
                else
                    propertyIndex--;
            }

        }
        swapLinks( chain, propertyIndex, index + 1 );

        
        
    }

    void swapLinks (List<LinkData> chain, int index1, int index2)
    {
        LinkData temp = chain[index1];
        chain[index1] = chain[index2];
        chain[index2] = temp; 
    }

    void updateStates()
    {
        if (playerReturn)
        {
            playerReturn = false;
            if (statePlayer == PlayerState.Attack)
                statePlayer = PlayerState.AttackReturn;
            else
                statePlayer = PlayerState.DodgeReturn;
        }
        else
        {
            if (indexPlayer == PlayerChain.Count - 1)
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

            if (statePlayer == PlayerState.Attack || statePlayer == PlayerState.Dodge)
                playerReturn = true;
        }


        if (enemyReturn)
        {
            enemyReturn = false;
            if (stateEnemy == PlayerState.Attack)
                stateEnemy = PlayerState.AttackReturn;
            else
                stateEnemy = PlayerState.DodgeReturn;
        }

        else
        {
            if (indexEnemy == EnemyChain.Count - 1)
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

            if (stateEnemy == PlayerState.Attack || stateEnemy == PlayerState.Dodge)
                enemyReturn = true;
        }


    }

    void Update ()
    {

       
        

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
