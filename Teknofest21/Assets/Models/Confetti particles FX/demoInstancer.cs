using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demoInstancer : MonoBehaviour
{

    public GameObject[] Prefabs;

    public int CurrentIndex = 0;
    private GameObject GO;

    public bool DestroyPerRestart = true;
    // Start is called before the first frame update
    void Start()
    {

        CurrentIndex = Random.Range(0, Prefabs.Length);
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            


            if(GO != null)
            {
                if (DestroyPerRestart == true)
                {
                       Destroy(GO);
                }


            }


            GO = Instantiate(Prefabs[CurrentIndex], transform.position, transform.rotation) as GameObject;

            for (int i = 0; i < Prefabs.Length; i++)
            {
                if (i == CurrentIndex)
                {

                  



                    if (CurrentIndex < Prefabs.Length - 1)
                    {
                        CurrentIndex++;

                        return;

                    }

                    else
                    {

                        CurrentIndex = 0;
                        return;
                    }


                }
               

              
                
         

            }

        }
        
    }
}
