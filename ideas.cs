/*

----1---

The idea of doing things differently!

this one is about the collision and the mesh deformation.

i'll put a script on every collider first.

onCannonBall script, i'll make whatever it touches, add a rigidbody to it.
set it to usegravity false, this way its going to move.

and i'll try to match that with the collider.

-then i'll delete it -


----2---
make thousands of little colliders for every 10 vertex or so.
and use our implemented technique to move them around. lets see if we can do that.
this looks promising but i can't say the same about the performance. lolz.
whatever lets go.


-----3---

create another simplified concave mesh.
but it needs to be with vertex less than 255.

its gonna be concave. we'll do it. and it wont be convex.
we'll make it convex.

then, the collider is gonna be generated off of that mesh.
but we'll update the actual mesh as well,
but use this mesh to convert it into the mesh colliders collider.
we need to hide this thing, so we'll use it as a depth mask as well.
that's 2 birds in one stone. 

lets do that first!

*/
