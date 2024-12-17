using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExpPlayer : MonoBehaviour
{
    [SerializeField] int ExpCur, ExpMax, LVLcur;
    [SerializeField] Slider ExpBar;
    [SerializeField] Text ExpTxt;


    public void Start()
    {
        ExpTxt = GameObject.FindGameObjectWithTag("UI").GetComponent<ObjectFinder1>().ExpText;
        ExpBar = GameObject.FindGameObjectWithTag("UI").GetComponent<ObjectFinder1>().ExpSlider;
        ExpBar.maxValue = ExpMax;
    }
    
    public void Update()
    {
        ExpTxt.text = ExpCur + "/" + ExpMax;
    }

    private void HandleExpChange(int newExp)
    {
        ExpCur += newExp;
        ExpBar.value = ExpCur;
        if(ExpCur >= ExpMax)
        {
            LevelUp();
        }
    }

    private void OnEnable()
    {
        ExpManager.Instance.OnExpChange += HandleExpChange;
    }

    private void OnDisable()
    {
        ExpManager.Instance.OnExpChange -= HandleExpChange;
    }

    private void LevelUp()
    {
        LVLcur++;
        ExpCur = 0;
        ExpMax += 1;
        ExpBar.maxValue = ExpMax;
        ExpBar.value = 0;
        gameObject.GetComponent<Health>().AddHpOnLvlUp(); 
        gameObject.GetComponent<PlayerAttack>().AddAtkOnLvlUp();
    }
}
