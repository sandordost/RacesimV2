using RaceSimulatorShared.Models.Equipments;

namespace RaceSimulatorTests.RaceSimulatorSharedTests;

public class IEquipmentTests
{
    [Fact]
    public void Car_TryBreak_ShouldReturnTrue()
    {
        var car = new Car(5, 1, 1);
        int breakAttepts = 100;

        for (int i = 0; i < breakAttepts; i++)
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
        var car = new Car(1, 5, 1);

        int breakAttepts = 200;
        for (int i = 0; i < breakAttepts; i++)
        {
            car.TryBreak();
        }

        Assert.True(car.IsBroken);

        int repairAttempts = 200;

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
        var car = new Car(1, 100, 1);
        var breakAttempts = 100;

        for (int i = 0; i < breakAttempts; i++)
        {
            car.TryBreak();
        }

        var repairAttempts = 100;
        for (int i = 0; i < repairAttempts; i++)
        {
            car.TryRepair();
        }

        Assert.True(car.Damage == 0);
    }

    [Fact]
    public void Car_PropertiesAreRandomized()
    {
        var car = new Car();

        Assert.True(car.Quality > 0 && car.Quality <= 100);
        Assert.True(car.Performance > 0 && car.Performance <= 100);
        Assert.True(car.Speed > 0 && car.Speed <= 100);
    }

    [Fact]
    public void Car_PropertiesAreNotRandomized()
    {
        var car = new Car(5, 5, 5);

        Assert.True(car.Quality == 5);
        Assert.True(car.Performance == 5);
        Assert.True(car.Speed == 5);
    }

    [Fact]
    public void Car_PropertiesCannotBeAbove100()
    {
        var car = new Car(101, 101, 101);

        Assert.True(car.Quality <= 100);
        Assert.True(car.Performance <= 100);
        Assert.True(car.Speed <= 100);
    }
}
