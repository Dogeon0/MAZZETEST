using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;
    private void Awake()
    {
        // Ensure only one instance of PlayerData exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Don't destroy this object when loading new scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public TextHealth text;
    
    
    public int hp;
    public int currentHP;
    public int atk;
    public int def;
    public int spd;

    void Start(){
        currentHP = hp;
        text.RefreshHealthText();
    }



    void Restart(){
        
    }
    
    public int GetAtk(){
        return atk;
    }
    public int GetDef(){
        return def;
    }
    public int GetSpd(){
        return spd;
    }
    public int GetCurrentHP(){
        return currentHP;
    }
    public int GetMaxHP(){
        return hp;
    }
    public void SetCurrentHP(int h){
        currentHP = h;
    }
}
