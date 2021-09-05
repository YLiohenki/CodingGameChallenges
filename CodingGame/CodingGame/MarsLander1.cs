using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Player1
{
    static void Main1(string[] args)
    {
        var gravity = 3.711;
        var maxYAcc = 4 - gravity;
        string[] inputs;
        int surfaceN = int.Parse(Console.ReadLine()); // the number of points used to draw the surface of Mars.
        Console.Error.WriteLine("{0}", surfaceN);
        var landX = new int[surfaceN];
        var landY = new int[surfaceN];
        for (int i = 0; i < surfaceN; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            landX[i] = int.Parse(inputs[0]); // X coordinate of a surface point. (0 to 6999)
            landY[i] = int.Parse(inputs[1]); // Y coordinate of a surface point. By linking all the points together in a sequential fashion, you form the surface of Mars.
            Console.Error.WriteLine("{0} {1}", landX[i], landY[i]);
        }

        // game loop
        while (true)
        {
            inputs = Console.ReadLine().Split(' ');
            int X = int.Parse(inputs[0]);
            int Y = int.Parse(inputs[1]);
            double hSpeed = int.Parse(inputs[2]); // the horizontal speed (in m/s), can be negative.
            double vSpeed = int.Parse(inputs[3]); // the vertical speed (in m/s), can be negative.
            double fuel = int.Parse(inputs[4]); // the quantity of remaining fuel in liters.
            int rotate = int.Parse(inputs[5]); // the rotation angle in degrees (-90 to 90).
            int power = int.Parse(inputs[6]); // the thrust power (0 to 4).

            Console.Error.WriteLine("{0} {1} {2} {3} {4} {5} {6}", X, Y, hSpeed, vSpeed, fuel, rotate, power);

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");

            var turnPower = power;
            var turnSpeed = vSpeed;
            var i = 0;
            for (; X <= landX[i]; ++i)
            { 
            }

            var height = Y - landY[i];
            double turnHeight = height;
            for (int turn = 0; turn < int.MaxValue; ++turn)
            {
                turnPower = turn != 0 ? Math.Min(4, turnPower + 1) : turnPower;
                turnSpeed = turnSpeed + turnPower - gravity;
                turnHeight = turnHeight + turnSpeed;
                if (turnHeight <= 0)
                {
                    if (turnSpeed > -20)
                    {
                        Console.WriteLine("0 {0}", power);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("0 {0}", Math.Min(4, power + 1));
                        break;
                    }
                }
                if (turnSpeed > 0)
                {
                    Console.WriteLine("0 0");
                    break;
                }
            }
        }
    }
}