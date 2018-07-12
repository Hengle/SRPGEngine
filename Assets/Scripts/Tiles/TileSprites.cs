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

    private SpriteRenderer spriteTile;

    private BoxCollider2D colliderBox;

    private Text descriptionText;


	// Use this for initialization
	void Start () {
        spriteTile = GetComponent<SpriteRenderer>();
        colliderBox = GetComponent<BoxCollider2D>();

        GameObject desc = GameObject.Find("Description");

        descriptionText = desc.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {  

	}
}
