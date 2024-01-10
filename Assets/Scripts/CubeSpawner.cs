using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] Cube cube;
    [SerializeField] Cube redCube;
    [SerializeField] Cube blueCube;
    [SerializeField] float xOffset;
    [SerializeField] float zOffset;
    List<Cube> cubeList = new List<Cube>();
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
          StartCoroutine(SpawCubes());
    }

    IEnumerator SpawCubes()
    {
        float z = 0;
        float x = -10;
        Vector3 pos = new Vector3(x, 0, z);
        for (int i = 0; i < 30; i++)
        {
            for (int j = 0; j < 40; j++)
            {
                Cube c;
                if (i == 13 && j == 7)
                {
                    c = Instantiate(redCube, pos, cube.transform.rotation);
                }
                else if(i == 15 && j == 25)
                {
                    c = Instantiate(blueCube, pos, cube.transform.rotation);
                }
                else
                {
                    c = Instantiate(cube, pos, cube.transform.rotation);
                }
                x += xOffset;
                pos = new Vector3(x, 0, z);
          
                cubeList.Add(c);
               
                yield return null;
            }
            z += zOffset;
            x = -10;
            pos = new Vector3(x, 0, z);
        }
    }
}
