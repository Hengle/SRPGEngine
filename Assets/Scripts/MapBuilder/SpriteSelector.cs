using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class SpriteSelector : MonoBehaviour {

    public GameObject defaultTile;

    public Texture2D currentAtlas;

    private Sprite[] storedSprites;
    private Component[] spriteRenderers;
    private Component[] mapColliders;

    public GameObject map;

    public bool spriteSelectEnabled = false;

    public GameObject uiPanel;
    public GameObject mapButton;
    public GameObject xAxisInput;
    public GameObject yAxisInput;

    private GameObject mapControls;

	// Use this for initialization
	void Start () {
        mapControls = Camera.main.gameObject;

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
		if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (!spriteSelectEnabled)
            {
                uiPanel.SetActive(true);
                mapButton.SetActive(false);
                xAxisInput.SetActive(false);
                yAxisInput.SetActive(false);
                

                for (int i = 0; i < this.transform.childCount; i++)
                {
                    this.transform.GetChild(i).gameObject.SetActive(true);
                }

                if (spriteRenderers == null)
                {
                    spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
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

                mapControls.GetComponent<MapEditorControls>().paintingActive = true;
                mapControls.GetComponent<MapEditorControls>().currentTile.GetComponent<SpriteRenderer>().enabled = true;

                spriteSelectEnabled = true;
            }
            else if (spriteSelectEnabled)
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

                for (int i = 0; i < this.transform.childCount; i++)
                {
                    this.transform.GetChild(i).gameObject.SetActive(false);
                }

                spriteSelectEnabled = false;
            }
        }
	}
}
