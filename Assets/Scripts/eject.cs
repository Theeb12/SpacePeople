using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eject : MonoBehaviour
{
    [SerializeField] GameObject airLockInterior;
    [SerializeField] GameObject door;
    public int state = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 1 && door.GetComponent<airlockDoor>().closed == true)
        {
            startEject();
            Invoke("endEject", 1.0f);
            state = 0;
        }
        else if(state == 1)
        {
            state = 0;
        }
    }
    void startEject()
    {
        airLockInterior.GetComponent<airLockInterior>().state = 1;
    }
    void endEject()
    {
        airLockInterior.GetComponent<airLockInterior>().state = 0;
    }
}
