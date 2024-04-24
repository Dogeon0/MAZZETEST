using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimary : MonoBehaviour
{

    public int hp;
    public int atk;
    public int def;
    public int spd;
    public int currentHP;
    
    
    // Start is called before the first frame update
    void Start()
    {
        hp = PlayerData.Instance.GetMaxHP();
        currentHP = PlayerData.Instance.GetCurrentHP();
    }



    

}
