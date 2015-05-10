using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public static GameObject blockDragged;
    Vector3 startPos;
    GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

	public void OnBeginDrag(PointerEventData eventData){


        blockDragged = this.gameObject;
        startPos = transform.position;
	}

	public void OnDrag(PointerEventData eventData){

        transform.position = Input.mousePosition;

	}

	public void OnEndDrag(PointerEventData eventData){

        int blocktoSpawn = Random.Range(0, 9);
        UIManager.Instance.Abilities[blocktoSpawn].UseAbility();
        
        gameManager.SpawnBlock(blocktoSpawn, 
                                gameManager.lastBlock.transform.position.x + 40.0f);
        
        transform.position = startPos;
        
        blockDragged = null;
	}

}
