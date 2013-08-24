using UnityEngine;
using System.Collections;

public class ButtonBehaviour : MonoBehaviour
{
	private const int LEFT_MOUSE_BUTTON = 0;
	
	public bool yesButton;
	public Vector3 _scaled = new Vector3(0.85f, 1.0f, 0.85f);
	private bool _mouseDownOnMe = false;
	private Camera _cam;
	
	private void Start()
	{
		_cam = Camera.main;
	}
	
	void Update()
	{
		
		if(_mouseDownOnMe && Input.GetMouseButtonUp(LEFT_MOUSE_BUTTON))
		{
			GameManagerBehaviour.Instance.Clicked(yesButton);
			transform.localScale = Vector3.one;
			_mouseDownOnMe = false;
			gameObject.audio.Play();
		}
		else if(!_mouseDownOnMe && Input.GetMouseButtonDown(LEFT_MOUSE_BUTTON))
		{
			Ray mouseRay = _cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(mouseRay, out hit, 10.0f) && hit.collider.gameObject == gameObject)
			{
				_mouseDownOnMe = true;
				transform.localScale = _scaled;
			}
		}
	}
}
