using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkButton : MonoBehaviour
{
    public void AtkButtonPress() 
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>().Attack();
    }
}
