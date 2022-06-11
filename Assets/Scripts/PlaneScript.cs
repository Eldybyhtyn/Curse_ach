using System;
using System.Collections;
using UnityEngine;

public class PlaneScript : MonoBehaviour {
    // Start is called before the first frame update
    private bool access = true;
    private GameObject gameobject;
    void Start() {
        gameobject = GameObject.Find("Hive");
        for (int i = 0; i < Convert.ToInt16(TextFlowerScript.numFlower); i++) {
            RandomCords();
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (access) {
            StartCoroutine(Wait());   
            access = false;
        }
    }

    private void RandomCords() {
        GameObject flower = Resources.Load("Flower", typeof(GameObject)) as GameObject;
        flower.transform.position = new Vector3(UnityEngine.Random.Range(-15, 16), 0.4f, UnityEngine.Random.Range(-3, 15));
        flower.GetComponent<Flower>().countOfPollen = 5;
        Instantiate(flower);
        flower = GameObject.Find("Flower(Clone)");
        gameobject.GetComponent<Hive>().flowers.Add(flower);
        foreach (GameObject elem in GameObject.FindGameObjectsWithTag("Flower")) {
            elem.name = "Flower";
        }
        access = true;    
    }

    private IEnumerator Wait() {
        yield return new WaitForSeconds(20);
        RandomCords();
    }

    public void LoadFlowers(ToSave.FlowerData data) {
        GameObject flowerObject = Resources.Load("Flower", typeof(GameObject)) as GameObject;
        flowerObject.transform.position = new Vector3(data.position.x, 0.4f, data.position.z);
        flowerObject.GetComponent<Flower>().countOfPollen = data.countOfPollenData;
        Instantiate(flowerObject);
        flowerObject = GameObject.Find("Flower(Clone)");
        flowerObject.name = "Flower";
        GameObject.FindGameObjectWithTag("Hive").GetComponent<Hive>().flowers.Add(flowerObject);

    }
}
