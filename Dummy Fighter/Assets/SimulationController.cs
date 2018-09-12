using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;
using System;
public class SimulationController : MonoBehaviour {

    // Use this for initialization
    private List<LinkData> PlayerChain;
    private List<LinkData> EnemyChain;
    public Image PlayerHealthBar;
    public Image EnemyHealthBar;
    public Animator enemyAnim;
    public Animator playerAnim;
    public bool playerReturn { get; set; }
    public bool enemyReturn { get; set; }
    int indexPlayer = 0;
    int indexEnemy = 0;
    int bartime = 0;
    bool gameEnd = false;
   
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
            if (UnityEngine.Random.Range(0, 1) == 1)
            {
                if (chain.Count > 1)
                    ChainSwapper(chain, index);
            }
            return PlayerState.Idle;
        }

        if (chain.Count > 1)
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

    public void OnExit ()
    {
        Application.Quit();
    }

     public void OnReplay ()
    {

        SceneManager.LoadScene(1);
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
                if (PlayerChain.Count > 1)
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
                if (EnemyChain.Count > 1)
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

    int UpdateBar(PlayerState p1 , PlayerState p2)
    {
        int damage = 0;

        if (p2 == PlayerState.Attack)
        {
            switch (p1)
            {
                case PlayerState.Attack:
                    damage = 3;
                    break;
                case PlayerState.DodgeReturn:
                    damage = 2;
                    break;
                case PlayerState.Idle:
                    damage = 1;
                    break;

            }
        }
        //Debug.Log(damage);
        return damage;

    }

   
    void Update ()
    {
       
            Observable.Timer(TimeSpan.FromSeconds(4)).Subscribe(x => {
               
             
            if (!gameEnd)
            {
                    updateStates();
                   
                    
                    PlayAnimation(statePlayer, playerAnim);
                    PlayAnimation(stateEnemy, enemyAnim);
                

                
                    Damage(PlayerHealthBar, UpdateBar(statePlayer, stateEnemy));
                    Damage(EnemyHealthBar, UpdateBar(stateEnemy, statePlayer));
                
                //Debug.Log(stateEnemy + " " +statePlayer);

                GameOver();
            }

            });
        
    }

    private void PlayAnimation(PlayerState statePlayer, Animator Anim)
    {
       switch (statePlayer)
        {
            case PlayerState.Attack:
                Anim.SetBool("Attack", true);
                Observable.Timer(TimeSpan.FromMilliseconds(500)).Subscribe(y => {
                Anim.SetBool("Attack", false);
                });
                break;
            case PlayerState.Dodge:
                Anim.SetBool("Dodge", true);
                Observable.Timer(TimeSpan.FromMilliseconds(500)).Subscribe(y => {
                    Anim.SetBool("Dodge", false);
                });
                break;
           
          
        }
    }

   

    public void Damage(Image bar, int damage)
    {
        float amount = (float)damage / 10f;
        Debug.Log(bar.fillAmount+" " + amount);
        if (amount < bar.fillAmount)
            bar.fillAmount = bar.fillAmount - amount;
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
                playerAnim.SetBool("Lose", true);
                enemyAnim.SetBool("Lose", true);
                Debug.Log("draw");
            }
            else
            {
                playerAnim.SetBool("Lose", true);
                enemyAnim.SetBool("Win", true);
                Debug.Log("Player Lose");
            }
            gameEnd = true;
            
        }
        else if (EnemyHealthBar.fillAmount == 0)
        {
            Debug.Log("Player Win");
            playerAnim.SetBool("Win", true);
            enemyAnim.SetBool("Lose", true);
            gameEnd = true;
            
        }

    }
	// Update is called once per frame
	
}
