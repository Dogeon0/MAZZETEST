using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBarEnemy : MonoBehaviour
{
    public Enemy enemyScript;
    public Image barFill;
    private int HP;
    private int currentHP;
    private int newHp;
    public BattleState bstate;


    void Start()
    {
        enemyScript.setValues();
        UpdateHPbars();
    }

    void UpdateHPbars(){
        
        HP = enemyScript.GetHP();
        currentHP = enemyScript.GetCurrentHP();
        Debug.Log("Enemy's currentHP: " + currentHP + " and maxHP: " + HP);
        float fillAmount = (float)currentHP / HP;
        fillAmount = Mathf.Clamp01(fillAmount);
        barFill.DOFillAmount(fillAmount,0.3f);
    }

    public void DoDamage(int newDamage)
    {
        if (newDamage <= 0) return;
        
        currentHP = enemyScript.GetCurrentHP();
        Debug.Log("deducting health: " + newDamage + " from Enemy, with the currentHP:  " + currentHP);
        enemyScript.SetCurrentHP(currentHP - newDamage);
        UpdateHPbars();
    }


    void Update()
    {
        
    }
}
