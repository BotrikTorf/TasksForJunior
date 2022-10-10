using System;


namespace TasksForJunior
{
    class Tasks
    {
        class Character
        {
            private int _life;
            private int _manna;
            private int _weight;

            public Character() 
            {
                _life = 0;
                _manna = 0;
                _weight = 0;
            }

            public Character(int life, int manna, int weight)
            {
                if (life > 0)
                    _life = life;
                else
                    _life = 0;

                if (manna > 0)
                    _manna = life;
                else 
                    _manna = 0;

                if (weight > 0)
                    _weight = weight;
                else
                    _weight = 0;
            }

            public void SetsLife(int life)
            {
                if (life > 0)
                    _life = life;
            }

            public void SetsManna(int manna)
            {
                if (manna > 0)
                    _manna = manna;
            }

            public void SetsWeight(int weight)
            {
                if (weight > 0)
                    _weight = weight;
            }

            public void ShowParams()
            {
                Console.WriteLine($"У персонажа {_life} очков жизни, {_manna} очков манны, его вес {_weight}.");
            }
        }

        static void Main()
        {
            Character character = new Character();
            character.ShowParams();
            character = new Character(100, 150, 80);
            character.ShowParams();
            character.SetsLife(90);
            character.ShowParams();
            character.SetsManna(-10);
            character.ShowParams();
            character.SetsWeight(90);
            character.ShowParams();
        }
    }
}
