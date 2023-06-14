
# Exergaming 
## Tele-rehabilitation software for MS Patients

Exergaming project is a game series developed to improve the treatment process of patients in need of rehabilitation. It includes motion capture based games thanks to the WebCam it uses. The movements of the users are captured by WebCam  and transferred to the Unity game engine. 

Unity, on the other hand, is responsible for all game logic and character object interactions except hardware-based motion capture. The game is planned to include an enhanced interior, lighting and animation package. It is a more realistic polished new version of the first version of the Exergaming game series developed in Python. 

### Project’s Scope and Target Audience
The aim of the project is to develop software for rehabilitation patients/disabled people to receive interactive physical therapy at home. Unity game engine and libraries will be used in software development. The software will enable patients doing physical therapy movements to perform physical movements by playing games. 

The games will be designed from a medical point of view by an expert group of doctors and physiotherapists. In the first stage, the games will be prepared only for Multiple Sclerosis (MS) patients. Game scenarios will include scenarios for physical therapy of patients with upper extremity disability. 

### Motivation for Development of Exergaming
Today, there are many rehabilitation methods and they vary according to the specific needs of each patient. Rehabilitation becomes a long-term, hospital-controlled activity that sometimes takes months to years. Since it is not a very critical and definitive treatment, and it can be done with the individual's effort, not all hospital sessions are covered by the state in long-term treatment. Due to the above reasons, it is neglected by patients over time and eventually abandoned.

In the scenarios prepared by the developer team, it is aimed to improve the treatment process of the patients with the gamification method. Unlike routine repetitive treatment methods, it is aimed to increase the active participation of the patients in the treatment and increase the efficiency. Each scenario provides the operation of special muscle and joint groups during the physical therapy of the patients.

### Exergaming Workflow
In the Exergaming game, the OPENCV library, which is included in the Python programming language, is used as CV2 and hand detection modules to detect the 21 points and 21 joint connections of each hand by the Webcam. The data obtained from here is transferred to the Unity game engine thanks to socket programming and the interaction of the hands with the objects is provided. Exergaming scenarios have been developed with the programming of these interactions.

The game also has analysis skills. The success score of the patients in the treatment process is kept in the background and processed. It is aimed that doctors and physiotherapists can more easily determine the degree of progression of patients and contribute to the treatment method. In order for the patients to have a more positive treatment process both physiologically and psychologically, an appropriate auxiliary visual interface and patient-specific difficulty levels have been added to the game.

### Software and Hardware Relation
In the Exergaming game, hand movements can be detected only with a computer camera or an external camera, without the need for an external sensor. It is sufficient for the users to have a computer with an internal or external camera. If desired, it is possible to project to large screens with HDMI to the extent that computers support.

Webcam is used to detect user movements. While the Webcam provides images on the hardware side, the motion data collected by the Webcam is processed by Python's image processing library OpenCV. Thanks to the HandDetector module, the joints and points of the hands are detected, the position and rotation data of the hands are separated for convenient use by the Numpy and Matplotlib libraries. The separated data is transferred to the Unity game engine thanks to the Socket. Unity.Engine is the general library responsible for general operations within Unity.

Unity game engine is an advanced game engine that supports the scene environment, physics engine, visual interface and objects in which the game is developed. It uses C# language on the programming side. Development for Unity Microsoft Visual Studio 2019 and JetBrains Rider IDE software provide a better quality development environment with the code editing and advanced debugging systems they support. JetBrains Pycharm IDE is used for the Python programming language. Python 3.9 and Unity versions 2020.3.27f1 were used during development.

