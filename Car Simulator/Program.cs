Random rand = new Random();

double raceLength = 2500;

// Declaration of variables for the users vehicle
int totalWeight = 0;
int horsepower = 0;
double topSpeed = 0;

// Declaration of variables for the opponents vehicle
int enemyWeight = 0;
int enemyHorse = 0;
double enemyTop = 0;

start:

Console.WriteLine("--[ Build your own car ]--");
Console.WriteLine("");
Console.WriteLine("[ Chassis ]");
Console.WriteLine("1: Classic Red Chassis");
Console.WriteLine("Weight: 550kg");
Console.WriteLine("");
Console.WriteLine("2: Sporty Chassis");
Console.WriteLine("Weight: 430kg");
Console.WriteLine("");
Console.WriteLine("3: Carbon-Fiber Aerodynamic");
Console.WriteLine("Weight: 210kg");
Console.WriteLine("");

bool selectedchas = false;

// This is the logic behind converting the input into a chassis for the car. The program keeps requesting an input until a valid input is given.

while (!selectedchas)
{
    try
    {
        Console.Write("[>] ");
        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1:
                totalWeight = 550;
                selectedchas = true;

                break;

            case 2:
                totalWeight = 430;
                selectedchas = true;

                break;
            case 3:
                totalWeight = 210;
                selectedchas = true;

                break;
            default:
                Console.WriteLine("Invalid input!");
                Thread.Sleep(2500);

                break;
        }
    } catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

Console.Clear();
Console.WriteLine("[ Engine ]");
Console.WriteLine("1: Inline-4 2.3L Engine");
Console.WriteLine("Weight: 150kg");
Console.WriteLine("130HP");
Console.WriteLine("");
Console.WriteLine("2: V6 4.0L Engine");
Console.WriteLine("Weight: 200kg");
Console.WriteLine("365HP");
Console.WriteLine("");
Console.WriteLine("3: V12 6.3L Engine");
Console.WriteLine("Weight: 230kg");
Console.WriteLine("730HP");
Console.WriteLine("");

// The same logic as choosing the chassis however this time it's for the engine.

bool engineSel = false;

while (!engineSel)
{
    try
    {
        Console.Write("[>] ");
        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            // We're adding the weight of the engine to the total weight of the vehicle.
            // Horsepower is crucial for calculating the vehicles top speed and acceleration.

            case 1:
                totalWeight += 150;
                horsepower = 130;

                engineSel = true;

                break;

            case 2:
                totalWeight += 200;
                horsepower = 365;

                engineSel = true;

                break;
            case 3:
                totalWeight += 230;
                horsepower = 730;

                engineSel = true;

                break;
            default:
                Console.WriteLine("Invalid input!");
                Thread.Sleep(2500);

                break;
        }

        // Custom mathematical formula for calculating the top speed of the vehicle based on how much horsepower the engine is outputting aswell as the total weight of the car.

        topSpeed = 7.60934 * Math.Cbrt((horsepower * 70000) / totalWeight);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

Car car = new Car(topSpeed, (topSpeed / 300));

Console.Clear();

Console.WriteLine("[System]: Generating enemy AI");

// This is the logic behind generating the opponents vehicle. It works almost exactly the same way the previous code for generating the users vehicle does except the system decides the vehicles engine and chassis

int enemyCase = rand.Next(1, 4);
rand = new Random();
int enemyEngine = rand.Next(1, 4);

switch (enemyCase)
{
    case 1:
        enemyWeight = 550;

        break;

    case 2:
        enemyWeight = 430;

        break;
    case 3:
        enemyWeight = 210;

        break;
    default:
        Console.WriteLine("Invalid input!");
        Thread.Sleep(2500);

        break;
}

switch (enemyEngine)
{
    case 1:
        enemyWeight += 150;
        enemyHorse = 130;

        break;

    case 2:
        enemyWeight += 200;
        enemyHorse = 365;

        break;
    case 3:
        enemyWeight += 230;
        enemyHorse = 730;

        break;
    default:
        Console.WriteLine("Invalid input!");
        Thread.Sleep(2500);

        break;
}

// Same formula we use for the user

enemyTop = 7.60934 * Math.Cbrt((enemyHorse * 70000) / enemyWeight);

Car enemy = new Car(enemyTop, enemyTop / 300);

Thread.Sleep(100);

Console.WriteLine("[System]: Finished generating!");

// Here is where the race actually occurs

Thread.Sleep(2500);

Console.Clear();

Console.WriteLine("[ Ready? ]");

Thread.Sleep(1500);

Console.Clear();

Console.WriteLine("[ Set ]");

Thread.Sleep(1500);

Console.Clear();

Console.WriteLine("[ Go! ]");

Thread.Sleep(1000);

while (true)
{
    // Every iteration we clear the console otherwise we would have text pilling on top of each other constantly
    Console.Clear();

    // Every iteration we move both vehicles via the Move function. The 0.02 will be explained in the class
    car.Move(0.02);
    enemy.Move(0.02);

    // We print out information about the vehicles such as distance traveled and speed
    Console.WriteLine($"[You] {Math.Round(car.distance)}/{raceLength}m");
    Console.WriteLine(Math.Round(car.speed) + " km/h");
    Console.WriteLine($"[Opponent] {Math.Round(enemy.distance)}/{raceLength}m");
    Console.WriteLine(Math.Round(enemy.speed) + " km/h");

    // Check if the users car reached the destination
    if (car.distance >= raceLength)
    {
        if (enemy.distance >= raceLength)
        {
            // If the enemy did as well at the same time then it's a tie
            Thread.Sleep(2000);

            Console.Clear();

            Console.WriteLine("Tie !");
            Console.ReadKey();

            break;
        } else
        {
            // Otherwise the user won
            Thread.Sleep(2000);

            Console.Clear();

            Console.WriteLine("You win!");
            Console.ReadKey();

            break;
        }
    }

    if (enemy.distance >= raceLength)
    {
        // If the enemy reached first then the enemy won
        Thread.Sleep(2000);

        Console.Clear();

        Console.WriteLine("You lost :(");

        Console.ReadKey();

        break;
    }

    Thread.Sleep(10);
}

Console.Clear();

// We return to the start if the user wants to play again
goto start;

class Car
{
    // Properties of the vehicle (not including engine)

    public double topSpeed;
    public double acceleration;
    public double speed = 1;
    public double distance = 0;

    // Constructor to declare the variables
    public Car(double top, double acc)
    {
        topSpeed = top;
        acceleration = acc;
    }

    // This function moves the vehicle depending on the properties of the vehicle
    public void Move(double fraction)
    {
        distance += (speed / 3.6) * fraction; // The fraction acts as a constant, in a while loop we run the code multiple times per second. If we didn't multiply by the fraction then the value would grow astronomically
        speed += acceleration;

        // Limit the speed from ever going above the top speed
        if (speed > topSpeed)
        {
            speed = topSpeed - 3;
        }
    }
}