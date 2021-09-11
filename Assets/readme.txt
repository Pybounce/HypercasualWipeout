Time Spent:
I spent around 5 days start to finish on the project, with about 30-40 hours in total, however this also includes making 3D models and other assets.

Difficulties:
Due to the tight deadline I decided to cut certain things out of the project that would have otherwise been extremely useful for developers/level designs. One of these things was the level editor - right now it requires filling in a ScriptableObject with a bunch of data and has 0 visualisation.

Larger Features:
The spline and level loading is likely the most complex part, especially due to the above mentioned comment. The spline and level creation system however, is very open ended and allows people to place continious walls, discrete wall elements, coins/collectables and obstacles, and they can toggle whether or not these things will snap to the path.
The path itself is extruded along a spline, which added levels of complexity due to the fact that everything in the game would then need to be along the same spline (the player, items etc)
There are ways to create a new type of path (Scripted2DSpline) so that in future updates there can be varied 'worlds' or 'chapters' to keep things new for the players and keep them engaged.

What makes it hypercasual?:
The way I judge if a game is hypercasual is largely on the input controls the player must use. This game has a swipe motion in all 4 directions and no other input is needed during the level. There is usually also a very clear goal, which in this game would be to collect all the diamonds and complete the level, however a shop system hasn't been implemented yet.

Controls:
Swipe up to jump.
Swipe down to slide.
Swipe left/right to move onto other lanes.

Assets:
No assets were used in the production of this project.

Other notes:
I'd just like to make clear that this is just a prototype, as stated in the brief I was sent at the start. This was an unusually busy week for me with family events, hence why I'm missing 2 days from the 7 given, otherwise I would have absolutely used them all.
Lots of things are subject to change, most noticably is the level loading system, right now it's temporary since I didn't use addressables. This is the reason it kicks you back to the main menu after each level, this is temporary.