using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
public class SaveLoad : MonoBehaviour
{
    BinaryFormatter formatter = new BinaryFormatter();
    string path;
    private void Start() {
        path = Application.persistentDataPath + "/save.gamesave";
    }
    public void Save() {
        ToSave savingData = new ToSave();
        savingData.Saving();
        using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate)) {
            formatter.Serialize(fs, savingData);
        }
    }
    ToSave loadedData;
    public void Load() {
        if (!File.Exists(path)) {
            return;
        }
        using (FileStream fs = new FileStream(path, FileMode.Open)) {
            loadedData = (ToSave)formatter.Deserialize(fs);
        }
        
        Debug.Log(SceneManager.GetActiveScene().name);
        foreach (GameObject elem in GameObject.FindGameObjectsWithTag("Bee")) {
            Destroy(elem);
        }
        foreach (GameObject elem in GameObject.FindGameObjectsWithTag("Flower")) {
            Destroy(elem);
        }
        GameObject.FindGameObjectWithTag("Hive").GetComponent<Hive>().flowers.Clear();
        for (int i = 0; i < loadedData.flowersData.Count; i++) {
            GameObject.FindGameObjectWithTag("Plane").GetComponent<PlaneScript>().LoadFlowers(loadedData.flowersData[i]);
        }

        for (int i = 0; i < loadedData.beesData.Count; i++) {
            GameObject.FindGameObjectWithTag("Hive").GetComponent<Hive>().Data(loadedData.beesData[i]);
            Debug.Log(GameObject.FindGameObjectWithTag("Hive").name); 
        }
        GameObject.FindGameObjectWithTag("Hive").GetComponent<Hive>().HiveData(loadedData.hiveData);  
    }
}
[Serializable]
public class ToSave {
    public List<BeeOnField> beesData = new List<BeeOnField>();
    public List<FlowerData> flowersData = new List<FlowerData>();
    public HiveData hiveData;

    [Serializable]
    public struct Vec {
        public float x, y, z;
        public Vec(float x, float y, float z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
    [Serializable]
    public struct HiveData {
        public int pollenInHive;
        

        public HiveData(int pollenInHive) {
            this.pollenInHive = pollenInHive;           
        }
    }
    [Serializable]
    public struct BeeOnField {
        public Vec position, flowerCords;
        public int wayData, pollenData;
        public bool takePollenData, toHiveData, toFlowerData;
        public float rotation;

        public BeeOnField(Vec position, float rotation, Vec flowerCords, int way, int pollen, bool takePollen, bool toHive, bool toFlower) {
            this.position = position;
            this.rotation = rotation;
            this.flowerCords = flowerCords;
            this.wayData = way;
            this.pollenData = pollen;
            this.takePollenData = takePollen;
            this.toHiveData = toHive;
            this.toFlowerData = toFlower;
        }
    }
    [Serializable]
    public struct FlowerData {
        public Vec position;
        public int countOfPollenData;

        public FlowerData(Vec position, int countOfPollenData) {
            this.position = position;
            this.countOfPollenData = countOfPollenData;
        }
    }

    public void Saving() {
        Hive hive = GameObject.FindGameObjectWithTag("Hive").GetComponent<Hive>();
        hiveData = new HiveData(hive.countOfPollen);
        
        if (GameObject.FindGameObjectsWithTag("Bee").Length != 0) {
            foreach (GameObject elem in GameObject.FindGameObjectsWithTag("Bee")) {
                
                
                BeeMove bee = elem.GetComponent<BeeMove>();
                BeeOnField beeData = new BeeOnField(new Vec(bee.transform.position.x, bee.transform.position.y, bee.transform.position.z),
                    bee.angle, new Vec(bee.currentFlower.transform.position.x, bee.currentFlower.transform.position.y, bee.currentFlower.transform.position.z),
                    bee.way, bee.pollen, bee.takePollen, bee.toHive, bee.toFlower);
                beesData.Add(beeData);
            }
        }

        if (GameObject.FindGameObjectsWithTag("Flower").Length != 0) {
            foreach (GameObject elem in GameObject.FindGameObjectsWithTag("Flower")) {
                Flower flower = elem.GetComponent<Flower>();
                FlowerData flowerData = new FlowerData(new Vec(flower.transform.position.x, flower.transform.position.y, flower.transform.position.z), flower.countOfPollen);
                flowersData.Add(flowerData);
            }
        }
    }
}
