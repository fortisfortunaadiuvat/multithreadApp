using System;
using System.Threading;

namespace Density_of_Elevator_with_MT_Control
{
    class Program
    {
        static int flag = 1;
        static void Show_Console()
        {
            Console.WriteLine("0.floor: queue:" + AVM.queue[0]);
            Console.WriteLine("1.floor: all:" + AVM.floor[1] + " queue:" + AVM.queue[1]);
            Console.WriteLine("2.floor: all:" + AVM.floor[2] + " queue:" + AVM.queue[2]);
            Console.WriteLine("3.floor: all:" + AVM.floor[3] + " queue:" + AVM.queue[3]);
            Console.WriteLine("4.floor: all:" + AVM.floor[4] + " queue:" + AVM.queue[4]);
            Console.WriteLine("exit count:" + AVM.exit_frag);
            Console.WriteLine();

            Elevator[] elevators = new Elevator[5];
            
            elevators[0] = AVM.elevator1;
            elevators[1] = AVM.elevator2;
            elevators[2] = AVM.elevator3;
            elevators[3] = AVM.elevator4;
            elevators[4] = AVM.elevator5;

            for(int i = 0; i < elevators.Length; i++)
            {
                Console.WriteLine("active:" + elevators[i].Get_active());
                Console.WriteLine("                  mode:" + elevators[i].Get_mode());
                Console.WriteLine("                  floor:" + elevators[i].Get_floor());
                Console.WriteLine("                  destination:" + elevators[i].Get_destination());
                Console.WriteLine("                  direction:" + elevators[i].Get_direction());
                Console.WriteLine("                  capacity:" + elevators[i].Get_capacity());
                Console.WriteLine("                  count_inside:" + elevators[i].Get_count_inside());
                Console.WriteLine("                  inside:" + elevators[i].Get_inside());
            }
        }

        static void Main()
        {            
            AVM avm = new AVM();

            int i = 0;

            while (i<3)
            {
                var startTimeSpan = TimeSpan.FromSeconds(11);
                var periodTimeSpan = TimeSpan.FromSeconds(11);

                var timer = new Timer((e) =>
                {
                    if(flag == 1)
                    {
                        Show_Console();
                        flag = 0;
                    }
                    
                }, null, startTimeSpan, periodTimeSpan);

                avm.AVM_Login_Thread();
                avm.AVM_Exit_Thread();
                i++;
            }
        }
    }
}
