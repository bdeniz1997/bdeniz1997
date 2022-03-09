/*
The idea behind this is to get the world height with the biggest amplitude and scale,
and use that Y value to change the sea amplitude throughout the world.
depending on if its deep or shallow.
lets say the y( heigtValue) is between (0~-500) wave amplitude is going to be between (0.31~ 7 or sth)
an inverse lerp and a lerp.
float amp = inverseLerp(0,-500,heightVal);
float realAmp = Lerp(0.31f,7,amp);

and so on.

we'll multiply the current amplitude with that value from our static class. and the buoyancy is done.
on shader side, i want to have the texture and use it in shader. that'll make it good.
or maybe ill texture grayscale pick on scripts as well. who knows :D

*/
