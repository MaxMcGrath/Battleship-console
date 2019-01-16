﻿using System;

namespace helloNet
{
    class Program
    {
        static char[,] gamePlayerBoard;
        static char[,] gamePlayerAtkBoard;
        static char[,] gameAiBoard;
        static char[] gamePieces = new char[] {'x','a','b','c','d','e'};

        static void Main(string[] args)
        {
            setUpNewGame();

            playerAttack();
            
        }

        static void setUpNewGame(){
            gamePlayerAtkBoard = setUpBoardData(gamePlayerAtkBoard);

            gameAiBoard = setUpBoardData(gameAiBoard);
            gameAiBoard = setUpRandomShip(gameAiBoard);

            bool userCheckBoard=true;
            do{
                userCheckBoard=true;
                gamePlayerBoard = setUpBoardData(gamePlayerBoard);
                gamePlayerBoard = setUpRandomShip(gamePlayerBoard);
                drawGameBoard();

                Console.WriteLine("Are you okay with the random ship placement? y/n");
                String userInputBoard = Console.ReadLine().ToLower();
                if(userInputBoard[0]!='y'){
                    userCheckBoard=false;
                }

            } while(!userCheckBoard);
        }

        static void drawGameBoard(){
            Console.Clear();
            Console.WriteLine("Battleship");
            Console.WriteLine("            AI Board                            Player Board");

            for(int x=0;x<10;++x){
                    Console.Write("  "+x);
            }
            Console.Write("         ");
            for(int x=0;x<10;++x){
                    Console.Write("  "+x);
            }

            char[] letterOnSide = {'A','B','C','D','E','F','G','H','I','J'};

            Console.WriteLine("");

            for(int x=0;x<10;++x){
                Console.Write(letterOnSide[x]+" ");
                for(int y=0;y<10;++y){
                    Console.Write(gameAiBoard[x,y]+"  ");
                }

                Console.Write("       " + letterOnSide[x]+" ");
                for(int y=0;y<10;++y){
                    Console.Write(gamePlayerBoard[x,y]+"  ");
                }

                Console.WriteLine("");
            }
        }

        static char[,] setUpBoardData(char[,] _board){
            _board = new char[10,10];

            for(int x=0;x<_board.GetLength(0);++x){
                for(int y=0;y<_board.GetLength(1);++y){
                    _board[x,y]='O';
                }
            }
            return _board;
        }

        static char[,] setUpRandomShip(char[,] _board){
            for(int x=0;x<gamePieces.Length;++x){
                _board=pickShipLocation(_board,x);
            }
            return _board;
        }

        static int getRandomNum(){
            Random random = new Random();
            return random.Next(0, 9);
        }

        static int[] getAiCords(){
            return new int[] {getRandomNum(),getRandomNum()};
        }

        static bool checkShipPlacePath(int[] _pickCords, char[,] _board, int _shipSize, bool shipDir){
            bool checkPath=true;

            if(_pickCords[0] + _shipSize > 9){
                return false;
            }
            if(_pickCords[1] + _shipSize > 9){
                return false;
            }

            if(shipDir){//horizontal
                for(int x=0;x<_shipSize;++x){
                    if(_board[_pickCords[0],_pickCords[1]+x] != 'O'){
                        return false;
                    }
                }
            } else {//vertical
                for(int x=0;x<_shipSize;++x){
                    if(_board[_pickCords[0]+x,_pickCords[1]] != 'O'){
                        return false;
                    }
                }
            }

            return checkPath;
        }

        static char[,] pickShipLocation(char[,] _gameBoard,int shipSize){
            bool checkCords=true;
            int[] pickCords;

            do{
                checkCords=true;
                pickCords = getAiCords();

                if(getRandomNum()>4){//horizontal
                    if(checkShipPlacePath(pickCords,_gameBoard, shipSize, true)){
                        for(int x=0;x<shipSize;++x){
                            _gameBoard[pickCords[0],pickCords[1]+x]=gamePieces[shipSize];
                        }
                    } else {
                        checkCords=false;
                    }

                } else {//vertical
                    if(checkShipPlacePath(pickCords,_gameBoard, shipSize, false)){
                        for(int x=0;x<shipSize;++x){
                            _gameBoard[pickCords[0]+x,pickCords[1]]=gamePieces[shipSize];
                        }
                    } else {
                        checkCords=false;
                    }
                }
        
            } while (!checkCords); 

            return _gameBoard;
        }

        static void playerAttack(){
            int[] userAtkCord;

            bool checkUserInput=true;
            do{
                Console.WriteLine("Time to Attack! Please enter attack coordinates. Example=C6");
                userAtkCord = getUserCords();
                gameAiBoard[userAtkCord[0],userAtkCord[1]]='X';
                drawGameBoard();
            } while (checkUserInput);
 
        }

        static int[] getUserCords(){
            int verLoca=0;
            int horLoca=0;
            bool checkInput=true;
            do{
                checkInput=true;
                String startLocation = Console.ReadLine().ToLower();
                verLoca = (int)startLocation[0] - 97;

                if(verLoca<0 || verLoca>9){
                    checkInput=false;
                }

                horLoca = (int)Char.GetNumericValue(startLocation[1]);
                if(horLoca<0 || horLoca>9){
                    checkInput=false;
                }

                if(!checkInput){
                    Console.WriteLine("Error with input: please try again.");
                }
                
            } while (!checkInput);

            return new int[] {verLoca,horLoca};
        }
    }
}