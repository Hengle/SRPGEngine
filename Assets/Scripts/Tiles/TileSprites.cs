using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileSprites : MonoBehaviour {

    public enum tileType
    {
        Plains,
        Forest,
        Mountain
    }

    public tileType typeOfTile;

    private BoxCollider2D colliderBox;

	// Use this for initialization
	void Start () {
        colliderBox = GetComponent<BoxCollider2D>();

        typeOfTile = tileType.Plains;
    }
	
	// Update is called once per frame
	void Update () {  

	}
}
