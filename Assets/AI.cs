using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public Transform Objetivo;
    public float velocidad;
    public NavMeshAgent IA;



    // Update is called once per frame
    void Update()
    {
        IA.speed = velocidad;
        IA.SetDestination(Objetivo.position);
    }
}
