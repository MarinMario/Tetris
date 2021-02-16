using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Tetris
{
    static class Tetromino
    {
        readonly static public string[][] L = new string[][] {
            new string[] {
                ".X.",
                ".X.",
                ".XX"
            },
            new string[] {
                "...",
                "XXX",
                "X.."
            },
            new string[] {
                "XX.",
                ".X.",
                ".X."
            },
            new string[] {
                "..X",
                "XXX",
                "..."
            }
        };
        
        readonly static public string[][] O = new string[][] {
            new string[] {
                "XX",
                "XX"
            },
        };

        readonly static public string[][] S = new string[][] {
            new string[] {
                ".X.",
                ".XX",
                "..X"
            },
            new string[] {
                "...",
                ".XX",
                "XX."
            },
            new string[] {
                "X..",
                "XX.",
                ".X."
            },
            new string[] {
                ".XX",
                "XX.",
                "..."
            }
        };

        static public List<Vector2> WallKick(int fromRotation, int toRotation)
        {
            var toReturn = new (int, int)[]{};
            switch((fromRotation, toRotation)) {
                case (0, 1):
                    toReturn = new (int, int)[] {(0, 0), (-1, 0), (-1, 1), (0,-2), (-1,-2)};
                    break;
                case (1, 0):
                    toReturn = new (int, int)[] {(0, 0), ( 1, 0), ( 1,-1), (0, 2), ( 1, 2)};
                    break;
                case (1, 2):
                    toReturn = new (int, int)[] {(0, 0), ( 1, 0), ( 1,-1), (0, 2), ( 1, 2)};
                    break;
                case (2, 1):
                    toReturn = new (int, int)[] {(0, 0), (-1, 0), (-1, 1), (0,-2), (-1,-2)};
                    break;
                case (2, 3):
                    toReturn = new (int, int)[] {(0, 0), ( 1, 0), ( 1, 1), (0,-2), ( 1,-2)};
                    break;
                case (3, 2):
                    toReturn = new (int, int)[] {(0, 0), (-1, 0), (-1,-1), (0, 2), (-1, 2)};
                    break;
                case (3, 0):
                    toReturn = new (int, int)[] {(0, 0), (-1, 0), (-1,-1), (0, 2), (-1, 2)};
                    break;
                case (0, 3):
                    toReturn = new (int, int)[] {(0, 0), ( 1, 0), ( 1, 1), (0,-2), ( 1,-2)};
                    break;
            }

            var toReturnConverted = new List<Vector2>{};
            foreach(var elem in toReturn)
                toReturnConverted.Add(new Vector2(elem.Item1, elem.Item2));
            return toReturnConverted;
        }

    }
}