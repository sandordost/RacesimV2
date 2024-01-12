﻿using RaceSimulatorShared.Models.Competitions.Equipments;
using RaceSimulatorShared.Models.Competitions.Participants;
using RaceSimulatorShared.Models.Competitions.Tracks.Sections;

namespace RaceSimulatorTests
{
    public class SectionTests
    {
        [Fact]
        public void MoveParticipant_ShouldReturnRemainingMovementAmount()
        {
            int maxSectionProgression = 132;
            int movementAmount = 79;

            Section section = new(SectionType.Straight, maxSectionProgression);
            IParticipant participant = new Driver("testDriver", new Car(10,10,10), TeamColor.Green);

            Assert.True(section.ParticipantSectionProgressions.Count == 0);
            Assert.True(section.MoveParticipant(participant, movementAmount) == maxSectionProgression - movementAmount);
            Assert.True(section.ParticipantSectionProgressions.Count == 1);
            Assert.True(section.MoveParticipant(participant, movementAmount) <= 0); // Participant exists and remaining movement amount is 0 or less.
            Assert.True(section.ParticipantSectionProgressions.Count == 0); // Participant should be removed from the section.
        }
    }
}