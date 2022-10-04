using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static Node[] nodes;
        static int currentNodeId;

        static Queue<int> allPosibleMoveNode = new Queue<int>();

        static void Main(string[] args)
        {
            nodes = new Node[9];
            nodes[0] = new Node
            {
                nodeId = 0,
                connectedNode = new int[] { 1, 4, 2 }
            };
            nodes[1] = new Node
            {
                nodeId = 1,
                connectedNode = new int[] { 0, 3, 4 }
            };
            nodes[2] = new Node
            {
                nodeId = 2,
                connectedNode = new int[] { 0, 3, 5, 7, 8 }
            };
            nodes[3] = new Node
            {
                nodeId = 3,
                connectedNode = new int[] { 1, 2, 6, 8 }
            };
            nodes[4] = new Node
            {
                nodeId = 4,
                connectedNode = new int[] { 0, 1 }
            };
            nodes[5] = new Node
            {
                nodeId = 5,
                connectedNode = new int[] { 2, 7 }
            };
            nodes[6] = new Node
            {
                nodeId = 6,
                connectedNode = new int[] { 3, 8 }
            };
            nodes[7] = new Node
            {
                nodeId = 7,
                connectedNode = new int[] { 2, 5, 8 }
            };
            nodes[8] = new Node
            {
                nodeId = 8,
                connectedNode = new int[] { 2, 3, 7 }
            };

            #region 01

            Console.Write("Current node ID = ");
            currentNodeId = int.Parse(Console.ReadLine());

            Console.Write("Rool = ");
            var count = Console.ReadLine();

            if (!int.TryParse(count, out var moveCount))
            {
                Console.WriteLine("Can't try parse 'Rool'");
                goto end;
            }

            var listPossible = new LinkedList<int>();
            var listParent = new LinkedList<int>();
            var pathList = new List<NodePath>();

            var nodeList = new List<Node>(nodes); //แปลง nodes เป็น List
            var nodeIndex = nodeList.FindIndex(x => x.nodeId == currentNodeId); //เอา List มาหา Index ของ nodes ที่เลือกไว้

            var currentNode = nodes[nodeIndex];
            allPosibleMoveNode.Enqueue(currentNode.nodeId); //Node ทั้งหมดที่สามารถเดินไปได้

            listPossible.AddLast(currentNode.nodeId);

            var parentNode = new Queue<int>();
            parentNode.Enqueue(-1); //Node ก่อนหน้านี้ (parent node) ที่ใส่ -1 ตอนแรกเพราะ node มันไม่ได้เอามาจากใคร (currentNode ตอนนี้เป็นตัวแรก)
            listParent.AddLast(-1);

            var distanceOfEachNode = new Queue<int>();
            distanceOfEachNode.Enqueue(moveCount); //บอกจำนวนที่จะต้องเดินใน Queue ของ allPosileMoveNode ว่าเหลืออีกกี่รอบ

            var path = new List<NodePath>();

            for (int i = 0; i < currentNode.connectedNode.Length; i++)
            {
                var tempPath = new NodePath(currentNode.nodeId);
                path.Add(tempPath);
            }

            var tempNodeIndex = 0;
            var tempNode = new int[0];

            #region 01
            /*
            while (moveCount > 0)
            {
                while (distanceOfEachNode.First() == moveCount) //check ว่าจำนวนที่จะต้องเดินของ Node ตัวแรก = moveCount หรือไม่
                {
                    tempNodeIndex = nodeList.FindIndex(x => x.nodeId == allPosibleMoveNode.First());
                    tempNode = nodes[tempNodeIndex].connectedNode; //หา Connect node ของ Node ตัวแรกใน Queue ว่า Connect กับ Node ไหนบ้าง
                    for (int i = 0; i < tempNode.Length; i++) //loop Connect node ทั้งหมด
                    {
                        if (tempNode[i] == parentNode.First()) //check ว่า connect node ตอนนี้เป็น node parent หรือไม่
                        {
                            continue;
                        }
                        //if (allPosibleMoveNode.Contains(tempNode[i]))
                        //{
                        //    continue;
                        //}
                        allPosibleMoveNode.Enqueue(tempNode[i]); //แล้วเอามาใส่ใน allPosibleMoveNode
                        distanceOfEachNode.Enqueue(moveCount - 1); //ใส่จำนวนการเดินที่เหลือของ Node
                        parentNode.Enqueue(allPosibleMoveNode.First()); //ใส่ parent node ว่าเอามาจาก node ไหน
                    }
                    allPosibleMoveNode.Dequeue(); //เอา node ตัวแรกที่ได้ connect node มาแล้วออก (เพราะได้ข้อมูลครบแล้ว)
                    distanceOfEachNode.Dequeue(); //เอาจำนวนการเดินของ node ตัวแรกที่ได้ connect node มาแล้วออก (เพราะได้ข้อมูลครบแล้ว)
                    parentNode.Dequeue(); //เอา parent node ตัวแรกออก (เพราะได้ข้อมูลครบแล้ว)
                }
                moveCount--;
            }
            */
            #endregion


            var nodeAdd = 0;
            var listPathIndex = 0;

            while (moveCount > 0)
            {
                nodeAdd = 0;
                while (distanceOfEachNode.First() == moveCount) //check ว่าจำนวนที่จะต้องเดินของ Node ตัวแรก = moveCount หรือไม่
                {
                    tempNodeIndex = nodeList.FindIndex(x => x.nodeId == listPossible.ElementAt(listPathIndex));
                    tempNode = nodes[tempNodeIndex].connectedNode; //หา Connect node ของ Node ตัวแรกใน Queue ว่า Connect กับ Node ไหนบ้าง

                    for (int i = 0; i < tempNode.Length; i++) //loop Connect node ทั้งหมด
                    {
                        if (tempNode[i] == parentNode.First()) //check ว่า connect node ตอนนี้เป็น node parent หรือไม่
                        {
                            continue;
                        }

                        allPosibleMoveNode.Enqueue(tempNode[i]); //แล้วเอามาใส่ใน allPosibleMoveNode
                        distanceOfEachNode.Enqueue(moveCount - 1); //ใส่จำนวนการเดินที่เหลือของ Node
                        parentNode.Enqueue(allPosibleMoveNode.First()); //ใส่ parent node ว่าเอามาจาก node ไหน

                        listPossible.AddLast(tempNode[i]);
                        listParent.AddLast(listPossible.ElementAt(listPathIndex));
                        nodeAdd++;
                    }

                    var node = allPosibleMoveNode.Dequeue(); //เอา node ตัวแรกที่ได้ connect node มาแล้วออก (เพราะได้ข้อมูลครบแล้ว)
                    distanceOfEachNode.Dequeue(); //เอาจำนวนการเดินของ node ตัวแรกที่ได้ connect node มาแล้วออก (เพราะได้ข้อมูลครบแล้ว)
                    var parent = parentNode.Dequeue(); //เอา parent node ตัวแรกออก (เพราะได้ข้อมูลครบแล้ว)

                    listPathIndex++;
                }
                moveCount--;
            }

            path.Count();

            var tempCount = allPosibleMoveNode.Count;
            Console.Write($"All posible move node = ");

            var ListOfPosibleNode = new List<int>();

            for (int i = 0; i < tempCount; i++)
            {
                if (allPosibleMoveNode.Count <= 0)
                {
                    break;
                }

                while (ListOfPosibleNode.Contains(allPosibleMoveNode.First()) || allPosibleMoveNode.First().Equals(currentNodeId))
                {
                    allPosibleMoveNode.Dequeue();

                    if (allPosibleMoveNode.Count <= 0)
                    {
                        break;
                    }
                }

                if (allPosibleMoveNode.Count <= 0)
                {
                    break;
                }

                ListOfPosibleNode.Add(allPosibleMoveNode.Dequeue());

                if (allPosibleMoveNode.Count <= 0)
                {
                    break;
                }
            }

            for (int i = 0; i < ListOfPosibleNode.Count; i++)
            {
                Console.Write($"{ListOfPosibleNode[i]}, ");
            }

            var tempPathCount = path.Count;

            var ListOfPath = new List<int>();

            Console.WriteLine();
            Console.Write("Path = ");

            for (int i = 0; i < tempPathCount; i++)
            {
                for (int x = 0; x < path[i].path.Count; x++)
                {
                    Console.Write($"{path[i].path[x]}, ");
                }
                Console.WriteLine();
            }
        #endregion

        end:
            Console.WriteLine();
            Console.WriteLine("-- End --");
            Console.ReadLine();
        }
    }

    internal class Node
    {
        public int nodeId;
        public int[] connectedNode;
    }

    internal class NodePath
    {
        public NodePath(int nodeId)
        {
            path.Add(nodeId);
        }
        public NodePath(List<int> nodes)
        {
            path = new List<int>(nodes);
        }

        public List<int> path = new List<int>();
    }
}