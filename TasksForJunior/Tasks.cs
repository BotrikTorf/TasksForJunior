using System;


namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            /*// First Task Variables
            const ushort MinLife = 0;

            bool bulletHit = false;
            byte amountOfLife = byte.MaxValue;
            sbyte maxMana = sbyte.MaxValue;
            short minMana = 0;
            int manaAvailable = 100;
            uint thingsInBackpack;
            float cardLength;
            char potionIndicator = 'H';
            string unitNames = "Drunk Tree";*/

            sbyte userPictures = 52;
            sbyte picturesInRow = 3;
            sbyte numberOfRows;
            numberOfRows = Convert.ToSByte(userPictures / picturesInRow);
            sbyte cortinasBeyondMeasure = Convert.ToSByte(userPictures % picturesInRow);
            Console.WriteLine("Чтобы повесить {0} картины по {1} в каждом ряду " +
                "необходимо {2} рядов. Количество картин которые не поместились {3}" +
                " для полного заполнения ряда",
                userPictures, picturesInRow, numberOfRows, cortinasBeyondMeasure);
        }
    }
}
