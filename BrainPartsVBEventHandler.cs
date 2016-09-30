using UnityEngine;

public class BrainPartsVBEventHandler : MonoBehaviour, IVirtualButtonEventHandler
{
	private GameObject _Brain;
	private GameObject _BrainWithParts;
	public float speed;
	public float rotationSpeed;
	private Vector3 myEul, targetEul;
	private Transform myTrans;
	private bool canRotate;
	
	void Start() 
	{
		VirtualButtonBehaviour[] vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
		for (int i = 0; i < vbs.Length; ++i) 
		{
			vbs[i].RegisterEventHandler(this);
		}
		_Brain = transform.FindChild("Brain_Model").gameObject;
		_BrainWithParts = transform.FindChild("Brain_ModelWithParts").gameObject;

		_Brain.SetActive(true);
		_BrainWithParts.SetActive(false);
		myTrans = _Brain.transform;
		myEul = myTrans.rotation.eulerAngles;
		targetEul = myEul;
	}
	
	void Update() 
	{
	}
	
	public void OnButtonPressed(VirtualButtonAbstractBehaviour vb) 
	{
		switch(vb.VirtualButtonName) 
		{
		case "WithParts":
			Debug.Log("WithParts VB is pressed ");
			_Brain.SetActive(false);
			_BrainWithParts.SetActive(true);
			break;
		case "WithoutParts":
			Debug.Log("WithoutParts VB is pressed ");
			_Brain.SetActive(true);
			_BrainWithParts.SetActive(false);
			break;
		case "rotate":
			Debug.Log ("Rotation VB Pressed");
			_Brain.SetActive(true);
			if (canRotate) {
				Rotate ();
			}
			checkRotation ();
			break;
		default:
			throw new UnityException("Button not supported: " + vb.VirtualButtonName);
			break;
		}
	}
	public void OnButtonReleased(VirtualButtonAbstractBehaviour vb) 
	{
		switch (vb.VirtualButtonName) {
		case "WithParts":
			Debug.Log ("WithParts VB is released ");
			break;
		case "WithoutParts":
			Debug.Log ("WithoutParts VB is released ");
			break;
		case "rotate":
			Debug.Log ("Rotation VB released");
			break;
		default:
			throw new UnityException ("Button not supported: " + vb.VirtualButtonName);
			break;
		}
	}
	
	
	void checkRotation ()
	{
		if (Vector3.Distance (targetEul, myEul) > 1.5f) {
			myEul = Vector3.Lerp (myEul, targetEul, speed * Time.deltaTime);
		} else {
			myEul = targetEul;
			canRotate = true;
		}
		myTrans.rotation = Quaternion.Euler (myEul);
	}
	
	private void Rotate()
	{
		Vector3 pos = _Brain.transform.localPosition * this.transform.localScale.x;
		Debug.Log("Initial Position :"+pos+"");
		VirtualButtonBehaviour vbs1 = GetComponentInChildren<VirtualButtonBehaviour>();
		if (vbs1) {
			myEul = myTrans.rotation.eulerAngles;
			targetEul = myEul;
			targetEul.y += 180;
			canRotate = false;
		} 
	}
	
}