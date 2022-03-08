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
and if those indices are moved, we move the collider accordingly.

right now i'm just thinking of moving them, rather than making small calculations like
dividing a box collider into little boxes, or resizing. i just want to move or rotate the collider according to the indices.
we'll think about that after we figure out how to move the colliders.

so we need to have references: 
	1 on the center of the collider.
	2 or maybe 4 on the 




*/
void OnCollisionEnter(Collision other){
	
}
