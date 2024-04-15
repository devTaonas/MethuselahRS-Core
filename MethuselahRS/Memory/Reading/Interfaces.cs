using MethuselahRS.Memory.Testing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace MethuselahRS.Memory.Reading
{
    internal class Interfaces
    {
        public static List<InterfaceComp5test> I_Madness;

        public static ulong gen_active1 = 0x8;           
        public static ulong gen_active2 = 0xc;

        public static ulong I_offback = 0x30;
        public static ulong I_ids = 0x28;
        public static ulong I_itemids = 0x198;
        public static ulong I_itemids2 = 0x190; 
        public static ulong I_itemids3 = 0x178;
        public static ulong I_itemids4 = 0x1c0;
        public static ulong I_00textP = 0x108;
        public static ulong I_text2 = 0x108 + 0x20;
        public static ulong I_itemstack = 0x198 + 0x8;//I_itemids + 0x8
        public static ulong I_textP = 0x90;
        public static ulong I_pixels = 0x204;
        public static ulong I_x0 = 0x58;
        public static ulong I_y0 = 0x58 + 0x4;
        public static ulong I_x3 = 0x70;
        public static ulong I_y3 = 0x74;
        public static ulong I_x4 = 0x78;
        public static ulong I_y4 = 0x7c;
        public static ulong mem_use_area = 0x2FFFFFFFF;
        public static bool I_bounds = true;
        public static ulong Interface_Address_start_pointer = 0x0;

        public static List<IInfo> InventoryList = new List<IInfo>();
        public static IInfo Pointer_InvD = new IInfo();
        public static IInfo Pointer_SideText = new IInfo();
        public static IInfo Pointer_MapFrame = new IInfo();

        public static List<IInfo> BankWArr2 = new List<IInfo>();
        public static List<IInfo> BankWInv2 = new List<IInfo>();

        public static List<string> PublicChat = new List<string>();


        // NEED TO FIND COMPASS


        public static async Task Map_Walker1(Point tilexy, int distance)
        {
            bool distanceokx = false;
            bool distanceoky = false;
            Player p = new Player();
            var xy = p.GetPlayerCoords();
            Point StorePlayerLoc = new Point((int)xy.Result.x, (int)xy.Result.y);
            if (Math.Abs(StorePlayerLoc.X - tilexy.X) < distance * 512)
            {
                distanceokx = true;
            }
            else
            {
                distanceokx = false;
            }
            if (Math.Abs(StorePlayerLoc.Y - tilexy.Y) < distance * 512)
            {
                distanceoky = true;
            }
            else
            {
                distanceoky = false;
            }

            if (distanceokx && distanceoky)
            {
                Point StorePlayerLocnew = new Point(0, 0);
                bool firstrun = false;
                for (int i = 0; i < 25; i++)
                {
                    var xy2 = p.GetPlayerCoords();
                    StorePlayerLocnew = new Point((int)xy2.Result.x, (int)xy2.Result.y);

                    Point click_tilexy = await Bresenham_step(tilexy);
                    if (click_tilexy.X > 0 && click_tilexy.Y > 0)
                    {
                        Mouse.MouseMove(click_tilexy.X, click_tilexy.Y, 0, 0);
                        await Task.Delay(new Random().Next(250, 600));
                        
                        await Mouse.leftClick(click_tilexy.X, click_tilexy.Y);
                        await Task.Delay(500);


                        //cautionary measures v1
                        if (Math.Abs(StorePlayerLoc.X - StorePlayerLocnew.X) > 50 * 512 ||
                            Math.Abs(StorePlayerLoc.Y - StorePlayerLocnew.Y) > 50 * 512)
                        {
                            break;
                        }

                        //cautionary measures v2
                        if (!firstrun)
                        {
                            firstrun = true;
                        }
                        else
                        {
                            if (Math.Abs(StorePlayerLoc.X - StorePlayerLocnew.X) > 1024 ||
                                Math.Abs(StorePlayerLoc.Y - StorePlayerLocnew.Y) > 1024)
                            {
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    //Getting close to target, time to stop
                    if (Math.Abs(StorePlayerLocnew.X - tilexy.X) < 7 || Math.Abs(StorePlayerLocnew.Y - tilexy.Y) < 7)
                    {
                        break;
                    }
                }
            }
            else 
            {
                MessageBox.Show("Map_Walker:distance too far");
            }
            await Task.Delay(500);
            
        }
        public static async Task<Point> Bresenham_step(Point tilexy)
        {
            Random random = new Random();
            IInfo p = Get_Check_Interface(Pointer_MapFrame, GetIbystringstatic("Map 1"), true);
            if (p != null)
            {
                if (p.memloc > 0)
                {
                    if (p.x > 0 && p.y > 0)
                    {

                        //give direction
                        Point xy2 = ToMapFFPOINT(tilexy);
                        if (xy2.X == 0 && xy2.Y == 0) { return new Point(0, 0); }

                        //center
                        double xd = (p.x + p.xs / 2) - 0.65;
                        double yd = (p.y + p.ys / 2) + 5.3;
                        int x = (int)xd;
                        int y = (int)yd;

                        //make a line between 2 points
                        List<Point> line_calc = MathBresenhamLine(x, y, xy2.X, xy2.Y);

                        //sort, as soon it is over step size but smaller than map
                        Point click_tile = new Point(0, 0);
                        int xstart = p.x;
                        int xsize = p.x + p.xs;
                        int ystart = p.y;
                        int ysize = p.y + p.ys;
                        int cornerxs = 0;
                        int cornerys = 0;
                        int cutcorners = 75;//bigger = cut more corners

                        if (line_calc.Count() > 0)
                        {

                            //find cutting point
                            for (int i = 0; i < line_calc.Count(); i++)
                            {

                                //lower corners
                                cornerxs = 0;
                                if (line_calc[i].Y + cutcorners > ysize)
                                {
                                    cornerxs = line_calc[i].Y - ysize + cutcorners;
                                }

                                //upper edge corners
                                cornerys = 0;
                                if (line_calc[i].Y - cutcorners - 17 < ystart)
                                {
                                    cornerys = line_calc[i].Y - ystart - cutcorners - 17;
                                }

                                if (line_calc[i].X > xsize - 8 - cornerxs + cornerys - random.Next(1, 22) - 1//right edge
                                    || line_calc[i].Y > ysize - 8 - random.Next(1, 22) - 1//bottom
                                    || line_calc[i].X < xstart + 8 + cornerxs - cornerys + random.Next(1, 22) - 1//left edge
                                    || line_calc[i].Y < ystart + 18 + random.Next(1, 22) - 1)
                                {
                                    click_tile = line_calc[i];
                                    //steps
                                    //   click_tile.z = i; //dont need z
                                    break;
                                }
                            }

                            //line was not generated to the edge, pick last
                            if (click_tile.X == 0 && click_tile.Y == 0)
                            {
                                click_tile = line_calc[line_calc.Count() - 1];
                                click_tile.X -= random.Next(1, 5) - random.Next(1, 5);
                                click_tile.Y -= random.Next(1, 5) - random.Next(1, 5);
                                //use as boolean, last step
                                //std::cout << "bres not at edge" << std::endl;
                            }

                            if (click_tile.X > p.x + 11 && click_tile.Y > p.y + 22 && click_tile.X < (p.x + p.xs - 11) && click_tile.Y < (p.y + p.ys - 10))
                            {
                                return click_tile;
                            }
                            //else {
                            //std::cout << "bres limit hit" << std::endl;
                            //}
                        }
                    }
                }
            }
            return new Point(0, 0);
        }


        public static Point ToMapFFPOINT(Point ItemCoord, bool map_limit = true)
        {
            Player p = new Player();
            var coords = p.GetPlayerCoords();
            Point pl = new Point((int)coords.Result.x, (int)coords.Result.y);

            if (ItemCoord.X > 0 && ItemCoord.Y > 0 && pl.X > 0 && pl.X > 0 && TheMess.Compass2 > 0)
            {
                var mapstatic = GetIbystringstatic("Map 1");
                if(mapstatic == null) { return new Point(0,0); }
                Pointer_MapFrame = Get_Check_Interface(Pointer_MapFrame, mapstatic, true);
                if (Pointer_MapFrame.x > 0 && Pointer_MapFrame.y > 0 && Pointer_MapFrame.memloc > 0)
                {

                    ReadVieWMatrix(false);
                    //correction for my mess
                    float cosAngle = TheMess.VMatrix[16] - 1.0f;
                    //reverse
                    float sinAngle = TheMess.VMatrix[17] * -1.0f;

                    float center_x = (float)(Pointer_MapFrame.x) + (float)(Pointer_MapFrame.xs) / 2.0f;
                    float center_y = (float)(Pointer_MapFrame.y) + (float)(Pointer_MapFrame.ys) / 2.0f;

                    float endcalc1 = (pl.X - ItemCoord.X) * 0.00791f;
                    float endcalc2 = (pl.Y - ItemCoord.Y) * 0.00791f;
                    Point endcalc = new Point((int)endcalc1 , (int)endcalc2);

                    float sx = center_x - (endcalc.X + ((cosAngle * endcalc.X) - (sinAngle * endcalc.Y)));
                    float sy = center_y + (endcalc.Y + ((sinAngle * endcalc.X) + (cosAngle * endcalc.Y)));

                    //compare that is within map
                    if (map_limit)
                    {
                        if (Pointer_MapFrame.x < sx && Pointer_MapFrame.x + Pointer_MapFrame.xs > sx)
                        {
                            if (Pointer_MapFrame.y < sy && Pointer_MapFrame.y + Pointer_MapFrame.ys > sy)
                            {
                                return new Point((int)sx, (int)sy);
                            }
                        }
                        return new Point(0, 0);
                    }
                    return new Point((int)sx, (int)sy);
                }
            }
            return new Point(0, 0);
        }
        public static List<InterfaceComp5> GetIbystringstatic(string text)
        {
            foreach(var get in TheMess.KnownInterfaces)
            {
                if (get.Name == text)
                {
                    List<InterfaceComp5> newlist = new List<InterfaceComp5>();
                    foreach (var item in get.IDstatics)
                    {
                        InterfaceComp5 if5 = new InterfaceComp5(item.id1,item.id2,item.id3,item.id4,item.memloc);
                        newlist.Add(if5);
                    }
                    return newlist;
                }
            }
            return null;
        }
        public static List<Point> MathBresenhamLine(int x1, int y1, int x2, int y2)
        {
            List<Point> endResults = new List<Point>();
            int x, y;
            int dx, dy;
            int incx, incy;
            int balance;

            if (x2 >= x1)
            {
                dx = x2 - x1;
                incx = 1;
            }
            else
            {
                dx = x1 - x2;
                incx = -1;
            }

            if (y2 >= y1)
            {
                dy = y2 - y1;
                incy = 1;
            }
            else
            {
                dy = y1 - y2;
                incy = -1;
            }

            x = x1;
            y = y1;

            if (dx >= dy)
            {
                dy <<= 1;
                balance = dy - dx;
                dx <<= 1;

                while (x != x2) // Assuming Endall condition is handled elsewhere
                {
                    endResults.Add(new Point(x, y));
                    if (balance >= 0)
                    {
                        y += incy;
                        balance -= dx;
                    }
                    balance += dy;
                    x += incx;
                }
                endResults.Add(new Point(x, y));
            }
            else
            {
                dx <<= 1;
                balance = dx - dy;
                dy <<= 1;

                while (y != y2) // Assuming Endall condition is handled elsewhere
                {
                    endResults.Add(new Point(x, y));
                    if (balance >= 0)
                    {
                        x += incx;
                        balance -= dy;
                    }
                    balance += dx;
                    y += incy;
                }
                endResults.Add(new Point(x, y));
            }

            return endResults;
        }
        public static bool ReadVieWMatrix(bool readwhole)
        {
            TheMess.VMatrix = new float[18];
            if (TheMess.Compass2 > 0)
            {
                if (readwhole)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        //TheMess.VMatrix[i] = Mem_read<float>(TheMess.Compass2 + TheMess.CompassSpot22 + ((ulong)(i) * 4));
                        TheMess.VMatrix[i] = BitConverter.ToSingle(MemoryReading.Read<float>(TheMess.Compass2 + TheMess.CompassSpot22 + ((ulong)(i) * 4)), 0);
                    }
                }
                else
                {
                    for (int i = 0; i < 2; i++)
                    {
                        //TheMess.VMatrix[i + 16] = Mem_read<float>(TheMess.Compass2 + TheMess.Compass2cosAngle2 + ((ulong)(i) * 32));
                        TheMess.VMatrix[i + 16] = BitConverter.ToSingle(MemoryReading.Read<float>(TheMess.Compass2 + TheMess.Compass2cosAngle2 + ((ulong)(i) * 32)), 0);
                    }
                }
                return true;
            }
            return false;
        }

        public static string FindMouseOverText()
        {
            var MENU_side = new List<InterfaceComp5>(new[]
            {
                new InterfaceComp5(1477, 871, 65535, 65535, 0),
                new InterfaceComp5(1477, 874, 65535, 871, 0),
                new InterfaceComp5(1477, 876, 65535, 874, 0),
                new InterfaceComp5(1477, 876,  0, 876, 0)
            });
            string s = "";
            if (TheMess.LocalPlayer != null)
            {
                IInfo aim = Get_Check_Interface(Pointer_SideText, MENU_side);
                if (aim != null)
                {
                    if (aim.memloc > 0)
                    {
                        if (Readint(aim.memloc + 0x78) != 0xFFFFFFF6)
                        {
                            var textaddress = BitConverter.ToUInt64(MemoryReading.Read<long>((aim.memloc + I_itemids3)), 0);
                            string returntext = ReadCharsPointer(textaddress);
                            returntext = StringFilter(returntext);
                            return returntext;
                        }
                    }
                }
            }
            return s;
        }

        public static List<IInfo> ScanForInterfaceTest2Get(bool target_under, List<InterfaceComp5> lv_ID)
        {
            if (target_under)
            {
                return GetInterfaceByIdsUnder(lv_ID, 0, true);
            }
            return GetInterfaceByIds(lv_ID, true);
        }
        public static List<IInfo> GetInterfaceByIds(List<InterfaceComp5> target, bool I_text)
        {
            if (target.Count() > 0)
            {
                List<IInfo> f_bulk = ScanForInterfaceTest223(target[0].id1, true, false, I_text, target);
                if (f_bulk.Count() == 1)
                {
                    InterfaceComp5 OP = GetIMadnessOP(new InterfaceComp5(target[0].id1, target[0].id2 ,target[0].id3 ,target[0].id4 , 0 ));
                    if (OP != null)
                    {
                        f_bulk[0].x += OP.id1;
                        f_bulk[0].y += OP.id2;
                        return f_bulk;
                    }
                }
            }
            return null;
        }
        public static List<IInfo> GetInterfaceByIdsUnder(List<InterfaceComp5> target, int offset, bool I_text)
        {
            List<IInfo> f_bulk = ScanForInterfaceTest223(target[0].id1, true, false, false, target);
            if (f_bulk.Count == 1)
            {
                InterfaceComp5 OP = GetIMadnessOP(new InterfaceComp5(target[0].id1, target[0].id2 ,target[0].id3 ,target[0].id4 , 0));
                List<IInfo> stack = new List<IInfo>();
                if (offset == 0)
                {
                    List<ulong> Addresses = GatherIUM(f_bulk[0].memloc);
                    foreach (ulong A in Addresses)  // Assuming 'Addresses' is an IEnumerable<ulong>
                    {
                        var item = ReadForInterfaceTest223(A, f_bulk[0].x, f_bulk[0].y, f_bulk[0].box_x, f_bulk[0].box_y, f_bulk[0].index + 1, I_text); 
                        item.x += OP.id1;
                        item.y += OP.id2;
                        stack.Add(item);
                    }
                }
                else
                {
                    List<ulong> Addresses = GatherIU(f_bulk[0].memloc, offset);
                    foreach (ulong A in Addresses)
                    {
                        var item = ReadForInterfaceTest223(A, f_bulk[0].x, f_bulk[0].y, f_bulk[0].box_x, f_bulk[0].box_y, f_bulk[0].index + 1, I_text);  
                        item.x += OP.id1;
                        item.y += OP.id2;
                        stack.Add(item);
                    }
                }
                return stack;
            }
            return null;
        }
        public static InterfaceComp5 GetIMadnessOP(InterfaceComp5 mad)
        {
            TheMess.KnownInterfaces.Clear();
            LoadKnownInterfacesIn();
            for (int i = 0; i < TheMess.KnownInterfaces.Count(); i++)
            {
                InterfaceComp3test item = TheMess.KnownInterfaces[i];
                if (item.IDdynamic != null)
                {
                    if (item.IDdynamic.id1 == mad.id1 && item.IDdynamic.id2 == mad.id2 && item.IDdynamic.id3 == mad.id3 && item.IDdynamic.id4 == mad.id4)
                    {
                        if (item.IDstatics.Count == 0)
                        {
                            return new InterfaceComp5(0, 0, -1, 0, 0);
                        }
                        List<InterfaceComp5> IDStatic = new List<InterfaceComp5>();
                        foreach (var st in item.IDstatics)
                        {
                            InterfaceComp5 icp5 = new InterfaceComp5(st.id1,st.id2,st.id3,st.id4,st.memloc);
                            IDStatic.Add(icp5);
                        }
                        List<IInfo> StaticBox = ScanForInterfaceTest223(item.IDstatics[0].id1, true, false, true, IDStatic);
                        if (StaticBox.Count > 0)
                        {
                            var firstStaticBox = StaticBox[0];
                            return new InterfaceComp5(firstStaticBox.x, firstStaticBox.y, i, 0, firstStaticBox.memloc);
                        }
                    }
                }
            }
            return new InterfaceComp5(0, 0, -1, 0, 0); ;
        }
        public static InterfaceComp5 GetIbystring(string name) 
        {
            foreach (InterfaceComp3test InterfaceComp in TheMess.KnownInterfaces)
            {
                if (InterfaceComp.Name == name)
                {
                    InterfaceComp5 ii = new InterfaceComp5(InterfaceComp.IDdynamic.id1,InterfaceComp.IDdynamic.id2, InterfaceComp.IDdynamic.id3, InterfaceComp.IDdynamic.id4, InterfaceComp.IDdynamic.memloc);
                    return ii;
                }
            }
            return null;
        }
        public static List<ulong> GatherIUM(ulong inputaddr)
        {
            if (inputaddr > 0)
            {
                List<ulong> newaddr = new List<ulong>();
                ulong start = 0;
                ulong end = 0;

                start = ReadUINT64(inputaddr + I_itemids2) + 0x8;
                end = ReadUINT64(inputaddr + I_itemids2 + 0x8) + 0x8;
                if (Mem_rangeWexe(start))
                {
                    if (Mem_rangeWexe(end))
                    {
                        if ((end - start) / 0x18 > 0 && (end - start) / 0x18 < 10000)
                        {
                            for (ulong i2 = start; i2 < end; i2 = i2 + 0x18)
                            {
                                if (Mem_rangeWexe(i2))
                                {
                                    ulong endval = ReadUINT64(i2);
                                    if (!Mem_rangeWexe(endval))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        ulong diff = i2 > endval ? i2 - endval : endval - i2;

                                        if (diff > 0x3000)
                                        {
                                            if (ReadINT16(endval + I_ids) < 2000)
                                            {
                                                newaddr.Add(endval);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                start = ReadUINT64(inputaddr + I_itemids3) + 0x8;
                end = ReadUINT64(inputaddr + I_itemids3 + 0x8) + 0x8;
                if (Mem_rangeWexe(start))
                {
                    if (Mem_rangeWexe(end))
                    {
                        if ((end - start) / 0x18 > 0 && (end - start) / 0x18 < 10000)
                        {
                            for (ulong i2 = start; i2 < end; i2 = i2 + 0x18)
                            {
                                if (Mem_rangeWexe(i2))
                                {
                                    ulong endval = ReadUINT64(i2);
                                    if (!Mem_rangeWexe(endval))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        ulong diff = i2 > endval ? i2 - endval : endval - i2;

                                        if (diff > 0x3000)
                                        {
                                            if (ReadINT16(endval + I_ids) < 2000)
                                            {
                                                newaddr.Add(endval);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                start = ReadUINT64(inputaddr + I_itemids4) + 0x8;
                end = ReadUINT64(inputaddr + I_itemids4 + 0x8) + 0x8;
                if (Mem_rangeWexe(start))
                {
                    if (Mem_rangeWexe(end))
                    {
                        if ((end - start) / 0x18 > 0 && (end - start) / 0x18 < 10000)
                        {
                            for (ulong i2 = start; i2 < end; i2 = i2 + 0x18)
                            {
                                if (Mem_rangeWexe(i2))
                                {
                                    ulong endval = ReadUINT64(i2);
                                    if (!Mem_rangeWexe(endval))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        ulong diff = i2 > endval ? i2 - endval : endval - i2;

                                        if (diff > 0x3000)
                                        {
                                            if (ReadINT16(endval + I_ids) < 2000)
                                            {
                                                newaddr.Add(endval);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return newaddr;
            }
            return null;
        }
        public static int Readint(ulong v)
        {
            return BitConverter.ToInt16(MemoryReading.Read<int>(v), 0);
        }
        public static int ReadINT16(ulong v)
        {
            return BitConverter.ToUInt16(MemoryReading.Read<int>(v), 0);
        }

        public static ulong ReadUINT64(ulong v)
        {
            return BitConverter.ToUInt64(MemoryReading.Read<long>(v), 0);
        }
        public static string ReadCharsPointer(ulong v)
        {
            return MemoryReading.ReadString(v);
        }
        public static bool Mem_rangeWexe(ulong Address)
        {
            if (TheMess.ScAdd1 == 0)
            {
                if (Address > 0x80000 && Address < (ulong)TheMess.RSExeStart)
                {//anything goes
                    return true;
                }
            }
            else
            {
                if (Address > 0x100000 && Address < (ulong)TheMess.RSExeStart)
                {
                    if (((ulong)TheMess.ScAdd1 - Address > 0 && (ulong)TheMess.ScAdd1 - Address < mem_use_area) || (Address - (ulong)TheMess.ScAdd1 > 0 && Address - (ulong)TheMess.ScAdd1 < mem_use_area))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static List<ulong> GatherIU(ulong inputaddr, int offset)
        {
            if (inputaddr > 0)
            {
                List<ulong> newaddr = new List<ulong>();
                ulong start = ReadUINT64(inputaddr + (ulong)offset) + 0x8;
                ulong end = ReadUINT64(inputaddr + (ulong)offset + 0x8) + 0x8;
                if (Mem_rangeWexe(start))
                {
                    if (Mem_rangeWexe(end))
                    {
                        if ((end - start) / 0x18 > 0 && (end - start) / 0x18 < 28000)
                        {
                            for (ulong i2 = start; i2 < end; i2 = i2 + 0x18)
                            {
                                if (Mem_rangeWexe(i2))
                                {
                                    ulong endval = ReadUINT64(i2);
                                    if (!Mem_rangeWexe(endval))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        ulong diff = i2 > endval ? i2 - endval : endval - i2;

                                        if (diff > 0x3000)
                                        {
                                            if (ReadINT16(endval + I_ids) < 2000)
                                            {
                                                newaddr.Add(endval);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return newaddr;
            }
            return null;
        }
        public static List<InterfaceComp5> ScanForInterfaceTest3(int aim_target, bool getonlytarget, bool I_debug)
        {
            List<InterfaceComp5> StackTop = new List<InterfaceComp5>();
            if (Interface_Address_start_pointer > 0)
            {
                //update
                ulong Interface_Address_start2 = BitConverter.ToUInt64(MemoryReading.Read<long>(Interface_Address_start_pointer + 0x58), 0);
                ulong Interface_Address_end2 = BitConverter.ToUInt64(MemoryReading.Read<long>(Interface_Address_start_pointer + 0x58 + 0x8), 0);
                if (Interface_Address_start2 > 0 && Interface_Address_end2 > 0 && Interface_Address_end2 - Interface_Address_start2 < 0x10000 && Interface_Address_end2 > Interface_Address_start2)
                {
                    List<InterfaceComp5> stack = new List<InterfaceComp5>();
                    ulong AP1 = 0;
                    ulong AP2 = 0;
                    int ID = 0;
                    ulong IPStart = 0;
                    ulong IPEnd = 0;
                    ulong SPOT = 0;
                    for (ulong i = Interface_Address_start2; i < Interface_Address_end2; i += 0x8)
                    {
                        AP1 = ReadUINT64(i);
                        if (AP1 == 0) { continue; }
                        i += 0x8;
                        AP2 = ReadUINT64(i);
                        if (AP2 == 0) { continue; }
                        if (AP2 > AP1 && AP2 - AP1 < 0x100)
                        {
                            ID = ReadINT16(AP2);
                            if (aim_target > 0 && !I_debug)
                            {
                                if (getonlytarget && aim_target != ID) { continue; }
                                if (ID != 1477 && ID != 906 && aim_target != ID) { continue; }
                            }
                            IPStart = BitConverter.ToUInt64(MemoryReading.Read<long>(AP2 + 0x20), 0);
                            IPStart = IPStart + 0x8;
                            IPEnd = BitConverter.ToUInt64(MemoryReading.Read<long>(AP2 + 0x28), 0);
                            IPEnd = IPEnd + 0x8;
                            if (IPStart > 0x10 && IPEnd > 0x10 && IPEnd > IPStart && IPEnd - IPStart < 0x50000)
                            {
                                for (ulong ii = IPStart; ii < IPEnd; ii += 0x18)
                                {
                                    SPOT = BitConverter.ToUInt64(MemoryReading.Read<long>(ii), 0);
                                    if (SPOT > 0 && ID > 0)
                                    {
                                        if (ID == 1477 || ID == 906)
                                        {
                                            StackTop.Add(new InterfaceComp5 ( ID, ReadINT16(SPOT + I_ids + 2), ReadINT16(SPOT + I_ids + 4), ReadINT16(SPOT + I_ids + 6), SPOT));
                                        }
                                        else
                                        {
                                            var ID2 = ReadINT16(SPOT + I_ids + 0x2);
                                            var ID3 = ReadINT16(SPOT + I_ids + 0x4);
                                            var ID4 = ReadINT16(SPOT + I_ids + 0x6);
                                            stack.Add(new InterfaceComp5 ( ID,ID2,ID3,ID4, SPOT));
                                        }
                                    }
                                }
                            }
                        }
			        }
			        StackTop.AddRange(stack);
		        }
	        }
	        return StackTop;
        }
        public static List<IInfo> ScanForInterfaceTest223(int aim_target, bool getonlytarget, bool I_debug, bool I_text, List<InterfaceComp5> It)
        {
            if (TheMess.RS3Found)
            {
                List<IInfo> IStackSS = new List<IInfo>();
                int Isize = It.Count();
                int Isize2 = Isize - 1;
                List<InterfaceComp5> IStackTopIDs = ScanForInterfaceTest3(aim_target, getonlytarget, I_debug);
                if (IStackTopIDs.Count() > 0)
                {
                    for (int i = 0; i < IStackTopIDs.Count(); i++)
                    {
                        IInfo clevel0 = ReadForInterfaceTest223(IStackTopIDs[i].memloc, 0, 0, 0, 0, 0, I_text);
                        if (!I_debug && Isize > 0) 
                        { 
                            if (clevel0.id1 != It[0].id1 || clevel0.id2 != It[0].id2 || clevel0.id3 != It[0].id3 || clevel0.id4 != It[0].id4) 
                            { 
                                continue; 
                            }
                        }
                        if (aim_target > 0 && !I_debug) { if (aim_target != clevel0.id1) { continue; } }
                        if (clevel0.id1 < 1) { continue; }
                        if (I_debug) { IStackSS.Add(clevel0); };
                        if (!I_debug && Isize2 == 0)
                        {
                            if (clevel0.id1 == It[Isize2].id1 && clevel0.id2 == It[Isize2].id2 && clevel0.id3 == It[Isize2].id3 && clevel0.id4 == It[Isize2].id4)
                            {
                                return new List<IInfo> { clevel0 };
                            }
                        }
                        if (aim_target > 0 && I_debug) { if (aim_target != clevel0.id1) { continue; } }
                        if (aim_target == 0 && I_debug) { continue; }
                        List<ulong> nextSt1 = GatherIUM(IStackTopIDs[i].memloc);
                        for (int i1 = 0; i1 < nextSt1.Count(); i1++)
                        {
                            IInfo clevel1 = ReadForInterfaceTest223(nextSt1[i1], clevel0.x, clevel0.y, clevel0.box_x, clevel0.box_y, 1, I_text, clevel0.fullpath, clevel0.fullIDpath, clevel0.scroll_y);
                            if (clevel1.id1 < 1) { continue; }
                            if (!I_debug && Isize > 1) { if (clevel1.id1 != It[1].id1 || clevel1.id2 != It[1].id2 || clevel1.id3 != It[1].id3 || clevel1.id4 != It[1].id4) { continue; } }
                            if (aim_target > 0) { if (aim_target != clevel1.id1) { continue; } }
                            if (I_debug) { IStackSS.Add(clevel1); };
                            if (!I_debug && Isize2 == 1) { if (clevel1.id1 == It[Isize2].id1 && clevel1.id2 == It[Isize2].id2 && clevel1.id3 == It[Isize2].id3 && clevel1.id4 == It[Isize2].id4) { return new List<IInfo> { clevel1 }; } }
                            List<ulong> nextSt2 = GatherIUM(nextSt1[i1]);
                            for (int i2 = 0; i2 < nextSt2.Count(); i2++)
                            {
                                IInfo clevel2 = ReadForInterfaceTest223(nextSt2[i2], clevel1.x, clevel1.y, clevel1.box_x, clevel1.box_y, 2, I_text, clevel1.fullpath, clevel1.fullIDpath, clevel1.scroll_y);
                                if (clevel2.id1 < 1) { continue; }
                                if (!I_debug && Isize > 2) { if (clevel2.id1 != It[2].id1 || clevel2.id2 != It[2].id2 || clevel2.id3 != It[2].id3 || clevel2.id4 != It[2].id4) { continue; } }
                                if (aim_target > 0) { if (aim_target != clevel2.id1) { continue; } }
                                if (I_debug) { IStackSS.Add(clevel2); };
                                if (!I_debug && Isize2 == 2) { if (clevel2.id1 == It[Isize2].id1 && clevel2.id2 == It[Isize2].id2 && clevel2.id3 == It[Isize2].id3 && clevel2.id4 == It[Isize2].id4) {  return new List<IInfo> { clevel2 }; } }
                                List<ulong> nextSt3 = GatherIUM(nextSt2[i2]);
                                for (int i3 = 0; i3 < nextSt3.Count(); i3++)
                                {
                                    IInfo clevel3 = ReadForInterfaceTest223(nextSt3[i3], clevel2.x, clevel2.y, clevel2.box_x, clevel2.box_y, 3, I_text, clevel2.fullpath, clevel2.fullIDpath, clevel2.scroll_y);
                                    if (clevel3.id1 < 1) { continue; }
                                    if (!I_debug && Isize > 3) { if (clevel3.id1 != It[3].id1 || clevel3.id2 != It[3].id2 || clevel3.id3 != It[3].id3 || clevel3.id4 != It[3].id4) { continue; } }
                                    if (aim_target > 0) { if (aim_target != clevel3.id1) { continue; } }
                                    if (I_debug) { IStackSS.Add(clevel3); };
                                    if (!I_debug && Isize2 == 3) { if (clevel3.id1 == It[Isize2].id1 && clevel3.id2 == It[Isize2].id2 && clevel3.id3 == It[Isize2].id3 && clevel3.id4 == It[Isize2].id4) { return new List<IInfo> { clevel3 }; } }
                                    List<ulong> nextSt4 = GatherIUM(nextSt3[i3]);
                                    for (int i4 = 0; i4 < nextSt4.Count(); i4++)
                                    {
                                        IInfo clevel4 = ReadForInterfaceTest223(nextSt4[i4], clevel3.x, clevel3.y, clevel3.box_x, clevel3.box_y, 4, I_text, clevel3.fullpath, clevel3.fullIDpath, clevel3.scroll_y);
                                        if (clevel4.id1 < 1) { continue; }
                                        if (!I_debug && Isize > 4) { if (clevel4.id1 != It[4].id1 || clevel4.id2 != It[4].id2 || clevel4.id3 != It[4].id3 || clevel4.id4 != It[4].id4) { continue; } }
                                        if (aim_target > 0) { if (aim_target != clevel4.id1) { continue; } }
                                        if (I_debug) { IStackSS.Add(clevel4); };
                                        if (!I_debug && Isize2 == 4) { if (clevel4.id1 == It[Isize2].id1 && clevel4.id2 == It[Isize2].id2 && clevel4.id3 == It[Isize2].id3 && clevel4.id4 == It[Isize2].id4) {  return new List<IInfo> { clevel4 }; } }
                                        List<ulong> nextSt5 = GatherIUM(nextSt4[i4]);
                                        for (int i5 = 0; i5 < nextSt5.Count(); i5++)
                                        {
                                            IInfo clevel5 = ReadForInterfaceTest223(nextSt5[i5], clevel4.x, clevel4.y, clevel4.box_x, clevel4.box_y, 5, I_text, clevel4.fullpath, clevel4.fullIDpath, clevel4.scroll_y);
                                            if (clevel5.id1 < 1) { continue; }
                                            if (!I_debug && Isize > 5) { if (clevel5.id1 != It[5].id1 || clevel5.id2 != It[5].id2 || clevel5.id3 != It[5].id3 || clevel5.id4 != It[5].id4) { continue; } }
                                            if (aim_target > 0) { if (aim_target != clevel5.id1) { continue; } }
                                            if (I_debug) { IStackSS.Add(clevel5); };
                                            if (!I_debug && Isize2 == 5) { if (clevel5.id1 == It[Isize2].id1 && clevel5.id2 == It[Isize2].id2 && clevel5.id3 == It[Isize2].id3 && clevel5.id4 == It[Isize2].id4) { return new List<IInfo> { clevel5 }; } }
                                            List<ulong> nextSt6 = GatherIUM(nextSt5[i5]);
                                            for (int i6 = 0; i6 < nextSt6.Count(); i6++)
                                            {
                                                IInfo clevel6 = ReadForInterfaceTest223(nextSt6[i6], clevel5.x, clevel5.y, clevel5.box_x, clevel5.box_y, 6, I_text, clevel5.fullpath, clevel5.fullIDpath, clevel5.scroll_y);
                                                if (clevel6.id1 < 1) { continue; }
                                                if (!I_debug && Isize > 6) { if (clevel6.id1 != It[6].id1 || clevel6.id2 != It[6].id2 || clevel6.id3 != It[6].id3 || clevel6.id4 != It[6].id4) { continue; } }
                                                if (aim_target > 0) { if (aim_target != clevel6.id1) { continue; } }
                                                if (I_debug) { IStackSS.Add(clevel6); };
                                                if (!I_debug && Isize2 == 6) { if (clevel6.id1 == It[Isize2].id1 && clevel6.id2 == It[Isize2].id2 && clevel6.id3 == It[Isize2].id3 && clevel6.id4 == It[Isize2].id4) { return new List<IInfo> { clevel6 }; } }
                                                List<ulong> nextSt7 = GatherIUM(nextSt6[i6]);
                                                for (int i7 = 0; i7 < nextSt7.Count(); i7++)
                                                {
                                                    IInfo clevel7 = ReadForInterfaceTest223(nextSt7[i7], clevel6.x, clevel6.y, clevel6.box_x, clevel6.box_y, 7, I_text, clevel6.fullpath, clevel6.fullIDpath, clevel6.scroll_y);
                                                    if (clevel7.id1 < 1) { continue; }
                                                    if (!I_debug && Isize > 7) { if (clevel7.id1 != It[7].id1 || clevel7.id2 != It[7].id2 || clevel7.id3 != It[7].id3 || clevel7.id4 != It[7].id4) { continue; } }
                                                    if (aim_target > 0) { if (aim_target != clevel7.id1) { continue; } }
                                                    if (I_debug) { IStackSS.Add(clevel7); };
                                                    if (!I_debug && Isize2 == 7) { if (clevel7.id1 == It[Isize2].id1 && clevel7.id2 == It[Isize2].id2 && clevel7.id3 == It[Isize2].id3 && clevel7.id4 == It[Isize2].id4) { return new List<IInfo> { clevel7 }; } }
                                                    List<ulong> nextSt8 = GatherIUM(nextSt7[i7]);
                                                    for (int i8 = 0; i8 < nextSt8.Count(); i8++)
                                                    {
                                                        IInfo clevel8 = ReadForInterfaceTest223(nextSt8[i8], clevel7.x, clevel7.y, clevel7.box_x, clevel7.box_y, 8, I_text, clevel7.fullpath, clevel7.fullIDpath, clevel7.scroll_y);
                                                        if (clevel8.id1 < 1) { continue; }
                                                        if (!I_debug && Isize > 8) { if (clevel8.id1 != It[8].id1 || clevel8.id2 != It[8].id2 || clevel8.id3 != It[8].id3 || clevel8.id4 != It[8].id4) { continue; } }
                                                        if (aim_target > 0) { if (aim_target != clevel8.id1) { continue; } }
                                                        if (I_debug) { IStackSS.Add(clevel8); };
                                                        if (!I_debug && Isize2 == 8) { if (clevel8.id1 == It[Isize2].id1 && clevel8.id2 == It[Isize2].id2 && clevel8.id3 == It[Isize2].id3 && clevel8.id4 == It[Isize2].id4) { return new List<IInfo> { clevel8 }; } }
                                                        List<ulong> nextSt9 = GatherIUM(nextSt8[i8]);
                                                        for (int i9 = 0; i9 < nextSt9.Count(); i9++)
                                                        {
                                                            IInfo clevel9 = ReadForInterfaceTest223(nextSt9[i9], clevel8.x, clevel8.y, clevel8.box_x, clevel8.box_y, 9, I_text, clevel8.fullpath, clevel8.fullIDpath, clevel8.scroll_y);
                                                            if (clevel9.id1 < 1) { continue; }
                                                            if (!I_debug && Isize > 4) { if (clevel9.id1 != It[9].id1 || clevel9.id2 != It[9].id2 || clevel9.id3 != It[9].id3 || clevel9.id4 != It[9].id4) { continue; } }
                                                            if (aim_target > 0) { if (aim_target != clevel9.id1) { continue; } }
                                                            if (I_debug) { IStackSS.Add(clevel9); };
                                                            if (!I_debug && Isize2 == 9) { if (clevel9.id1 == It[Isize2].id1 && clevel9.id2 == It[Isize2].id2 && clevel9.id3 == It[Isize2].id3 && clevel9.id4 == It[Isize2].id4) { return new List<IInfo> { clevel9 }; } }
                                                            List<ulong> nextSt10 = GatherIUM(nextSt9[i9]);
                                                            for (int i10 = 0; i10 < nextSt10.Count(); i10++)
                                                            {
                                                                IInfo clevel10 = ReadForInterfaceTest223(nextSt10[i10], clevel9.x, clevel9.y, clevel9.box_x, clevel9.box_y, 10, I_text, clevel9.fullpath, clevel9.fullIDpath, clevel9.scroll_y);
                                                                if (clevel10.id1 < 1) { continue; }
                                                                if (!I_debug && Isize > 10) { if (clevel10.id1 != It[10].id1 || clevel10.id2 != It[10].id2 || clevel10.id3 != It[10].id3 || clevel10.id4 != It[10].id4) { continue; } }
                                                                if (aim_target > 0) { if (aim_target != clevel10.id1) { continue; } }
                                                                if (I_debug) { IStackSS.Add(clevel10); };
                                                                if (!I_debug && Isize2 == 10) { if (clevel10.id1 == It[Isize2].id1 && clevel10.id2 == It[Isize2].id2 && clevel10.id3 == It[Isize2].id3 && clevel10.id4 == It[Isize2].id4) { return new List<IInfo> { clevel10 }; } }
                                                                List<ulong> nextSt11 = GatherIUM(nextSt10[i10]);
                                                                for (int i11 = 0; i11 < nextSt11.Count(); i11++)
                                                                {
                                                                    IInfo clevel11 = ReadForInterfaceTest223(nextSt11[i11], clevel10.x, clevel10.y, clevel10.box_x, clevel10.box_y, 11, I_text, clevel10.fullpath, clevel10.fullIDpath, clevel10.scroll_y);
                                                                    if (clevel11.id1 < 1) { continue; }
                                                                    if (!I_debug && Isize > 11) { if (clevel11.id1 != It[11].id1 || clevel11.id2 != It[11].id2 || clevel11.id3 != It[11].id3 || clevel11.id4 != It[11].id4) { continue; } }
                                                                    if (aim_target > 0) { if (aim_target != clevel11.id1) { continue; } }
                                                                    if (I_debug) { IStackSS.Add(clevel11); };
                                                                    if (!I_debug && Isize2 == 11) { if (clevel11.id1 == It[Isize2].id1 && clevel11.id2 == It[Isize2].id2 && clevel11.id3 == It[Isize2].id3 && clevel11.id4 == It[Isize2].id4) { return new List<IInfo> { clevel11 }; } }
                                                                    List<ulong> nextSt12 = GatherIUM(nextSt11[i11]);
                                                                    for (int i12 = 0; i12 < nextSt12.Count(); i12++)
                                                                    {
                                                                        IInfo clevel12 = ReadForInterfaceTest223(nextSt12[i12], clevel11.x, clevel11.y, clevel11.box_x, clevel11.box_y, 12, I_text, clevel11.fullpath, clevel11.fullIDpath, clevel11.scroll_y);
                                                                        if (clevel12.id1 < 1) { continue; }
                                                                        if (!I_debug && Isize > 12) { if (clevel12.id1 != It[12].id1 || clevel12.id2 != It[12].id2 || clevel12.id3 != It[12].id3 || clevel12.id4 != It[12].id4) { continue; } }
                                                                        if (aim_target > 0) { if (aim_target != clevel12.id1) { continue; } }
                                                                        if (I_debug) { IStackSS.Add(clevel12); };
                                                                        if (!I_debug && Isize2 == 12) { if (clevel12.id1 == It[Isize2].id1 && clevel12.id2 == It[Isize2].id2 && clevel12.id3 == It[Isize2].id3 && clevel12.id4 == It[Isize2].id4) { return new List<IInfo> { clevel12 }; } }
                                                                        List<ulong> nextSt13 = GatherIUM(nextSt12[i12]);
                                                                        for (int i13 = 0; i13 < nextSt13.Count(); i13++)
                                                                        {
                                                                            IInfo clevel13 = ReadForInterfaceTest223(nextSt13[i13], clevel12.x, clevel12.y, clevel12.box_x, clevel12.box_y, 13, I_text, clevel12.fullpath, clevel12.fullIDpath, clevel12.scroll_y);
                                                                            if (clevel13.id1 < 1) { continue; }
                                                                            if (!I_debug && Isize > 13) { if (clevel13.id1 != It[13].id1 || clevel13.id2 != It[13].id2 || clevel13.id3 != It[13].id3 || clevel13.id4 != It[13].id4) { continue; } }
                                                                            if (aim_target > 0) { if (aim_target != clevel13.id1) { continue; } }
                                                                            if (I_debug) { IStackSS.Add(clevel13); };
                                                                            if (!I_debug && Isize2 == 13) { if (clevel13.id1 == It[Isize2].id1 && clevel13.id2 == It[Isize2].id2 && clevel13.id3 == It[Isize2].id3 && clevel13.id4 == It[Isize2].id4) { return new List<IInfo> { clevel13 }; } }
                                                                            List<ulong> nextSt14 = GatherIUM(nextSt13[i13]);
                                                                            for (int i14 = 0; i14 < nextSt14.Count(); i14++)
                                                                            {
                                                                                IInfo clevel14 = ReadForInterfaceTest223(nextSt14[i14], clevel13.x, clevel13.y, clevel13.box_x, clevel13.box_y, 14, I_text, clevel13.fullpath, clevel13.fullIDpath, clevel13.scroll_y);
                                                                                if (clevel14.id1 < 1) { continue; }
                                                                                if (!I_debug && Isize > 14) { if (clevel14.id1 != It[14].id1 || clevel14.id2 != It[14].id2 || clevel14.id3 != It[14].id3 || clevel14.id4 != It[14].id4) { continue; } }
                                                                                if (aim_target > 0) { if (aim_target != clevel14.id1) { continue; } }
                                                                                if (I_debug) { IStackSS.Add(clevel14); };
                                                                                if (!I_debug && Isize2 == 14) { if (clevel14.id1 == It[Isize2].id1 && clevel14.id2 == It[Isize2].id2 && clevel14.id3 == It[Isize2].id3 && clevel14.id4 == It[Isize2].id4)  { return new List<IInfo> { clevel14 }; } }
                                                                                List<ulong> nextSt15 = GatherIUM(nextSt14[i14]);
                                                                                for (int i15 = 0; i15 < nextSt15.Count(); i15++)
                                                                                {
                                                                                    IInfo clevel15 = ReadForInterfaceTest223(nextSt15[i15], clevel14.x, clevel14.y, clevel14.box_x, clevel14.box_y, 15, I_text, clevel14.fullpath, clevel14.fullIDpath, clevel14.scroll_y);
                                                                                    if (clevel15.id1 < 1) { continue; }
                                                                                    if (!I_debug && Isize > 15) { if (clevel15.id1 != It[15].id1 || clevel15.id2 != It[15].id2 || clevel15.id3 != It[15].id3 || clevel15.id4 != It[15].id4) { continue; } }
                                                                                    if (aim_target > 0) { if (aim_target != clevel15.id1) { continue; } }
                                                                                    if (I_debug) { IStackSS.Add(clevel15); };
                                                                                    if (!I_debug && Isize2 == 15) { if (clevel15.id1 == It[Isize2].id1 && clevel15.id2 == It[Isize2].id2 && clevel15.id3 == It[Isize2].id3 && clevel15.id4 == It[Isize2].id4) { return new List<IInfo> { clevel15 }; } }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return IStackSS;
            }
            return null;
        }

        //Gather data
        public static IInfo ReadForInterfaceTest223(ulong mem, int prev_x = 0, int prev_y = 0, int prev_Box_x = 0, 
            int prev_Box_y = 0, int level = 0, bool readtext = false, string memstream = "", string IDSstream = "", int scroll_yyy = 0) 
        {

            IInfo nextSt = new IInfo();
            nextSt.index = level;
	        nextSt.memloc = mem;
	        nextSt.id1 = ReadINT16(mem + I_ids);
	        nextSt.id2 = ReadINT16(mem + I_ids + 2);
	        nextSt.id3 = ReadINT16(mem + I_ids + 4);
	        nextSt.id4 = ReadINT16(mem + I_ids + 6);
	        //
	        int Boxyt = 0;
                int Boxyb = 0;
                int lv_ytest = 0;
                int lv_ystest = 0;
                int lv_x = prev_x + Readint(mem + I_x3);
                int lv_xs = Readint(mem + I_x4);
                ulong lv2_y_scrollcheck = ReadUINT64(mem + I_pixels - 0x4);
                ulong lv2_y_scrolllimit = ReadUINT64(mem + I_pixels + 0x4);
	        if(((lv2_y_scrollcheck >> 32) & 0x0000ffff) > 0 || ((lv2_y_scrolllimit >> 32) & 0x0000ffff) > 0) {
		        if(((lv2_y_scrollcheck >> 32) & 0x0000ffff) < 10000 && ((lv2_y_scrolllimit >> 32) & 0x0000ffff) < 10000 &&
			        ((lv2_y_scrollcheck >> 0) & 0x0000ffff) == 0 && ((lv2_y_scrolllimit >> 0) & 0x0000ffff) == 0) {
			        Boxyt = prev_y + Readint(mem + I_y3);
			        Boxyb = Readint(mem + I_y4);
		        } else {
			        lv2_y_scrollcheck = 0;
		        }
	        } 
            else
            {
                lv2_y_scrollcheck = 0;
            }
            nextSt.box_x = prev_Box_x + Boxyt;
            nextSt.box_y = prev_Box_y + Boxyb;
            int lv_y = prev_y + Readint(mem + I_y3);
            int lv_ys = Readint(mem + I_y4);
            if (lv2_y_scrollcheck == 0 && (prev_Box_x != 0 || prev_Box_y != 0) && I_bounds)
            {
                int negativey1 = lv_y - prev_Box_x;
                int removey1 = 0;
                if (negativey1 < 0)
                {
                    removey1 = negativey1;
                }
                int negativey2 = prev_Box_x + prev_Box_y - lv_y - lv_ys;
                int removey2 = 0;
                if (negativey2 < 0)
                {
                    removey2 = negativey2;
                }
                lv_ytest = lv_y - removey1;
                lv_ystest = lv_ys + removey1 + removey2;
                //outside of bounding box
                if (lv_y + lv_ys <= prev_Box_x || lv_y >= prev_Box_x + prev_Box_y)
                {
                    nextSt.notvisible = true;
                }
            }
            else
            {
                lv_ytest = lv_y;
                lv_ystest = lv_ys;
            }
            nextSt.x = lv_x;
            nextSt.xs = lv_xs;
            nextSt.y = lv_ytest + scroll_yyy;//lv_ytest - scroll_yyy;
            nextSt.ys = lv_ystest;//lv_ystest;
            nextSt.scroll_y = scroll_yyy - (int)((lv2_y_scrollcheck >> 32) & 0x0000ffff);
                //
            if (readtext)
            {
                nextSt.itemid1 = Readint(mem + I_itemids);
                nextSt.itemid1_size = ReadUINT64(mem + I_itemstack);
                nextSt.itemid2 = Readint(mem + I_itemids2);
                //nextSt.hov = ReadINT8(mem + I2offhhh);
                var textaddress = BitConverter.ToUInt64(MemoryReading.Read<long>((ulong)mem + I_textP), 0);
                nextSt.textitem = StringFilter(ReadCharsPointer(textaddress));
                nextSt.textids = ReadCharsPointer(mem + I_itemids3);
            }
            //
            StringBuilder IDSstreamCopy = new StringBuilder();
            IDSstreamCopy.Append(IDSstream); // Assuming IDSstream is a string or compatible type
            IDSstreamCopy.AppendFormat(", {{ {0},{1},{2},{3},0 }}", nextSt.id1, nextSt.id2, nextSt.id3, nextSt.id4);
            nextSt.fullIDpath = IDSstreamCopy.ToString();

            StringBuilder memstreamCopy = new StringBuilder();
            memstreamCopy.Append(memstream); // Assuming memstream is a string or compatible type
            memstreamCopy.Append(" : ");
            memstreamCopy.AppendFormat("{0:X}", mem); // Formatting 'mem' as hex
            nextSt.fullpath = memstreamCopy.ToString();
            //
            return nextSt;
        }

        public static IInfo Get_Check_Interface(IInfo current_data, List<InterfaceComp5> search_components, bool refresh_data = true, bool refresh_text = true)
        {
            if (current_data != null)
            {
                if (current_data.memloc == 0)
                {
                    List<IInfo> aim = ScanForInterfaceTest2Get(false, search_components);
                    if (aim != null)
                    {
                        if (aim.Count() > 0)
                        {
                            InterfaceComp5 OP = GetIMadnessOP(search_components[0]);
                            aim[0].firstIF = new InterfaceComp5(search_components[0].id1, search_components[0].id2, search_components[0].id3, search_components[0].id4, OP.memloc);
                            current_data = aim[0];
                            GetInterfaceDataIInfo(current_data, refresh_data, refresh_text);
                            current_data.x += OP.id1;
                            current_data.y += OP.id2;
                            return current_data;
                        }
                    }
                }
                else
                {
                    //actives
                    int bb3 = Readint(current_data.memloc + gen_active1);
                    int bb4 = Readint(current_data.memloc + gen_active2);
                    //ids
                    int bb5 = ReadINT16(current_data.memloc + I_ids);
                    int bb7 = ReadINT16(current_data.memloc + I_ids + 2);
                    int bb8 = ReadINT16(current_data.memloc + I_ids + 4);
                    int bb9 = ReadINT16(current_data.memloc + I_ids + 6);
                    //pointers
                    ulong PPP1 = ReadUINT64(current_data.memloc + 0x10);
                    ulong PPP2 = ReadUINT64(current_data.memloc + I_offback);
                    int bb55 = ReadINT16(PPP2 + I_ids);
                    if (PPP1 == current_data.memloc && PPP1 > 0 && PPP2 > 0 && bb55 == current_data.id1)
                    {
                        if (bb3 > 0 && bb3 < 255)
                        {
                            if (bb4 > 0 && bb4 < 255)
                            {
                                if (bb5 == current_data.id1 && bb7 == current_data.id2 && bb8 == current_data.id3 && bb9 == current_data.id4)
                                {
                                    InterfaceComp5 OP = GetIMadnessOP(current_data.firstIF);
                                    GetInterfaceDataIInfo(current_data, refresh_data, refresh_data);
                                    current_data.x += OP.id1;
                                    current_data.y += OP.id2;
                                    return current_data;
                                }
                            }
                        }
                    }
                    //failed, try again
                    List<IInfo> aim = ScanForInterfaceTest2Get(false, search_components);
                    if (aim.Count() > 0)
                    {
                        InterfaceComp5 OP = GetIMadnessOP(search_components[0]);
                        aim[0].firstIF = new InterfaceComp5( search_components[0].id1,search_components[0].id2,search_components[0].id3,search_components[0].id4, OP.memloc );
                        current_data = aim[0];
                        GetInterfaceDataIInfo(current_data, refresh_data, refresh_text);
                        current_data.x += OP.id1;
                        current_data.y += OP.id2;
                        return current_data;
                    }
                }
                current_data = null;//nothing
                return current_data;
            }
            return null;
        }
        public static IInfo GetInterfaceDataIInfo(IInfo database, bool refresh_data, bool refresh_text)
        {
            if (database != null)
            {
                if (database.memloc > 0)
                {
                    if (refresh_data)
                    {
                        //reverse
                        List<ulong> p_list = new List<ulong>();
                        ulong pointer = database.memloc;
                        for (int i = 0; i < 20; i++)
                        {
                            p_list.Add(pointer);
                            pointer = ReadUINT64(pointer + I_offback);
                            if (pointer == 0 || (ulong)TheMess.RSExeStart < pointer) { break; }
                        }
                        p_list.Reverse();
                        database.x = 0;
                        database.y = 0;
                        foreach (ulong p in p_list)
                        {
                            int scroll = Readint(p + I_pixels);
                            if (scroll > 0 && scroll < 20001)
                            {
                                database.y -= scroll;
                            }
                            database.x += ReadINT16(p + I_x0);
                            database.y += ReadINT16(p + I_y0);
                            //database.xy = { database.x, database.y };
                        }
                        database.xs = ReadINT16(p_list[p_list.Count - 1] + I_x4);
                        database.ys = ReadINT16(p_list[p_list.Count - 1] + I_y4);
                        database.xy = new Point( database.x + database.xs / 2, database.y + database.ys / 2);
                        for (int i = 0; i < p_list.Count() - 1; i++)
                        {
                            if (ReadINT16(p_list[i] + I_y4) < database.y + database.ys / 2)
                            {
                                database.notvisible = true;
                            }
                        }
                        if (database.y < -database.ys / 2)
                        {
                            database.notvisible = true;
                        }
                        database.itemid1 = Readint(database.memloc + I_itemids);
                        database.itemid1_size = ReadUINT64(database.memloc + I_itemstack);
                    }
                    if (refresh_text)
                    {
                        database.textitem = InterfaceGetALLText(database.memloc);
                    }
                    return database;
                }
            }
            return null;
        }

        public static string InterfaceGetALLText(ulong baseadd)
        {
            string result = "";
            result = StringFilter2(ReadCharsPointer(baseadd + I_textP));
            if (result.Length == 0)
            {
                result = StringFilter2(ReadCharsPointer(baseadd + I_itemids2));
                if (result.Length == 0)
                {
                    result = StringFilter2(ReadCharsPointer(baseadd + I_itemids3));
                    if (result.Length == 0)
                    {
                        result = StringFilter2(ReadCharsPointer(baseadd + I_itemids4));
                    }
                }
            }
            return result;
        }

        internal static async Task LoadKnownInterfacesIn()
        {
            #region MAP1
            TheMess.I_Madness = new InterfaceComp3test();
            TheMess.I_Madness.Name = "Map 1";
            InterfaceComp3 IDdynamic = new InterfaceComp3();
            IDdynamic.id1 = 1465;
            IDdynamic.id2 = 0;
            IDdynamic.id3 = 65535;
            IDdynamic.id4 = 65535;
            IDdynamic.id5 = 0;
            TheMess.I_Madness.IDdynamic = IDdynamic;

            InterfaceComp3 IDstatics = new InterfaceComp3();
            TheMess.I_Madness.IDstatics = new List<InterfaceComp3>();

            IDstatics.id1 = 1477;
            IDstatics.id2 = 25;
            IDstatics.id3 = 65535;
            IDstatics.id4 = 65535;
            IDstatics.id5 = 0;
            TheMess.I_Madness.IDstatics.Add(IDstatics);

            InterfaceComp3 IDstatics2 = new InterfaceComp3();
            IDstatics2.id1 = 1477;
            IDstatics2.id2 = 58;
            IDstatics2.id3 = 65535;
            IDstatics2.id4 = 25;
            IDstatics2.id5 = 0;
            TheMess.I_Madness.IDstatics.Add(IDstatics2);

            InterfaceComp3 IDstatics3 = new InterfaceComp3();
            IDstatics3.id1 = 1477;
            IDstatics3.id2 = 90;
            IDstatics3.id3 = 65535;
            IDstatics3.id4 = 58;
            IDstatics3.id5 = 0;
            TheMess.I_Madness.IDstatics.Add(IDstatics3);

            InterfaceComp3 IDstatics4 = new InterfaceComp3();
            IDstatics4.id1 = 1477;
            IDstatics4.id2 = 91;
            IDstatics4.id3 = 65535;
            IDstatics4.id4 = 90;
            IDstatics4.id5 = 0;
            TheMess.I_Madness.IDstatics.Add(IDstatics4);
            TheMess.KnownInterfaces.Add(TheMess.I_Madness);
            #endregion

            #region INVENTORY
            TheMess.I_Madness = new InterfaceComp3test();
            TheMess.I_Madness.Name = "Inventory";
            IDdynamic = new InterfaceComp3();
            IDdynamic.id1 = 1473;
            IDdynamic.id2 = 0;
            IDdynamic.id3 = 65535;
            IDdynamic.id4 = 65535;
            IDdynamic.id5 = 0;
            TheMess.I_Madness.IDdynamic = IDdynamic;

            IDstatics = new InterfaceComp3();
            TheMess.I_Madness.IDstatics = new List<InterfaceComp3>();

            IDstatics.id1 = 1477;
            IDstatics.id2 = 25;
            IDstatics.id3 = 65535;
            IDstatics.id4 = 65535;
            IDstatics.id5 = 0;
            TheMess.I_Madness.IDstatics.Add(IDstatics);

            IDstatics2 = new InterfaceComp3();
            IDstatics2.id1 = 1477;
            IDstatics2.id2 = 58;
            IDstatics2.id3 = 65535;
            IDstatics2.id4 = 25;
            IDstatics2.id5 = 0;
            TheMess.I_Madness.IDstatics.Add(IDstatics2);

            IDstatics3 = new InterfaceComp3();
            IDstatics3.id1 = 1477;
            IDstatics3.id2 = 99;
            IDstatics3.id3 = 65535;
            IDstatics3.id4 = 58;
            IDstatics3.id5 = 0;
            TheMess.I_Madness.IDstatics.Add(IDstatics3);

            IDstatics4 = new InterfaceComp3();
            IDstatics4.id1 = 1477;
            IDstatics4.id2 = 101;
            IDstatics4.id3 = 65535;
            IDstatics4.id4 = 99;
            IDstatics4.id5 = 0;
            TheMess.I_Madness.IDstatics.Add(IDstatics4);
            TheMess.KnownInterfaces.Add(TheMess.I_Madness);
            #endregion

            #region EQUIPMENT
            TheMess.I_MadnessEquipment = new InterfaceComp3test();
            TheMess.I_MadnessEquipment.Name = "Equipment";
            IDdynamic = new InterfaceComp3();
            IDdynamic.id1 = 1464;
            IDdynamic.id2 = 0;
            IDdynamic.id3 = 65535;
            IDdynamic.id4 = 65535;
            IDdynamic.id5 = 0;
            TheMess.I_MadnessEquipment.IDdynamic = IDdynamic;

            IDstatics = new InterfaceComp3();
            TheMess.I_MadnessEquipment.IDstatics = new List<InterfaceComp3>();

            IDstatics.id1 = 1477;
            IDstatics.id2 = 25;
            IDstatics.id3 = 65535;
            IDstatics.id4 = 65535;
            IDstatics.id5 = 0;
            TheMess.I_MadnessEquipment.IDstatics.Add(IDstatics);

            IDstatics2 = new InterfaceComp3();
            IDstatics2.id1 = 1477;
            IDstatics2.id2 = 58;
            IDstatics2.id3 = 65535;
            IDstatics2.id4 = 25;
            IDstatics2.id5 = 0;
            TheMess.I_MadnessEquipment.IDstatics.Add(IDstatics2);

            IDstatics3 = new InterfaceComp3();
            IDstatics3.id1 = 1477;
            IDstatics3.id2 = 110;
            IDstatics3.id3 = 65535;
            IDstatics3.id4 = 58;
            IDstatics3.id5 = 0;
            TheMess.I_MadnessEquipment.IDstatics.Add(IDstatics3);

            IDstatics4 = new InterfaceComp3();
            IDstatics4.id1 = 1477;
            IDstatics4.id2 = 112;
            IDstatics4.id3 = 65535;
            IDstatics4.id4 = 110;
            IDstatics4.id5 = 0;
            TheMess.I_MadnessEquipment.IDstatics.Add(IDstatics4);

            TheMess.KnownInterfaces.Add(TheMess.I_MadnessEquipment);

            #endregion

            #region GetActionBar
            TheMess.I_MadnessActionBar = new InterfaceComp3test();
            TheMess.I_MadnessActionBar.Name = "MainActionBar";
            IDdynamic = new InterfaceComp3();
            IDdynamic.id1 = 1430;
            IDdynamic.id2 = 0;
            IDdynamic.id3 = 65535;
            IDdynamic.id4 = 65535;
            IDdynamic.id5 = 0;
            IDstatics = new InterfaceComp3();
            TheMess.I_MadnessActionBar.IDstatics = new List<InterfaceComp3>();

            IDstatics.id1 = 1477;
            IDstatics.id2 = 25;
            IDstatics.id3 = 65535;
            IDstatics.id4 = 65535;
            IDstatics.id5 = 0;
            TheMess.I_MadnessActionBar.IDstatics.Add(IDstatics);

            IDstatics2 = new InterfaceComp3();
            IDstatics2.id1 = 1477;
            IDstatics2.id2 = 58;
            IDstatics2.id3 = 65535;
            IDstatics2.id4 = 25;
            IDstatics2.id5 = 0;
            TheMess.I_MadnessActionBar.IDstatics.Add(IDstatics2);

            IDstatics3 = new InterfaceComp3();
            IDstatics3.id1 = 1477;
            IDstatics3.id2 = 64;
            IDstatics3.id3 = 65535;
            IDstatics3.id4 = 58;
            IDstatics3.id5 = 0;
            TheMess.I_MadnessActionBar.IDstatics.Add(IDstatics3);

            TheMess.KnownInterfaces.Add(TheMess.I_MadnessActionBar);

            #endregion

            #region BANK


            TheMess.I_MadnessBank1 = new InterfaceComp3test();
            TheMess.I_MadnessBank1.Name = "Bank1";
            IDdynamic = new InterfaceComp3();
            IDdynamic.id1 = 517;
            IDdynamic.id2 = 0;
            IDdynamic.id3 = 65535;
            IDdynamic.id4 = 65535;
            IDdynamic.id5 = 0;
            TheMess.I_MadnessBank1.IDdynamic = IDdynamic;

            IDstatics = new InterfaceComp3();
            TheMess.I_MadnessBank1.IDstatics = new List<InterfaceComp3>();

            IDstatics.id1 = 1477;
            IDstatics.id2 = 25;
            IDstatics.id3 = 65535;
            IDstatics.id4 = 65535;
            IDstatics.id5 = 0;
            TheMess.I_MadnessBank1.IDstatics.Add(IDstatics);

            IDstatics2 = new InterfaceComp3();
            IDstatics2.id1 = 1477;
            IDstatics2.id2 = 683;
            IDstatics2.id3 = 65535;
            IDstatics2.id4 = 25;
            IDstatics2.id5 = 0;
            TheMess.I_MadnessBank1.IDstatics.Add(IDstatics2);

            IDstatics3 = new InterfaceComp3();
            IDstatics3.id1 = 1477;
            IDstatics3.id2 = 684;
            IDstatics3.id3 = 65535;
            IDstatics3.id4 = 683;
            IDstatics3.id5 = 0;
            TheMess.I_MadnessBank1.IDstatics.Add(IDstatics3);
            TheMess.KnownInterfaces.Add(TheMess.I_MadnessBank1);
            //////////////////////////////////////////////

            TheMess.I_MadnessBank2 = new InterfaceComp3test();
            TheMess.I_MadnessBank2.Name = "Bank 2";
            IDdynamic = new InterfaceComp3();
            IDdynamic.id1 = 517;
            IDdynamic.id2 = 302;
            IDdynamic.id3 = 65535;
            IDdynamic.id4 = 65535;
            IDdynamic.id5 = 0;
            //IDdynamic.id5 = 0xffff;
            TheMess.I_MadnessBank2.IDdynamic = IDdynamic;

            IDstatics = new InterfaceComp3();
            TheMess.I_MadnessBank2.IDstatics = new List<InterfaceComp3>();

            IDstatics.id1 = 1477;
            IDstatics.id2 = 25;
            IDstatics.id3 = 65535;
            IDstatics.id4 = 65535;
            IDstatics.id5 = 0;
            TheMess.I_MadnessBank2.IDstatics.Add(IDstatics);

            IDstatics2 = new InterfaceComp3();
            IDstatics2.id1 = 1477;
            IDstatics2.id2 = 683;
            IDstatics2.id3 = 65535;
            IDstatics2.id4 = 25;
            IDstatics2.id5 = 0;
            TheMess.I_MadnessBank2.IDstatics.Add(IDstatics2);

            IDstatics3 = new InterfaceComp3();
            IDstatics3.id1 = 1477;
            IDstatics3.id2 = 684;
            IDstatics3.id3 = 65535;
            IDstatics3.id4 = 683;
            IDstatics3.id5 = 0;
            TheMess.I_MadnessBank2.IDstatics.Add(IDstatics3);

            /////////////////////////////////////////////////////////////////////////////////////

            TheMess.I_MadnessBank3 = new InterfaceComp3test();
            TheMess.I_MadnessBank3.Name = "Bank 3";
            IDdynamic = new InterfaceComp3();
            IDdynamic.id1 = 517;
            IDdynamic.id2 = 314;
            IDdynamic.id3 = 65535;
            IDdynamic.id4 = 65535;
            IDdynamic.id5 = 0;
            TheMess.I_MadnessBank3.IDdynamic = IDdynamic;

            IDstatics = new InterfaceComp3();
            TheMess.I_MadnessBank3.IDstatics = new List<InterfaceComp3>();

            IDstatics.id1 = 1477;
            IDstatics.id2 = 25;
            IDstatics.id3 = 65535;
            IDstatics.id4 = 65535;
            IDstatics.id5 = 0;
            TheMess.I_MadnessBank3.IDstatics.Add(IDstatics);

            IDstatics2 = new InterfaceComp3();
            IDstatics2.id1 = 1477;
            IDstatics2.id2 = 683;
            IDstatics2.id3 = 65535;
            IDstatics2.id4 = 25;
            IDstatics2.id5 = 0;
            TheMess.I_MadnessBank3.IDstatics.Add(IDstatics2);

            IDstatics3 = new InterfaceComp3();
            IDstatics3.id1 = 1477;
            IDstatics3.id2 = 684;
            IDstatics3.id3 = 65535;
            IDstatics3.id4 = 683;
            IDstatics3.id5 = 0;
            TheMess.I_MadnessBank3.IDstatics.Add(IDstatics3);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            #endregion

            #region SMITHING INTERFACE

            TheMess.I_MadnessSmithing = new InterfaceComp3test();
            TheMess.I_MadnessSmithing.Name = "Smithing";
            IDdynamic = new InterfaceComp3();
            IDdynamic.id1 = 37;
            IDdynamic.id2 = 17;
            IDdynamic.id3 = 65535;
            IDdynamic.id4 = 65535;
            IDdynamic.id5 = 0;
            TheMess.I_MadnessSmithing.IDdynamic = IDdynamic;

            IDstatics = new InterfaceComp3();
            TheMess.I_MadnessSmithing.IDstatics = new List<InterfaceComp3>();

            IDstatics.id1 = 1477;
            IDstatics.id2 = 25;
            IDstatics.id3 = 65535;
            IDstatics.id4 = 65535;
            IDstatics.id5 = 0;
            TheMess.I_MadnessSmithing.IDstatics.Add(IDstatics);

            IDstatics2 = new InterfaceComp3();
            IDstatics2.id1 = 1477;
            IDstatics2.id2 = 680;
            IDstatics2.id3 = 65535;
            IDstatics2.id4 = 25;
            IDstatics2.id5 = 0;
            TheMess.I_MadnessSmithing.IDstatics.Add(IDstatics2);

            IDstatics3 = new InterfaceComp3();
            IDstatics3.id1 = 1477;
            IDstatics3.id2 = 682;
            IDstatics3.id3 = 65535;
            IDstatics3.id4 = 680;
            IDstatics3.id5 = 0;
            TheMess.I_MadnessSmithing.IDstatics.Add(IDstatics3);
            TheMess.KnownInterfaces.Add(TheMess.I_MadnessSmithing);
            #endregion

            #region LOOT WINDOW

            TheMess.I_MadnessLootWindow = new InterfaceComp3test();
            TheMess.I_MadnessLootWindow.Name = "Loot window";
            IDdynamic = new InterfaceComp3();
            IDdynamic.id1 = 1622;
            IDdynamic.id2 = 4;
            IDdynamic.id3 = 65535;
            IDdynamic.id4 = 65535;
            IDdynamic.id5 = 0;
            TheMess.I_MadnessLootWindow.IDdynamic = IDdynamic;

            IDstatics = new InterfaceComp3();
            TheMess.I_MadnessLootWindow.IDstatics = new List<InterfaceComp3>();

            IDstatics.id1 = 1477;
            IDstatics.id2 = 25;
            IDstatics.id3 = 65535;
            IDstatics.id4 = 65535;
            IDstatics.id5 = 0;
            TheMess.I_MadnessLootWindow.IDstatics.Add(IDstatics);

            IDstatics2 = new InterfaceComp3();
            IDstatics2.id1 = 1477;
            IDstatics2.id2 = 692;
            IDstatics2.id3 = 65535;
            IDstatics2.id4 = 25;
            IDstatics2.id5 = 0;
            TheMess.I_MadnessLootWindow.IDstatics.Add(IDstatics2);

            IDstatics3 = new InterfaceComp3();
            IDstatics3.id1 = 1477;
            IDstatics3.id2 = 694;
            IDstatics3.id3 = 65535;
            IDstatics3.id4 = 693;
            IDstatics3.id5 = 0;
            TheMess.I_MadnessLootWindow.IDstatics.Add(IDstatics3);
            TheMess.KnownInterfaces.Add(TheMess.I_MadnessLootWindow);
            #endregion

            #region ALL CHAT

            TheMess.I_MadnessAllChat = new InterfaceComp3test();
            TheMess.I_MadnessAllChat.Name = "All Chat";
            IDdynamic = new InterfaceComp3();
            IDdynamic.id1 = 137;
            IDdynamic.id2 = 0;
            IDdynamic.id3 = 65535;
            IDdynamic.id4 = 65535;
            IDdynamic.id5 = 0;
            TheMess.I_MadnessAllChat.IDdynamic = IDdynamic;

            IDstatics = new InterfaceComp3();
            TheMess.I_MadnessAllChat.IDstatics = new List<InterfaceComp3>();

            IDstatics.id1 = 1477;
            IDstatics.id2 = 25;
            IDstatics.id3 = 65535;
            IDstatics.id4 = 65535;
            IDstatics.id5 = 0;
            TheMess.I_MadnessAllChat.IDstatics.Add(IDstatics);

            IDstatics2 = new InterfaceComp3();
            IDstatics2.id1 = 1477;
            IDstatics2.id2 = 58;
            IDstatics2.id3 = 65535;
            IDstatics2.id4 = 25;
            IDstatics2.id5 = 0;
            TheMess.I_MadnessAllChat.IDstatics.Add(IDstatics2);

            IDstatics3 = new InterfaceComp3();
            IDstatics3.id1 = 1477;
            IDstatics3.id2 = 405;
            IDstatics3.id3 = 65535;
            IDstatics3.id4 = 58;
            IDstatics3.id5 = 0;
            TheMess.I_MadnessAllChat.IDstatics.Add(IDstatics3);

            IDstatics4 = new InterfaceComp3();
            IDstatics4.id1 = 1477;
            IDstatics4.id2 = 407;
            IDstatics4.id3 = 65535;
            IDstatics4.id4 = 405;
            IDstatics4.id5 = 0;
            TheMess.I_MadnessAllChat.IDstatics.Add(IDstatics4);
            TheMess.KnownInterfaces.Add(TheMess.I_MadnessAllChat);
            #endregion

            #region Lobby Frame 1

            TheMess.I_MadnessGameStatus = new InterfaceComp3test();
            TheMess.I_MadnessGameStatus.Name = "LobbyFrame1";
            IDdynamic = new InterfaceComp3();
            IDdynamic.id1 = 906;
            IDdynamic.id2 = 0;
            IDdynamic.id3 = 65535;
            IDdynamic.id4 = 65535;
            IDdynamic.id5 = 0;
            TheMess.I_MadnessGameStatus.IDdynamic = IDdynamic;
            TheMess.KnownInterfaces.Add(TheMess.I_MadnessGameStatus);
            #endregion
            #region Lobby Frame 2

            TheMess.I_MadnessGameStatus = new InterfaceComp3test();
            TheMess.I_MadnessGameStatus.Name = "LobbyFrame2";
            IDdynamic = new InterfaceComp3();
            IDdynamic.id1 = 906;
            IDdynamic.id2 = 155;
            IDdynamic.id3 = 65535;
            IDdynamic.id4 = 65535;
            IDdynamic.id5 = 0;
            TheMess.I_MadnessGameStatus.IDdynamic = IDdynamic;
            TheMess.KnownInterfaces.Add(TheMess.I_MadnessGameStatus);
            #endregion
            #region Lobby Frame 3

            TheMess.I_MadnessGameStatus = new InterfaceComp3test();
            TheMess.I_MadnessGameStatus.Name = "LobbyFrame3";
            IDdynamic = new InterfaceComp3();
            IDdynamic.id1 = 906;
            IDdynamic.id2 = 157;
            IDdynamic.id3 = 65535;
            IDdynamic.id4 = 65535;
            IDdynamic.id5 = 0;
            TheMess.I_MadnessGameStatus.IDdynamic = IDdynamic;
            TheMess.KnownInterfaces.Add(TheMess.I_MadnessGameStatus);
            #endregion
            #region DIALOG CHOOSE OPTION WINDOW

            TheMess.I_MadnessDialogChooseOption = new InterfaceComp3test();
            TheMess.I_MadnessDialogChooseOption.Name = "Dialog";
            IDdynamic = new InterfaceComp3();
            IDdynamic.id1 = 1188;
            IDdynamic.id2 = 5;
            IDdynamic.id3 = 65535;
            IDdynamic.id4 = 65535;
            IDdynamic.id5 = 0;
            TheMess.I_MadnessDialogChooseOption.IDdynamic = IDdynamic;

            IDstatics = new InterfaceComp3();
            TheMess.I_MadnessDialogChooseOption.IDstatics = new List<InterfaceComp3>();

            IDstatics.id1 = 1477;
            IDstatics.id2 = 25;
            IDstatics.id3 = 65535;
            IDstatics.id4 = 65535;
            IDstatics.id5 = 0;
            TheMess.I_MadnessDialogChooseOption.IDstatics.Add(IDstatics);

            IDstatics2 = new InterfaceComp3();
            IDstatics2.id1 = 1477;
            IDstatics2.id2 = 703;
            IDstatics2.id3 = 65535;
            IDstatics2.id4 = 25;
            IDstatics2.id5 = 0;
            TheMess.I_MadnessDialogChooseOption.IDstatics.Add(IDstatics2);

            IDstatics3 = new InterfaceComp3();
            IDstatics3.id1 = 1477;
            IDstatics3.id2 = 705;
            IDstatics3.id3 = 65535;
            IDstatics3.id4 = 703;
            IDstatics3.id5 = 0;
            TheMess.I_MadnessDialogChooseOption.IDstatics.Add(IDstatics3);
            TheMess.KnownInterfaces.Add(TheMess.I_MadnessDialogChooseOption);
            #endregion

            #region DIALOG WINDOW UNSURE WHICH ONE WILL UPDATE LATER

            TheMess.I_MadnessDialog = new InterfaceComp3test();
            TheMess.I_MadnessDialog.Name = "Dialog";
            IDdynamic = new InterfaceComp3();
            IDdynamic.id1 = 1184;
            IDdynamic.id2 = 0;
            IDdynamic.id3 = 2;
            IDdynamic.id4 = 0xffff;
            IDdynamic.id5 = 0xffff;
            TheMess.I_MadnessDialog.IDdynamic = IDdynamic;

            IDstatics = new InterfaceComp3();
            TheMess.I_MadnessDialog.IDstatics = new List<InterfaceComp3>();

            IDstatics.id1 = 1477;
            IDstatics.id2 = 0;
            IDstatics.id3 = 24;
            IDstatics.id4 = 65535;
            IDstatics.id5 = 65535;
            TheMess.I_MadnessDialog.IDstatics.Add(IDstatics);

            IDstatics2 = new InterfaceComp3();
            IDstatics2.id1 = 1477;
            IDstatics2.id2 = 0;
            IDstatics2.id3 = 702;
            IDstatics2.id4 = 65535;
            IDstatics2.id5 = 24;
            TheMess.I_MadnessDialog.IDstatics.Add(IDstatics2);

            IDstatics3 = new InterfaceComp3();
            IDstatics3.id1 = 1477;
            IDstatics3.id2 = 0;
            IDstatics3.id3 = 704;
            IDstatics3.id4 = 65535;
            IDstatics3.id5 = 702;
            TheMess.I_MadnessDialog.IDstatics.Add(IDstatics3);
            TheMess.KnownInterfaces.Add(TheMess.I_MadnessDialog);
            #endregion

            #region SELECT TOOL WINDOW

            TheMess.I_MadnessSelectItem = new InterfaceComp3test();
            TheMess.I_MadnessSelectItem.Name = "Select item";
            IDdynamic = new InterfaceComp3();
            IDdynamic.id1 = 1371;
            IDdynamic.id2 = 7;
            IDdynamic.id3 = 65535;
            IDdynamic.id4 = 65535;
            IDdynamic.id5 = 0;
            TheMess.I_MadnessSelectItem.IDdynamic = IDdynamic;

            IDstatics = new InterfaceComp3();
            TheMess.I_MadnessSelectItem.IDstatics = new List<InterfaceComp3>();

            IDstatics.id1 = 1477;
            IDstatics.id2 = 25;
            IDstatics.id3 = 65535;
            IDstatics.id4 = 65535;
            IDstatics.id5 = 0;
            TheMess.I_MadnessSelectItem.IDstatics.Add(IDstatics);

            IDstatics2 = new InterfaceComp3();
            IDstatics2.id1 = 1477;
            IDstatics2.id2 = 680;
            IDstatics2.id3 = 65535;
            IDstatics2.id4 = 25;
            IDstatics2.id5 = 0;
            TheMess.I_MadnessSelectItem.IDstatics.Add(IDstatics2);

            IDstatics3 = new InterfaceComp3();
            IDstatics3.id1 = 1477;
            IDstatics3.id2 = 690;
            IDstatics3.id3 = 65535;
            IDstatics3.id4 = 680;
            IDstatics3.id5 = 0;
            TheMess.I_MadnessSelectItem.IDstatics.Add(IDstatics3);
            TheMess.KnownInterfaces.Add(TheMess.I_MadnessSelectItem);
            #endregion

        }
        public static List<IInfo> ReadInvArrays33()
        {
            InventoryList = new List<IInfo>();
            InterfaceComp5 interf0 = GetIbystring("Inventory");
            InterfaceComp5 interf1 = new InterfaceComp5(interf0.id1, 2, interf0.id3, interf0.id2, 0);
            InterfaceComp5 interf2 = new InterfaceComp5(interf1.id1, 5, interf1.id3, interf1.id2, 0);
            List<IInfo> aim = Get_Check_Interface_Under(Pointer_InvD, new List<InterfaceComp5>{interf0, interf1, interf2});
            if(aim != null)
            {
                if (aim.Count() > 0)
                {
                    int count = 0;
                    foreach (IInfo INV in aim)
                    {
                        if (count > 28) { break; }
                        IInfo InvArr = new IInfo();
                        InvArr.id1 = INV.id1;
                        InvArr.id2 = INV.id2;
                        InvArr.id3 = INV.id3;
                        InvArr.id4 = INV.id4;
                        InvArr.index = count;
                        InvArr.itemid1_size = INV.itemid1_size;
                        InvArr.textitem = INV.textitem;
                        InvArr.itemid1 = INV.itemid1;
                        InvArr.x = INV.x + (INV.xs / 2);
                        InvArr.y = INV.y + (INV.ys / 2);
                        InvArr.xs = INV.xs;
                        InvArr.ys = INV.ys;
                        InvArr.xy = new Point(InvArr.x, InvArr.y);
                        InventoryList.Add(InvArr);
                        count++;
                    }
                    if (count == 28)
                    {
                        return InventoryList;
                    }
                }
            }
            return null;
        }
        public static List<IInfo> Get_Check_Interface_Under(IInfo current_data, List<InterfaceComp5> search_components)
        {
            if (current_data != null)
            {
                if (current_data.memloc == 0)
                {
                    List<IInfo> aim = ScanForInterfaceTest2Get(false, search_components);
                    if (aim != null)
                    {
                        if (aim.Count() > 0)
                        {
                            InterfaceComp5 OP = GetIMadnessOP(search_components[0]);
                            aim[0].firstIF = new InterfaceComp5(search_components[0].id1, search_components[0].id2, search_components[0].id3, search_components[0].id4, OP.memloc);
                            List<IInfo> result = new List<IInfo>();
                            current_data = aim[0];
                            List<ulong> data = GatherIUM(current_data.memloc);
                            foreach (ulong d in data)
                            {
                                result.Add(GetInterfaceDataMem(d, true, true));
                                result[result.Count - 1].x += OP.id1;
                                result[result.Count - 1].y += OP.id2;
                            }
                            return result;
                        }
                    }
                }
                else
                {
                    //actives
                    int bb3 = Readint(current_data.memloc + gen_active1);
                    int bb4 = Readint(current_data.memloc + gen_active2);
                    //ids
                    int bb5 = ReadINT16(current_data.memloc + I_ids);
                    int bb7 = ReadINT16(current_data.memloc + I_ids + 2);
                    int bb8 = ReadINT16(current_data.memloc + I_ids + 4);
                    int bb9 = ReadINT16(current_data.memloc + I_ids + 6);
                    //pointers
                    ulong PPP1 = ReadUINT64(current_data.memloc + 0x10);
                    ulong PPP2 = ReadUINT64(current_data.memloc + I_offback);
                    int bb55 = ReadINT16(PPP2 + I_ids);
                    if (PPP1 == current_data.memloc && PPP1 > 0 && PPP2 > 0 && bb55 == current_data.id1)
                    {
                        if (bb3 > 0 && bb3 < 255)
                        {
                            if (bb4 > 0 && bb4 < 255)
                            {
                                if (bb5 == current_data.id1 && bb7 == current_data.id2 && bb8 == current_data.id3 && bb9 == current_data.id4)
                                {
                                    InterfaceComp5 OP = GetIMadnessOP(current_data.firstIF);
                                    List<IInfo> result = new List<IInfo>();
                                    List<ulong> data = GatherIUM(current_data.memloc);
                                    foreach (ulong d in data)
                                    {
                                        result.Add(GetInterfaceDataMem(d, true, true));
                                        result[result.Count - 1].x += OP.id1;
                                        result[result.Count - 1].y += OP.id2;
                                    }
                                    return result;
                                }
                            }
                        }
                    }
                    //failed, try again
                    List<IInfo> aim = ScanForInterfaceTest2Get(false, search_components);
                    if (aim.Count() > 0)
                    {
                        InterfaceComp5 OP = GetIMadnessOP(search_components[0]);
                        aim[0].firstIF = new InterfaceComp5 ( search_components[0].id1,search_components[0].id2,search_components[0].id3,search_components[0].id4, OP.memloc );
                        List<IInfo> result = new List<IInfo>();
                        current_data = aim[0];
                        List<ulong> data = GatherIUM(current_data.memloc);
                        foreach (ulong d in data)
                        {
                            result.Add(GetInterfaceDataMem(d, true, true));
                            result[result.Count - 1].x += OP.id1;
                            result[result.Count - 1].y += OP.id2;
                        }
                        return result;
                    }
                }
                current_data = null;//reset
                return null;
            }
            return null;
        }
        public static IInfo GetInterfaceDataMem(ulong database, bool refresh_data, bool refresh_text)
        {
            if (database > 0)
            {
                IInfo result = new IInfo();
                result.memloc = database;
                result.id1 = ReadINT16(database + I_ids);
                result.id2 = ReadINT16(database + I_ids + 2);
                result.id3 = ReadINT16(database + I_ids + 4);
                result.id4 = ReadINT16(database + I_ids + 6);
                if (refresh_data)
                {
                    //reverse
                    List<ulong> p_list = new List<ulong>();
                    ulong pointer = database;
                    for (int i = 0; i < 20; i++)
                    {
                        p_list.Add(pointer);
                        pointer = ReadUINT64(pointer + I_offback);
                        if (pointer == 0 || (ulong)TheMess.RSExeStart < pointer) { break; }
                    }
                    p_list.Reverse();
                    foreach (ulong p in p_list)
                    {
                        int scroll = Readint(p + I_pixels);
                        if (scroll > 0 && scroll < 20001)
                        {
                            result.y -= scroll;
                        }
                        result.x += ReadINT16(p + I_x0);
                        result.y += ReadINT16(p + I_y0);
                        result.xy = new Point ( result.x, result.y);
                    }
                    //result.xs = ReadINT16(p_list.back() + I_x4);
                    result.xs = ReadINT16(p_list[p_list.Count - 1] + I_x4);
                    //result.ys = ReadINT16(p_list.back() + I_y4);
                    result.ys = ReadINT16(p_list[p_list.Count - 1] + I_x4);
                    for (int i = 0; i < p_list.Count() - 1; i++)
                    {
                        if (ReadINT16(p_list[i] + I_y4) < result.y + result.ys / 2)
                        {
                            result.notvisible = true;
                        }
                    }
                    if (result.y < -result.ys / 2)
                    {
                        result.notvisible = true;
                    }
                    result.itemid1 = ReadINT16(database + I_itemids);
                    result.itemid1_size = ReadUINT64(database + I_itemstack);
                }
                if (refresh_text)
                {
                    var textaddress = BitConverter.ToUInt64(MemoryReading.Read<long>(database + I_textP), 0);
                    string blah = ReadCharsPointer(textaddress);
                    result.textitem = StringFilter(ReadCharsPointer(textaddress));
                    if (result.textitem.Length < 2)
                    {
                        result.textitem = StringFilter(ReadCharsPointer(textaddress));
                    }
                }
                return result;
            }
            return null;
        }
        public static string StringFilter(string to)
        {
            if (to.Length > 0)
            {
                var finalS = new StringBuilder();
                bool startCharFound = true;

                foreach (char ch in to)
                {
                    if (ch == '<')
                    {
                        startCharFound = false;
                    }
                    if (startCharFound)
                    {
                        if (finalS.Length > 250) { break; }
                        finalS.Append(ch);
                    }
                    if (ch == '>')
                    {
                        startCharFound = true;
                    }
                }

                return finalS.ToString();
            }

            return string.Empty;
        }
        public static string StringFilter2(string to)
        {
            if (to.Length > 0)
            {
                StringBuilder finalS = new StringBuilder();
                bool startCharFound = true;

                foreach (char ch in to)
                {
                    if (ch == '<')
                    {
                        startCharFound = false;
                    }
                    else if (ch == '>')
                    {
                        startCharFound = true;
                        continue;
                    }

                    if (startCharFound)
                    {
                        finalS.Append(ch);
                    }
                }

                return finalS.ToString();
            }

            return "";
        }

    }
}

public class InterfaceComp5
{
    public int id1 = 0;
    public int id2 = 0;
    public int id3 = 0;
    public int id4 = 0;
    public ulong memloc = 0;
    public InterfaceComp5(int _id1, int _id2, int _id3, int _id4, ulong _memloc)
    {
        id1 = _id1;
        id2 = _id2;
        id3 = _id3;
        id4 = _id4;
        memloc = _memloc;
    }
}
public class InterfaceComp5test
{
    public string Name { get; set; }
    public InterfaceComp5 IDdynamic { get; set; }
    public List<InterfaceComp5> IDstatics { get; set; }
}

