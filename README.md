# Mole Defense

A highly strategic game about a mole defending its lair.

## World

A hole in the ground. Not a hobbit hole. A mole hole. A simple hole.
This hole is being invaded by evil roots, trying to destroy this simple, mole made, hole.

### Presentation
A 2d side view, cross section round hole in the ground.

## Mole

The mole is the main character of the game, the player controls the character. 

### Movement
The mole will move in a circular motion around it's hole.
It will also interact and drop items in it's position when the interaction key is pressed.

### Stats:

* __HP__: this is actually the hole occupation rate, measured in how many roots reached the hole center;
* __Speed__: The speed at which the mole rotates;
* __Attack rate__: the rate of applied damage to the roots;
* __Damage__: The damage dealt at each rate;
* __Vision__: The sight of the mole. It affects the vignette aroud the screen, allowing the player to see further;

## Roots

### Growth
An animation is played of a root spreading from the edge of the screen towards the center of the screen. When it reaches the mole hole edge, a the facto root grows to the center of the screen.

### Death
If the mole is standing on a root, the mole eats the root, playing animations and emmitting particles. The root will shrink and vanish.

### Types
There will be a variety of roots based on the following stats:
* Weaknesses;
* Thoughness;
* Behaviour.

## Waves

The game loop will be controlled by spawn waves. These are configurable by design and will be pre determinated until round 10. After this round, the waves will be randomnly generated, getting harder and harder to beat.

### Waves stats:
* __Number of roots__;
* __Spawn zones__;
* __Available store items__;
* __Rest time__;
* __Boss?__;

## Deployables

Inbetween waves, at rest time, the player will be allowed to pick an item from the store. These deployable items will need to be put down before the wave starts.
The available deploayable items will be:
* __Herbicida__: Adds a poisonous cloud that stops root growth;
* __TNT__: Explodes on contact with roots; 
* __Trampolim__: Projects the mole into the oposite side.

## Upgrades

These are special abilities that affect the mole performance directly:
* __Glasses__: expand vision;
* __Boots__: increase speed;
* __Teeth__: increase damage;


## Input

* __Left Arrow__ and __Right Arrow__: rotate left or right
* __Left joystik__: rotate accordingly
* __X or A__: Interact
