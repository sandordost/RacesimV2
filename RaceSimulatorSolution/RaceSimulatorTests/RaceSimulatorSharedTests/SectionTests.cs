﻿using RaceSimulatorShared.Models.Equipments;
using RaceSimulatorShared.Models.Participants;
using RaceSimulatorShared.Models.Tracks.Sections;

namespace RaceSimulatorTests.RaceSimulatorSharedTests;

public class SectionTests
{
    [Fact]
    public void MoveParticipant_ShouldReturnRemainingMovementAmount()
    {
        int maxSectionProgression = 132;
        int movementAmount = 79;

        Section section = new(SectionType.Straight, Direction.Right, maxSectionProgression);
        IParticipant participant = new Driver("testDriver", new Car(100, 100, 10), TeamColor.Green);

        Assert.True(section.ParticipantSectionProgressions.Count == 0);
        section.PlaceParticipant(participant);

        Assert.True(section.MoveParticipant(participant, movementAmount) == maxSectionProgression - movementAmount);
        Assert.True(section.ParticipantSectionProgressions.Count == 1);
        var nextSectionProgression = section.MoveParticipant(participant, movementAmount);
        Assert.True(nextSectionProgression < 0); // Participant exists and remaining movement amount is 0 or less.
        Assert.True(section.ParticipantSectionProgressions.Count == 0); // Participant should be removed from the section.
    }

    [Fact]
    public void PlaceParticpant_ShouldAddParticipantToSection()
    {
        Section section = new(SectionType.Straight, Direction.Right, 132);
        IParticipant participant = new Driver("testDriver", new Car(10, 10, 10), TeamColor.Green);

        Assert.True(section.ParticipantSectionProgressions.Count == 0);
        section.PlaceParticipant(participant);
        Assert.True(section.ParticipantSectionProgressions.Count == 1);
    }
}
