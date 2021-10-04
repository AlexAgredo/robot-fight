using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float healt;
    public float maxHealth;

    private void Awake()
    {
        healt = maxHealth;
    }
}
