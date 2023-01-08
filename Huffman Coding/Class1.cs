using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class Node
    {
        public double Probability { get; set; }
        public char Symbol { get; set; }
        public Node? Left { get; set; }
        public Node? Right { get; set; }

        public Node(char symbol, double probability)
        {
            Probability = probability;
            Symbol = symbol; 
            Left = null;
            Right = null;
        }
    }

