using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtlasSprites : MonoBehaviour {

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
    void Start() {
        spriteTile = GetComponent<SpriteRenderer>();
        colliderBox = GetComponent<BoxCollider2D>();

        GameObject desc = GameObject.Find("Description");

        descriptionText = desc.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            if (hit)
            {
                if (hit.collider != null && hit.collider == colliderBox)
                {
                    descriptionText.text = "You've clicked on: " + spriteTile.transform.name + ", " + spriteTile.sprite.name;
                    Camera.main.GetComponentInChildren<SpriteRenderer>().sprite = spriteTile.sprite;
                }
            }
        }
    }
}
