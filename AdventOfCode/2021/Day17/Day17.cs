using System;
using System.Linq;
using AdventOfCode.Instrumentation;

namespace AdventOfCode._2021.Day17
{
    public class Day17
    {
        private int _velocityX;
        private int _velocityY;

        private readonly int _maxY;
        private readonly int _targetsHit;

        public Day17()
        {
            var targetArea = Helpers.ReadInputArray<string>("2021/Day17/Input.txt").Single();
            //var targetArea = Helpers.ReadInputArray<string>("2021/Day17/ExampleInput.txt").Single();
            var coordinates = targetArea.Replace("target area: ", string.Empty).Split(',');
            var xValues = coordinates[0].Replace("x=", string.Empty).Split("..");
            var yValues = coordinates[1].Replace("y=", string.Empty).Split("..");
            (int min, int max) xLimits = (int.Parse(xValues[0]), int.Parse(xValues[1]));
            (int min, int max) yLimits = (int.Parse(yValues[0]), int.Parse(yValues[1]));

            for (int y = yLimits.min; y < 1000; y++)
            {
                for (int x = 0; x <= xLimits.max; x++)
                {
                    _velocityX = x;
                    _velocityY = y;
                    var maxY = 0;

                    Coordinate2D probePosition = new(0, 0);

                    while (true)
                    {
                        if (probePosition.y > maxY)
                        {
                            maxY = probePosition.y;
                        }

                        if (probePosition.x >= xLimits.min && probePosition.x <= xLimits.max &&
                            probePosition.y >= yLimits.min && probePosition.y <= yLimits.max)
                        {
                            _targetsHit++;
                            if (maxY > _maxY)
                            {
                                _maxY = maxY;
                            }
                            break;
                        }

                        if (probePosition.x > xLimits.max || probePosition.y < yLimits.min)
                        {
                            break;
                        }
                        probePosition = Step(probePosition, _velocityX, _velocityY);
                    }
                }
            }
        }

        public void Part1()
        {
            Console.WriteLine(_maxY);
        }

        public void Part2()
        {

            Console.WriteLine(_targetsHit);
        }

        public Coordinate2D Step(Coordinate2D position, int velocityX, int velocityY)
        {
            var positionX = position.x + velocityX;
            var positionY = position.y + velocityY;
            if (_velocityX > 0)
            {
                _velocityX--;
            }
            else if (_velocityX < 0)
            {
                _velocityX++;
            }

            _velocityY--;

            var newPosition = new Coordinate2D(positionX, positionY);
            return newPosition;

        }
    }
}
