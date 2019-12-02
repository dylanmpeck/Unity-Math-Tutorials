using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject tank;
    public GameObject fuel;
    public Text tankPosition;
    public Text fuelPosition;
    public Text energyAmt;

    public void AddEnergy(string amt)
    {
        int n;
        if (int.TryParse(amt, out n))
        {
            energyAmt.text = amt;
        }
    }

    public void ApplyAngle(string angle)
    {
        float a;
        if (float.TryParse(angle, out a))
        {
            tank.transform.up = HolisticMath.Rotate(new Coords(0, 1, 0),
                                                    a / 180.0f * Mathf.PI,
                                                          false).ToVector();

        }
    }


    // Use this for initialization
	void Start () {
        tankPosition.text = tank.transform.position + "";
        fuelPosition.text = fuel.GetComponent<ObjectManager>().objPosition + "";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
