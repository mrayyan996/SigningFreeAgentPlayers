﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigningFreeAgentPlayers
{
    class Program
    {
        static Player[,] p_;
        static void Main(string[] args)
        {

            Player[,] p = CreatePlayers(); // get Set of Players Created by helper Function.
            FreeAgentPlayers(p, 2, 2, 5); // needs to create p
            Console.ReadLine();
        }


        static public Player[,] CreatePlayers(){

            // create 2 players on each position.
            // p11 p12 on 1th Position
            // p21 p22 on 2nd Position

            Player p11, p12, p21, p22; 
            p11 = new Player("Ramiz", 1,2);
            p12 = new Player("shanza", 1, 1);
            p21 = new Player("ali", 1, 1);
            p22 = new Player("rayyan", 1, 1);
            Player[,] p = { {null,null,null},{null,p11,p12},{null,p21,p22}}; // ZERO INDEX NEEDs TO BE EMPTY
            return p;
        }


        static public void CostVorpOfAllPlayers(Player[,] p,int N,int P) {

            p_ = new Player[N+1, P+1]; 
            
            for (int i = 1; i < p.GetLength(1); i++) {
                int sum = 0;
                int max = Int32.MinValue;
                for (int j = 1;  j < p.GetLength(0); j++) {
                   p_[i, j] = new Player();
                    sum = sum + p[i, j].cost;
                   if (p[i, j].vorp > max) {
                        max = p[i,j].vorp;
                       
                    }
                    p_[i, j].vorp = max;// max vorp at i position
                    p_[i, j].cost = sum; // total cost at i position
                }
               
            }
        }

        static public void FreeAgentPlayers(Player[,] p, int N, int P, int X)
        {

            //p = set of players, N = number of players , P = players availble for each positions , X = Total Budget.
            int[,] v = new int[N+1,X+1]; // Create new table (2d array) for mainting max for at price cap
            int[,] who = new int[N+1, X+1];  // Create new table (2d array) for mainting min cost for at price cap
            CostVorpOfAllPlayers(p,N,P);


            for (int x =0; x<=X; x++) {
                who[N, x] = 0;
                v[N, x] = Int32.MinValue;
                for (int k = 1; k <= P; k++) {
                   if (p_[N, k].cost <= X && p_[N, k].vorp > v[N, x])
                    {
                        v[N, x] = p[N, k].vorp;
                        who[N, x] = k;
                    }
                }
            }

            for (int i = N-1; i >= 1; i--) {
                for (int x = 0; x <= X; x++) {
                    v[i, x] = v[i+1, x];
                    who[i, x] = 0;
                    for (int k = 1; k <= P; k++) {
                        Console.WriteLine("p "+i+","+k+" c:"+p_[i,k].cost);
                        if (p_[i, k].cost <= x && v[i + 1, x - p_[i, k].cost] + p_[i, k].vorp > v[i, x]) {
                            v[i, x] = v[i + 1, x - p_[i, k].cost] + p_[i, k].vorp;
                            who[i, x] = k;

                        }
                      
                    }
                }
            }

            Console.WriteLine("The maximum value of VORP is " + v[1, X]);
            int amt = X;
            for (int i = 1; i<=N; i++){
                int k = who[i, amt];
                if (k != 0)
                {
                    Console.WriteLine("sign player " + p[i, k]);
                    amt = amt - p_[i, k].cost;
                }
                   
            }

            Console.WriteLine("The total money spent is "+ (X - amt));
        }

        static public void printArray(int[,] arr)
        {

            var rowCount = arr.GetLength(0);
            var colCount = arr.GetLength(1);
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount; col++)
                    Console.Write(String.Format("{0}\t", arr[row, col]));
                Console.WriteLine();
            }
        }
        static public void printArray(Player[,] arr)
        {

            var rowCount = arr.GetLength(0);
            var colCount = arr.GetLength(1);
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount; col++)
                    Console.Write(String.Format("{0}\t", (arr[row, col] ==null? "x":arr[row, col].ToString() )));
                Console.WriteLine();
            }
        }
    }




}
