using System.Collections;
using UnityEngine;
using System;

public class BeeMove : MonoBehaviour {
    public GameObject currentFlower;
    public int way = 0;
    public bool takePollen = true;
    public bool toFlower = true;
    public GameObject hive;
    public float lifeLength = 100;
    Vector3 move;
    // Start is called before the first frame update
    float x, y, z, xBee, yBee, zBee, dist;
    public float angle;
    [SerializeField] float speed;
    public bool toHive = false;
    public int pollen = 0;
    private void FixedUpdate() {
        if (toFlower) {
            var direction = currentFlower.transform.position - transform.position;
            angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            GetComponentInChildren<Transform>().Rotate(0, angle, 0);
            toFlower = !toFlower;
        }
        moveToFlower(); 
    }

    private IEnumerator Wait() {
        way = 2;      
        if (takePollen) {
            currentFlower.GetComponent<Flower>().countOfPollen -= 1;
            pollen += 1;
            takePollen = false;
            
            toHive = true;
        }
        GetComponentInChildren<Transform>().Rotate(0, 180, 0);
        x = hive.transform.position.x;
        y = hive.transform.position.y - 0.3f;
        z = hive.transform.position.z;
        yield return new WaitForSeconds(2);   
    }

    private void moveToFlower() {
        xBee = transform.position.x;
        yBee = transform.position.y;
        zBee = transform.position.z;
        move = new Vector3((x - xBee), (y - yBee), (z - zBee));
        dist = (float)Math.Sqrt(Math.Pow(x - xBee, 2) + Math.Pow(y - yBee, 2) + Math.Pow(z - zBee, 2));

        if (way == 0 && dist > 0.2) {
            x = currentFlower.transform.position.x;
            y = currentFlower.transform.position.y;
            z = currentFlower.transform.position.z;
            transform.position = Vector3.Lerp(transform.position, currentFlower.transform.position, speed);
        } else if (dist <= 0.2 && way == 0) {
            way = 1;
            takePollen = true;
        }
        if (way == 1) {
            if (currentFlower.GetComponent<Flower>().countOfPollen - 1 >= 0) StartCoroutine(Wait());
            else {
                way = 2;
                GetComponentInChildren<Transform>().Rotate(0, 180, 0);
                x = hive.transform.position.x;
                y = hive.transform.position.y - 0.3f;
                z = hive.transform.position.z;
                takePollen = false;
                toHive = true;
            }
        }
        if (way == 2 && dist > 0.2) {
            move = new Vector3(x, y, z);
            transform.position = Vector3.Lerp(transform.position, move, speed);
        }
    }
}

