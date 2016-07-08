# planetarium-unity
Interactive solar system with informations about the planets and (an attemp to) an AI.

Project for the practical part of the Web Design and User Experience exam.

# Main Features
- The planets rotate around the Sun and around their axis with their actual real speed.
- Rotating and orbital speed of the planets and the Sun customizable.
- The spaceship can move freely back and forth, rotate around its axis and ascend/discend.
- When the spaceship is nearby a planet, some info about that planet will pop up.
- When the spaceship is **too** nearby a planet, a warning will pop up and an alarm sound will be played.
- Two cameras: one in third person and the other in first person (around the bridge deck of the ship).
- Both of the cameras are free to rotate to let the user look around.
- Autopilot mode: select a planet (or the Sun) and the spaceship will travel until it will reach its destination.

# Normalization
The planets' sizes and distances are in scale, more precisely an algorithm scale has been used in order to emphasize the smallest distances/sizes over the biggest distances/sizes. The Sun's size has been taken out of this scale (because it would have messed up the normalization since it's much much more bigger than the other planets) and its size has been calculated as 109 times the Earth; similarly, the Moon was much more smaller than the other planets and it's distance from the Earth was much more smaller too so it's size and distance have been determined by eye.

## Autopilot mode
The autopilot is an AI who tries its best to reach the destination selected by the user. The "algorithm" behind its reasoning it's the following:

1. Find the planet's position in the universe.
2. Rotate until the spaceship points towards it.
3. Move forward until it "senses" to be in some planet's atmosphere.
  1. If the planet's atmosphere belongs to the destination, it stops.
  2. If the planet's atmosphere doesn't belong to the destination, start a manoeuvre to get away.
    1. Rotate the spaceship in order to let it be tangent with the planet.
    2. Move forward for a fixed distance.
      1. If during this manoeuvre, the spaceship doesn't enter in another planet's atmosphere, restart from the first point of this list.
      2. Otherwise, repeat the manoeuvre to get away.

The AI uses a "trial-and-error" approach: it knows where the destination is but doesn't know if there are some other objects in between until it enters in their atmosphere; in this case it can only try to get away from the planet to avoid collision. It may take a lot of time to reach its destination but sooner or later it does reach it without colliding.

In the worst case scenario the autopilot finds itself in a "loop", bouncing from planet to planet without reaching the destination and doing the same choices over and over again. The only solution to this is disable the autopilot and regain control of the spaceship.

## Informations
While moving around the solar system, passing nearby a planet will make a panel containing informations pop up. In this panel there will be:

1. Planet's mass.
2. Planet's diameter.
3. Planet's gravity.
4. Planet's mean temperature.
5. Planet's number of moons.
6. Planet's orbital speed.
7. Planet's rotation speed.
 
All the values of these informations are taken from the [NASA's website] (http://nssdc.gsfc.nasa.gov/planetary/factsheet/).

# The scripts
## Autopilot.cs
Implements the AI discussed before. During the autopilot mode, all the warnings and the information are disabled: this because the autopilot knows where it's going so the user won't have to worry about collision and because the panel info won't be shown for much time during the spaceship movements so they will be not so useful (if not impossible) to read. Exiting from the autopilot mode will automatically re-enable both warnings and information.

## DetectCollision.cs
Constantly monitors the surroundings of the spaceship and when it enters in a planet's atmosphere, it will play a sound and display a flickering warning. If the spaceship will continue going forward and collide with a planet's surface, the scene will be resetted from the initial point.

## MoonRotateAround.cs
Makes the Moon rotate around the Earth according to the real time data (unless the orbital speed is changed).

## MoonRotation.cs
Makes the Moon rotate around its own axis according to the real time data (unless the rotation speed is changed).

## MoveSpaceship.cs
Makes the spaceship move around the solar system. The spaceship can move only back and forth, so it will need to rotate before starting to move if the destination is sideways. The user can either move the spaceship with the keyboard or look around using the mouse, one thing disables the other (unless in pilot mode, when the user doesn't have to move the spaceship).

## NearestPlanet.cs
Constantly monitors the surroundings of the spaceship and when it's nearby a planet (before entering in its atmosphere), it will show a panel containing data about that planet.

## ObjectRotateAround.cs
Makes a planet rotate around the Sun according to the real time data (unless the orbital speed is changed).

## ObjectRotating.cs
Makes a planet rotate around its own axis according to the real time data (unless the rotation speed is changed).

## PlanetariumSettings.cs
Manages the orbital and the rotation speeds and determine wheter the used pressed the start button or not.

## SunRotate.cs
Makes the Sun rotate around its own axis according to the real time data (unless the rotation speed is changed).

## SwitchCamera.cs
Switch between the third person camera (default one at the beginning) and the first person camera. In both configurations the user can do the same actions and its selection will remain even if he moves the spaceship or enables the autopilot.

# Credits
- Arcadia's 3D model made by [DocMikeB](http://docmikeb.deviantart.com/art/Harlock-s-Arcadia-209167000)
- Nasalization font by [typodermicfonts](http://typodermicfonts.com/nasalization/)
