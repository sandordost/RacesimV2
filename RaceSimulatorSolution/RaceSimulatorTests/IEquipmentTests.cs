using RaceSimulatorShared.Models.Competitions.Equipments;

namespace RaceSimulatorTests
{
    public class IEquipmentTests
    {
        [Fact]
        public void Car_TryBreak_ShouldReturnTrue()
        {
            var car = new Car(5, 0, 0);
            int breakAttepts = 100;

            for(int i = 0; i < breakAttepts; i++)
            {
                if (car.TryBreak())
                {
                    Assert.True(car.IsBroken);
                    break;
                }
            }
        }

        [Fact]
        public void Car_TryRepair_ShouldReturnTrue()
        {
            var car = new Car(0, 5, 0);

            car.TryBreak();
            Assert.True(car.IsBroken);

            int repairAttempts = 100;

            for (int i = 0; i < repairAttempts; i++)
            {
                if (car.TryRepair())
                {
                    Assert.True(!car.IsBroken);
                    break;
                }
            }
        }

        [Fact]
        public void Car_Damage_ShouldBeZeroAfterRepair()
        {
            var car = new Car(0, 10000, 0);
            car.TryBreak();
            car.TryRepair();

            Assert.True(car.Damage == 0);
        }
    }
}
