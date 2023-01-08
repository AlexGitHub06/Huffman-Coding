using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HuffmanCoding
{
    static class Codes //probably not ideal to store as global but passing list through recursion is not nice
    {
        public static Dictionary<char,int> codes = new();
    }
    internal class Program
    {
        static void Main()
        {
            //string text = GenerateText(10);
            //string text = Console.ReadLine();
            string text = "MISSISSIPPI";
            Console.WriteLine($"message: {text}");
            PriorityQueue<Node,double> pQueue=  GenerateQueue(text);
            Node rootNode = GenerateHuffmanTree(pQueue);
            //Console.WriteLine(rootNode.Probability);

            TraverseTree(rootNode, text.Length, "");
            PrintStats(Codes.codes, text);
        }

        public static string GenerateText(int length)
        {
            StringBuilder sb = new StringBuilder();
            Random rand = new Random();
            int randN;

            for (int i = 0; i < length; i++)
            {
                randN = rand.Next(0,26);
                char c = Convert.ToChar(randN + 65);
                sb.Append(c);
            }

            return sb.ToString();
        }

        public static PriorityQueue<Node,double> GenerateQueue(string text)
        {
            Dictionary<char,int> dict = new Dictionary<char,int>();

            foreach (char c in text)
            {

                if (!dict.ContainsKey(c))
                {
                    dict.Add(c, 1);
                }

                else
                {
                    dict[c]++;
                }
            }

            PriorityQueue<Node, double> pQueue = new PriorityQueue<Node, double>();

            foreach(char key in dict.Keys)
            {
                Node curNode = new Node(key, (double)dict[key] / text.Length);
                pQueue.Enqueue(curNode, curNode.Probability);
            }

            return pQueue;
        }

        public static Node? GenerateHuffmanTree(PriorityQueue<Node, double> pQueue) //remove ?
        {

            while(pQueue.Count != 1) //what value to asign connecting nodes
            {
                pQueue.TryDequeue(out Node? n1, out double p1);
                pQueue.TryDequeue(out Node? n2, out double p2);

                Node curNode = new Node('?', p1 + p2);
                curNode.Left = n1;
                curNode.Right = n2;

                pQueue.Enqueue(curNode, curNode.Probability);

            }

            Node rootNode = pQueue.Dequeue();

            return rootNode;
        }   

        public static void TraverseTree(Node node, int len, string path)
        {
            if(node.Left == null)
            {
                Console.WriteLine($"Symbol: {node.Symbol}, Occurances: {len * node.Probability}, Code: {path}");
                Codes.codes[node.Symbol] = path.Length;
            }

            else
            {
                TraverseTree(node.Left, len, path+'0');
                TraverseTree(node.Right, len, path+'1');
            }            
        }

        public static void PrintStats(Dictionary<char, int> codes, string text)
        {
            double avgEach = 0;
            int count;

            foreach (KeyValuePair<char, int> entry in codes)
            {
                count = text.Count(x => x == entry.Key);
                avgEach += entry.Value * (count / (double)text.Length);
            }

            Console.WriteLine($"\nAverage bits per symbol: {Math.Round(avgEach, 2)}");
            Console.WriteLine($"Bits to store compressed text: {avgEach * text.Length} (EXCLUDING TREE DATA)");
            Console.WriteLine($"Bits to store original text assuming each symbol 8 bits: {text.Length * 8}");
            Console.WriteLine($"Percentage of original: {Math.Round(100 * (avgEach * text.Length) / (text.Length * 8),2)} (EXCLUDING TREE DATA)");
        }

        
        
    }

    
}