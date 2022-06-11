using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hive : MonoBehaviour
{
    public GameObject gameOver;
    public int countOfPollen;
    public List<GameObject> bees = new List<GameObject>();
    GameObject beeObject;
    public List<GameObject> flowers;
    private bool access = true;
    
    void Start() {
        flowers = new List<GameObject>();
        countOfPollen = Convert.ToInt16(TextPollenScript.numPollen) * 5;
        foreach (GameObject elem in GameObject.FindGameObjectsWithTag("Flower")) flowers.Add(elem);
    }
    void FixedUpdate() { 
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.up);
        if (Physics.Raycast(ray, out hit)) {
            beeObject = hit.transform.gameObject; 
            if (beeObject.TryGetComponent(out BeeMove script)) {
                if (script.toHive) {
                    script.toHive = !script.toHive;
                    countOfPollen += script.pollen;
                    beeObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    bees.Add(beeObject);
                    beeObject.SetActive(false);
                } 
            }
        }
        
        if (bees.Count != 0 && access && flowers.Count != 0) {
            access = false;
            StartCoroutine(Wait(1));
        }
        if (countOfPollen >= 5) {
            StartCoroutine(SpawnNewBee(Convert.ToInt16(TextLifeScript.numLife)));
            countOfPollen -= 5;
        }
        if (GameObject.FindGameObjectsWithTag("Bee").Length == 0 && countOfPollen < 5 && bees.Count == 0) {
            Time.timeScale = 0f;
            gameOver.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }

    }

    private IEnumerator SpawnNewBee(int time) {
        GameObject gameObject = Resources.Load("Bee", typeof(GameObject)) as GameObject;    
        Instantiate(gameObject);
        gameObject = GameObject.Find("Bee(Clone)");
        bees.Add(gameObject);
        gameObject.tag = "Bee";
        Destroy(gameObject, time);
        gameObject.SetActive(false);
        gameObject.name = "Bee";
        yield return new WaitForSeconds(time);
    }

    private IEnumerator Wait(int time) {
        yield return new WaitForSeconds(time);
        GameObject flower = findFlower(ref flowers);
        if (flower.GetComponent<Flower>().countOfPollen > 0) {
            if (bees[0] == null) bees.Remove(bees[0]);
            else {
                bees[0].GetComponent<BeeMove>().currentFlower = flower;
                bees[0].GetComponent<BeeMove>().hive = GameObject.Find("Hive");
                bees[0].transform.position = new Vector3(-5.664f, 0.819f, -6.616f);
                bees[0].GetComponent<BeeMove>().toHive = false;
                bees[0].GetComponent<BeeMove>().takePollen = true;
                bees[0].GetComponent<BeeMove>().way = 0;
                bees[0].GetComponent<BeeMove>().toFlower = true;
                bees[0].SetActive(true);
                bees.Remove(bees[0]);
            }
        }
        access = true;
    }

    private GameObject findFlower(ref List<GameObject> flowers) {
        GameObject flower = flowers[0];
        flowers.RemoveAt(0);
        flowers.Add(flower);
        return flower;
    }

    public void Data(ToSave.BeeOnField beeOnField) {
        GameObject gameObject = Resources.Load("BeeObject", typeof(GameObject)) as GameObject;
        Instantiate(gameObject);
        gameObject = GameObject.Find("BeeObject(Clone)");
        foreach (GameObject elem in GameObject.FindGameObjectsWithTag("Flower")) {
            Flower flowerPos = elem.GetComponent<Flower>();
            if (flowerPos.transform.position.x == beeOnField.flowerCords.x && flowerPos.transform.position.z == beeOnField.flowerCords.z) {
                gameObject.GetComponent<BeeMove>().currentFlower = elem;
            }
        }
        BeeMove beeScript = gameObject.GetComponent<BeeMove>();
        gameObject.tag = "Bee";
        gameObject.name = "Bee";
        Destroy(gameObject, 60);
        gameObject.transform.position = new Vector3(beeOnField.position.x, beeOnField.position.y, beeOnField.position.z);
        
        beeScript.way = beeOnField.wayData;
        beeScript.angle = beeOnField.rotation;
        beeScript.pollen = beeOnField.pollenData;
        beeScript.takePollen = beeOnField.takePollenData;
        beeScript.toHive = beeOnField.toHiveData;
        if (beeScript.toHive) {
            gameObject.transform.Rotate(0, beeOnField.rotation + 180, 0);
        } else gameObject.transform.Rotate(0, beeOnField.rotation, 0);
        beeScript.toFlower = beeOnField.toFlowerData;
        beeScript.hive = GameObject.FindGameObjectWithTag("Hive");
    }

    public void HiveData(ToSave.HiveData hiveData) {
        countOfPollen = hiveData.pollenInHive;
    }
}


