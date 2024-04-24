using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleState : MonoBehaviour
{
    public enum DifficultyLevel{
        Easy = 0,
        Medium = 1,
        Hard = 2,
    }
    public DifficultyLevel currentDifficulty;

    public Player player;
    public Enemy enemy;

    public bool myTurn = true;
    public bool enemyTurn = false;

    public LevelLoader loadScene;

    public HealthBarPlayer healthBarPlayer;
    public HealthBarEnemy healthBarEnemy;

    public Animator animPlayer;
    public Animator animEnemy;

    
    public BattleStateMenuTransitions menuTransitions;

    public ButtonKeyboardNavigation battleMenu;
    public ButtonKeyboardNavigation attackMenu;

//Player options
    public void FightOption()
    {
        if (myTurn && !enemyTurn)
        {
            //bring the attack options panel
            menuTransitions.bringAttackOptions();
            battleMenu.isActive = false;
            attackMenu.isActive = true;
        }
    }
    public void RunOption(){
        if(myTurn && !enemyTurn){
            loadScene.LoadXLevel(1);
        }
    }
    public void DefendOption(){
        if(myTurn && !enemyTurn){
            animPlayer.SetTrigger("Block");
            player.PlayerDefend();
            myTurn = false;
            enemyTurn = true;
        }
    }

    //player attack options
    public void slash(){
        if(menuTransitions.transitionsDone == true && myTurn == true){
            Debug.Log("Slashing");
            animPlayer.SetTrigger("Slash");
            StartCoroutine(slashStuff(1.5f));
        }
    }
    public void superSlash(){
        if(menuTransitions.transitionsDone == true && myTurn == true){
            Debug.Log("Super Slashing");
            animPlayer.SetTrigger("CSlash");
            StartCoroutine(CSlashStuff(2f));
        }
    }


//Enemy Turn shi

    public void EnemyTurn(){
        if(enemy.currentHP <= 0){
            Debug.Log("SETTING SCENE TO 1");
            PlayerData.Instance.SetCurrentHP(player.currentHP);
            myTurn = true;
            enemyTurn = false;
            loadScene.LoadXLevel(1);
        }else{
            animEnemy.SetTrigger("Attack");
            StartCoroutine(enemyAtk(1));
        }
        
    }




    //Animations IEnumerators
    
    IEnumerator enemyAtk(float seg){
        yield return new WaitForSeconds(seg);
        Debug.Log("Enemy attacking");
        if(player.isDefending){
        }
        else{

            int damageDone = enemy.EnemyAttack(player);
            healthBarPlayer.DoDamage(damageDone);

            if(player.currentHP <= 0){
                enemyTurn = false;
                animEnemy.SetBool("enemyTurn",false);
                myTurn = true;
                loadScene.LoadXLevel(0);
                
            }else{
                enemyTurn = false;
                animEnemy.SetBool("enemyTurn",false);
                myTurn = true;
            }
        }
    }
    
    IEnumerator slashStuff(float seg){
        if(myTurn == true){
            myTurn = false;
            yield return new WaitForSeconds(seg);
            Debug.Log("Slash normal");
            int damageDone = player.PlayerAttack(enemy,false);
            Debug.Log("Slash DAMAGE:  " + damageDone);
            healthBarEnemy.DoDamage(damageDone);
            enemyTurn = true;
            animEnemy.SetBool("enemyTurn",true);
            EnemyTurn();
        }
    }

    IEnumerator CSlashStuff(float seg){
        if(myTurn == true){
            myTurn = false;
            yield return new WaitForSeconds(seg);
            Debug.Log("CHARGED SL:ASH");
            int damageDone = player.PlayerAttack(enemy,true);
            healthBarEnemy.DoDamage(damageDone);
            enemyTurn = true;
            animEnemy.SetBool("enemyTurn",true);
            EnemyTurn();
        }
    }
}
