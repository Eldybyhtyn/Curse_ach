using System.Collections;

using UnityEngine;


public class Flower : MonoBehaviour
{
    public int countOfPollen = 5;
    private bool access = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (access) {
            if (countOfPollen < 5) {
                access = false;
                StartCoroutine(AddPollen());
            }
        }
         
    }   

    IEnumerator AddPollen() {
        yield return new WaitForSeconds(30);
        if (countOfPollen == 0) GameObject.Find("Hive").GetComponent<Hive>().flowers.Add(this.gameObject);
        countOfPollen++;
        access = true;
    }
}
