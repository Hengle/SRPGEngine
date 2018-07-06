using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateMap : MonoBehaviour {

    public GameObject tile;

    public InputField xAxis;
    public InputField yAxis;

    public Text notification;

    public Transform map;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void CreateMap()
    {
        if(map.childCount > 0)
        {
            foreach (Transform child in map)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        int x;
        int y;

        if( int.TryParse(xAxis.text, out x) && int.TryParse(yAxis.text, out y))
        {
            if (x <= 50 && y <= 50)
            {
                Debug.Log(x + " " + y);
                for (int i = 0; i < y; i++)
                {
                    for (int j = 0; j < x; j++)
                    {
                        GameObject newTile = Instantiate(tile, new Vector3(j-3, 3-i, 0), Quaternion.identity, map);
                        newTile.name = "TileRow" + i.ToString();
                    }
                }
            }
            else
            {
                notification.text = "Please enter a number below 50.";
            }
        }
        else
        {
            notification.text = "Please enter a valid number.";
        }
    }
}
