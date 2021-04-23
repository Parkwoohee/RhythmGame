More information about this package and its development repository at: https://bitbucket.org/neurorehablab/kave/overview
Information about how it works, please consult the paper at: https://doi.org/10.1145/3229092
A video of the system being used to create a 4 projectors VR CAVE can be seen at https://youtu.be/ukwrnnsnV9M
and an example of it being used on a normal desktop screen here https://youtu.be/TcIiOqlwryA

The author can be contacted at afonso.goncalves@m-iti.org


The KAVE is a set of prefabs and scripts that use the Kinect V2 to add parallax effect to unity projects and also full body tracking for interaction with virtual objects.
The parallax effect happens when you move your head relative to the screen and the image reacts as if you are looking through a window to a virtual world or something is poping out from the screen,
parallax effect is one of several visual cues that the human brain uses to perceive depth.
In practice, virtual objects that are beyond the screen/projection plane will move in the opposite direction than the head, objects that are closer than the screen will move in the same direction, this effect makes it look as if the user is in the virtual environment.
To create this effect the system needs to know where the screens or projection surfaces (in case you are using projectors) and the Kinect are located in space, so that it knows the position of the user head relative to the screens.
The KAVE also uses the Kinect to provide full body tracking in form of a 25 joints skeleton that can collide with other virtual objects.

Summary:
	Adds parallax effect to Unity projects through Kinect v2 head tracking
	Supports up to 8 screens/projectors in any physical configuration (normal desktop screen, CAVE, etc...)
	Features full skeleton tracking for interaction with virtual objects
	It requires a calibration file with the position, orientation and size of the screens, projectors, projection surfaces and Kinect sensor. For this you can use our calibration software.

Requirements:
	A Microsoft Kinect v2 sensor with adapter for Windows

	The Kinect SDK must be installed, "Kinect for Windows SDK 2.0" download at:
		https://www.microsoft.com/en-us/download/details.aspx?id=44561
		https://www.microsoft.com/en-us/download/confirmation.aspx?id=44561	(direct link)

	Microsoft's official Kinect2 unity package must be added to the project,
	extract "Kinect.2.0.1410.19000.unitypackage" from "Unity Pro packages", download at:
		https://developer.microsoft.com/en-us/windows/kinect	(Kinect for Windows homepage, download Unity Pro packages)
		https://go.microsoft.com/fwlink/p/?LinkId=513177	(direct link)

	A KAVE calibration file describing the physical setup (position and orientation of screens, projectors and Kinect)
	Use this software to create a calibration file (instructions included in the archive):
		https://bitbucket.org/neurorehablab/kave/downloads/KAVECalibrator.rar

	You should use the calibration software to generate a file describing your spatial setup as the system needs to know the position and orientation of all displays, projectors and Kinect being used.
	The calibration software generates a file "Calibration.xml" with this information that needs to be placed at the "StreamingAssets" folder of your project. This file can be changed after the project is built by replacing it in the streamming assets folder of the build.
	The KAVE package also comes with example calibration files in the "StreamingAssets" folder, the default file describes a 22" desktop screen with a Kinect mounted on top.

How to use this package:
	
	1. Have a Kinect v2 connected to your PC and its SDK installed
	2. Add Microsoft's "Kinect.2.0.1410.19000.unitypackage" package to your project (see requirements)
	3. Add the KAVE package to your project
	4. Put the "Calibration.xml" that describes your physical setup in the "StreamingAssets" folder of your project (or use one of the examples provided with the package).
	5. Add the VRManager prefab to the scene
	6. Disable or delete all other game cameras as VRManager will create its own cameras
	7. Test it and move the "VRManager" prefab in your game as you would move a camera, modify the prefab "User View Camera" as you would customize any standard Unity camera with visual effects and so on

Common issue:
	If you try to use a calibration file that specifies a number of screens/projectors higher than the number of screens/projectors connected to your PC
	it will work on the editor multiple game windows but not on the build, where it will not display anything.

If you use or adapt this software in your research please cite:

	Afonso Gonçalves and Sergi Bermúdez i Badia. 2018.
	KAVE: Building Kinect Based CAVE Automatic Virtual Environments, Methods for Surround-Screen Projection Management, Motion Parallax and Full-Body Interaction Support.
	PACM on Human-Computer Interaction, Vol. 1, EICS, Article 10 (June 2018), 15 pages.
	https://doi.org/10.1145/3229092