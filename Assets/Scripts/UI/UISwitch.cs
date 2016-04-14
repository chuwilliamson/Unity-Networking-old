using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UISwitch : MonoBehaviour
{

 
	public void SetState ()
	{		
		gameObject.SetActive (!gameObject.activeSelf);
	}

	RectTransform r;

	private void Awake ()
	{
		r = GetComponent<RectTransform> ();
	}

	private void Update()
	{
		mouse = Input.mousePosition;
	}

	public void OnDragBegin()
	{
		GetComponent<ScrollRect> ().enabled = false;
	}
	public void OnDrag ()
	{
		deltaMouse = Input.mousePosition - mouse;
		r.position = new Vector3 (r.anchoredPosition.x + deltaMouse.x, r.anchoredPosition.y + deltaMouse.y, zOffset);

	}

	public void OnDragEnd()
	{
		GetComponent<ScrollRect> ().enabled = true;
	}

	[SerializeField]
	Vector3 deltaMouse;
	[SerializeField]
	float MouseToLeft;
	[SerializeField]
	float MouseToBottom;

	[SerializeField]
	Vector3 mouse;

	[SerializeField]
	Vector3 pos;



	[SerializeField]
	float newX, newY, zOffset;

		
}
