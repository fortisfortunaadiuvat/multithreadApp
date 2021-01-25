using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Density_of_Elevator_with_MT_Control
{
    class AVM
    {
        //Katlardaki kişi sayısının bilgisi tutuldu.
        public static int[] floor = new int[5];

        //Asansör kuyruğunu ifade eder.
        public static int[] queue = new int[5];

        //Çıkış kuyruğundaki bekleyen kişi sayısı ile ilgili.
        public static int queue_exit ;

        //Çıkan kişi sayısını belirler.
        public static int exit_frag;

        //Asansör kuyruğundakilerin hangi kata gideceğinin bilgisini tutar.
        int destination;

        public static Elevator elevator1 = new Elevator();
        public static Elevator elevator2 = new Elevator();
        public static Elevator elevator3 = new Elevator();
        public static Elevator elevator4 = new Elevator();
        public static Elevator elevator5 = new Elevator();

        public static Elevator[] elevators = { elevator1, elevator2, elevator3, elevator4, elevator5 };
        

        Random r = new Random();

        static int consumer = 0;

        public void AVM_Login_Thread()
        {
            for(int i = 0; i < 5; i++)
            {
                floor[i] = 0;
            }

            elevator1.Set_active(true);
            elevator1.Set_mode("working");
            elevator1.Set_direction(1);

            consumer += r.Next(1, 11);
            int[] consumer_list = new int[consumer];

            for(int i = 0; i < consumer; i++)
            {
                destination = r.Next(1, 5);
                consumer_list[i] = destination;
            }
            queue[0] += consumer;

            if(elevator1.Get_active() == true)
            {
                int count = consumer;
                int count2 = consumer + elevator1.Get_count_inside();
                if (count2 <= 10)
                {
                    elevator1.Set_count_inside(count);
                    queue[0] -= count;
                } 
            }
            Elevator1_Thread(consumer_list);
            Control_Thread();

            if (elevator2.Get_active() == true)
            {
                Elevator2_Thread(consumer_list);
            }

            if (elevator3.Get_active() == true)
            {
                Elevator3_Thread(consumer_list);
            }

            if (elevator4.Get_active() == true)
            {
                Elevator4_Thread(consumer_list);
            }

            if (elevator5.Get_active() == true)
            {
                Elevator5_Thread(consumer_list);
            }

            Thread.Sleep(500);
        }

        public void AVM_Exit_Thread()
        {
            int random_number = r.Next(1, 6);

            if (random_number > AVM.queue_exit)
            {
                consumer -= AVM.queue_exit;
                AVM.exit_frag += AVM.queue_exit;
            }
            else
            {
                consumer -= random_number;
                AVM.exit_frag += random_number;
            }

            Thread.Sleep(1000);
        }

        public void Elevator1_Thread(int[] consumer_list)
        {
            //Asansör yukarı çıkmaktadır.
            if(elevator1.Get_direction() == 1)
            {
                for (int i = 0; i < consumer_list.Length; i++)
                {
                    Thread.Sleep(200);
                    elevator1.Set_floor(1);
                    elevator1.Set_destination(2);

                    if (consumer_list[i] == 1)
                    {
                        floor[1] += 1;
                        
                        //Katlardaki kuyrukların kontrol edilmesi işlemi
                        //Do some stuff!!

                        int random_x = r.Next(0, floor[1]);
                        queue[1] += random_x;

                        //Kata müşterinin bırakıldığının bilgisi.
                        int get_inside = elevator1.Get_count_inside();
                        get_inside--;
                        if (get_inside > 0)
                        {
                            elevator1.Set_count_inside(get_inside);
                        }
                        
                    }

                    Thread.Sleep(200);
                    elevator1.Set_floor(2);
                    elevator1.Set_destination(3);

                    if (consumer_list[i] == 2)
                    {
                        floor[2] += 1;
                        int random_x = r.Next(0, floor[2]);
                        queue[2] += random_x;

                        int get_inside = elevator1.Get_count_inside();
                        get_inside--;
                        if (get_inside > 0)
                        {
                            elevator1.Set_count_inside(get_inside);
                        }
                    }

                    Thread.Sleep(200);
                    elevator1.Set_floor(3);
                    elevator1.Set_destination(4);

                    if (consumer_list[i] == 3)
                    {
                        floor[3] += 1;

                        int random_x = r.Next(0, floor[3]);
                        queue[3] += random_x;

                        int get_inside = elevator1.Get_count_inside();
                        get_inside--;
                        if (get_inside > 0)
                        {
                            elevator1.Set_count_inside(get_inside);
                        }
                    }

                    Thread.Sleep(200);
                    elevator1.Set_floor(4);
                    elevator1.Set_destination(3);

                    if (consumer_list[i] == 4)
                    {
                        floor[4] += 1;

                        int random_x = r.Next(0, floor[4]);
                        queue[4] += random_x;

                        int get_inside = elevator1.Get_count_inside();
                        get_inside--;
                        if (get_inside > 0)
                        {
                            elevator1.Set_count_inside(get_inside);
                        }
                    }
                }
            }

            elevator1.Set_direction(0);

            //Asansör aşağı inmektedir.
            if(elevator1.Get_direction() == 0)
            {
                if (floor[4] > 0 && queue[4] > 0)
                {
                    if (elevator1.Get_count_inside() <= 10)
                    {
                        while (queue[4] > 0)
                        {
                            queue[4] -= 1;

                            int get_inside = elevator1.Get_count_inside();
                            get_inside++;
                            if (get_inside <= 10)
                            {
                                elevator1.Set_count_inside(get_inside);
                            }
                        }
                    }
                }                
                Thread.Sleep(200);
                elevator1.Set_floor(3);
                elevator1.Set_destination(2);

                if (floor[3] > 0 && queue[3] > 0)
                {
                    if (elevator1.Get_count_inside() <= 10)
                    {
                        while (queue[3] > 0)
                        {
                            queue[3] -= 1;

                            int get_inside = elevator1.Get_count_inside();
                            get_inside++;
                            if (get_inside <= 10)
                            {
                                elevator1.Set_count_inside(get_inside);
                            }
                        }
                    }
                }
                Thread.Sleep(200);
                elevator1.Set_floor(2);
                elevator1.Set_destination(1);

                if (floor[2] > 0 && queue[2] > 0)
                {
                    if (elevator1.Get_count_inside() <= 10)
                    {
                        while (queue[2] > 0)
                        {
                            queue[2] -= 1;

                            int get_inside = elevator1.Get_count_inside();
                            get_inside++;
                            if (get_inside <= 10)
                            {
                                elevator1.Set_count_inside(get_inside);
                            }
                        }
                    }
                }
                Thread.Sleep(200);
                elevator1.Set_floor(1);
                elevator1.Set_destination(0);

                if (floor[1] > 0 && queue[1] > 0)
                {
                    if (elevator1.Get_count_inside() <= 10)
                    {
                        while (queue[1] > 0)
                        {
                            queue[1] -= 1;

                            int get_inside = elevator1.Get_count_inside();
                            get_inside++;
                            if (get_inside <= 10)
                            {
                                elevator1.Set_count_inside(get_inside);
                            }
                        }
                    }
                }
                Thread.Sleep(200);
                elevator1.Set_floor(0);
                elevator1.Set_destination(1);
            }
            AVM.queue_exit += elevator1.Get_count_inside();
            
        }
        public void Elevator2_Thread(int[] consumer_list)
        {
            elevator2.Set_direction(1);
            //Asansör yukarı çıkmaktadır.
            if (elevator2.Get_direction() == 1)
            {
                for (int i = 0; i < consumer_list.Length; i++)
                {
                    Thread.Sleep(200);
                    elevator2.Set_floor(1);
                    elevator2.Set_destination(2);

                    if (consumer_list[i] == 1)
                    {
                        floor[1] += 1;

                        //Katlardaki kuyrukların kontrol edilmesi işlemi
                        //Do some stuff!!

                        int random_x = r.Next(0, floor[1]);
                        queue[1] += random_x;

                        //Kata müşterinin bırakıldığının bilgisi.
                        int get_inside = elevator2.Get_count_inside();
                        get_inside--;
                        if (get_inside > 0)
                        {
                            elevator2.Set_count_inside(get_inside);
                        }

                    }
                    
                    Thread.Sleep(200);
                    elevator2.Set_floor(2);
                    elevator2.Set_destination(3);

                    if (consumer_list[i] == 2)
                    {
                        floor[2] += 1;
                        int random_x = r.Next(0, floor[2]);
                        queue[2] += random_x;

                        int get_inside = elevator2.Get_count_inside();
                        get_inside--;
                        if (get_inside > 0)
                        {
                            elevator2.Set_count_inside(get_inside);
                        }
                    }

                    Thread.Sleep(200);
                    elevator2.Set_floor(3);
                    elevator2.Set_destination(4);

                    if (consumer_list[i] == 3)
                    {
                        floor[3] += 1;

                        int random_x = r.Next(0, floor[3]);
                        queue[3] += random_x;

                        int get_inside = elevator2.Get_count_inside();
                        get_inside--;
                        if (get_inside > 0)
                        {
                            elevator2.Set_count_inside(get_inside);
                        }
                    }

                    Thread.Sleep(200);
                    elevator2.Set_floor(4);
                    elevator2.Set_destination(3);

                    if (consumer_list[i] == 4)
                    {
                        floor[4] += 1;

                        int random_x = r.Next(0, floor[4]);
                        queue[4] += random_x;

                        int get_inside = elevator2.Get_count_inside();
                        get_inside--;
                        if (get_inside > 0)
                        {
                            elevator2.Set_count_inside(get_inside);
                        }
                    }
                }
            }

            elevator2.Set_direction(0);

            //Asansör aşağı inmektedir.
            if (elevator2.Get_direction() == 0)
            {
                if (floor[4] > 0 && queue[4] > 0)
                {
                    if (elevator2.Get_count_inside() <= 10)
                    {
                        while (queue[4] > 0)
                        {
                            queue[4] -= 1;

                            int get_inside = elevator2.Get_count_inside();
                            get_inside++;
                            if (get_inside <= 10)
                            {
                                elevator2.Set_count_inside(get_inside);
                            }
                        }
                    }
                }
                Thread.Sleep(200);
                elevator2.Set_floor(3);
                elevator2.Set_destination(2);

                if (floor[3] > 0 && queue[3] > 0)
                {
                    if (elevator2.Get_count_inside() <= 10)
                    {
                        while (queue[3] > 0)
                        {
                            queue[3] -= 1;

                            int get_inside = elevator2.Get_count_inside();
                            get_inside++;
                            if (get_inside <= 10)
                            {
                                elevator2.Set_count_inside(get_inside);
                            }
                        }
                    }
                }
                Thread.Sleep(200);
                elevator2.Set_floor(2);
                elevator2.Set_destination(1);

                if (floor[2] > 0 && queue[2] > 0)
                {
                    if (elevator2.Get_count_inside() <= 10)
                    {
                        while (queue[2] > 0)
                        {
                            queue[2] -= 1;

                            int get_inside = elevator2.Get_count_inside();
                            get_inside++;
                            if (get_inside <= 10)
                            {
                                elevator2.Set_count_inside(get_inside);
                            }
                        }
                    }
                }
                Thread.Sleep(200);
                elevator2.Set_floor(1);
                elevator2.Set_destination(0);

                if (floor[1] > 0 && queue[1] > 0)
                {
                    if (elevator2.Get_count_inside() <= 10)
                    {
                        while (queue[1] > 0)
                        {
                            queue[1] -= 1;

                            int get_inside = elevator2.Get_count_inside();
                            get_inside++;
                            if (get_inside <= 10)
                            {
                                elevator2.Set_count_inside(get_inside);
                            }
                        }
                    }
                }
                Thread.Sleep(200);
                elevator2.Set_floor(0);
                elevator2.Set_destination(1);
            }
            AVM.queue_exit += elevator2.Get_count_inside();
        }
        public void Elevator3_Thread(int[] consumer_list)
        {
            elevator3.Set_direction(1);
            //Asansör yukarı çıkmaktadır.
            if (elevator3.Get_direction() == 1)
            {
                for (int i = 0; i < consumer_list.Length; i++)
                {
                    Thread.Sleep(200);
                    elevator3.Set_floor(1);
                    elevator3.Set_destination(2);

                    if (consumer_list[i] == 1)
                    {
                        floor[1] += 1;

                        //Katlardaki kuyrukların kontrol edilmesi işlemi
                        //Do some stuff!!

                        int random_x = r.Next(0, floor[1]);
                        queue[1] += random_x;

                        //Kata müşterinin bırakıldığının bilgisi.
                        int get_inside = elevator3.Get_count_inside();
                        get_inside--;
                        if (get_inside > 0)
                        {
                            elevator3.Set_count_inside(get_inside);
                        }

                    }

                    Thread.Sleep(200);
                    elevator3.Set_floor(2);
                    elevator3.Set_destination(3);

                    if (consumer_list[i] == 2)
                    {
                        floor[2] += 1;
                        int random_x = r.Next(0, floor[2]);
                        queue[2] += random_x;

                        int get_inside = elevator3.Get_count_inside();
                        get_inside--;
                        if (get_inside > 0)
                        {
                            elevator3.Set_count_inside(get_inside);
                        }
                    }

                    Thread.Sleep(200);
                    elevator3.Set_floor(3);
                    elevator3.Set_destination(4);

                    if (consumer_list[i] == 3)
                    {
                        floor[3] += 1;

                        int random_x = r.Next(0, floor[3]);
                        queue[3] += random_x;

                        int get_inside = elevator3.Get_count_inside();
                        get_inside--;
                        if (get_inside > 0)
                        {
                            elevator3.Set_count_inside(get_inside);
                        }
                    }

                    Thread.Sleep(200);
                    elevator3.Set_floor(4);
                    elevator3.Set_destination(3);

                    if (consumer_list[i] == 4)
                    {
                        floor[4] += 1;

                        int random_x = r.Next(0, floor[4]);
                        queue[4] += random_x;

                        int get_inside = elevator3.Get_count_inside();
                        get_inside--;
                        if (get_inside > 0)
                        {
                            elevator3.Set_count_inside(get_inside);
                        }
                    }
                }
            }

            elevator3.Set_direction(0);

            //Asansör aşağı inmektedir.
            if (elevator3.Get_direction() == 0)
            {
                if (floor[4] > 0 && queue[4] > 0)
                {
                    if (elevator3.Get_count_inside() <= 10)
                    {
                        while (queue[4] > 0)
                        {
                            queue[4] -= 1;

                            int get_inside = elevator3.Get_count_inside();
                            get_inside++;
                            if (get_inside <= 10)
                            {
                                elevator3.Set_count_inside(get_inside);
                            }
                        }
                    }
                }
                Thread.Sleep(200);
                elevator3.Set_floor(3);
                elevator3.Set_destination(2);

                if (floor[3] > 0 && queue[3] > 0)
                {
                    if (elevator3.Get_count_inside() <= 10)
                    {
                        while (queue[3] > 0)
                        {
                            queue[3] -= 1;

                            int get_inside = elevator3.Get_count_inside();
                            get_inside++;
                            if (get_inside <= 10)
                            {
                                elevator3.Set_count_inside(get_inside);
                            }
                        }
                    }
                }
                Thread.Sleep(200);
                elevator3.Set_floor(2);
                elevator3.Set_destination(1);

                if (floor[2] > 0 && queue[2] > 0)
                {
                    if (elevator3.Get_count_inside() <= 10)
                    {
                        while (queue[2] > 0)
                        {
                            queue[2] -= 1;

                            int get_inside = elevator3.Get_count_inside();
                            get_inside++;
                            if (get_inside <= 10)
                            {
                                elevator3.Set_count_inside(get_inside);
                            }
                        }
                    }
                }
                Thread.Sleep(200);
                elevator3.Set_floor(1);
                elevator3.Set_destination(0);

                if (floor[1] > 0 && queue[1] > 0)
                {
                    if (elevator3.Get_count_inside() <= 10)
                    {
                        while (queue[1] > 0)
                        {
                            queue[1] -= 1;

                            int get_inside = elevator3.Get_count_inside();
                            get_inside++;
                            if (get_inside <= 10)
                            {
                                elevator3.Set_count_inside(get_inside);
                            }
                        }
                    }
                }
                Thread.Sleep(200);
                elevator3.Set_floor(0);
                elevator3.Set_destination(1);
            }
            AVM.queue_exit += elevator3.Get_count_inside();
        }
        public void Elevator4_Thread(int[] consumer_list)
        {
            elevator4.Set_direction(1);
            //Asansör yukarı çıkmaktadır.
            if (elevator4.Get_direction() == 1)
            {
                for (int i = 0; i < consumer_list.Length; i++)
                {
                    Thread.Sleep(200);
                    elevator4.Set_floor(1);
                    elevator4.Set_destination(2);

                    if (consumer_list[i] == 1)
                    {
                        floor[1] += 1;

                        //Katlardaki kuyrukların kontrol edilmesi işlemi
                        //Do some stuff!!

                        int random_x = r.Next(0, floor[1]);
                        queue[1] += random_x;

                        //Kata müşterinin bırakıldığının bilgisi.
                        int get_inside = elevator4.Get_count_inside();
                        get_inside--;
                        if (get_inside > 0)
                        {
                            elevator4.Set_count_inside(get_inside);
                        }

                    }
                    Thread.Sleep(200);
                    elevator4.Set_floor(2);
                    elevator4.Set_destination(3);

                    if (consumer_list[i] == 2)
                    {
                        floor[2] += 1;
                        int random_x = r.Next(0, floor[2]);
                        queue[2] += random_x;

                        int get_inside = elevator4.Get_count_inside();
                        get_inside--;
                        if (get_inside > 0)
                        {
                            elevator4.Set_count_inside(get_inside);
                        }
                    }

                    Thread.Sleep(200);
                    elevator4.Set_floor(3);
                    elevator4.Set_destination(4);

                    if (consumer_list[i] == 3)
                    {
                        floor[3] += 1;

                        int random_x = r.Next(0, floor[3]);
                        queue[3] += random_x;

                        int get_inside = elevator4.Get_count_inside();
                        get_inside--;
                        if (get_inside > 0)
                        {
                            elevator4.Set_count_inside(get_inside);
                        }
                    }

                    Thread.Sleep(200);
                    elevator4.Set_floor(4);
                    elevator4.Set_destination(3);

                    if (consumer_list[i] == 4)
                    {
                        floor[4] += 1;

                        int random_x = r.Next(0, floor[4]);
                        queue[4] += random_x;

                        int get_inside = elevator4.Get_count_inside();
                        get_inside--;
                        if (get_inside > 0)
                        {
                            elevator4.Set_count_inside(get_inside);
                        }
                    }
                }
            }

            elevator4.Set_direction(0);

            //Asansör aşağı inmektedir.
            if (elevator4.Get_direction() == 0)
            {
                if (floor[4] > 0 && queue[4] > 0)
                {
                    if (elevator4.Get_count_inside() <= 10)
                    {
                        while (queue[4] > 0)
                        {
                            queue[4] -= 1;

                            int get_inside = elevator4.Get_count_inside();
                            get_inside++;
                            if (get_inside <= 10)
                            {
                                elevator4.Set_count_inside(get_inside);
                            }
                        }
                    }
                }
                Thread.Sleep(200);
                elevator4.Set_floor(3);
                elevator4.Set_destination(2);

                if (floor[3] > 0 && queue[3] > 0)
                {
                    if (elevator4.Get_count_inside() <= 10)
                    {
                        while (queue[3] > 0)
                        {
                            queue[3] -= 1;

                            int get_inside = elevator4.Get_count_inside();
                            get_inside++;
                            if (get_inside <= 10)
                            {
                                elevator4.Set_count_inside(get_inside);
                            }
                        }
                    }
                }
                Thread.Sleep(200);
                elevator4.Set_floor(2);
                elevator4.Set_destination(1);

                if (floor[2] > 0 && queue[2] > 0)
                {
                    if (elevator4.Get_count_inside() <= 10)
                    {
                        while (queue[2] > 0)
                        {
                            queue[2] -= 1;

                            int get_inside = elevator4.Get_count_inside();
                            get_inside++;
                            if (get_inside <= 10)
                            {
                                elevator4.Set_count_inside(get_inside);
                            }
                        }
                    }
                }
                Thread.Sleep(200);
                elevator4.Set_floor(1);
                elevator4.Set_destination(0);

                if (floor[1] > 0 && queue[1] > 0)
                {
                    if (elevator4.Get_count_inside() <= 10)
                    {
                        while (queue[1] > 0)
                        {
                            queue[1] -= 1;

                            int get_inside = elevator4.Get_count_inside();
                            get_inside++;
                            if (get_inside <= 10)
                            {
                                elevator4.Set_count_inside(get_inside);
                            }
                        }
                    }
                }
                Thread.Sleep(200);
                elevator4.Set_floor(0);
                elevator4.Set_destination(1);
            }
            AVM.queue_exit += elevator4.Get_count_inside();
        }
        public void Elevator5_Thread(int[] consumer_list)
        {
            elevator5.Set_direction(1);
            //Asansör yukarı çıkmaktadır.
            if (elevator5.Get_direction() == 1)
            {
                for (int i = 0; i < consumer_list.Length; i++)
                {
                    Thread.Sleep(200);
                    elevator5.Set_floor(1);
                    elevator5.Set_destination(2);

                    if (consumer_list[i] == 1)
                    {
                        floor[1] += 1;

                        //Katlardaki kuyrukların kontrol edilmesi işlemi
                        //Do some stuff!!

                        int random_x = r.Next(0, floor[1]);
                        queue[1] += random_x;

                        //Kata müşterinin bırakıldığının bilgisi.
                        int get_inside = elevator5.Get_count_inside();
                        get_inside--;
                        if (get_inside > 0)
                        {
                            elevator5.Set_count_inside(get_inside);
                        }

                    }

                    Thread.Sleep(200);
                    elevator5.Set_floor(2);
                    elevator5.Set_destination(3);

                    if (consumer_list[i] == 2)
                    {
                        floor[2] += 1;
                        int random_x = r.Next(0, floor[2]);
                        queue[2] += random_x;

                        int get_inside = elevator5.Get_count_inside();
                        get_inside--;
                        if (get_inside > 0)
                        {
                            elevator5.Set_count_inside(get_inside);
                        }
                    }

                    Thread.Sleep(200);
                    elevator5.Set_floor(3);
                    elevator5.Set_destination(4);

                    if (consumer_list[i] == 3)
                    {
                        floor[3] += 1;

                        int random_x = r.Next(0, floor[3]);
                        queue[3] += random_x;

                        int get_inside = elevator5.Get_count_inside();
                        get_inside--;
                        if (get_inside > 0)
                        {
                            elevator5.Set_count_inside(get_inside);
                        }
                    }

                    Thread.Sleep(200);
                    elevator5.Set_floor(4);
                    elevator5.Set_destination(3);

                    if (consumer_list[i] == 4)
                    {
                        floor[4] += 1;

                        int random_x = r.Next(0, floor[4]);
                        queue[4] += random_x;

                        int get_inside = elevator5.Get_count_inside();
                        get_inside--;
                        if (get_inside > 0)
                        {
                            elevator5.Set_count_inside(get_inside);
                        }
                    }
                }
            }

            elevator5.Set_direction(0);

            //Asansör aşağı inmektedir.
            if (elevator5.Get_direction() == 0)
            {
                if (floor[4] > 0 && queue[4] > 0)
                {
                    if (elevator5.Get_count_inside() <= 10)
                    {
                        while (queue[4] > 0)
                        {
                            queue[4] -= 1;

                            int get_inside = elevator5.Get_count_inside();
                            get_inside++;
                            if (get_inside <= 10)
                            {
                                elevator5.Set_count_inside(get_inside);
                            }
                        }
                    }
                }
                Thread.Sleep(200);
                elevator5.Set_floor(3);
                elevator5.Set_destination(2);

                if (floor[3] > 0 && queue[3] > 0)
                {
                    if (elevator5.Get_count_inside() <= 10)
                    {
                        while (queue[3] > 0)
                        {
                            queue[3] -= 1;

                            int get_inside = elevator5.Get_count_inside();
                            get_inside++;
                            if (get_inside <= 10)
                            {
                                elevator5.Set_count_inside(get_inside);
                            }
                        }
                    }
                }
                Thread.Sleep(200);
                elevator5.Set_floor(2);
                elevator5.Set_destination(1);

                if (floor[2] > 0 && queue[2] > 0)
                {
                    if (elevator5.Get_count_inside() <= 10)
                    {
                        while (queue[2] > 0)
                        {
                            queue[2] -= 1;

                            int get_inside = elevator5.Get_count_inside();
                            get_inside++;
                            if (get_inside <= 10)
                            {
                                elevator5.Set_count_inside(get_inside);
                            }
                        }
                    }
                }
                Thread.Sleep(200);
                elevator5.Set_floor(1);
                elevator5.Set_destination(0);

                if (floor[1] > 0 && queue[1] > 0)
                {
                    if (elevator5.Get_count_inside() <= 10)
                    {
                        while (queue[1] > 0)
                        {
                            queue[1] -= 1;

                            int get_inside = elevator5.Get_count_inside();
                            get_inside++;
                            if (get_inside <= 10)
                            {
                                elevator5.Set_count_inside(get_inside);
                            }
                        }
                    }
                }
                Thread.Sleep(200);
                elevator5.Set_floor(0);
                elevator5.Set_destination(1);
            }
            AVM.queue_exit += elevator5.Get_count_inside();
        }

        public void Control_Thread()
        {
            //Katlardaki kuyrukları kontrol eder. Kuyrukta bekleyen kişilerin
            //toplam sayısı asansörün kapasitesinin 2 katını aştığı durumda(20) yeni asansörü aktif
            //hale getirir.Kuyrukta bekleyen kişilerin toplam sayısı asansör kapasitenin altına
            // indiğinde asansörlerden biri pasif hale gelir. Bu işlem tek asansörün çalıştığı durumda
            //geçerli değildir.

            int toplam = queue[0] + queue[1] + queue[2] + queue[3] + queue[4];

            if (toplam > 20 && toplam <= 40)
            {
                elevator2.Set_active(true);
                elevator2.Set_mode("working");
            }

            else if(toplam > 40 && toplam <= 60)
            {
                elevator3.Set_active(true);
                elevator3.Set_mode("working");
            }

            else if (toplam > 60 && toplam <= 80)
            {
                elevator4.Set_active(true);
                elevator4.Set_mode("working");
            }

            else if (toplam > 80)
            {
                elevator5.Set_active(true);
                elevator5.Set_mode("working");
            }

            if(toplam<80 && elevator5.Get_active() == true)
            {
                elevator5.Set_active(false);
                elevator5.Set_mode("idle");
            }

            if (toplam < 60 && elevator4.Get_active() == true)
            {
                elevator4.Set_active(false);
                elevator4.Set_mode("idle");
            }

            if (toplam < 40 && elevator3.Get_active() == true)
            {
                elevator3.Set_active(false);
                elevator3.Set_mode("idle");
            }

            if (toplam < 20 && elevator2.Get_active() == true)
            {
                elevator2.Set_active(false);
                elevator2.Set_mode("idle");
            }

        }
    }
}
