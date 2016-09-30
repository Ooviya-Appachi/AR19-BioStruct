using UnityEngine;
public class VBEasterBunnyEventHandler :     MonoBehaviour,  IVirtualButtonEventHandler
{
	private GameObject mHeart;  
	private bool mIsRolling = false;
	private float mTimeRolling = 0.0f;
	private float mForce = 0.4f;    
	
	public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
	{
		Debug.Log("Rotation Button Pressed");     
		KickSoccerball();
	}
	
	public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)  
	{
		Debug.Log("Rotation Button Released");  
	}
	
	void Start()   
	{
		VirtualButtonBehaviour vb = GetComponentInChildren<VirtualButtonBehaviour>();      	
		if (vb)     
		{
			vb.RegisterEventHandler(this);        
		}
		mForce *= transform.localScale.y;    
	}
	
	
	void Update() 
	{
		mTimeRolling += Time.deltaTime;       
		if (mIsRolling && mTimeRolling > 1.0f && mHeart.GetComponent<Rigidbody>().velocity.magnitude < 5)
		{
			mHeart.GetComponent<Rigidbody>().Sleep();          
			mIsRolling = false;
		}
	}
	
	private void KickSoccerball()
	{
		Bounds targetBounds = this.GetComponent<Collider>().bounds;     
		Rect targetRect = new Rect( -targetBounds.extents.x, -targetBounds.extents.z, targetBounds.size.x,
		                           targetBounds.size.z);
		Vector2 randomDir = new Vector2();    
		for (int i = 0; i < 20; i++)
		{
			randomDir = Random.insideUnitCircle.normalized;
			Vector3 pos = mHeart.transform.localPosition * this.transform.localScale.y;
			Vector2 finalPos = new Vector2(pos.y, pos.z) +
				randomDir * mForce * 1.5f;
			if (targetRect.Contains(finalPos))       
			{              
				break;
			}
		}
		Vector3 kickDir = new Vector3(randomDir.x, 0, randomDir.y).normalized;
		Vector3 torqueDir = Vector3.Cross(Vector3.up, kickDir).normalized;
		mHeart.GetComponent<Rigidbody>().AddForce(kickDir * mForce,
		                               ForceMode.VelocityChange);
		mHeart.GetComponent<Rigidbody>().AddTorque(torqueDir * mForce,
		                                ForceMode.VelocityChange);
		mIsRolling = true;
		mTimeRolling = 0.0f;
	}
}
