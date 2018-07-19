using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class SpriteSelector : MonoBehaviour {

    public GameObject defaultTile;

    public Texture2D currentAtlas;

    private Sprite[] storedSprites;

	// Use this for initialization
	void Start () {
        storedSprites = Resources.LoadAll<Sprite>("Sprites/Map/"+currentAtlas.name);

        int x = 32;
        int y = 32;

        int currentTileNumber = 0;
        
        for (int i = 0; i < y; i++)
        {
            
            for (int j = 0; j < x; j++)
            {
                GameObject newTile = Instantiate(defaultTile, new Vector3(j*0.3f - 3, 4.5f - i*0.3f, 0), Quaternion.identity, this.transform);
                newTile.name = "SpriteSelection" + i.ToString();
                newTile.AddComponent<SpriteRenderer>();
                newTile.GetComponent<SpriteRenderer>().enabled = false;
                newTile.SetActive(false);
                //Debug.Log(currentTileNumber);
                if (currentTileNumber == 1023)
                {
                    newTile.GetComponent<SpriteRenderer>().sprite = storedSprites[1022];
                }
                else
                {
                    newTile.GetComponent<SpriteRenderer>().sprite = storedSprites[currentTileNumber];
                }
                currentTileNumber++;
            }
        }   
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
