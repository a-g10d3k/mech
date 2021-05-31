using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using System;
using System.ComponentModel;

public class Health : MonoBehaviour
{
    public int MaxHP;
    public int HP { get => _hp; set => SetHP(value); }
    [SerializeField]
    private int _hp = -1;
    [Description("Kill(Debug)")]
    public bool Kill;
    private bool _dead = false;

    private void Start()
    {
        if (_hp == -1) { _hp = MaxHP; }
    }

    private void OnValidate()
    {
        if (Kill)
        {
            Kill = false;
            if (Application.isPlaying)
            {
                HP = 0;
            }
        }
    }

    private void SetHP(int value)
    {
        if(value > MaxHP)
        {
            _hp = MaxHP;
        }
        else
        {
            _hp = value;
        }
        if(value <= 0 && !_dead)
        {
            OnDeath(EventArgs.Empty);
            _dead = true;
        }
    }

    private void OnDeath(EventArgs e)
    {
        Death(this, e);
    }
    public event EventHandler Death;
}
