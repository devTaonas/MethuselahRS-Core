using MethuselahRS.Memory.Testing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethuselahRS.Memory.Reading
{
    internal class Traversal
    {
        //internal static async Task<bool> MapWalker2(int x, int y)
        //{
        //    Player player = new Player();
        //    var sens = (1000 * Math.Pow(175, -1));
        //    float cosAngle = 0.0f;
        //    float sinAngle = 0.0f;
        //    while (!await player.PInArea(x, 8, y, 8, 0))
        //    {
        //        var pc = await player.GetPlayerCoords();
        //        Point currxy = new Point((int)pc.x, (int)pc.y);
        //        int loops = 0;
        //        var math1 = Math.Pow(x - currxy.X, 2);
        //        var math2 = Math.Pow(y - currxy.Y, 2);
        //        float dist = (float)Math.Sqrt(math1 + math2);
        //        var theta = Math.Atan2((x - currxy.X), (y - currxy.Y));
        //        if (dist > 15.0f)
        //        {
        //            dist = 15.0f;
        //        }
        //        var item1 = currxy.X + (dist * Math.Sin(theta));
        //        var item2 = currxy.Y + (dist * Math.Cos(theta));
        //        Point ItemXY = new Point((int)item1, (int)item2);

        //        await ClickMapTile_2(ItemXY);

        //        await Task.Delay(1000);
        //        await RotateCamera(ItemXY, currxy, sens);
        //        int CamRandRot = RandomGener2(1, 5);

        //        while (!await player.PInArea(ItemXY.X, 3, ItemXY.Y, 3, 0))
        //        {
        //            await Task.Delay(RandomGener2(1000, 2000));
        //            loops++;
        //            if (loops == CamRandRot)
        //            {
        //                await RotateCamera(ItemXY, currxy, sens);
        //            }
        //            if (loops > 5)
        //            {
        //                break;
        //            }
        //        }
                
        //        if (await player.PInArea(x, 5, y, 5, 0))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        internal static async Task<bool> RotateCamera(Point ItemXY, Point currxy, double sens)
        {
            var pi = Math.Atan(1) * 4;
            var itemTheta = (Math.Atan2((ItemXY.X - currxy.X), (ItemXY.Y - currxy.Y)) * 180) / pi;

            TheMess.Compass2 = await TheMess.SearchCompass22();
            TheMess.VMatrix = new float[18];
            for (ulong i = 0; i < 2; i++)
            {
                float buffer = 0;
                buffer = BitConverter.ToSingle(MemoryReading.Read<float>(TheMess.Compass2 + 0x0 + (i * 32)), 0);
                TheMess.VMatrix[i + 16] = buffer;
            }
            var cosAngle = TheMess.VMatrix[16];
            var sinAngle = TheMess.VMatrix[17];
            var currTheta = (Math.Atan2(cosAngle, sinAngle) * 180) / pi;

            currTheta = currTheta + 360 + 90 - 360;

            var deltaTheta = (itemTheta - currTheta + 540) - 360;
            if (deltaTheta > 180)
            {
                deltaTheta = deltaTheta - 360;
            }
            var ms = Math.Round(sens * deltaTheta);
            if (deltaTheta < -20)
            {
                await Keyboard.sendKey("D", (int)-ms);
            }
            else if (deltaTheta > 20)
            {
                await Keyboard.sendKey("A", (int)ms);
            }
            return true;
        }
        //internal static async Task<bool> ClickMapTile_2(Point ItemCoord2)
        //{
        //    Point ItemCoord;
        //    Player pp = new Player();
        //    var coords = await pp.GetPlayerCoords();
        //    Point pl = new Point((int)coords.x, (int)coords.y);
        //    Point center = new Point(0, 0);
        //    Point endcalc = new Point(0, 0);
        //    InterfaceComp p;
        //    float sx = 0.0f;
        //    float sy = 0.0f;
        //    float cosAngle;
        //    float sinAngle;

        //    ItemCoord = new Point(ItemCoord2.X * 512, ItemCoord2.Y * 512);
        //    if (ItemCoord.X > 0 && ItemCoord.Y > 0 && pl.X > 0 && pl.Y > 0 && TheMess.Compass2 > 0)
        //    {
        //        if (Interfaces.InterfCheck(TheMess.MapBoxMemoryLoc, TheMess.MAPBOXID0).Result)
        //        {
        //            p = Interfaces.GetInterfaceData(TheMess.MapBoxMemoryLoc);
        //            if (p.xys.X > 0 && p.xys.Y > 0)
        //            {
        //                // ReadVieWMatrix(false);
        //                TheMess.Compass2 = await TheMess.SearchCompass22();
        //                TheMess.VMatrix = new float[18];
        //                for (ulong i = 0; i < 2; i++)
        //                {
        //                    float buffer = 0;
        //                    buffer = BitConverter.ToSingle(MemoryReading.Read<float>(TheMess.Compass2 + 0x0 + (i * 32)), 0);
        //                    TheMess.VMatrix[i + 16] = buffer;
        //                }

        //                cosAngle = TheMess.VMatrix[16] - 1.0f;
        //                sinAngle = TheMess.VMatrix[17] * -1.0f;

        //                //get map centre
        //                float x = (p.xy.X + p.xys.X / 2.0f) - 0.65f;
        //                float y = (p.xy.Y + p.xys.Y / 2.0f) + 5.3f;
        //                center = new Point((int)x, (int)y);

        //                //diffrence between been player and object in tiles
        //                int xx = pl.X - ItemCoord.X;
        //                int yy = pl.Y - ItemCoord.Y;

        //                //calculates pixels for map
        //                float aaaa = xx * 0.00791f;
        //                endcalc.X = (int)aaaa;
        //                float bbbb = yy * 0.00791f;
        //                endcalc.Y = (int)bbbb;

        //                //rotates map
        //                sx = center.X - (endcalc.X + ((cosAngle * endcalc.X) - (sinAngle * endcalc.Y)));
        //                sy = center.Y + (endcalc.Y + ((sinAngle * endcalc.X) + (cosAngle * endcalc.Y)));

        //                if (sx > p.xy.X && sy > p.xy.Y && sx < (p.xy.X + p.xys.X) && sy < (p.xy.Y + p.xys.Y))
        //                {
        //                    float sx1 = sx + 2;
        //                    float sy1 = sy - 2;
        //                    Mouse.MouseMove((int)sx1, (int)sy1);
        //                    await Task.Delay(400);
        //          //          Mouse.LeftClick((int)sx1, (int)sy1);
        //                    return true;
        //                }
        //                else
        //                {
        //                }
        //            }
        //        }
        //        else
        //        {
        //            TheMess.MapBoxMemoryLoc = Interfaces.LocateInterfaceTest33(1, TheMess.MAPBOXID0, true).Result;
        //        }
        //    }
            
        //    return false;
            
        //}
        internal static int RandomGener2(int min, int max)
        {
            if (min >= 0 && max > min)
            {
                Random rand = new Random();
                return rand.Next(min, max);
            }
            return 1;
        }
            
    }
}
