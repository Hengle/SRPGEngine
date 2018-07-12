using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEditorControls : MonoBehaviour {

    private Text descriptionText;

    // Use this for initialization
    void Start () {

        GameObject desc = GameObject.Find("Description");

        descriptionText = desc.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow)){
            transform.Translate(-Time.deltaTime * 4, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow)){
            transform.Translate(Time.deltaTime * 4, 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow)){
            transform.Translate(0, Time.deltaTime * 4, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow)){
            transform.Translate(0, -Time.deltaTime * 4, 0);
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            if (hit)
            {
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.GetComponent<TileSprites>())
                    {
                        SpriteRenderer spriteTile = hit.collider.gameObject.GetComponent<SpriteRenderer>();
                        descriptionText.text = "You've clicked on: " + spriteTile.transform.name + ", " + spriteTile.sprite.name;
                        spriteTile.sprite = Camera.main.GetComponentInChildren<SpriteRenderer>().sprite;
                    }
                    else if(hit.collider.gameObject.name.Contains("SpriteSelection"))
                    {
                        SpriteRenderer spriteTile = hit.collider.gameObject.GetComponent<SpriteRenderer>();
                        descriptionText.text = "You've clicked on: " + spriteTile.transform.name + ", " + spriteTile.sprite.name;
                        Camera.main.GetComponentInChildren<SpriteRenderer>().sprite = spriteTile.sprite;
                    }
                }
            }
        }
    }
}
