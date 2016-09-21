using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {
	private Image bgImg;
	private Image joystickImg;
	private Vector3 inputVector;

	public virtual void OnPointerDown(PointerEventData ped){
		OnDrag (ped);
	}

	public virtual void OnPointerUp(PointerEventData ped){
		inputVector = Vector3.zero;
		joystickImg.rectTransform.anchoredPosition = Vector3.zero;
	}

	public virtual void OnDrag(PointerEventData ped){
		Vector2 pos;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle (bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos)) {
			pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
			pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

			inputVector = new Vector3 (pos.x * 2 + 1, 0, pos.y * 2 - 1);
			inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

			joystickImg.rectTransform.anchoredPosition = new Vector3 (inputVector.x * (bgImg.rectTransform.sizeDelta.x / 2),
				inputVector.z * (bgImg.rectTransform.sizeDelta.y / 2));
		}
	}

	public float Horizontal(){
		if (inputVector.x != 0) {
			if (inputVector.x > 0 && Mathf.Abs (inputVector.x) > Mathf.Abs (inputVector.z)) {
				return 1;
			} else if (inputVector.x < 0 && Mathf.Abs (inputVector.x) > Mathf.Abs (inputVector.z)) {				
				return -1;
			} else
				return inputVector.x;
		}
		else
			return Input.GetAxis ("Horizontal");
	}

	public float Vertical(){
		if (inputVector.z != 0)
		if (inputVector.z > 0 && Mathf.Abs (inputVector.z) > Mathf.Abs (inputVector.x)) {
			return 1;
		} else if (inputVector.z < 0 && Mathf.Abs (inputVector.z) > Mathf.Abs (inputVector.x)) {				
			return -1;
		} else
			return inputVector.z;
		else
			return Input.GetAxis ("Vertical");
	}

	// Use this for initialization
	void Start () {
		bgImg = GetComponent<Image> ();
		joystickImg = transform.GetChild (0).GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
