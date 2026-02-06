using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueBasico : MonoBehaviour
{
    public GameObject Puño;
    public GameObject Puño2;
    private void Start()
    {
        Puño2.GetComponent<BoxCollider>().isTrigger = false;
        Puño.GetComponent<BoxCollider>().isTrigger = false;
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            gameObject.GetComponent<Animation>().Play("Puñetazo");
            Puño.GetComponent<BoxCollider>().isTrigger = true;
            Puño2.GetComponent<BoxCollider>().isTrigger = true;
            Invoke("DesactivarHitbox", 0.6f);


        }

    }
    void DesactivarHitbox()
    {
        Puño2.GetComponent<BoxCollider>().isTrigger = false;
        Puño.GetComponent<BoxCollider>().isTrigger = false;
    }
}
