using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

//TaskTrain.taskTrain();
public class TaskTrain
{
    public static void taskTrain()
    {
        Debug.Print("taskTrain start.");
        TimeSpan delay = TimeSpan.FromMilliseconds(1000);
        const int repeat = 3;
        bool terminate = false;
        bool toggle = false;
        Task clockTask = Task.Run(() => {
            Debug.Print("clockTask start.");
            Stopwatch stopwatch = new();
            stopwatch.Restart();
            for (int i = 0; i < repeat; ++i) {
                if (terminate)
                    break;
                    if (stopwatch.Elapsed > delay) {
                        toggle = !toggle;
                        Debug.Print("toggled.");
                        stopwatch.Restart();
                    }
                Thread.Sleep(200);
            }
            Debug.Print("clockTask end.");
        });
        Task keyTask = Task.Run(() => {
            Debug.Print("keyTask start.");
            for(int i = 0; i < repeat; ++i) {
                if (Console.KeyAvailable) {
                    if (Console.ReadKey(true).Key == ConsoleKey.Escape){
                        terminate = true;
                        Debug.Print("terminate turned true.");
                    }
                }
                Thread.Sleep(300);
            }
            Debug.Print("keyTask end.");
        });
        Task dispTask = Task.Run(() => {
            Debug.Print("dispTask start.");
            bool last_toggle = toggle;
            for(int i = 0; i < repeat; ++i) {
                if (toggle != last_toggle) {
                    Console.WriteLine("Toggled.");
                    last_toggle = toggle;
                    Debug.Print("last_togle is set as toggle.");
                }
                Thread.Sleep(450);
            }
            Debug.Print("dispTask end.");
        });
    }
}