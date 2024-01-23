<h1>Improved Race Simulator (V2)</h1>
The first project was very unfinished and did not meet the <b>clean-code</b> and <b>SOLID</b> standards, which is the reason the project has been remade. This project is made with C# .NET 8.0.

<h2>Features</h2>

<h3>Shared Library Project (Core)</h3>
The RaceSimulatorShared project acts as the core library for the Race Simulator application. It defines the essential models and components used throughout the simulation. The project is organized into several directories, each focusing on different aspects of the race simulation:

Models/Competition.cs: Defines the structure and properties of a competition, which may involve multiple races or tracks.

Models/Equipments/: Contains classes related to racing equipment:

Car.cs: Represents a car with specific attributes or capabilities.
IEquipment.cs: An interface that outlines the standard structure and functionalities for equipment used by participants.
Models/Participants/: Includes classes representing participants in the race:

Driver.cs: Defines a driver with individual characteristics and associated equipment.
IParticipant.cs: An interface that sets the blueprint for what constitutes a race participant.
TeamColors.cs: Enumerates different team colors that can be associated with participants.
Models/Tracks/: Holds the structure and logic for race tracks and related events:

Track.cs: Describes a race track, including its name and composed sections.
Events/:
ParticipantChangedEventArgs.cs: Details events related to changes in participant status or position.
ParticipantLappedEventArgs.cs: Captures events when a participant completes a lap.
TrackAdvancedEventArgs.cs: Used for events signaling the progress of the race track.
TrackEventsManager.cs: Manages and handles various track-related events.
Sections/:
Direction.cs: Enumerates possible directions in a track section.
Section.cs: Represents a single section of the race track, with type and direction.
SectionType.cs: Enumerates different types of track sections (e.g., start, finish, corner).
This project provides a foundational framework for the race simulation, defining the key elements like tracks, participants, and equipment, along with the necessary events for race progression.

<h3>Controller Project</h3>
The RaceSimulatorController project serves as the control center for managing the race logic and data flow in the Race Simulator application. It consists of several key components:

Data.cs: Centralizes the data management for the application, handling the creation and progression of races, managing competition data, and storing finished races. It acts as a hub for race-related data and events.

Events/: Contains custom event classes used in the race simulation:

RaceChangedEventArgs.cs: Defines an event that occurs when there's a significant change in the current race, such as starting a new race.
RaceFinishedEventArgs.cs: Represents the event data when a race is completed.
Exceptions/: Includes custom exception classes specific to the race simulation:

NoTracksException.cs: An exception that is thrown when no tracks are available for racing.
RaceNotFoundException.cs: Indicates that a requested race could not be found.
Race.cs: Represents a single race, encapsulating the logic for starting the race, handling participant laps, and determining when the race finishes. It connects the race track with participants and monitors their progress.

Score.cs: Defines the scoring system for the race, keeping track of each participant's laps and time elapsed.

The RaceSimulatorController project is crucial for orchestrating the race simulation, ensuring that races are conducted according to the rules and progress logically. It interacts closely with the shared models defined in the RaceSimulatorShared project to bring the simulation to life.

<h3>Console Project</h3>
The RaceSimulatorConsole project provides a text-based interface for interacting with the Race Simulator. It is designed for simplicity and ease of use, allowing users to engage with the simulation through a console window. The project includes:

Program.cs: The main entry point for the console application. It orchestrates the user interaction, race initialization, and progression. It is responsible for starting the simulation and responding to user inputs.

Tools/:

TrackPrinter.cs: A utility class for printing the race track and participant positions to the console. It visualizes the race progress in a text-based format.
TrackVisualizer.cs: This class enhances the visualization of the race track within the console. It provides a more detailed view of the track, including sections and participant locations, making the race easier to follow in a console environment.

The RaceSimulatorConsole project offers a straightforward way to interact with the race simulator using command-line inputs. It's ideal for demonstrations, quick testing, or users who prefer a minimalistic, text-based interface.

<h3>WPF Project (RaceSimulatorWPF.zip)</h3>
The RaceSimulatorWPF project offers a Graphical User Interface (GUI) using Windows Presentation Foundation (WPF). It provides an interactive and visually engaging interface for the Race Simulator. The key components include:
<b>App.xaml.cs:</b> Initializes the WPF application and sets up the main environment.
<b>MainWindow.xaml:</b> The primary layout file for the application's main window, defining the GUI structure.
<b>MainWindow.xaml.cs:</b> Contains the logic for the main window's user interactions and responses.
<b>RaceDataContext.cs:</b> Manages the data binding for the application, ensuring dynamic updates to the UI with race data.
<b>TeamColorToBrushConverter.cs:</b> A utility class for converting team colors to brush objects for UI rendering, enhancing the visual aspects of the application.
<h3>Test Project (RaceSimulatorTests.zip)</h3>
The Test Project contains unit tests for various components of the Race Simulator, ensuring robustness and reliability. The structure is as follows:
<b>RaceSimulatorControllerTests/DataTests.cs:</b> Tests the data management functionalities in the RaceSimulatorController.
<b>RaceSimulatorControllerTests/RaceTests.cs:</b> Focuses on testing the logic and behavior of races in the controller project.
<b>RaceSimulatorSharedTests/CompetitionTests.cs:</b> Verifies the integrity and functionality of the Competition model in the shared library.
<b>RaceSimulatorSharedTests/IEquipmentTests.cs:</b> Ensures that the equipment interface works correctly and as expected.
<b>RaceSimulatorSharedTests/SectionTests.cs:</b> Tests related to the Section model, covering different track sections and their properties.
<b>RaceSimulatorSharedTests/TrackTests.cs:</b> Concentrates on testing the Track model, examining the structure and behaviors of race tracks.
These tests play a crucial role in maintaining the quality and consistency of the Race Simulator application.
