﻿using RaceSimulatorShared.Models.Equipments;
using RaceSimulatorShared.Models.Participants;
using RaceSimulatorShared.Models.Tracks;
using RaceSimulatorShared.Models.Tracks.Sections;

namespace RaceSimulatorTests.RaceSimulatorSharedTests;

public class TrackTests
{
    [Fact]
    public void InitializeSections_ShouldInitializeSections()
    {
        var sections = new SectionType[] { SectionType.Start, SectionType.Finish };
        Track track = new("testTrack", sections, 100);

        Assert.True(track.Sections.Count == 2);
    }

    [Fact]
    public void PlaceParticipantsOnStart_ShouldPlaceParticipantsOnFirstSection()
    {
        var sections = new SectionType[] { SectionType.Start, SectionType.Finish };
        Track track = new("testTrack", sections, 100);
        var participants = new List<IParticipant>()
        {
            new Driver("testDriver1", new Car(), TeamColor.Green),
            new Driver("testDriver2", new Car(), TeamColor.Grey),
            new Driver("testDriver3", new Car(), TeamColor.Red)
        };

        Assert.True(track.Sections.First?.Value.ParticipantSectionProgressions.Count == 0);
        track.PlaceParticipantsOnStart(participants);
        Assert.True(track.Sections.First.Value.ParticipantSectionProgressions.Count == 3);
    }

    [Fact]
    public void AdvanceParticipantsInAllSections_ShouldAdvanceParticipants()
    {
        var sections = new SectionType[] { SectionType.Start, SectionType.Finish };
        Track track = new("testTrack", sections, 100);

        var participants = new List<IParticipant>()
        {
            new Driver("testDriver1", new Car(100, 100, 51), TeamColor.Green),
            new Driver("testDriver2", new Car(100, 100, 51), TeamColor.Grey),
            new Driver("testDriver3", new Car(100, 100, 51), TeamColor.Red)
        };

        track.PlaceParticipantsOnStart(participants);

        track.AdvanceParticipantsInAllSections();
        Assert.True(track.Sections.First?.Value.ParticipantSectionProgressions.Count == 3);

        track.AdvanceParticipantsInAllSections();
        Assert.True(track.Sections.First?.Value.ParticipantSectionProgressions.Count == 0);
        Assert.True(track.Sections.Last?.Value.ParticipantSectionProgressions.Count == 3);
    }

    [Fact]
    public void AdvanceParticipantsInAllSections_ShouldPlaceParticipantsOnStartAfterFinish()
    {
        var sections = new SectionType[] { SectionType.Start, SectionType.Finish };
        Track track = new("testTrack", sections, 100);
        var speed = 51;

        var participants = new List<IParticipant>()
        {
            new Driver("testDriver1", new Car(100, 100, speed), TeamColor.Green),
            new Driver("testDriver2", new Car(100, 100, speed), TeamColor.Grey),
            new Driver("testDriver3", new Car(100, 100, speed), TeamColor.Red)
        };

        track.PlaceParticipantsOnStart(participants);

        track.AdvanceParticipantsInAllSections();
        Assert.True(track.Sections.First?.Value.ParticipantSectionProgressions.All(progression => progression.Value == speed));
        Assert.True(track.Sections.First?.Value.ParticipantSectionProgressions.Count == 3);

        track.AdvanceParticipantsInAllSections();
        var newProgression = Math.Abs(100 - speed * 2);
        Assert.True(track.Sections.First?.Value.ParticipantSectionProgressions.Count == 0);
        Assert.True(track.Sections.Last?.Value.ParticipantSectionProgressions.All(progression => progression.Value == newProgression));
        Assert.True(track.Sections.Last?.Value.ParticipantSectionProgressions.Count == 3);

        track.AdvanceParticipantsInAllSections();
        track.AdvanceParticipantsInAllSections();
        Assert.True(track.Sections.First?.Value.ParticipantSectionProgressions.Count == 3);
        Assert.True(track.Sections.Last?.Value.ParticipantSectionProgressions.Count == 0);
    }

    [Fact]
    public void RemoveParticipantFromTrack_ShouldRemoveParticipantFromAllSections()
    {
        var sections = new SectionType[] { SectionType.Start, SectionType.Finish };
        Track track = new("testTrack", sections, 100);
        var speed = 51;

        var participants = new List<IParticipant>()
        {
            new Driver("testDriver1", new Car(100, 100, speed), TeamColor.Green),
            new Driver("testDriver2", new Car(100, 100, speed), TeamColor.Grey),
            new Driver("testDriver3", new Car(100, 100, speed), TeamColor.Red)
        };

        track.PlaceParticipantsOnStart(participants);

        track.RemoveParticipantFromSections(participants[0]);

        Assert.True(track.Sections.First?.Value.ParticipantSectionProgressions.Count == 2);
    }

    [Fact]
    public void CalculateTrackLength_ShouldReturnCorrectLength()
    {
        var sections = new SectionType[] { SectionType.Start, SectionType.Finish };
        Track track = new("testTrack", sections, 100);

        Assert.True(track.CalculateTrackLength() == 200);
    }

    [Fact]
    public void HasActiveParticipants_ShouldReturnTrue()
    {
        var sections = new SectionType[] { SectionType.Start, SectionType.Finish };
        Track track = new("testTrack", sections, 100);
        var speed = 51;

        var participants = new List<IParticipant>()
        {
            new Driver("testDriver1", new Car(100, 100, speed), TeamColor.Green),
            new Driver("testDriver2", new Car(100, 100, speed), TeamColor.Grey),
            new Driver("testDriver3", new Car(100, 100, speed), TeamColor.Red)
        };

        track.PlaceParticipantsOnStart(participants);

        Assert.True(track.HasActiveParticipants());
    }

    [Fact]
    public void HasActiveParticipants_ShouldReturnFalse()
    {
        var sections = new SectionType[] { SectionType.Start, SectionType.Finish };
        Track track = new("testTrack", sections, 100);

        Assert.False(track.HasActiveParticipants());
    }

    [Fact]
    public void DetectDirectionChange_ShouldReturnNewDirection()
    {
        var direction = Direction.Up;

        direction = Track.DetectDirectionChange(SectionType.LeftCorner, direction);

        Assert.Equal(Direction.Left, direction);

        direction = Track.DetectDirectionChange(SectionType.RightCorner, direction);

        Assert.Equal(Direction.Up, direction);

        direction = Track.DetectDirectionChange(SectionType.RightCorner, direction);
        direction = Track.DetectDirectionChange(SectionType.RightCorner, direction);

        Assert.Equal(Direction.Down, direction);
    }
}
