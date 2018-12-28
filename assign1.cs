
/// *    Note


// Compiler used -  C# compiler (csc)

//steps to execute program :

// 1 . Go to https://repl.it/repls/TanGorgeousBuck

// 2.  Copy and paste all source code from assign1.cs

// 3. developer: tushar borchate/ 2003931216 /306-529-7874
// This code contains all comments describing logic.

//4. please clear all code then copy and paste

//5. Try to use simple inputs :P
 
//6.Last update : 27/01/18 16 33
//////////////////////////////////////////////////////////////////////////

//  code starts here ////////


using System;
using System.Collections.Generic;
using System.Linq;

namespace Strip8Puzzle
{
    public class Program
    {
        static void Main(string[] args)
        {
            //program starts here
            Console.WriteLine("Assignment No 1 : Strips applied to the 8-puzzle");
            Console.WriteLine(Environment.NewLine);

            //create new object of Strip8Puzzle class
            Strip8Puzzle strip8Puzzle = new Strip8Puzzle();

            //get input from user for inital block   true for initial input
            strip8Puzzle.GetPuzzleConfiguration(true);

            //get input from user for goal block   false for goal input
            strip8Puzzle.GetPuzzleConfiguration(false);

            //move empty tile if greater than 7
            strip8Puzzle.ChangeGoalEmptyTile();


            //we will set numbers layerwise from top to bottom 
            //setLayer
            strip8Puzzle.SetLayer(1);
            strip8Puzzle.SetLayer(2);

            //move empty tile to its original position
            strip8Puzzle.SetGoalEmptyTile();

            //display puzzle result
            strip8Puzzle.PuzzleResult();

            Console.Read();
        }
    }

    // class containing logic 
    public class Strip8Puzzle
    {
        #region common initial + goal Config + display

        // multidimensional array to store initial block 
        int[,] intialConfig = new int[3, 3];

        // list of int to original initial block 
        List<int> listoforiginalInitialNumbers = new List<int>();


        // list of int to manipulate initial block 
        List<int> listofInitialNumbers = new List<int>();

        // multidimensional array to store goal block 
        int[,] goalConfig = new int[3, 3];

        // list of int to original initial block 
        List<int> listoforiginalGoalNumbers = new List<int>();


        // list of int to manipulate goal block 
        List<int> listofGoalNumbers = new List<int>();

        //to track no of moves
        int moveNo = 0;

        //puzzle input
        public void GetPuzzleConfiguration(bool isInitial)
        {
            //multidimensional array for local scope 
            int[,] commonConfig = new int[3, 3];

            // list of int to manipulate  block 
            List<int> listofNumbers = new List<int>();

            try
            {
                Console.Write(Environment.NewLine);
                for (int i = 0; i <= 2; i++)
                {
                    for (int j = 0; j <= 2; j++)
                    {
                        // this line will be change dynamically according to inital / goal input
                        Console.WriteLine(string.Format("please enter {1} value for block No: {0}", listofNumbers.Count() + 1, isInitial ? "Initial" : "Goal"));
                        try
                        {
                            commonConfig[i, j] = Convert.ToInt32(Console.ReadLine());
                            //check if number follows rules otherwise ask input for same block again.
                            if (!IsContain(commonConfig[i, j], listofNumbers) && Islessthan8(commonConfig[i, j]))
                            {
                                listofNumbers.Add(commonConfig[i, j]);
                            }
                            else
                            {
                                j = j - 1;
                            }

                        }
                        catch (Exception ex)
                        {
                            //no should be between 0-8
                            Console.WriteLine("Please enter valid value between 0-8.");
                            j = j - 1;

                        }


                    }
                }

                //put local variable values to global variable accroding to input (initial/goal)
                if (isInitial)
                {
                    intialConfig = commonConfig;
                    listofInitialNumbers = listofNumbers;
                    foreach (var item in listofNumbers)
                    {
                        listoforiginalInitialNumbers.Add(item);
                    }
                    DisplayPuzzle(true, listofNumbers);
                }
                else
                {
                    goalConfig = commonConfig;
                    listofGoalNumbers = listofNumbers;
                    foreach (var item in listofNumbers)
                    {
                        listoforiginalGoalNumbers.Add(item);
                    }
                    DisplayPuzzle(false, listofNumbers);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured.." + ex.ToString());
            }
        }

        //puzzle result
        public void PuzzleResult()
        {
            if (CheckInitialBlockMatchesGoalBlock(3))
            {
                Console.WriteLine("Third layer of puzzle has been solved.");
                Console.WriteLine(string.Format("Puzzle has been solved after {0} moves.", moveNo));

            }
            else
            {
                Random r = new Random();
                int rInt = r.Next(1, 3); //for ints
                while (!CheckInitialBlockMatchesGoalBlock(2) 
                    &&!CheckInitialBlockMatchesGoalBlock(3))
                {
                    rInt = r.Next(1, 3);
                    switch (rInt)
                    {
                        case 1:
                            DynamicMoveBlockAntiClockWise(3, 5, 7, 8);
                            DisplayPuzzle(listofInitialNumbers);
                            break;

                        case 2:
                            DynamicMoveBlockAntiClockWise(3, 4, 6, 7);
                            DisplayPuzzle(listofInitialNumbers);
                            break;

                        case 3:
                            DynamicMoveBlockAntiClockWise(4, 5, 7, 8);
                            DisplayPuzzle(listofInitialNumbers);
                            break;

                        default:
                            DynamicMoveBlockAntiClockWise(3, 4, 6, 7);
                            DisplayPuzzle(listofInitialNumbers);

                            break;
                    }

                }
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("First two layers has been solved.");
                Console.WriteLine("please try to change input.To solve third layer puzzle will go infinite loop.");
                Console.WriteLine(Environment.NewLine);
              }
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("your input : ");
            DisplayPuzzle(false, listoforiginalInitialNumbers);



            Console.WriteLine("your goal : ");
            DisplayPuzzle(false, listoforiginalGoalNumbers);


            Console.WriteLine("puzzle current state : ");
            DisplayPuzzle(false, listofInitialNumbers);

        }

        #region input rules
        public bool IsContain(int number, List<int> listofNumbers)
        {
            try
            {

                if (listofNumbers.Contains(number))
                {

                    Console.WriteLine(string.Format("--Alert-- Value {0} is already entered in puzzle", number));
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured.." + ex.ToString());
                return false;
            }

        }
        public bool Islessthan8(int number)
        {
            try
            {
                if (number > 8)
                {
                    Console.WriteLine(string.Format("--Alert-- Value {0} is greater than 8 ", number));
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        //display puzzle values reusable method for initial/goal/solution
        public void DisplayPuzzle(bool isInitial, List<int> listofNumbers)
        {
            if (moveNo == 0)
            {
                Console.WriteLine(string.Format("{0} configuration set.", isInitial ? "Initial" : "Goal"));
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("----------------------------------------------------");
            }
            int count = 0;
            foreach (var item in listofNumbers)
            {
                if (count % 3 == 0 && count != 0)
                {
                    Console.WriteLine(" ");
                    Console.Write(string.Format("|{0}|", item));
                }
                else
                {
                    Console.Write(string.Format("|{0}|", item));
                }


                count++;
            }
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("----------------------------------------------------");

        }


        //change goal if 0 position is greater than 7
        public void ChangeGoalEmptyTile()
        {
            try
            {
                int emptytileposition = listofGoalNumbers.IndexOf(0);
                if (emptytileposition != 8)
                {
                    while (emptytileposition != 8)
                    {
                        int value = 0;
                        value = emptytileposition + 3 <= 8 ? 3 : 1;
                        // Console.WriteLine("moving empty tile to 8 position");
                        listofGoalNumbers[emptytileposition] = listofGoalNumbers[emptytileposition + value];
                        listofGoalNumbers[emptytileposition + value] = 0;
                        emptytileposition = listofGoalNumbers.IndexOf(0);

                    }
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        //change goal if 0 position is greater than 7
        public void SetGoalEmptyTile()
        {
            try
            {
                int emptytileposition = listofInitialNumbers.IndexOf(0);
                int originalemptytileposition = listoforiginalGoalNumbers.IndexOf(0);
                if (emptytileposition != originalemptytileposition)
                {
                    while (emptytileposition != listoforiginalGoalNumbers.IndexOf(0))
                    {
                        int value = 0;
                        value = (emptytileposition - originalemptytileposition) % 3 == 0 ? 3 : 1;
                        Console.WriteLine("moving empty tile to original position");
                        listofInitialNumbers[emptytileposition] = listofInitialNumbers[emptytileposition - value];
                        listofInitialNumbers[emptytileposition - value] = 0;
                        emptytileposition = listofInitialNumbers.IndexOf(0);
                        DisplayPuzzle(true, listofInitialNumbers);
                        moveNo = moveNo + 1;
                    }
                }

            }
            catch (Exception)
            {

                Console.WriteLine("error occured in setgoalemptytile");
            }

        }


        #endregion

        #region puzzle solution 

        public void SetLayer(int LayerNo)
        {
            try
            {
                //get position of empty tile
                int emptyTilePosition = GetPositionOfEmptyTile();

                //check if initial and goal match
                bool layerresult = CheckInitialBlockMatchesGoalBlock(LayerNo);

                //if not start solution
                if (!layerresult)
                {
                    //check nos are available in required layer
                    bool result = CheckBlockAvailableinLayer(LayerNo);
                    //move all values anticlockwisely until required nos not get into required layer
                    while (!result)
                    {
                        MoveAnticlockWiseAllLayer();
                        Console.WriteLine(string.Format("Puzzle state after {0} move : ", moveNo));
                        DisplayPuzzle(true, listofInitialNumbers);
                        result = CheckBlockAvailableinLayer(LayerNo);
                    }
                    if (result)
                    {
                        Console.WriteLine("puzzle current state");
                        DisplayPuzzle(true, listofInitialNumbers);
                        Console.WriteLine("Stop Moving . going inside layer no : " + LayerNo);
                    }
                }
                ///solution part1 has over.


                //solution part2 started.
                bool resultGoalMatch = false;


                #region layer1 solution
                //lets move all tiles from 1st layer to check if they meet goal
                if (LayerNo == 1)
                {


                    resultGoalMatch = CheckInitialBlockMatchesGoalBlock(1);
                    if (resultGoalMatch)
                    {
                        Console.WriteLine("First layer of puzzle solved.");
                        DisplayPuzzle(true, listofInitialNumbers);
                    }


                    //blocks are not in order but minimum two blocks are in one grid
                    //break layer in two grids
                    if (!resultGoalMatch)
                    {
                        //solution part 2.1

                        //check if all three blocks are in one grid

                        //lets sort them and put them in different grid

                        bool checkthreeblocksinonegrid = CheckthreeblockinGrid(1, 0, 1, 3, 4);

                        // if yes lets check if they are sorted
                        if (checkthreeblocksinonegrid)
                        {
                            //blocks in order move them until get result
                            for (int i = 0; i < 30; i++)
                            {
                                DynamicMoveBlockAntiClockWise(0, 2, 3, 5);
                                if (CheckInitialBlockMatchesGoalBlock(1))
                                {
                                    resultGoalMatch = true;
                                    break;
                                }
                            }

                        }
                        else
                        {
                            //check in second grid
                            bool checkthreeblocksinsecondgrid = CheckthreeblockinGrid(1, 1, 2, 4, 5);

                            // if yes lets check if they are sorted
                            if (checkthreeblocksinsecondgrid)
                            {
                                for (int i = 0; i < 30; i++)
                                {
                                    DynamicMoveBlockAntiClockWise(0, 2, 3, 5);
                                    if (CheckInitialBlockMatchesGoalBlock(1))
                                    {
                                        resultGoalMatch = true;
                                        break;
                                    }
                                }

                            }

                        }



                        if (!resultGoalMatch)
                        {


                            //if not got result yet lets go to other part 


                            //solution part 2.2
                            //check which grid has two alternative numbers
                            bool checkalternativeResultGrid1 = CheckAlternativeNumbersinGrid(1, 0, 1, 3, 4);

                            if (checkalternativeResultGrid1)
                            {


                                //solution part 2.2

                                //get value of middle block and move 
                                //set position 
                                int position = listofInitialNumbers.IndexOf(listofGoalNumbers[1]);

                                switch (position)
                                {
                                    case 2:
                                        int firstPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[0]);

                                        while (firstPosition != 1)
                                        {
                                            DynamicMoveBlockAntiClockWise(0, 1, 3, 4);
                                            firstPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[0]);
                                        }

                                        while (position != 0 || firstPosition != 3)
                                        {

                                            DynamicMoveBlockAntiClockWise(0, 2, 3, 5);
                                            firstPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[0]);
                                            position = listofInitialNumbers.IndexOf(listofGoalNumbers[1]);
                                        }
                                        int secondPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[2]);

                                        while (secondPosition != 1)
                                        {
                                            DynamicMoveBlockAntiClockWise(1, 2, 4, 5);
                                            secondPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[2]);

                                        }

                                        break;

                                    case 5:
                                        firstPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[2]);

                                        while (firstPosition != 4)
                                        {
                                            DynamicMoveBlockAntiClockWise(0, 1, 3, 4);
                                            firstPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[2]);
                                        }
                                        while (position != 2 || firstPosition != 5)
                                        {

                                            DynamicMoveBlockAntiClockWise(0, 2, 3, 5);
                                            firstPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[2]);
                                            position = listofInitialNumbers.IndexOf(listofGoalNumbers[1]);
                                        }
                                        secondPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[0]);

                                        while (secondPosition != 1)
                                        {
                                            DynamicMoveBlockAntiClockWise(0, 1, 3, 4);
                                            secondPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[0]);

                                        }


                                        break;


                                    default:
                                        break;
                                }

                            }
                            else
                            {
                                //same logic for grid2
                                bool checkalternativeResultGrid2 = CheckAlternativeNumbersinGrid(1, 1, 2, 4, 5);
                                while (!checkalternativeResultGrid2)
                                {
                                    DynamicMoveBlockAntiClockWise(0, 2, 3, 5);
                                    checkalternativeResultGrid2 = CheckAlternativeNumbersinGrid(1, 1, 2, 4, 5);
                                }
                                if (checkalternativeResultGrid2)
                                {


                                    //solution part 2.2

                                    //get value of middle block and move 

                                    int position = listofInitialNumbers.IndexOf(listofGoalNumbers[1]);

                                    switch (position)
                                    {
                                        case 0:
                                            int firstPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[2]);

                                            while (firstPosition != 1)
                                            {
                                                DynamicMoveBlockAntiClockWise(1, 2, 4, 5);
                                                firstPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[2]);
                                            }

                                            while (position != 2 || firstPosition != 5)
                                            {

                                                DynamicMoveBlockAntiClockWise(0, 2, 3, 5);
                                                firstPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[2]);
                                                position = listofInitialNumbers.IndexOf(listofGoalNumbers[1]);
                                            }
                                            int secondPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[0]);

                                            while (secondPosition != 1)
                                            {
                                                DynamicMoveBlockAntiClockWise(0, 1, 3, 4);
                                                secondPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[0]);

                                            }

                                            break;

                                        case 3:
                                            firstPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[0]);

                                            while (firstPosition != 4)
                                            {
                                                DynamicMoveBlockAntiClockWise(1, 2, 4, 5);
                                                firstPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[0]);
                                            }

                                            while (position != 5 || firstPosition != 2)
                                            {

                                                DynamicMoveBlockAntiClockWise(0, 2, 3, 5);
                                                firstPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[0]);
                                                position = listofInitialNumbers.IndexOf(listofGoalNumbers[1]);
                                            }
                                            secondPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[2]);

                                            while (secondPosition != 4)
                                            {
                                                DynamicMoveBlockAntiClockWise(0, 1, 3, 4);
                                                secondPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[2]);

                                            }


                                            break;


                                        default:
                                            break;
                                    }

                                }
                            }


                            //move blocks until we get result
                            bool firstLayerResult = CheckInitialBlockMatchesGoalBlock(1);
                            while (!firstLayerResult)
                            {
                                DynamicMoveBlockAntiClockWise(0, 2, 3, 5);
                                firstLayerResult = CheckInitialBlockMatchesGoalBlock(1);
                            }



                        }
                    }
                    Console.WriteLine("First layer of puzzle solved.");

                }

                #endregion


                //layer2 solution


                #region layer2 solution
                //lets move all tiles from 1st layer to check if they meet goal
                if (LayerNo == 2)
                {


                    resultGoalMatch = CheckInitialBlockMatchesGoalBlock(2);
                    if (resultGoalMatch)
                    {
                        Console.WriteLine("second layer of puzzle solved.");
                        DisplayPuzzle(true, listofInitialNumbers);
                    }


                    //blocks are not in order but minimum two blocks are in one grid
                    //break layer in two grids
                    if (!resultGoalMatch)
                    {
                        //solution part 2.1

                        //check if all three blocks are in one grid

                        //lets sort them and put them in different grid

                        bool checkthreeblocksinonegrid = CheckthreeblockinGrid(2, 3, 4, 6, 7);

                        // if yes lets check if they are sorted
                        if (checkthreeblocksinonegrid)
                        {
                            for (int i = 0; i < 30; i++)
                            {
                                DynamicMoveBlockAntiClockWise(3, 5, 6, 8);
                                if (CheckInitialBlockMatchesGoalBlock(2))
                                {
                                    resultGoalMatch = true;
                                    break;
                                }
                            }

                        }
                        else
                        {
                            bool checkthreeblocksinsecondgrid = CheckthreeblockinGrid(2, 4, 5, 7, 8);

                            // if yes lets check if they are sorted
                            if (checkthreeblocksinsecondgrid)
                            {
                                for (int i = 0; i < 30; i++)
                                {
                                    DynamicMoveBlockAntiClockWise(3, 5, 6, 8);
                                    if (CheckInitialBlockMatchesGoalBlock(2))
                                    {
                                        resultGoalMatch = true;
                                        break;
                                    }
                                }

                            }

                        }



                        if (!resultGoalMatch)
                        {


                            //if not got result yet lets go to other part 


                            //solution part 2.2
                            //check which grid has two alternative numbers
                            bool checkalternativeResultGrid1 = CheckAlternativeNumbersinGrid(2, 3, 4, 6, 7);

                            if (checkalternativeResultGrid1)
                            {


                                //solution part 2.2

                                //get value of middle block and move 

                                int position = listofInitialNumbers.IndexOf(listofGoalNumbers[4]);

                                switch (position)
                                {
                                    case 5:
                                        int firstPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[3]);

                                        while (firstPosition != 4)
                                        {
                                            DynamicMoveBlockAntiClockWise(3, 4, 6, 7);
                                            firstPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[3]);
                                        }

                                        while (position != 3 || firstPosition != 6)
                                        {

                                            DynamicMoveBlockAntiClockWise(3, 5, 6, 8);
                                            firstPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[3]);
                                            position = listofInitialNumbers.IndexOf(listofGoalNumbers[4]);
                                        }
                                        int secondPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[5]);

                                        while (secondPosition != 4)
                                        {
                                            DynamicMoveBlockAntiClockWise(4, 5, 7, 8);
                                            secondPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[5]);
                                        }

                                        break;

                                    case 8:
                                        firstPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[5]);

                                        while (firstPosition != 7)
                                        {
                                            DynamicMoveBlockAntiClockWise(3, 4, 6, 7);
                                            firstPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[5]);
                                        }
                                        while (position != 5 || firstPosition != 8)
                                        {

                                            DynamicMoveBlockAntiClockWise(3, 5, 6, 8);
                                            firstPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[5]);
                                            position = listofInitialNumbers.IndexOf(listofGoalNumbers[4]);
                                        }
                                        secondPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[3]);

                                        while (secondPosition != 4)
                                        {
                                            DynamicMoveBlockAntiClockWise(3, 4, 6, 7);
                                            secondPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[3]);

                                        }


                                        break;


                                    default:
                                        break;
                                }



                            }
                            else
                            {
                                bool checkalternativeResultGrid2 = CheckAlternativeNumbersinGrid(2, 4, 5, 7, 8);
                                while (!checkalternativeResultGrid2)
                                {
                                    DynamicMoveBlockAntiClockWise(3, 5, 6, 8);
                                    checkalternativeResultGrid2 = CheckAlternativeNumbersinGrid(2, 4, 5, 7, 8);
                                }
                                if (checkalternativeResultGrid2)
                                {


                                    //solution part 2.2

                                    //get value of middle block and move 

                                    int position = listofInitialNumbers.IndexOf(listofGoalNumbers[4]);

                                    switch (position)
                                    {
                                        case 3:
                                            int firstPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[5]);

                                            while (firstPosition != 4)
                                            {
                                                DynamicMoveBlockAntiClockWise(4, 5, 7, 8);
                                                firstPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[5]);
                                            }

                                            while (position != 5 || firstPosition != 8)
                                            {

                                                DynamicMoveBlockAntiClockWise(3, 5, 7, 8);
                                                firstPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[5]);
                                                position = listofInitialNumbers.IndexOf(listofGoalNumbers[4]);
                                            }
                                            int secondPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[3]);

                                            while (secondPosition != 4)
                                            {
                                                DynamicMoveBlockAntiClockWise(3, 4, 6, 7);
                                                secondPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[3]);

                                            }

                                            break;

                                        case 6:
                                            firstPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[3]);

                                            while (firstPosition != 7)
                                            {
                                                DynamicMoveBlockAntiClockWise(4, 5, 7, 8);
                                                firstPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[3]);
                                            }

                                            while (position != 8 || firstPosition != 5)
                                            {

                                                DynamicMoveBlockAntiClockWise(3, 5, 6, 8);
                                                firstPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[3]);
                                                position = listofInitialNumbers.IndexOf(listofGoalNumbers[4]);
                                            }
                                            secondPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[5]);

                                            while (secondPosition != 7)
                                            {
                                                DynamicMoveBlockAntiClockWise(3, 4, 6, 7);
                                                secondPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[5]);

                                            }


                                            break;


                                        default:
                                            break;
                                    }

                                }
                            }


                            //move blocks until we get result
                            bool secondLayerResult = CheckInitialBlockMatchesGoalBlock(2);
                            while (!secondLayerResult)
                            {
                                DynamicMoveBlockAntiClockWise(3, 5, 6, 8);
                                secondLayerResult = CheckInitialBlockMatchesGoalBlock(2);
                            }



                        }
                    }
                    Console.WriteLine("second layer of puzzle solved.");

                }
                

                #endregion


            }
            catch (Exception ex)
            {


                Console.WriteLine("Error occured in SetLayer.. Layer No - " + LayerNo);
            }
        }


        public int GetPositionOfEmptyTile()
        {
            int emptyTilePosition = 0;
            try
            {
                //get position of empty tile
                emptyTilePosition = listofInitialNumbers.IndexOf(0);

            }
            catch (Exception ex)
            {

                Console.WriteLine("Error in getting position of empty tile");
            }
            return emptyTilePosition;

        }

        #region solution part1
        public bool CheckBlockAvailableinLayer(int layerNo)
        {
            //variable to store output
            bool result = false;

            int emptyTilePosition = 0;

            //check if  layer of inital config contains all nos from goal config
            List<int> initialblockNosList = new List<int>();
            List<int> goalblockNosList = new List<int>();

            try
            {
                emptyTilePosition = GetPositionOfEmptyTile();

                switch (layerNo)
                {
                    case 1:
                        initialblockNosList = listofInitialNumbers.Take(6).ToList();
                        goalblockNosList = listofGoalNumbers.Take(3).ToList();
                        break;
                    case 2:
                        initialblockNosList = listofInitialNumbers.Skip(3).Take(6).ToList();
                        goalblockNosList = listofGoalNumbers.Skip(3).Take(3).ToList();
                        break;

                    case 3:

                        break;

                    default:
                        break;
                }


                int count = 0;
                foreach (var item in goalblockNosList)
                {
                    if (initialblockNosList.Contains(item))
                    {
                        count++;
                    }
                }


                if (count != 3)
                {
                    result = false;
                }
                else
                {
                    if (emptyTilePosition > 5)
                    {
                        if (!goalblockNosList.Contains(listofInitialNumbers[emptyTilePosition - 3]))
                        {
                            Console.WriteLine(Environment.NewLine);
                            Console.WriteLine("Got no in required layer but empty tile is outside..Let's move it inside layer");
                            listofInitialNumbers[emptyTilePosition] = listofInitialNumbers[emptyTilePosition - 3];
                            listofInitialNumbers[emptyTilePosition - 3] = 0;
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }
                    }
                    else
                    {
                        result = true;
                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in check availability in layer");
            }
            return result;

        }

        public void MoveAnticlockWiseAllLayer()
        {
            try
            {
                int emptyTilePosition = GetPositionOfEmptyTile();
                int noToswipe = 0;
                if (emptyTilePosition == 0 || emptyTilePosition == 1)
                {
                    noToswipe = listofInitialNumbers[emptyTilePosition + 1];
                    listofInitialNumbers[emptyTilePosition] = noToswipe;
                    listofInitialNumbers[emptyTilePosition + 1] = 0;
                }
                if (emptyTilePosition == 2 || emptyTilePosition == 5)
                {
                    noToswipe = listofInitialNumbers[emptyTilePosition + 3];
                    listofInitialNumbers[emptyTilePosition] = noToswipe;
                    listofInitialNumbers[emptyTilePosition + 3] = 0;
                }
                if (emptyTilePosition == 3 || emptyTilePosition == 6)
                {
                    noToswipe = listofInitialNumbers[emptyTilePosition - 3];
                    listofInitialNumbers[emptyTilePosition] = noToswipe;
                    listofInitialNumbers[emptyTilePosition - 3] = 0;
                }
                if (emptyTilePosition == 8 || emptyTilePosition == 4 || emptyTilePosition == 7)
                {
                    noToswipe = listofInitialNumbers[emptyTilePosition - 1];
                    listofInitialNumbers[emptyTilePosition] = noToswipe;
                    listofInitialNumbers[emptyTilePosition - 1] = 0;
                }
                moveNo = moveNo + 1;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion


        #region solution part2

        //check if initial values matches with goal values layerwise
        public bool CheckInitialBlockMatchesGoalBlock(int layerNo)
        {
            //variable to store output
            bool result = false;

            int emptyTilePosition = 0;

            //check if  layer of inital config contains all nos from goal config
            List<int> initialblockNosList = new List<int>();
            List<int> goalblockNosList = new List<int>();

            try
            {
                emptyTilePosition = GetPositionOfEmptyTile();

                switch (layerNo)
                {
                    case 1:
                        initialblockNosList = listofInitialNumbers.Take(3).ToList();
                        goalblockNosList = listofGoalNumbers.Take(3).ToList();
                        break;
                    case 2:
                        initialblockNosList = listofInitialNumbers.Skip(3).Take(3).ToList();
                        goalblockNosList = listofGoalNumbers.Skip(3).Take(3).ToList();
                        break;

                    case 3:
                        initialblockNosList = listofInitialNumbers.Skip(6).Take(3).ToList();
                        goalblockNosList = listoforiginalGoalNumbers.Skip(6).Take(3).ToList();

                        break;

                    default:
                        break;
                }


                int count = 0;
                foreach (var item in goalblockNosList)
                {
                    if (initialblockNosList.Contains(item))
                    {
                        count++;
                    }
                }


                if (count != 3)
                {
                    result = false;
                }
                else
                {
                    result = true;
                }


                if (initialblockNosList[0] == goalblockNosList[0] && initialblockNosList[1] == goalblockNosList[1] && initialblockNosList[2] == goalblockNosList[2])
                {
                    result = true;
                }
                else
                {
                    result = false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in check initial block with goal ");
            }
            return result;

        }

        //move blocks
        public void DynamicMoveBlockAntiClockWise(int min1, int min2, int max1, int max2)
        {
            try
            {
                int emptyTilePosition = GetPositionOfEmptyTile();
                int noToswipe = 0;
                if (emptyTilePosition == min1)
                {
                    noToswipe = listofInitialNumbers[min1 + 1];
                    listofInitialNumbers[emptyTilePosition] = noToswipe;
                    listofInitialNumbers[min1 + 1] = 0;
                }
                if (emptyTilePosition == min2)
                {
                    noToswipe = listofInitialNumbers[min2 + 3];
                    listofInitialNumbers[emptyTilePosition] = noToswipe;
                    listofInitialNumbers[min2 + 3] = 0;
                }
                if (emptyTilePosition == max1)
                {
                    noToswipe = listofInitialNumbers[min1];
                    listofInitialNumbers[emptyTilePosition] = noToswipe;
                    listofInitialNumbers[min1] = 0;
                }
                if (emptyTilePosition == max2)
                {
                    noToswipe = listofInitialNumbers[max2 - 1];
                    listofInitialNumbers[emptyTilePosition] = noToswipe;
                    listofInitialNumbers[max2 - 1] = 0;
                }
                if (emptyTilePosition > min1 && emptyTilePosition < min2)
                {
                    noToswipe = listofInitialNumbers[emptyTilePosition + 1];
                    listofInitialNumbers[emptyTilePosition] = noToswipe;
                    listofInitialNumbers[emptyTilePosition + 1] = 0;
                }
                if (emptyTilePosition > max1 && emptyTilePosition < max2)
                {
                    noToswipe = listofInitialNumbers[max1];
                    listofInitialNumbers[emptyTilePosition] = noToswipe;
                    listofInitialNumbers[max1] = 0;
                }
                moveNo = moveNo + 1;
                Console.WriteLine(string.Format("Puzzle state after {0} move : ", moveNo));
                DisplayPuzzle(true, listofInitialNumbers);

            }

            catch (Exception ex)
            {
                Console.WriteLine("error in dynamicmoveblockanticlockwise");
            }
        }


        //check if two alternative numbers in grid
        public bool CheckAlternativeNumbersinGrid(int layerNo, int min1, int min2, int max1, int max2)
        {
            bool result = false;
            try
            {
                switch (layerNo)
                {

                    case 1:
                        if ((listofGoalNumbers[0] == listofInitialNumbers[min1] || listofGoalNumbers[0] == listofInitialNumbers[min2]
                            || listofGoalNumbers[0] == listofInitialNumbers[max1] || listofGoalNumbers[0] == listofInitialNumbers[max2])
                            &&
                            (listofGoalNumbers[2] == listofInitialNumbers[min1] || listofGoalNumbers[2] == listofInitialNumbers[min2]
                            || listofGoalNumbers[2] == listofInitialNumbers[max1] || listofGoalNumbers[2] == listofInitialNumbers[max2]))
                        {

                            int emptyTileposition = GetPositionOfEmptyTile();
                            if (emptyTileposition != min1 || emptyTileposition != max1
                                || emptyTileposition != max1 || emptyTileposition != max2)
                            {


                                bool checkalternativenumbers = false;
                                while (!checkalternativenumbers && (emptyTileposition != min1
                                    || emptyTileposition != min2 || emptyTileposition != max1 || emptyTileposition != max2))
                                {

                                    DynamicMoveBlockAntiClockWise(0, 2, 3, 5);

                                    emptyTileposition = GetPositionOfEmptyTile();
                                    if ((listofGoalNumbers[0] == listofInitialNumbers[min1] || listofGoalNumbers[0] == listofInitialNumbers[min2]
                             || listofGoalNumbers[0] == listofInitialNumbers[max1] || listofGoalNumbers[0] == listofInitialNumbers[max2])
                             &&
                             (listofGoalNumbers[2] == listofInitialNumbers[min1] || listofGoalNumbers[2] == listofInitialNumbers[min2]
                             || listofGoalNumbers[2] == listofInitialNumbers[max1] || listofGoalNumbers[2] == listofInitialNumbers[max2])
                     && (emptyTileposition == min1 || emptyTileposition == min2 || emptyTileposition == max1 || emptyTileposition == max2))
                                    {
                                        checkalternativenumbers = true;
                                        result = true;
                                    }
                                }
                            }

                            if ((listofGoalNumbers[1] == listofInitialNumbers[min1] || listofGoalNumbers[1] == listofInitialNumbers[min2]
                             || listofGoalNumbers[1] == listofInitialNumbers[max1] || listofGoalNumbers[1] == listofInitialNumbers[max2]))
                            {
                                int firstPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[0]);
                                int secondposition = listofInitialNumbers.IndexOf(listofGoalNumbers[1]);
                                int thirdPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[2]);

                                int min, max = 0;
                                min = min1 == 1 ? 2 : 0;
                                max = max2 == 5 ? 5 : 3;
                                bool firstresult = false;
                                while (!firstresult)
                                {

                                    if (max2 == 5)
                                    {
                                        DynamicMoveBlockAntiClockWise(1, 2, 4, 5);
                                    }
                                    else
                                    {
                                        DynamicMoveBlockAntiClockWise(0, 1, 3, 4);
                                    }
                                    firstPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[0]);
                                    thirdPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[2]);
                                    if ((firstPosition == min || firstPosition == max) && (thirdPosition == min || thirdPosition == max))
                                    {
                                        firstresult = true;
                                        break;
                                    }
                                }
                                if (max2 == 5)
                                {
                                    bool thirdresult = false;
                                    while (!thirdresult)
                                    {

                                        DynamicMoveBlockAntiClockWise(0, 1, 3, 4);
                                        secondposition = listofInitialNumbers.IndexOf(listofGoalNumbers[1]);
                                        emptyTileposition = GetPositionOfEmptyTile();
                                        if (secondposition == 0 && (emptyTileposition == 1 || emptyTileposition == 4))
                                        {
                                            thirdresult = true;
                                        }
                                    }
                                }
                                else
                                {
                                    bool thirdresult = false;
                                    while (!thirdresult)
                                    {
                                        DynamicMoveBlockAntiClockWise(1, 2, 4, 5);
                                        emptyTileposition = GetPositionOfEmptyTile();
                                        secondposition = listofInitialNumbers.IndexOf(listofGoalNumbers[1]);
                                        if (secondposition == 2 && (emptyTileposition == 1 || emptyTileposition == 4))
                                        {
                                            thirdresult = true;
                                        }
                                    }
                                }
                            }




                            result = true;

                        }

                        break;
                    case 2:
                        if ((listofGoalNumbers[3] == listofInitialNumbers[min1] || listofGoalNumbers[3] == listofInitialNumbers[min2]
                           || listofGoalNumbers[3] == listofInitialNumbers[max1] || listofGoalNumbers[3] == listofInitialNumbers[max2])
                           &&
                           (listofGoalNumbers[5] == listofInitialNumbers[min1] || listofGoalNumbers[5] == listofInitialNumbers[min2]
                           || listofGoalNumbers[5] == listofInitialNumbers[max1] || listofGoalNumbers[5] == listofInitialNumbers[max2]))
                        {

                            int emptyTileposition = GetPositionOfEmptyTile();
                            if (emptyTileposition != min1 || emptyTileposition != max1
                                || emptyTileposition != max1 || emptyTileposition != max2)
                            {


                                bool checkalternativenumbers = false;
                                while (!checkalternativenumbers && (emptyTileposition != min1
                                    || emptyTileposition != min2 || emptyTileposition != max1 || emptyTileposition != max2))
                                {

                                    DynamicMoveBlockAntiClockWise(3, 5, 6, 8);

                                    emptyTileposition = GetPositionOfEmptyTile();
                                    if ((listofGoalNumbers[3] == listofInitialNumbers[min1] || listofGoalNumbers[3] == listofInitialNumbers[min2]
                             || listofGoalNumbers[3] == listofInitialNumbers[max1] || listofGoalNumbers[3] == listofInitialNumbers[max2])
                             &&
                             (listofGoalNumbers[5] == listofInitialNumbers[min1] || listofGoalNumbers[5] == listofInitialNumbers[min2]
                             || listofGoalNumbers[5] == listofInitialNumbers[max1] || listofGoalNumbers[5] == listofInitialNumbers[max2])
                     && (emptyTileposition == min1 || emptyTileposition == min2 || emptyTileposition == max1 || emptyTileposition == max2))
                                    {
                                        checkalternativenumbers = true;
                                        result = true;
                                    }
                                }
                            }

                            if ((listofGoalNumbers[4] == listofInitialNumbers[min1] || listofGoalNumbers[4] == listofInitialNumbers[min2]
                             || listofGoalNumbers[4] == listofInitialNumbers[max1] || listofGoalNumbers[4] == listofInitialNumbers[max2]))
                            {
                                int firstPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[3]);
                                int secondposition = listofInitialNumbers.IndexOf(listofGoalNumbers[4]);
                                int thirdPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[5]);

                                int min, max = 0;
                                min = min1 == 4 ? 5 : 3;
                                max = max2 == 8 ? 8 : 6;

                                bool firstresult = false, secondresult = false, thirdresult = false;


                                while (!firstresult)
                                {

                                    if (max2 == 8)
                                    {
                                        DynamicMoveBlockAntiClockWise(4, 5, 7, 8);
                                    }
                                    else
                                    {
                                        DynamicMoveBlockAntiClockWise(3, 4, 6, 7);
                                    }
                                    firstPosition = listofInitialNumbers.IndexOf(listofGoalNumbers[3]);
                                    secondposition = listofInitialNumbers.IndexOf(listofGoalNumbers[5]);

                                    if ((firstPosition == min || firstPosition == max) && (secondposition == min || secondposition == max))
                                    {
                                        firstresult = true;

                                    }
                                }
                                if (max2 == 8)
                                {
                                    while (!secondresult)
                                    {
                                        DynamicMoveBlockAntiClockWise(3, 4, 6, 7);

                                        emptyTileposition = GetPositionOfEmptyTile();
                                        secondposition = listofInitialNumbers.IndexOf(listofGoalNumbers[4]);
                                        if (secondposition == 3 && (emptyTileposition == 4 || emptyTileposition == 7))
                                        { secondresult = true; }
                                    }
                                }
                                else
                                {
                                    while (!secondresult)
                                    {

                                        DynamicMoveBlockAntiClockWise(4, 5, 7, 8);
                                        emptyTileposition = GetPositionOfEmptyTile();


                                        secondposition = listofInitialNumbers.IndexOf(listofGoalNumbers[4]);

                                        if (secondposition == 5 && (emptyTileposition == 4 || emptyTileposition == 7))
                                        {
                                            secondresult = true;
                                        }
                                    }
                                }
                            }




                            result = true;

                        }

                        break;

                    default:
                        break;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }


        //check three consecutive numbers in one grid
        public bool CheckthreeblockinGrid(int layerNo, int min1, int min2, int max1, int max2)
        {
            bool result = false;
            try
            {
                switch (layerNo)
                {

                    case 1:
                        if ((listofGoalNumbers[0] == listofInitialNumbers[min1] || listofGoalNumbers[0] == listofInitialNumbers[min2]
                            || listofGoalNumbers[0] == listofInitialNumbers[max1] || listofGoalNumbers[0] == listofInitialNumbers[max2])
                            &&
                            (listofGoalNumbers[2] == listofInitialNumbers[min1] || listofGoalNumbers[2] == listofInitialNumbers[min2]
                            || listofGoalNumbers[2] == listofInitialNumbers[max1] || listofGoalNumbers[2] == listofInitialNumbers[max2])
                            &&

                            (listofGoalNumbers[1] == listofInitialNumbers[min1] || listofGoalNumbers[1] == listofInitialNumbers[min2]
                            || listofGoalNumbers[1] == listofInitialNumbers[max1] || listofGoalNumbers[1] == listofInitialNumbers[max2]))
                        {

                            result = true;
                        }

                        break;

                    case 2:
                        if ((listofGoalNumbers[3] == listofInitialNumbers[min1] || listofGoalNumbers[3] == listofInitialNumbers[min2]
                            || listofGoalNumbers[3] == listofInitialNumbers[max1] || listofGoalNumbers[3] == listofInitialNumbers[max2])
                            &&
                            (listofGoalNumbers[5] == listofInitialNumbers[min1] || listofGoalNumbers[5] == listofInitialNumbers[min2]
                            || listofGoalNumbers[5] == listofInitialNumbers[max1] || listofGoalNumbers[5] == listofInitialNumbers[max2])
                            &&

                            (listofGoalNumbers[4] == listofInitialNumbers[min1] || listofGoalNumbers[4] == listofInitialNumbers[min2]
                            || listofGoalNumbers[4] == listofInitialNumbers[max1] || listofGoalNumbers[4] == listofInitialNumbers[max2]))
                        {
                            result = true;
                        }

                        break;

                    default:
                        break;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        //check three consecutive numbers in one corner
        public bool CheckAlternativeNumbersinCorner(int layerNo, int min1, int max1)
        {
            bool result = false;
            try
            {
                switch (layerNo)
                {

                    case 1:
                        if ((listofGoalNumbers[0] == listofInitialNumbers[min1]
                            || listofGoalNumbers[0] == listofInitialNumbers[max1])
                            &&
                            (listofGoalNumbers[2] == listofInitialNumbers[min1]
                            || listofGoalNumbers[2] == listofInitialNumbers[max1]))
                        {
                            result = true;
                        }

                        break;

                    default:
                        break;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        //check three consecutive numbers sorted
        public bool CheckblockSorted(int layerNo, int min1, int max1, int min2, int max2)
        {
            bool result = false;
            try
            {
                int value, position, nextBlock, nextblockPosition, thirdblock, thirdblockPosition = 0;
                switch (layerNo)
                {

                    case 1:

                        value = listofGoalNumbers[0];
                        nextBlock = listofGoalNumbers[0 + 1];
                        thirdblock = listofGoalNumbers[0 + 2];
                        position = listofInitialNumbers.IndexOf(value);
                        nextblockPosition = listofInitialNumbers.IndexOf(nextBlock);
                        thirdblockPosition = listofInitialNumbers.IndexOf(thirdblock);

                        while (position != max2)
                        {
                            DynamicMoveBlockAntiClockWise(min1, min2, max1, max2);
                            position = listofInitialNumbers.IndexOf(value);
                        }
                        if (position == max2 && (nextblockPosition == max2 - 1 || nextblockPosition == max2 - 4) && (thirdblockPosition == nextblockPosition + 1 || thirdblockPosition == nextblockPosition - 3))
                        {
                            while (position != min2 || nextblockPosition != max2 || thirdblockPosition != max1)
                            {
                                DynamicMoveBlockAntiClockWise(min1, min2, max1, max2);
                                position = listofInitialNumbers.IndexOf(value);
                                nextblockPosition = listofInitialNumbers.IndexOf(nextBlock);
                                thirdblockPosition = listofInitialNumbers.IndexOf(thirdblock);

                            }
                            result = true;
                        }
                        break;

                    case 2:
                        value = listofGoalNumbers[3];
                        nextBlock = listofGoalNumbers[3 + 1];
                        thirdblock = listofGoalNumbers[3 + 2];
                        position = listofInitialNumbers.IndexOf(value);
                        nextblockPosition = listofInitialNumbers.IndexOf(nextBlock);
                        thirdblockPosition = listofInitialNumbers.IndexOf(thirdblock);

                        while (position != max2)
                        {
                            DynamicMoveBlockAntiClockWise(min1, min2, max1, max2);
                            position = listofInitialNumbers.IndexOf(value);
                        }
                        if (position == max2 && (nextblockPosition == max2 - 1 || nextblockPosition == max2 - 4) && (thirdblockPosition == nextblockPosition + 1 || thirdblockPosition == nextblockPosition - 3))
                        {
                            while (position != min2 || nextblockPosition != max2 || thirdblockPosition != max1)
                            {
                                DynamicMoveBlockAntiClockWise(min1, min2, max1, max2);
                                position = listofInitialNumbers.IndexOf(value);
                                nextblockPosition = listofInitialNumbers.IndexOf(nextBlock);
                                thirdblockPosition = listofInitialNumbers.IndexOf(thirdblock);

                            }
                            result = true;
                        }
                        break;




                    default:
                        break;
                }




            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        #endregion

        #endregion

    }

}
