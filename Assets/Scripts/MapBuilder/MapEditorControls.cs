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

    public bool spriteSelectEnabled = false;

    public GameObject spriteSelector;

    public GameObject uiPanel;
    public GameObject mapButton;
    public GameObject xAxisInput;
    public GameObject yAxisInput;

    private Component[] spriteRenderers;
    private Component[] mapColliders;

    public GameObject map;

    // Use this for initialization
    void Start () {
        paintingActive = true;

        GameObject desc = GameObject.Find("Description");

        descriptionText = desc.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {

        //Bring up sprite selector by pressing tab. This renders or de-renders the sprite sheet tiles
        //generated in SpriteSelector. Painting is disabled while sprite selection is active.

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!spriteSelectEnabled)
            {
                uiPanel.SetActive(true);
                mapButton.SetActive(false);
                xAxisInput.SetActive(false);
                yAxisInput.SetActive(false);

                for (int i = 0; i < spriteSelector.transform.childCount; i++)
                {
                    spriteSelector.transform.GetChild(i).gameObject.SetActive(true);
                }

                if (spriteRenderers == null)
                {
                    spriteRenderers = spriteSelector.GetComponentsInChildren<SpriteRenderer>();
                }

                foreach (SpriteRenderer renderers in spriteRenderers)
                {
                    renderers.enabled = true;
                }

                mapColliders = map.GetComponentsInChildren<BoxCollider2D>();

                foreach (BoxCollider2D colliders in mapColliders)
                {
                    colliders.enabled = false;
                }

                paintingActive = false;
                currentTile.GetComponent<SpriteRenderer>().enabled = true;

                spriteSelectEnabled = true;
            }
            else if(spriteSelectEnabled)
            {
                uiPanel.SetActive(false);
                mapButton.SetActive(true);
                xAxisInput.SetActive(true);
                yAxisInput.SetActive(true);

                if (spriteRenderers == null)
                {
                    spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
                }

                foreach (SpriteRenderer renderers in spriteRenderers)
                {
                    renderers.enabled = false;
                }

                mapColliders = map.GetComponentsInChildren<BoxCollider2D>();

                foreach (BoxCollider2D colliders in mapColliders)
                {
                    colliders.enabled = true;
                }

                for (int i = 0; i < spriteSelector.transform.childCount; i++)
                {
                    spriteSelector.transform.GetChild(i).gameObject.SetActive(false);
                }

                spriteSelectEnabled = false;
            }
        }

        //If painting mode is active, allow changing the current selected tile's type by using the mouse scroll wheel

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

        //Enable or disable paint mode with left control

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (paintingActive)
            {
                if (spriteSelectEnabled)
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

        //Use arrow keys to move camera around

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

        //If left click is down, check for any colliders, performing different tasks if the generated map tiles or the sprite atlas tiles are clicked

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
                        //If painting mode is active, set the selected tile as the current tile and apply the selected sprite as displayed on the CurrentTile game object
                        //If painting mode isn't active, only set the selected tile as the current tile

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

                        //If sprite selection mode is active, change the CurrentTile gameobject's sprite to the selected sprite on the sprite atlas

                        SpriteRenderer spriteTile = hit.collider.gameObject.GetComponent<SpriteRenderer>();
                        descriptionText.text = "You've clicked on: " + spriteTile.transform.name + ", " + spriteTile.sprite.name;
                        Camera.main.GetComponentInChildren<SpriteRenderer>().sprite = spriteTile.sprite;
                    }
                }
            }
        }
    }
}
