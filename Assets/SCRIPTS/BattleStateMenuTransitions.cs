using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BattleStateMenuTransitions : MonoBehaviour
{
    public bool transitionsDone = false;
    public GameObject mainMenu;
    public GameObject fightOptionMenu;
    public float seg;
    public Vector3 finalPosMain;
    public Vector3 finalPosFightOption;
    public Vector3 startPosMain;
    public Vector3 startPosFightOption;

    public ButtonKeyboardNavigation battleMenu;
    public ButtonKeyboardNavigation attackMenu;

    void Start(){
        mainMenu.transform.DOMove(finalPosMain,seg);
        StartCoroutine(waity());
    }

    public void bringAttackOptions(){
        transitionsDone = false;
        Debug.Log("Showing attack Menu");
        fightOptionMenu.transform.DOMove(finalPosFightOption,seg);
        StartCoroutine(waity());
    }

    public void hideAttackOptions(){
        transitionsDone = false;
        Debug.Log("Hiding attack Menu");
        attackMenu.isActive = false;
        battleMenu.isActive = true;
        fightOptionMenu.transform.DOMove(startPosFightOption,seg);
        StartCoroutine(waity());
    }

    IEnumerator waity(){
        yield return new WaitForSeconds(seg);
        Debug.Log("transitions are done");
        transitionsDone = true;
    }
}
