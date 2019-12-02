using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject tank;
    public GameObject fuel;
    public Text tankPos;
    public Text fuelPos;
    public Text energyAmt;

    public void AddEnergy(string amt)
    {
        int n;
        if (int.TryParse(amt, out n))
        {
            energyAmt.text = amt;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        tankPos.text = tank.transform.position.ToString();
        fuelPos.text = fuel.GetComponent<ObjectManager>().objPosition.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
