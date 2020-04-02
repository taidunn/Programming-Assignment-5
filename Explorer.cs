using System;

namespace Explorer
{
   class Rover
   {
      private int location; // current location of the rover
      private int target; // unknown target location of the rover
      private int distance; // distance between the original location of the rover and the target
      private int steps; // the number of steps taken by the rover

      public void ComputeDistance() // method computing distance
      {
         distance = location - target;
         if (distance < 0) distance = -distance;
      }

      public float Factor() // Efficiency factor of your solution, less steps you take to reach the target is better
      {
         return ((float)steps / (float)distance);
      }

      public bool TargetFound() // test whether the target is reached
      {
         if (location == target)
         {
            return true;
         }
         else
         {
            return false;
         }
      }

      public void Move(string direction) // move to the left or two the right
      {
         if (direction == "left")
         {
            location--; steps++;
         }
         else
         {
            location++; steps++;
         }

      }

      public void SetRover(int start, int finish) // set initial parameters: original location, target and steps
      {
         location = start;
         target = finish;
         steps = 0;
      }


   }

   class Program // the main class starts here
   {
      static void Main(string[] args) // the main method starts here
      {
         Rover Spirit = new Rover(); // create a new rover, name it Spirit

         int iterations = 100; // this is used for testing, 100 times
         float maxFactor = 0; // the worst performance factor

         for (int i = 0; i < iterations; ++i) // run the rover experiment 100 times
         {
            Random random = new Random(); // random integer from the range (-100,100)

            Spirit.SetRover(0, random.Next(-1000, 1000)); // Set new experiment, with starting location 0 and target at distance < 1000
            Spirit.ComputeDistance(); // compute original distance

            bool notFound = true; // true until the target is found, false otheriwse

            // In your solution you can use any computations.
            // HOWEVER the only allowed access to Spirit object
            // is via public method Spirit.Move("left"); Spirit.Move("right"); or Spirit.TargetFound();
            // WRITE YOUR CODE BELOW THS LINE

            int trys = 1;
            int range = 2; // the search range, initally small, gradually increased according to your strategy, by default by +1

            while (notFound) // search until you find a target
            {

               // explore to the right within the (positive) range

               for (int j = 0; ((j < range) && notFound); ++j)
               {
                  if (Spirit.TargetFound())
                  {
                     notFound = false;
                  }
                  else
                  {
                     Spirit.Move("right");
                  }
               }

               // go back to the original location

               for (int j = 0; ((j < range) && notFound); ++j)
               {
                  Spirit.Move("left");
               }

               // explore to the left within the (negative) range

               for (int j = 0; ((j < range) && notFound); ++j)
               {
                  if (Spirit.TargetFound())
                  {
                     notFound = false;
                  }
                  else
                  {
                     Spirit.Move("left");
                  }
               }

               // go back to the original location

               for (int j = 0; ((j < range) && notFound); ++j)
               {
                  Spirit.Move("right");
               }

               trys++;
               range = Convert.ToInt32(Math.Pow(2, trys)); // increase the range, here give a good thought to the task

            }

            // WRITE YOUR CODE ABOVE THS LINE
            // Console.WriteLine($"Efficiency factor of your solution is: {Spirit.Factor()}");
            if (maxFactor < Spirit.Factor()) maxFactor = Spirit.Factor(); // you can uncomment this to see what factors you get
         }
         Console.WriteLine($"Maximum efficiency factor of your solution is: {maxFactor}"); // raport the worst factor observed
      }
   }
}
