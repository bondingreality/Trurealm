using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class GameManager
    : MonoBehaviour
{
    [SerializeField]
    private Player player;

    private NPC currentTarget;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ClickTarget();
    }

    private void ClickTarget()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, Common.GetLayerMaskID("Clickable"));

            if(hit.collider != null && hit.collider.tag == "Enemy")
            {
                if(currentTarget != null)
                {
                    currentTarget.Deselect();
                }

                currentTarget = hit.collider.GetComponent<NPC>();

                player.MyTarget = currentTarget.Select();

                UIManager.MyInstance.ShowTargetFrame(currentTarget);
            }
            else
            {
                if(currentTarget != null) {
                    currentTarget.Deselect();
                }

                currentTarget = null;
                player.MyTarget = null;
                UIManager.MyInstance.HideTargetFrame();
            }
        }
        else if(Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, Common.GetLayerMaskID("Clickable"));

            if (hit.collider != null && (hit.collider.tag == "Enemy" || hit.collider.tag == "Interactable"))
            {
                player.Interact();
            }
        }
    }
}
