using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace DungeonPrototype
{
    class MapLayer
    {

        public const int W = 32;
        int[,] map;

        public MapLayer(string mapFile)
        {
            var lines = File.ReadAllLines(mapFile);
            for (var y = 0; y < lines.Length; y++)
            {
                var line = lines[y].Trim().ToCharArray();

                if (map == null)
                    map = new int[line.Length, lines.Length];

                for (var x = 0; x < line.Length; x++)
                    map[x, y] = int.Parse(line[x].ToString());

            }
        }

        public int getWidth()
        {
            return map.GetLength(0);
        }

        public int getHeight()
        {
            return map.GetLength(1);
        }

        public bool IsWall(int x, int y)
        {
            var tileX = (int)Math.Floor((float)x / (float)W);
            var tileY = (int)Math.Floor((float)y / (float)W);

            if(tileX < 0 || tileY < 0 || tileX >= getWidth() || tileY >= getHeight())
            {
                return false;
            }

            var tileType = map[tileX, tileY];
            return tileType == 1 || tileType == 2 || tileType == 3 || tileType == 5; 
        }

        public bool IsStairUp(int x, int y)
        {
            var tileX = (int)Math.Floor((float)x / (float)W);
            var tileY = (int)Math.Floor((float)y / (float)W);

            if (tileX < 0 || tileY < 0 || tileX >= getWidth() || tileY >= getHeight())
            {
                return false;
            }

            var tileType = map[tileX, tileY];
            return tileType == 8;
        }

        public bool IsStairDown(int x, int y)
        {
            var tileX = (int)Math.Floor((float)x / (float)W);
            var tileY = (int)Math.Floor((float)y / (float)W);

            if (tileX < 0 || tileY < 0 || tileX >= getWidth() || tileY >= getHeight())
            {
                return false;
            }

            var tileType = map[tileX, tileY];
            return tileType == 9;
        }

        public void Draw(SpriteBatch sb, Texture2D sheet)
        {
            for (var x = 0; x < getWidth(); x++)
            {
                for (var y = 0; y < getHeight(); y++)
                {
                    var sheetPos = map[x, y];
                    if (sheetPos == 8)
                        sheetPos = 3;
                    else if (sheetPos == 9)
                        sheetPos = 0;
                    sb.Draw(sheet, new Rectangle(x * W, y * W, W, W), new Rectangle(sheetPos * W, 0, W, W), Color.White);
                }
            }
        }
    }
}
