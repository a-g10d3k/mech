using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovement Movement { get => gameObject.GetComponent<PlayerMovement>(); }
    public PlayerAnimation Animation { get => gameObject.GetComponent<PlayerAnimation>(); }
    public PlayerAudio Audio { get => gameObject.GetComponent<PlayerAudio>(); }
    public MouseLook MouseLook { get => gameObject.GetComponent<MouseLook>(); }
    public AutoGunConvergence AutoGunConvergence { get => gameObject.GetComponent<AutoGunConvergence>(); }
    public PlayerAiming Aiming { get => gameObject.GetComponent<PlayerAiming>(); }
    public GameObject Viewmodel; //the viewmodel object
}
