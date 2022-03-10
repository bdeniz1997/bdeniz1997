#using UnityEngine;
/*
about my pirate game, i've implemented a mesh deform system when the ship gets it by a cannonball.
and it deforms the mesh, but currently its only affecting the mesh and not the colliders.
I've tried using mesh collider, but it needs to be convex, and you cant walk through the ship if i use that.
so i decided to build the whole ship colliders out of a bunch of primitive box colliders, and 
will try to deform them, to make the actual walkable area change.
because right now, by looks it is fine but when you try to step on it, you'll probably be walking on air :D
so, here goes my solution:

the goal here is to move the colliders along with the deformed vertices. we need to set a couple of indices for each collider for reference.
and if those indicecs are moved, we move the collider accordingly.

right now i'm just thinking of moving them, rather than making small calculations like
dividing a box collider into little boxes, or resizing. i just want to move or rotate the collider according to the indices.
we'll think about that after we figure out how to move the colliders.

1) so we need to have references: 
	on the corners of the box. so, the thing we need to do is: find through vertices and check for the closest point to the corner.
	
	
2) so, the dots on the picture. we can have 3 dots there for the transform and rotation.
1st = top left corner
2nd = top right corner
3rd bottom left corner

2.1) rotation is going to be :
	forward = top left - bottom left.
	up = top left - top right
forward and backwards may vary depending on the colliders face.

2.2) position:
	i think we're going to make this based on how 3 of them moved. 
	we'll get the diff vector as: after - before = diffVector;
	diffVector[0~2] as we have 3 of them.
	well sum em up and divide by 3. this is going to be an average thing.
	
//////////////////////////////////////////
i'll try to code this and lets see if it works.

*/
public Vector3[,] colliderPoint;
public int[,] coliderVertindex;

void Start(){
	SetPointsForColliders();
	colliderPoints = new Vector3[transform.ChildCount,3];
}

void SetPointsForColliders(){
	for (int i = 0; i < colliders.childCount; i++){
            Transform collidTransform = colliders.GetChild(i);
            BoxCollider collid = collidTransform.GetComponent<BoxCollider>();
            Vector3 size = collid.bounds.size;
            size.x *= 0.5f;
            size.z *= 2f;
            
            colliderPoint[i,0] = collid.center + new Vector3(-size.x, -size.y, size.z) * 0.5f;
            colliderPoint[i,1] = collid.center + new Vector3(size.x, size.y, size.z) * 0.5f;
            colliderPoint[i,2] = collid.center + new Vector3(-size.x, size.y, size.z) * 0.5f;

            //make sure to show them in Gizmo Editor to see where they go :)
        }	
}

void SetIndicesForColliders(){
	//search the closest. use a compute shader maybe.
}

private void OnDrawGizmos() {
	//each one has a different color to be able to see it.
        Color[] clrs = { Color.magenta, Color.yellow, Color.cyan };
        for (int i = 0; i < colliders.childCount; i++) {
            Transform collidTransform = colliders.GetChild(i);
            for (int j = 0; j < 3; j++) {
                Gizmos.color = clrs[j];
                Gizmos.DrawSphere(collidTransform.TransformPoint(colliderPoint[i, j]), 0.1f);
            }
        }
    }

//find a way to know which collider is gonna get affected.
//meshdeform.cs would inform us with that.
public void adjustCollider(Transform hitColliderTransform, int index){
	//I'll assume that the only thing that hits is cannon.
	adjustPosition(hitColliderTransform, index);
	adjustRotation(hitColliderTransform, index);
}

private void adjustPosition(Transform hitColliderTransform, int index){
	
}

private void adjustRotation(){
	
}
