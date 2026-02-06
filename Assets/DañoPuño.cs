using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DañoPuño : MonoBehaviour
{
    [SerializeField]
    private int daño = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemigo enemigo = other.GetComponent<Enemigo>();
            if (enemigo != null)
            {
                enemigo.RecibirDaño(daño);
            }
        }
    }
}
