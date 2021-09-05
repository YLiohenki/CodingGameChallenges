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
class Player2
{
    static void Main2(string[] args)
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

        var centered = false;
        var inited = false;
        var landingX = 0d;
        var landXLeft = 0d;
        var landXRight = 0d;
        var landingY = 0d;
        var goLeft = true;
        var highestPoint = 0d;
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

            var foundPoint = 0;
            var i = 1;
            if (!inited)
            {
                foundPoint = 0;
                for (; i < surfaceN; ++i)
                {
                    if (landY[i - 1] == landY[i])
                    {
                        landXLeft = landX[i - 1] * 0.95 + landX[i] * 0.05;
                        landXRight = landX[i - 1] * 0.05 + landX[i] * 0.95;
                        if (foundPoint == 0)
                        {
                            goLeft = true;
                            landingX = landXRight;
                        }
                        else
                        {
                            goLeft = false;
                            landingX = landXLeft;
                        }
                        landingY = landY[i];
                        foundPoint++;
                    }
                    if (X < landX[i] && landX[i - 1] < X)
                    {
                        foundPoint++;
                    }
                    if (foundPoint == 1)
                    {
                        highestPoint = Math.Max(highestPoint, landY[i]);
                    }
                }
                inited = true;
            }

            foundPoint = 0;
            highestPoint = 0;
            for (; i < surfaceN; ++i)
            {
                if (landY[i - 1] == landY[i])
                {
                    foundPoint++;
                }
                if (X < landX[i] && landX[i - 1] < X)
                {
                    foundPoint++;
                }
                if (foundPoint == 1)
                {
                    highestPoint = Math.Max(highestPoint, landY[i]);
                }
            }

            if (X < landXLeft)
            {
                goLeft = false;
                landingX = landXLeft;
            }
            if (X > landXRight)
            {
                goLeft = true;
                landingX = landXRight;
            }

            var leanAngle = 25;
            if (!centered && (Math.Abs(hSpeed) > 3 || X < landXLeft - 5 || X > landXRight + 5))
            {
                if (highestPoint + 50 > Y)
                {
                    Console.WriteLine("0 4");
                    continue;
                }

                var turnX = X + 0d;
                var turnXSpeed = hSpeed;
                var turnRotate = rotate;
                for (int turn = 0; turn < int.MaxValue; ++turn)
                {
                    if (turn != 0)
                    {
                        turnRotate = goLeft ? Math.Max(turnRotate - 15, -leanAngle) : Math.Min(turnRotate + 15, leanAngle);
                    }
                    turnXSpeed = turnXSpeed - Math.Sin(Math.PI * turnRotate / 360d) * 4d;
                    turnX = turnX + turnXSpeed;
                    if (turnX <= 0 || turnX >= 7000)
                    {
                        Console.WriteLine("{0} {1}", goLeft ? leanAngle : -leanAngle, 4);
                        break;
                    }
                    if (goLeft ? turnX < landingX : turnX > landingX)
                    {
                        if ((goLeft && turnXSpeed < 0) || (!goLeft && turnXSpeed > 0))
                        {
                            Console.WriteLine("{0} {1}", goLeft ? Math.Max(rotate - 15, -leanAngle) : Math.Min(rotate + 15, leanAngle), 4);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("{0} {1}", goLeft ? leanAngle : -leanAngle, 4);
                            break;
                        }
                    }
                }
                continue;
            }

            centered = true;
            var turnPower = power;
            var turnSpeed = vSpeed;
            var height = Y - landingY;
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