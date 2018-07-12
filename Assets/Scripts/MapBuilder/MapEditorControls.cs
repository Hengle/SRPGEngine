using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEditorControls : MonoBehaviour {

    private Text descriptionText;

    public GameObject currentTile;

    public GameObject activeTile;

    public Text tileProperty;

    public float scrollInput;

    public bool paintingActive;

    // Use this for initialization
    void Start () {
        paintingActive = true;

        GameObject desc = GameObject.Find("Description");

        descriptionText = desc.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {

        if (!paintingActive)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (activeTile != null)
                {
                    if (activeTile.GetComponent<TileSprites>().typeOfTile == TileSprites.tileType.Mountain)
                    {
                        activeTile.GetComponent<TileSprites>().typeOfTile = TileSprites.tileType.Plains;
                        tileProperty.text = activeTile.GetComponent<TileSprites>().typeOfTile.ToString();
                    }
                    else
                    {
                        activeTile.GetComponent<TileSprites>().typeOfTile++;
                        tileProperty.text = activeTile.GetComponent<TileSprites>().typeOfTile.ToString();
                    }
                }
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (activeTile != null)
                {
                    if (activeTile.GetComponent<TileSprites>().typeOfTile == TileSprites.tileType.Plains)
                    {
                        activeTile.GetComponent<TileSprites>().typeOfTile = TileSprites.tileType.Mountain;
                        tileProperty.text = activeTile.GetComponent<TileSprites>().typeOfTile.ToString();
                    }
                    else
                    {
                        activeTile.GetComponent<TileSprites>().typeOfTile--;
                        tileProperty.text = activeTile.GetComponent<TileSprites>().typeOfTile.ToString();
                    }
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (paintingActive)
            {
                if (GameObject.Find("SpriteSelector").GetComponent<SpriteSelector>().spriteSelectEnabled)
                {
                    Debug.Log("Can't paint while sprite selector is open.");
                }
                else
                {
                    currentTile.GetComponent<SpriteRenderer>().enabled = false;
                    paintingActive = false;
                }
            }
            else if(!paintingActive)
            {
                currentTile.GetComponent<SpriteRenderer>().enabled = true;
                paintingActive = true;
            }
        }

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
                        if (paintingActive)
                        {
                            SpriteRenderer spriteTile = hit.collider.gameObject.GetComponent<SpriteRenderer>();
                            activeTile = hit.collider.gameObject;
                            descriptionText.text = "You've clicked on: " + spriteTile.transform.name + ", " + spriteTile.sprite.name;
                            tileProperty.text = activeTile.GetComponent<TileSprites>().typeOfTile.ToString();
                            spriteTile.sprite = Camera.main.GetComponentInChildren<SpriteRenderer>().sprite;
                        }
                        else if(!paintingActive)
                        {
                            SpriteRenderer spriteTile = hit.collider.gameObject.GetComponent<SpriteRenderer>();
                            activeTile = hit.collider.gameObject;
                            descriptionText.text = "You've clicked on: " + spriteTile.transform.name + ", " + spriteTile.sprite.name;
                            tileProperty.text = activeTile.GetComponent<TileSprites>().typeOfTile.ToString();
                        }
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
