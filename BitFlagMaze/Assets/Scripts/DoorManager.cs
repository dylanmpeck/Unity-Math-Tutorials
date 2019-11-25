using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    int doorType = 0;

    private void Start()
    {
        if (this.tag == "MAGIC_DOOR")
            doorType = AttributeManager.MAGIC;
        else if (this.tag == "CHARISMA_DOOR")
            doorType = AttributeManager.CHARISMA;
        else if (this.tag == "INVISIBLE_DOOR")
            doorType = AttributeManager.INVISIBLE;
        else if (this.tag == "FLY_DOOR")
            doorType = AttributeManager.FLY;
        else if (this.tag == "INTELLIGENCE_DOOR")
            doorType = AttributeManager.INTELLIGENCE;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.GetComponent<AttributeManager>().attributes & doorType) != 0)
        {
            this.GetComponent<BoxCollider>().isTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        this.GetComponent<BoxCollider>().isTrigger = false;
    }
}
