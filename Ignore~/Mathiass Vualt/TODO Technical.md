# Technical
- [x] start using unity input system?
	- [x] is it worth it to start using this?

## Ground Movment
- [x] Basic Ground Movment
- [x] Fix so that ground movment is properly capped
- [x] Fix Countermovment so that is works when you look down
- [x] Remove Sprinting
	- [ ] Not fulle removed, just disabled
- [ ] Test Sebastians [[ScratchPad]] ideas
	- [ ] Space is Glide and or jump
	- [ ] Remove Jump


## Bugs
- [ ] Sometimes the wallrunning animation repeats in air, og plyer hovers (as if wallrunning) for some time in air while losing XZ momentum

## Grapplinghook
- [x] Physics ray collision
- [x] Make grapplingphysics more consistent
- [x] temp GH returning mode
- [x] Simple GH animations
- [ ] Can be made more optimal
	- [x] check if object if grappeable, if not => bounce random off
- [x] make hook follow object that it is hooked to
- [x] Maxdistance
- [x] cant refil meter in air
- [ ] hook is hooked to far from object sometimes


## Scriptableobjects
- Make all variables use it?
- Make only the infocenter use it?

## Sliding
- [x] temperairy sliding
- [ ] make sliding more refined
- [ ] Better animation
- [x] Crouncing while in air
- [x] toggle sliding
- [x] use CTRL to slide
- [x] Have some control when sliding

## Settings Menu
- [x] sensitivity
- [x] Esc stops the game
- [ ] Cant Set CTRL to a Key
	- [ ] crouch/sliding key
		- [ ] Toggle sliding
- [ ] Menu Selecting levels
- [ ] Save SettingsBetween Levels (Scripable Object?)

## Wallrunning
- [x] Wallrunning
- [x] Make the player stick more to the wall
- [x] Refine Wallrunning further
- [x] Must have a certian speed to wallrun
- [x] jumping off wals should be more in direction for momentum
- [x] do not slow upwards momuntum when entering wallrun
- [x] do use inputvector or aristafe if wallrunning
- [ ] Make if holding away from wall, falloff?
- [x] can look compleatly 180 degrees while wallrunning
- [x] Fix disabling wallrunning on start of wallrun[[EarlyStartWallrunGlitch]]
- [x] fix bug where you can use aircontrol the moment you jump off wall
- [ ] have a buffer phase between detectWallrun and Wallrun of 0.1seconds to improve relaiability

## Air Control
- [x] Make airstrafing easier to do
- [x] Make s stop more rapidly
	- [x] Make the button used be relative to the direction
- [x] Make more WASD control in air
- [x] Remake Airstrafe?
	- [x] Airstrafe key? instead of AD? ask people
- [x] Some control if low speeds in air
- [ ] Too much control in air
- [ ] Disable while grappled/reduced?
- [x] Float for airbrake func

## Weapons
- [x] Small delay when rotating weapons
	- [x] Made it more pleasing
	- [x] Try with it rotating TOWARDS the rotation of the player not after it
	- [x] rotation is not smooth all the time, due to framerates i think
- [x] Make an alt version for the turning animation, utylizing position rather than rotation
- [x] Bug: Sometimes the grapplinghook decresses speed to a hold when uncoupling (beacouse collider to GH)
- [x] Readd shorten rope powerup (see how it feels (Game test?))
- [ ] 