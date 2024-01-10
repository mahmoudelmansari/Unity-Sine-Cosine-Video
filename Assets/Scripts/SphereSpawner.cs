using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    [SerializeField] GameObject sphere;
    [SerializeField] float xOffset;
    [SerializeField] float zOffset;
    List<GameObject> spheres = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            StartCoroutine(SpawnSpheres());

        if (Input.GetKeyDown(KeyCode.RightShift))
            RemoveSpheres();
    }



    IEnumerator SpawnSpheres()
    {
        float z = 0;
        float x = -10;
        Vector3 pos = new Vector3(x, 0, z);
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                float randomX = Random.Range(-8f, 8f);
                float randomZ = Random.Range(-8f, 8f);
                GameObject sp = Instantiate(sphere, pos + new Vector3(randomX, 0, randomZ), Quaternion.identity);
                spheres.Add(sp);
                x += xOffset;
                pos = new Vector3(x, 0, z);
                yield return null;
            }

            z += zOffset;
            x = -10;
            pos = new Vector3(x, 0, z);
        }
    }

    void RemoveSpheres()
    {
        foreach(GameObject s in spheres)
        {
            s.GetComponent<Sphere>().RemoveSphere();
        }
    }
}
