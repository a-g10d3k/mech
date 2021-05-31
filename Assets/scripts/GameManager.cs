using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager I;
    public GameObject Player;

    private void Awake()
    {
        if (I != null)
        {
            Destroy(this);
        }
        else
        {
            I = this;
        }
    }
}
