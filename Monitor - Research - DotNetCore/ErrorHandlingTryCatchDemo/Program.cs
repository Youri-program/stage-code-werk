// Simple app for demo Error Handling with try catch statement
// Rock paper Scissors game with errors :)

// Make a random variable for the computer to use
Random random = new Random();
// Make some ints for the player score and the computer score, both set to 0
int playerScore = 0;
int computerScore = 0;
// Welcome message with some information
Console.WriteLine("Welcome to this rock paper scissors game!");
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("FIRST one that has 3 WINS has won!");
Console.ResetColor();
// While loop until one of the players score is 3, endscore can't be a tie. 
try
{
    while (playerScore != 3 && computerScore != 3)
    {
        // Displays scores of the user and the computer, after that enter your decision
        Console.WriteLine("Player score -> " + playerScore + " | Enemy score -> " + computerScore);
        Console.WriteLine("Enter 'r' for rock, 'p' for paper or anything else for scissors");
        // Using 'Parse', this wil put out a error. Just to show how try catch is working.
        string input = "Hello";
        playerScore = Int32.Parse(input);

        string? playerChoice = Console.ReadLine();
        // Variable computerChoice which is filled with a random number
        int computerChoice = random.Next(0, 3);
        // If computerChoice will be equal to 0 the computer has chosen rock, combined with your answer, a given result comes out
        if (computerChoice == 0)
        {
            Console.WriteLine("Computer has chosen rock.");

            switch (playerChoice)
            {
                case "r":
                    Console.WriteLine("It's a Tie!");
                    break;
                case "p":
                    Console.WriteLine("You won this round!");
                    // playerScore goes up with 1 point
                    playerScore++;
                    break;
                default:
                    Console.WriteLine("Computer wins this round!");
                    // computerScore goes up with 1 point
                    computerScore++;
                    break;
            }
        }
        //  Else If computerChoice will be equal to 1 the computer has chosen paper, combined with your answer, a given result comes out
        else if (computerChoice == 1)
        {
            Console.WriteLine("Computer has chosen paper.");

            switch (playerChoice)
            {
                case "r":
                    Console.WriteLine("Computer wins this round!");
                    // computerScore goes up with 1 point
                    computerScore++;
                    break;
                case "p":
                    Console.WriteLine("It's a Tie!");
                    break;
                default:
                    Console.WriteLine("You won this round!");
                    // playerScore goes up with 1 point
                    playerScore++;
                    break;
            }
        }
        //  Else computerChoice will be equal to all other choices (2) the computer has chosen scissors, combined with your answer, a given result comes out
        else
        {
            Console.WriteLine("Computer has chosen scissors.");

            switch (playerChoice)
            {
                case "r":
                    Console.WriteLine("You won this round!");
                    // playerScore goes up with 1 point
                    playerScore++;
                    break;
                case "p":
                    Console.WriteLine("Computer wins this round!");
                    // computerScore goes up with 1 point
                    computerScore++;
                    break;
                default:
                    Console.WriteLine("It's a Tie!");
                    break;
            }
        }
    }
}
catch (Exception e)
{
    Console.ForegroundColor = ConsoleColor.DarkBlue;
    Console.WriteLine(e.Message);
}
Console.ResetColor();

// Read the file where all the results are stored.
// Showing a way how try catch could be used.
var file = @"C:\Users\Henry\Desktop\Assessments\Programming and data structures\grades_multiple.txt";
string[] lines;
// The try-catch statement consists of a try block followed by one or more catch clauses, which specify handlers for different exceptions.
try
{
    lines = File.ReadAllLines(file);
}
catch (FileNotFoundException exnotfound)
{
    // File not found exception
    Console.ForegroundColor = ConsoleColor.DarkBlue;
    Console.WriteLine(exnotfound.Message);
}
catch (Exception ex)
{
    // Handle other exceptions
    Console.ForegroundColor = ConsoleColor.DarkBlue;
    Console.WriteLine(ex.Message);
}
Console.ResetColor();

// Ending result. If playersScore is equal to 3 points than player has won, when its not equal to 3 computer has won.
if (playerScore == 3)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("You've won!");
}
else
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("You've lost!");
}
// Resets the given color to text above.
Console.ResetColor();

