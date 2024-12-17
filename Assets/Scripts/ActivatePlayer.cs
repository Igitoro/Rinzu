using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePlayer : MonoBehaviour
{
    public GameObject[] prefabsPlayer;

    void Start()
    {
        int index = PlayerPrefs.GetInt("SelectPlayer");
        Instantiate(prefabsPlayer[index], transform.position, Quaternion.identity, transform);
    }
}
