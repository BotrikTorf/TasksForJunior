using System;
using System.Collections.Generic;
using System.Linq;


namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {

        }
    }

    class FoodManna
    {
        int _amountManna;
        string _name;

        public FoodManna(int amountManna, string name)
        {
            _amountManna = amountManna;
            _name = name;
        }

        public int AmountManna { get { return _amountManna; } private set { } }
        public string Name { get { return _name; } private set { } }
    }

    class FoodHP
    {
        int _amountHP;
        string _name;

        public FoodHP(int amountHP, string name)
        {
            _amountHP = amountHP;
            _name = name;
        }

        public int AmountHP { get { return _amountHP; } private set { } }
        public string Name { get { return _name; } private set { } }
    }

    class Weapon
    {
        private int _maxDamage;
        private int _minDamage;
        private string _weaponType;

        public Weapon(int maxDamage, int minDamage, string weaponType)
        {
            _minDamage = minDamage;
            _maxDamage = maxDamage;
            _weaponType = weaponType;
        }

        public int Maxdamage { get { return _maxDamage; } private set { } }
        public int Mindamage { get { return _minDamage; } private set { } }
        public string WeaponType { get { return _weaponType; } private set { } }
    }

    class Player
    {

    }

    class Salesman
    {
        private List<Weapon> _weapons;
        private List<FoodManna> _foodsManna;
        private List<FoodHP> _foodsHP;

        public Salesman()
        {
            _weapons = new List<Weapon>();

            string[] oneHandedWeapon = { "OneHandedSword", "OneHandedAxe", "OneHandedHammer" };
            string[] twoHandedWeapon = { "TwoHandedSword", "TwoHandedAxe", "TwoHandedHammer" };
            Random random = new Random();

            for (int i = 0; i < oneHandedWeapon.Length; i++)
            {
                _weapons.Add(new Weapon(random.Next(1, 3), random.Next(3, 6), oneHandedWeapon[i]));
            }

            for (int i = 0; i < twoHandedWeapon.Length; i++)
            {
                _weapons.Add(new Weapon(random.Next(10, 13), random.Next(13, 16), twoHandedWeapon[i]));
            }

            _foodsHP = new List<FoodHP>();

            string[] foodsNameHP = { "Apple", "Pear", "Bread" };

            for (int i = 0; i < foodsNameHP.Length; i++)
            {
                _foodsHP.Add(new FoodHP(i * 10, foodsNameHP[i]));
            }

            _foodsManna = new List<FoodManna>();

            string[] foodsNameManna = { "Water", "Milk", "Tea" };

            for (int i = 0; i < foodsNameManna.Length; i++)
            {
                _foodsManna.Add(new FoodManna(i * 10, foodsNameManna[i]));
            }
        }
    }


    class Bargain
    {

    }
}
