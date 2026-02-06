using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int vida = 80;
    public float speed = 4f;
    private Rigidbody eRb;
    private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eRb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        eRb.AddForce((player.transform.position - transform.position).normalized * speed);
    }
}
