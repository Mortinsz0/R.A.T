using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
  
        public GameObject hormiga;
        private Vector3 coordinates = new Vector3(0f, 4f, 10f);

        private int xzLimits = 30;
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        float xzRPosition = Random.Range (-xzLimits, xzLimits);
        Vector3 randomSpawn = new Vector3(xzRPosition, 4f, xzRPosition);
        Instantiate(hormiga,randomSpawn,hormiga.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
