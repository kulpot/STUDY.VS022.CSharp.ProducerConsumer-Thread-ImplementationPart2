using System;

//ref link:https://www.youtube.com/watch?v=JXRSmlCFo1o&list=PLRwVmtr-pp06KcX24ycbC-KkmAISAFKV5&index=14
// part 2
// ctrl+m+o -- collapse all line

class MainClass
{
    static Queue<int> numbers = new Queue<int>(); // Queue Structure -- requires knowledge in data structures
    static Random rand = new Random();
    const int NumThreads = 3;
    static int[] sums = new int[NumThreads]; // for total added array
    static void ProduceNumbers()    // Producing Method
    {
        for (int i = 0; i < 10; i++)
        {
            int numToEnqueue = rand.Next(10);
            //numbers.Enqueue(rand.Next(10));
            Console.WriteLine("Producing thread adding " + numToEnqueue + " to the queue.");
            numbers.Enqueue(numToEnqueue);
            Thread.Sleep(rand.Next(1000));
        }
    }
    static void SumNumbers(object threadNumber)   // Consuming Method
    {   //---------poorman's method of synchronization technique---------- needs improvements
        DateTime startTime = DateTime.Now;
        int mySum = 0;
        while ((DateTime.Now - startTime).Seconds < 11)
        {
            if (numbers.Count != 0)
            {
                //mySum += numbers.Dequeue();
                int numToSum = numbers.Dequeue();
                mySum += numToSum;
                Console.WriteLine("Consuming thread adding " 
                    + numToSum + " to its total sum making " 
                    + numToSum + " for the thread total.");
            }
        }
        sums[(int)threadNumber] = mySum;
    }
    static void Main()
    {
        var producingThread = new Thread(ProduceNumbers);
        producingThread.Start();
        Thread[] threads = new Thread[NumThreads];
        for (int i = 0; i < NumThreads; i++)
        {
            threads[i] = new Thread(SumNumbers);
            threads[i].Start();
        }
        for (int i = 0; i < NumThreads; i++)     
            threads[i].Join();
        int totalSum = 0;
        for (int i = 0; i < NumThreads; i++)
            totalSum += sums[i];
        Console.WriteLine("Done adding. Total is " + totalSum);s
    }
}