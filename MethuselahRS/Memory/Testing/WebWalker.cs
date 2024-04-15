using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MethuselahRS.Memory.Testing
{
    public partial class WebWalker : Form
    {
        public WebWalker()
        {
            InitializeComponent();
            this.pictureBox1.MouseMove += new MouseEventHandler(pictureBox1_MouseMove);
        }
        private Node[,] grid;
        private List<Node> nodes = new List<Node>();
        private void WebWalker_Load(object sender, EventArgs e)
        {
            for (int x = 0; x < pictureBox1.Image.Width; x++)
            {
                for (int y = 0; y < pictureBox1.Image.Height; y++)
                {
                    Point sc = ScreenToWorldCoordinates(x, y);
                    Bitmap bm = pictureBox1.Image as Bitmap;
                    Color px = bm.GetPixel(x, y);
                    bool nw = IsNonWalkableColor(px);
                    Node node = new Node(sc, nw, new Point(x, y));
                    nodes.Add(node);
                }
            }
        }
        private bool IsNonWalkableColor(Color color)
        {
            // Define the colors that correspond to non-walkable areas
            Color targetFenceColor = Color.FromArgb(204, 204, 204); // color for fence
            Color targetWaterColor = Color.FromArgb(136, 144, 157); // color for water
            Color targetTextColor = Color.FromArgb(255, 255, 255); // color for text
            int variance = 1; // Variance in color

            if (IsColorInRange(color, targetFenceColor, variance))
            {
                return false;
            }
            if(IsColorInRange(color,targetWaterColor, variance))
            {
                return false;
            }
            if(IsColorInRange(color, targetTextColor, variance))
            {
                return false;
            }

            return true;
        }

        private bool IsColorInRange(Color color, Color targetColor, int variance)
        {
            return Math.Abs(color.R - targetColor.R) <= variance &&
                   Math.Abs(color.G - targetColor.G) <= variance &&
                   Math.Abs(color.B - targetColor.B) <= variance;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int screenX = e.X;
            int screenY = e.Y;

            Point worldCoords = ScreenToWorldCoordinates(screenX, screenY);

            labelScreenCoords.Text = $"Screen: ({screenX}, {screenY})";
            labelWorldCoords.Text = $"World: ({worldCoords.X}, {worldCoords.Y})";
            using (Bitmap bitmap = new Bitmap(1, 1))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(Cursor.Position, Point.Empty, new Size(1, 1));
                }
                Color pixelColor = bitmap.GetPixel(0, 0);
                PixelColor.BackColor = pixelColor;
                lbl_PixelColor.Text = pixelColor.ToString();
            }
            var matchingNode = nodes.FirstOrDefault(node => node.ScreenCoords == new Point(screenX, screenY));
            label1.Text = matchingNode.ScreenCoords.ToString() + " : " + matchingNode.WorldPosition.ToString() + " : " + matchingNode.Walkable.ToString();
            
        }
        private Point ScreenToWorldCoordinates(int screenX, int screenY)
        {
            int worldX = (int)Math.Round(screenX / 1.49) + 1798;
            int worldY = (int)Math.Round((pictureBox1.Height - screenY) / 1.49) + 2465;

            return new Point(worldX, worldY);
        }











        private void DrawPath(List<Node> path)
        {
            if (pictureBox1.Image == null)
            {
                return;
            }

            Bitmap bmp = new Bitmap(pictureBox1.Image);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Pen pen = new Pen(Color.Red, 2); 

                for (int i = 0; i < path.Count - 1; i++)
                {
                    Node a = path[i];
                    Node b = path[i + 1];

                    Node blah1 = nodes.FirstOrDefault(node => node.WorldPosition == a.WorldPosition);
                    Node blah2 = nodes.FirstOrDefault(node => node.WorldPosition == b.WorldPosition);
                    Point aScreen = blah1.ScreenCoords;
                    Point bScreen = blah2.ScreenCoords;

                    // Draw a line between the nodes
                    g.DrawLine(pen, aScreen, bScreen);
                }
            }

            pictureBox1.Image = bmp;
        }


        private void btn_CreatePath_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not even close to ready ");
            
            int sx = int.Parse(tb_sx.Text);
            int sy = int.Parse(tb_sy.Text);
            int ex = int.Parse(tb_ex.Text);
            int ey = int.Parse(tb_ey.Text);
            Point start = new Point(sx, sy);
            Point end = new Point(ex, ey);
            List<Node> path = FindPath(start, end);
            DrawPath(path);
            
        }




        public List<Node> FindPath(Point startWorldPos, Point endWorldPos)
        {
            Node startNode = GetNodeAt(startWorldPos);
            Node endNode = GetNodeAt(endWorldPos);

            SortedSet<Node> openSet = new SortedSet<Node>(new NodeComparer());
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet.Min;
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == endNode)
                {
                    return RetracePath(startNode, endNode);
                }

                foreach (Node neighbor in GetNeighbors(currentNode))
                {
                    if (!neighbor.Walkable || closedSet.Contains(neighbor))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbor = currentNode.GCost + GetDistance(currentNode, neighbor);
                    if (newMovementCostToNeighbor < neighbor.GCost || !openSet.Contains(neighbor))
                    {
                        neighbor.GCost = newMovementCostToNeighbor;
                        neighbor.HCost = GetDistance(neighbor, endNode);
                        neighbor.Parent = currentNode;

                        if (!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor);
                        }
                        else
                        {
                            // Reorder the node in the open set
                            openSet.Remove(neighbor);
                            openSet.Add(neighbor);
                        }
                    }
                }
            }

            return new List<Node>(); // No path found
        }
        public Node GetNodeAt(Point worldPosition)
        {
            return nodes.FirstOrDefault(node => node.WorldPosition == worldPosition);
        }
        private List<Node> RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }
            path.Add(startNode); // Optional: Include the start node in the path
            path.Reverse(); // Reverse the path to start from the beginning

            return path;
        }
        List<Node> neighbors = new List<Node>();

        private List<Node> GetNeighbors(Node node)
        {
            List<Node> neighbors = new List<Node>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;

                    // Skip diagonal neighbors
                    if (x != 0 && y != 0)
                        continue;

                    int checkX = node.WorldPosition.X + x;
                    int checkY = node.WorldPosition.Y + y;

                    Node neighbor = nodes.FirstOrDefault(n => n.WorldPosition.X == checkX && n.WorldPosition.Y == checkY);
                    if (neighbor != null && neighbor.Walkable)
                    {
                        neighbors.Add(neighbor);
                    }
                }
            }

            return neighbors;
        }
        private int GetDistance(Node nodeA, Node nodeB)
        {
            int distX = Math.Abs(nodeA.WorldPosition.X - nodeB.WorldPosition.X);
            int distY = Math.Abs(nodeA.WorldPosition.Y - nodeB.WorldPosition.Y);

            return (int)Math.Sqrt(distX * distX + distY * distY);
        }


    }
}
public class NodeComparer : IComparer<Node>
{
    public int Compare(Node x, Node y)
    {
        int compare = x.FCost.CompareTo(y.FCost);
        if (compare == 0)
        {
            compare = x.HCost.CompareTo(y.HCost);
        }
        return compare;
    }
}
public class Node
{
    public Point WorldPosition { get; set; }
    public bool Walkable { get; set; }
    public Point ScreenCoords { get; set; }
    public int GCost { get; set; }
    public int HCost { get; set; }
    public int FCost { get { return GCost + HCost; } }
    public Node Parent { get; set; }
    public Node(Point worldPos, bool walkable, Point sc)
    {
        WorldPosition = worldPos;
        Walkable = walkable;
        ScreenCoords = sc;
    }
}
