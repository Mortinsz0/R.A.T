using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [Header("Atributos del enemigo")]
    public int vida = 80;
    public float rangoAtaque = 1.5f; // Distancia mínima para atacar
    public int daño = 10;            // Daño por ataque
    public float tiempoEntreAtaques = 1.5f; // Cooldown entre ataques

    private Transform jugador;
    private bool puedeAtacar = true;

    void Start()
    {
        // Busca al jugador por tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            jugador = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("?? No se encontró ningún objeto con el tag 'Player' en la escena.");
        }
    }

    void Update()
    {
        if (jugador == null) return;

        // Calcula distancia solo en el plano (sin eje Y)
        Vector3 pos1 = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 pos2 = new Vector3(jugador.position.x, 0, jugador.position.z);
        float distancia = Vector3.Distance(pos1, pos2);

        // Si el jugador está dentro del rango de ataque
        if (distancia <= rangoAtaque && puedeAtacar)
        {
            Debug.Log("Jugador dentro del rango de ataque. Distancia: " + distancia);
            StartCoroutine(AtacarJugador());
        }
    }

    IEnumerator AtacarJugador()
    {
        puedeAtacar = false;

        // Recalcula la distancia para evitar ataques fantasmas
        Vector3 pos1 = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 pos2 = new Vector3(jugador.position.x, 0, jugador.position.z);
        float distancia = Vector3.Distance(pos1, pos2);

        if (jugador != null && distancia <= rangoAtaque)
        {
            // Busca el script del jugador incluso si está en un hijo
            PLAYERSCRIPTS jugadorScript = jugador.GetComponentInChildren<PLAYERSCRIPTS>();

            if (jugadorScript != null)
            {
                jugadorScript.vida -= daño;
                Debug.Log("?? Jugador atacado. Vida actual: " + jugadorScript.vida);

                // Si el jugador muere
                if (jugadorScript.vida <= 0)
                {
                    Debug.Log("?? El jugador ha muerto.");
                    jugadorScript.gameObject.SetActive(false);
                }
            }
            else
            {
                Debug.LogWarning("?? No se encontró el script PLAYERSCRIPTS en el jugador o sus hijos.");
            }
        }

        // Espera antes del siguiente ataque
        yield return new WaitForSeconds(tiempoEntreAtaques);
        puedeAtacar = true;
    }

    public void RecibirDaño(int cantidad)
    {
        vida -= cantidad;
        Debug.Log("?? Vida del enemigo: " + vida);

        if (vida <= 0)
        {
            Debug.Log("?? Enemigo destruido.");
            Destroy(gameObject);
        }
    }
}