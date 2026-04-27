using UnityEngine;

public class NodeBehaviour : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color orig_color;

    public bool hasTower = false;



    public GameObject towerData;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        orig_color = spriteRenderer.material.color;
    }
    void OnMouseOver()
    {
        if (!hasTower && GameMaster.instance.CanBuild())
        {
            spriteRenderer.color = Color.red;
            if (!hasTower && Input.GetMouseButtonDown(0))
            {
                towerData = Instantiate(GameMaster.instance.selectedTowerPrefab, transform.position, Quaternion.identity);
                hasTower = true;
                Debug.Log("turret placed");
                GameMaster.instance.SpendMoney(GameMaster.instance.selectedTowerCost);
            }

        }
        else if (hasTower)
        {
            spriteRenderer.color = Color.green;
            if (Input.GetMouseButtonDown(0))
            {
                if (GameMaster.instance.selectedNode == this.gameObject)
                {
                    GameMaster.instance.ReturnToShop();
                    spriteRenderer.color = orig_color;

                }
                else
                {
                    GameMaster.instance.SelectNode(this.gameObject);
                    spriteRenderer.color = Color.yellow;
                }
            }
        } 
    }

    public void RemoveTower()
    {
        if (towerData != null)
        {
            Destroy(towerData);
            hasTower = false;
            towerData = null;
        }
    }
    void OnMouseExit()
    {
        spriteRenderer.color = orig_color;
    }


}
