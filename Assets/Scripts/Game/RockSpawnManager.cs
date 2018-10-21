using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawnManager : MonoBehaviour {

    public GameObject[] rocks;
    public GameObject tree;
    public Transform treeSpawn;
    public Transform[] spawnPoints;
	// Use this for initialization
	public void Spawn () {

		foreach (Transform sp in spawnPoints)
        {
            int numOfRocks = (int)Random.Range(5, 10);
            for (int i = 0; i < numOfRocks; i++)
            {
                int xx = (int)Random.Range(sp.position.x - 2, sp.position.x + 2);
                int zz = (int)Random.Range(sp.position.z - 50, sp.position.z + 50);
                if (sp.name.Equals("RockSpawn1"))
                {
                     xx = (int)Random.Range(sp.position.x - 2, sp.position.x + 40);
                }
                else
                {
                     xx = (int)Random.Range(sp.position.x - 40, sp.position.x + 2);
                }
                
                int rock = Random.Range(0, 3);
                
                GameObject r = Instantiate(rocks[rock], sp.position, Quaternion.identity);
                r.transform.position = new Vector3(xx, 10.9f, zz);


            }
        }
         
        int numOfTrees = (int)Random.Range(10, 15);
        for (int j = 0; j < numOfTrees; j++)
        {
            int x1 = (int)Random.Range(treeSpawn.position.x - 20, treeSpawn.position.x - 50);
            int zz = (int)Random.Range(treeSpawn.position.z - 50, treeSpawn.position.z + 50);
            GameObject t = Instantiate(tree, treeSpawn.position, Quaternion.Euler(new Vector3(-90f, 0f, 0f)));
            Debug.Log(t.name + ";" + x1);
            t.transform.position = new Vector3(x1, 9.9f, zz);
            //t.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
        for (int j = 0; j < numOfTrees; j++)
        {
            int x2 = (int)Random.Range(treeSpawn.position.x + 20, treeSpawn.position.x + 50);
            int zz = (int)Random.Range(treeSpawn.position.z - 50, treeSpawn.position.z + 50);
            GameObject t = Instantiate(tree, treeSpawn.position, Quaternion.Euler(new Vector3(-90f, 0f, 0f)));
            t.transform.position = new Vector3(x2, 9.9f, zz);
            //t.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }

    }

	
	
}
