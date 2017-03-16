using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item01 : MonoBehaviour, Items {

	private string itemName = "Item01";
	private string itemDescription = "simple Water that deng dueng";

    private int itemQuantity = 0;

	public string names
    {
        get{return itemName;}

        set{ itemName = value;}
        
    } 
	
    public string description 
    {
        get { return itemDescription; }

        set{ itemDescription = value;}
    } 

    public int quantity
    {
        get { return itemQuantity; }

        set{ itemQuantity = value;}
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "cHIBI_bone")
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionExit(Collision col)
    {
    }


	// Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
