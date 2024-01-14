// See https://aka.ms/new-console-template for more information
using static System.Net.Mime.MediaTypeNames;
using System.Threading;
using System.Diagnostics.Metrics;
using System.ComponentModel;



class Program {

    static Char movement_direction = 's';

    static async Task Main(string[] args) {

        //game type
        int space_size = 10;
        int speed = 500;
        String[,] space = new String[space_size, space_size];
        int hor_start = 4;
        int ver_start = 4;
        int hor_end = 4;
        int ver_end = 4;
        bool game = true;

        //game data
        Random rnd = new Random();
        int rnd1, rnd2;
        bool apple = false;
        bool apple_present = false;
        int apple_count = 0;

        //fill board
        for (int i = 0; i < space_size; i++)
        {
            for (int j = 0; j < space_size; j++)
            {
                space[i, j] = ". ";
            }
        }

        //listen for key
        Task.Run(async () => await CheckKeyPress());

        //game start
        while (game==true)
        {

            //apple
            if (!apple)
            {
                rnd1 = rnd.Next(space_size);
                rnd2 = rnd.Next(space_size);

                space[rnd1, rnd2] = "x ";

                apple = true;
            }
            //apple present?
            if (space[ver_start, hor_start] == "x ")
            {
                apple_present = true;
                apple_count++;
                apple = false;
                if (speed > 30) {
                    speed = speed - 20;
                }
            }

            //snake head
            space[ver_start, hor_start] = "o ";

            //board
            for (int i = 0; i < space_size; i++)
            {
                for (int j = 0; j < space_size; j++)
                {
                    Console.Write(space[i, j]);
                }
                Console.WriteLine(' ');
            }

            //delete last head
            if (apple_present) {
                space[ver_end, hor_end] = ". ";
                // apple_present = false;
            }
            else
            {
                space[ver_start, hor_start] = ". ";
            }

            ver_end = ver_start;
            hor_end = hor_start;

            switch (movement_direction)
            {
                case 'w':
                    ver_start--;
                    break;
                case 's':
                    ver_start++;
                    break;
                case 'a':
                    hor_start--;
                    break;
                case 'd':
                    hor_start++;
                    break;

                default:
                    break;
            }

            if ((hor_start >= space_size)|| (ver_start >= space_size) || (hor_start < 0) || (ver_start < 0))
            {
                game = false;
                Over(apple_count);
            }

            Thread.Sleep(speed);
            Console.Clear();
        }
    }

    static void Over(int score){
        Console.Clear();
        Console.WriteLine("game over");
        Console.WriteLine("score: " + score);
        Thread.Sleep(2000);
    }

    static async Task CheckKeyPress()
    {
        while (true)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                HandleKeyPress(keyInfo);
            }
            //await Task.Delay(10);
        }
    }

    static void HandleKeyPress(ConsoleKeyInfo keyInfo)
    {
        //Console.WriteLine($"Key pressed: {keyInfo.KeyChar}");
        switch (keyInfo.KeyChar)
        {
            case 'w':
                movement_direction = 'w';
                break;
            case 'a':
                movement_direction = 'a';
                break;
            case 's':
                movement_direction = 's';
                break;
            case 'd':
                movement_direction = 'd';
                break;

            default:
                break;
        }

    }
}

