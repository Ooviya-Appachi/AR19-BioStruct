using UnityEngine;

public class PartsVirtualButtonEventHandler : MonoBehaviour, IVirtualButtonEventHandler
{
    private GameObject _Heart;
    private GameObject _HeartWithParts;
	private bool mIsRolling = false;
	private float mTimeRolling = 0.0f;
	private float mForce = 0.4f;  
    
	void Start() 
	{
		VirtualButtonBehaviour[] vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
        for (int i = 0; i < vbs.Length; ++i) 
		{
                    vbs[i].RegisterEventHandler(this);
        }
		_Heart = transform.FindChild("Heart").gameObject;
		_HeartWithParts = transform.FindChild("HeartWithParts").gameObject;
		_HeartWithParts.SetActive(false);
		//_Heart.SetActive(false);
    }

	void Update() 
	{
		mTimeRolling += Time.deltaTime;       
		if (mIsRolling && mTimeRolling > 1.0f && _Heart.GetComponent<Rigidbody>().velocity.magnitude < 5)
		{
			_Heart.GetComponent<Rigidbody>().Sleep();          
			mIsRolling = false;
		}
	}
 
    public void OnButtonPressed(VirtualButtonAbstractBehaviour vb) {
        switch(vb.VirtualButtonName) {
            case "WithParts":
				Debug.Log("WithParts VB is pressed ");
				_Heart.SetActive(false);
                _HeartWithParts.SetActive(true);
                    break;
            case "WithoutParts":
			Debug.Log("WithoutParts VB is pressed ");
                _Heart.SetActive(true);
                _HeartWithParts.SetActive(false);
                    break;
			case "Rotation" :
				Debug.Log("Rotation VB is pressed ");
				KickSoccerball();
				break;
            default:
                throw new UnityException("Button not supported: " + vb.VirtualButtonName);
                    break;
        }
    }
    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb) { }

	private void KickSoccerball()
	{
		Bounds targetBounds = this.GetComponent<Collider>().bounds;     
		Rect targetRect = new Rect( -targetBounds.extents.x, -targetBounds.extents.z, targetBounds.size.x,
		                           targetBounds.size.z);
		Vector2 randomDir = new Vector2();    
		for (int i = 0; i < 20; i++)
		{
			randomDir = Random.insideUnitCircle.normalized;
			Vector3 pos = _Heart.transform.localPosition * 
				this.transform.localScale.x;
			Vector2 finalPos = new Vector2(pos.x, pos.z) +
				randomDir * mForce * 1.5f;
			if (targetRect.Contains(finalPos))       
			{              
				break;
			}
		}
		Vector3 kickDir = new Vector3(randomDir.x, 0, randomDir.y).normalized;
		Vector3 torqueDir = Vector3.Cross(Vector3.up, kickDir).normalized;
		_Heart.GetComponent<Rigidbody>().AddForce(kickDir * mForce,
		                               ForceMode.VelocityChange);
		_Heart.GetComponent<Rigidbody>().AddTorque(torqueDir * mForce,
		                                ForceMode.VelocityChange);
		mIsRolling = true;
		mTimeRolling = 0.0f;
	}

	
	
	
}