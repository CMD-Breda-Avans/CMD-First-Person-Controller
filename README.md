# CMD First Person Controller
A collection of scripts to easily control a first person character in Unity

## How to use
1. Create a Capsule with a Capsule Collider that's the size of what your character should be
1. Add the **Player Look** and **Player Move** scripts to the object
1. Parent the camera to the capsule, where the face would be
1. Add the **Camera Look Detector** script to the camera
1. Create a canvas with an Image in the center, with a crosshair Source Image
1. Add the **Mouse Cursor** script to the Image
1. The player should now be able to move and look around
1. Add the **Object Interaction** script to every object that the player needs to interact with
1. Follow further instructions in the **Object Interaction** script
