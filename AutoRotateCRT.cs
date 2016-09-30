using UnityEngine;
public class AutoRotateCRT : MonoBehaviour , IVirtualButtonEventHandler
{
	private GameObject measterbunny;
	public float speed = 10.0F;
	public float rotationSpeed = 100.0F;
	private Vector3 myEul, targetEul;
	private Transform myTrans;
	private bool canRotate;

	public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
	{
		Debug.Log ("Rotation VB Pressed");
		if (canRotate) {
			Rotate ();
		}
		checkRotation ();
	}

	public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
	{
		Debug.Log("Rotation VB Released");
	}

	void Start () 
	{
		measterbunny = transform.FindChild ("Brain_Model").gameObject;
		VirtualButtonBehaviour vbs = GetComponentInChildren<VirtualButtonBehaviour>();
		if (vbs) 
		{
			vbs.RegisterEventHandler(this);
		}
		myTrans = measterbunny.transform;
		myEul = myTrans.rotation.eulerAngles;
		targetEul = myEul;
		measterbunny.SetActive(true);
	}
	
	void Update () 
	{ 
		}

	void checkRotation ()
	{
		measterbunny.SetActive(true);
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
		measterbunny.SetActive(true);
		Vector3 pos = measterbunny.transform.localPosition * this.transform.localScale.x;
		Debug.Log("Initial Position :"+pos+"");
		VirtualButtonBehaviour vbs1 = GetComponentInChildren<VirtualButtonBehaviour>();
		if (vbs1) {
			measterbunny.SetActive(true);
						myEul = myTrans.rotation.eulerAngles;
						targetEul = myEul;
						targetEul.y += 180;
						canRotate = false;
				} 
	}

}
