using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType{
        Goblin = 0,
        Skeleton = 1,
    }
    public EnemyType enemyType;
    public Sprite[] sprites;
    public int currentHP;
    private SpriteRenderer spriteRenderer;
    public int hp;
    public int atk;
    public int def;
    public int spd;

    public BattleState bstate;
    
    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        //Debug.Log("Inside the start of enemy class the current and max hp are:  " + currentHP + "     "+ hp);
        SetSprite();
    }

    public void setValues(){
        int dif = (int)bstate.currentDifficulty;
        switch(dif){
            //Easy Difficulty
            case 0:
                hp = Random.Range(15,20);
                atk = Random.Range(2,4) + 5;
                def = Random.Range(2,4) + 5;
                spd = 2;
            break;
            //Normal Difficulty
            case 1:
                hp = Random.Range(25,40);
                atk = Random.Range(2,6) + 5;
                def = Random.Range(2,6) + 5;
                spd = Random.Range(2,6) + 5;
            break;
            //Hard Difficulty
            case 2:
                hp = Random.Range(40,60);
                atk = Random.Range(4,8) + 10;
                def = Random.Range(4,8) + 10;
                spd = Random.Range(4,6) + 10;
            break;
        }
        currentHP = hp;

    }

    private void SetSprite(){
        if (sprites != null && sprites.Length > 0)
        {
            int randomIndex = Random.Range(0, 2);
            enemyType = (EnemyType)randomIndex;
            spriteRenderer.sprite =  sprites[randomIndex];
        }
        else
        {
            Debug.LogError("Sprites array is not assigned or empty!");
        }
    }

    public int EnemyAttack(Player player){
        return atk;
    }

    public void SetCurrentHP(int h){
        Debug.Log("setting enemyhealth to:  " + h);
        currentHP = h;
    }

    public int GetHP(){
        return hp;}     public int GetCurrentHP(){return currentHP;}




}
