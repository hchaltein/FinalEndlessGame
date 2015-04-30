using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public GameObject blockDragged;
    Vector3 startPos;


	public void OnBeginDrag(PointerEventData eventData){

        blockDragged = gameObject;
        startPos = transform.position;

	}

	public void OnDrag(PointerEventData eventData){

	}

	public void OnEndDrag(PointerEventData eventData){

	}

}
