namespace RaceSimulatorShared.Models.Equipments;

public class Car : IEquipment
{
    private readonly int minProperty = 30;
    private readonly int maxProperty = 100;
    private readonly Random random = new();

    public int Quality { get; set; }
    public int Performance { get; set; }
    public int Speed { get; set; }
    public bool IsBroken { get; set; } = false;
    public int Damage { get; set; } = 0;

    /// <summary>
    /// Car properties are randomized between 1 and 100.
    /// </summary>
    public Car()
    {
        Random r = new();
        Quality = r.Next(minProperty, maxProperty);
        Performance = r.Next(minProperty, maxProperty);
        Speed = r.Next(minProperty, maxProperty);
    }

    /// <summary>
    /// Default property value of 0 will be randomized between 1 and 100.
    /// </summary>
    public Car(int quality, int performance, int speed)
    {
        Random r = new();

        quality = quality > maxProperty ? maxProperty : quality;
        performance = performance > maxProperty ? maxProperty : performance;
        speed = speed > maxProperty ? maxProperty : speed;

        Quality = quality == 0 ? r.Next(minProperty, maxProperty) : quality;
        Performance = performance == 0 ? r.Next(minProperty, maxProperty) : performance;
        Speed = speed == 0 ? r.Next(minProperty, maxProperty) : speed;
    }

    public bool TryBreak()
    {
        // Random chance to break the car. (Refined by fiddeling with the numbers)
        if (random.Next(-maxProperty * 10, maxProperty) > Quality && !IsBroken)
        {
            IsBroken = true;
            Damage = random.Next(1, maxProperty * 10);
            return true;
        }
        return false;
    }

    public bool TryRepair()
    {
        if (IsBroken)
        {
            Damage -= 10 + random.Next(0, Math.Clamp(Performance - 10, 0, maxProperty));

            if (Damage <= 0)
            {
                IsBroken = false;
                Damage = 0;
                return true;
            }
        }

        return false;
    }
}
