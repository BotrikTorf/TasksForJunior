using System;
using System.Collections.Generic;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            int minSquadSize = 5;
            

            Console.WriteLine("Добро пожаловать в программу МОДЕЛИРОВАНИЕ ВОЙНЫ." +
                "Будут созданы два отряда с одинаковым количествой войнов." +
                "В каждом отряде будет свой командир, он дает плюс ко всем пораметрам остальных байцов и наносит урон." +
                $"Размер отрядов: минимальный {minSquadSize} а максимальный не ограничен." +
                $"");

        }
    }

    class Soldier
    {
        public Soldier(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }

    class Sniper : Soldier
    {
        public Sniper() : base("Sniper")
        {

        }
    }

    class MachineGunner : Soldier
    {
        public MachineGunner() : base("MachineGunner")
        {

        }
    }

    class Gunner : Soldier
    {

        public Gunner() : base("Gunner")
        {

        }
    }

    class StormTrooper : Soldier
    {
        public StormTrooper() : base("StormTrooper")
        {

        }
    }

    class Commander : Soldier
    {
        public Commander() : base("Commander")
        {

        }
    }

}
