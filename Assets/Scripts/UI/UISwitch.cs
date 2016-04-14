using UnityEngine;
using System.Collections;

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

	public void OnDrag ()
	{
		deltaMouse = Input.mousePosition - mouse;
		r.position = new Vector3 (r.anchoredPosition.x + deltaMouse.x, r.anchoredPosition.y + deltaMouse.y, zOffset);

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
