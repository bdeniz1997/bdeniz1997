using UnityEngine;

Class climbing{
	Transform bodyTransform;
	Collider bodyCollid;
	bool ClimbHit;
	
  void climbCheck(){
		//raycast 3 times top mid bottom
		raycasthit hit;

		ClimbHit=false;
		for(int i=0;i<3;i++){
			Vector3 orig=bodyTransform.position+(bodyCollid.size.bounds.y*0.5f* (i-1));
			RayCast(orig, dir = bodyTransform.forward, out hit,0.4f );
			if(hit){ClimbHit=true; break;}
			// so hit var is the first 
		}
	}
	
	void climbRotation(){
		//change body X rot parallel to hit x rot using normals.
		bodyTransform.rotation　*= Quaternion.FromToRotation(Vector3.right, hit.normal);
		//try the code above. right is (1,0,0)
	}
	
	void Update(){
		climbCheck();
	}
	
	void onCollisionStay(Collision other){
		if(ClimbHit && state==pstate.climbing){
			climbRotation();
			//if movement happens, it will move character depending on its transformation, so its ok.
			//idk if the movement tis fixed or not though, need to check the move function.
			//try the thing out like this first.
		}
	}
}
