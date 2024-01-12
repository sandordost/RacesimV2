namespace RaceSimulatorShared.Models.Competitions.Equipments
{
    internal class Car(int quality, int performance, int speed) : IEquipment
    {
        public int Quality { get; set; } = quality;
        public int Performance { get; set; } = performance;
        public int Speed { get; set; } = speed;
        public bool IsBroken { get; set; } = false;
        public int Damage { get; set; } = 0;

        private readonly Random random = new();

        public bool TryBreak()
        {
            if (random.Next(0, 100) > Quality && !IsBroken)
            {
                IsBroken = true;
                Damage = random.Next(0, 100);
                return true;
            }
            return false;
        }

        public bool TryRepair()
        {
            if (IsBroken)
            {
                Damage -= 10 + random.Next(0, Performance);
                
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
}
