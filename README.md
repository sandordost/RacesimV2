<h1>Improved Race Simulator (V2)</h1>
The initial version of our project didn't fully align with clean-code and SOLID principles, prompting this comprehensive remake. This iteration is developed using C# .NET 8.0 and showcases significant improvements.

<h2>Features</h2>
<h3>Race Simulator Shared (Core) Project</h3>
The <b>RaceSimulatorShared</b> project forms the backbone of the Race Simulator application, defining key models and components integral to the simulation. This core library is organized into various directories, each tailored to specific simulation aspects:

<h4>Models</h4>
<hr>
<ul>
  <li><b>Competition.cs</b>: Structures and properties of a competition, including multiple races or tracks.</li>
  <li>
    <b>Equipments/</b>:
    <ul>
      <li><b>Car.cs</b>: Representation of a car with unique attributes and capabilities.</li>
      <li><b>IEquipment.cs</b>: Interface outlining standard functionalities for participants' equipment.</li>
    </ul>
  </li>
  <li>
    <b>Participants/</b>:
    <ul>
      <li><b>Driver.cs</b>: Individual driver characteristics and associated equipment.</li>
      <li><b>IParticipant.cs</b>: Blueprint interface for a race participant.</li>
      <li><b>TeamColors.cs</b>: Enumerates team colors associated with participants.</li>
    </ul>
  </li>
  <li><b>Tracks/</b>:
    <ul>
      <li><b>Track.cs</b>: Details of a race track including name and sections.</li>
    </ul>
  </li>
</ul>

<h4>Events</h4>
<hr>
<ul>
  <li><b>ParticipantChangedEventArgs.cs</b>: Events for participant status or position changes.</li>
  <li><b>ParticipantLappedEventArgs.cs</b>: Events marking a participant's lap completion.</li>
  <li><b>TrackAdvancedEventArgs.cs</b>: Signals race track progress.</li>
  <li><b>TrackEventsManager.cs</b>: Manages track-related events.</li>
</ul>

<h4>Sections</h4>
<hr>
<ul>
  <li><b>Direction.cs</b>: Enumerates possible directions in a track section.</li>
  <li><b>Section.cs</b>: Represents a single section of the race track, detailing type and direction.</li>
  <li><b>SectionType.cs</b>: Defines different types of track sections, such as start, finish, and corner.</li>
</ul>

<h3>Controller Project</h3>
The <b>RaceSimulatorController</b> project serves as the control center for managing the race logic and data flow. It consists of several key components:
<ul>
  <li><b>Data.cs</b>: Centralizes the data management for the application, handling race creation, progression, and storage.</li>
  <li>
    <b>Events/</b>:
    <ul>
      <li><b>RaceChangedEventArgs.cs</b>: Defines an event that occurs when there's a significant change in the current race.</li>
      <li><b>RaceFinishedEventArgs.cs</b>: Represents the event data when a race is completed.</li>
    </ul>
  </li>
  <li>
    <b>Exceptions/</b>:
    <ul>
      <li><b>NoTracksException.cs</b>: Thrown when no tracks are available for racing.</li>
      <li><b>RaceNotFoundException.cs</b>: Indicates that a requested race could not be found.</li>
    </ul>
  </li>
  <li><b>Race.cs</b>: Represents a single race, encompassing the logic for starting the race, handling participant laps, and determining the race's conclusion.</li>
  <li><b>Score.cs</b>: Defines the scoring system, keeping track of each participant's laps and time elapsed.</li>
</ul>

<h3>Console Project</h3>
The <b>RaceSimulatorConsole</b> project provides a text-based interface for interacting with the Race Simulator. It includes:
<ul>
  <li><b>Program.cs</b>: The main entry point for the console application, orchestrating user interaction, race initialization, and progression.</li>
  <li>
    <b>Tools/</b>:
    <ul>
      <li><b>TrackPrinter.cs</b>: A utility class for printing the race track and participant positions to the console, visualizing race progress in a text-based format.</li>
      <li><b>TrackVisualizer.cs</b>: Enhances the visualization of the race track within the console, providing a more detailed view.</li>
    </ul>
  </li>
</ul>

<h3>WPF Project (RaceSimulatorWPF.zip)</h3>
The <b>RaceSimulatorWPF</b> project introduces a Graphical User Interface using Windows Presentation Foundation (WPF). It includes:
<ul>
  <li><b>App.xaml.cs</b>: Initializes the WPF application and sets up the main environment.</li>
  <li><b>MainWindow.xaml</b>: The primary layout file for the application's main window.</li>
  <li><b>MainWindow.xaml.cs</b>: Contains the logic for the main window's user interactions and responses.</li>
  <li><b>RaceDataContext.cs</b>: Manages the data binding for the application, ensuring dynamic updates to the UI with race data.</li>
  <li><b>TeamColorToBrushConverter.cs</b>: A utility class for converting team colors to brush objects for UI rendering.</li>
</ul>
<p><i>Image Suggestion: Insert a screenshot of the main window from the WPF application here to showcase the GUI.</i></p>

<h3>Test Project (RaceSimulatorTests.zip)</h3>
The <b>Test Project</b> contains unit tests for various components of the Race Simulator, ensuring robustness and reliability:
<ul>
  <li>
    <b>RaceSimulatorControllerTests/</b>:
    <ul>
      <li><b>DataTests.cs</b>: Tests the data management functionalities in the RaceSimulatorController.</li>
      <li><b>RaceTests.cs</b>: Focuses on testing the logic and behavior of races in the controller project.</li>
    </ul>
  </li>
  <li>
    <b>RaceSimulatorSharedTests/</b>:
    <ul>
      <li><b>CompetitionTests.cs</b>: Verifies the integrity and functionality of the Competition model in the shared library.</li>
      <li><b>IEquipmentTests.cs</b>: Ensures that the equipment interface works correctly and as expected.</li>
      <li><b>SectionTests.cs</b>: Tests related to the Section model, covering different track sections and their properties.</li>
      <li><b>TrackTests.cs</b>: Concentrates on testing the Track model, examining the structure and behaviors of race tracks.</li>
    </ul>
  </li>
</ul>
