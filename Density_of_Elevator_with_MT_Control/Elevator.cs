using System;
using System.Collections.Generic;
using System.Text;

namespace Density_of_Elevator_with_MT_Control
{
    class Elevator
    {
        bool active;
        string mode = "idle";
        int floor;
        int destination;
        int direction;
        int capacity = 10;
        int count_inside;
        int inside;

        public Elevator() { }

        public Elevator(bool active,string mode,int floor,
                        int destination,int direction,int capacity,
                        int count_inside,int inside) {
            this.active = active;
            this.mode = mode;
            this.floor = floor;
            this.destination = destination;
            this.direction = direction;
            this.capacity = capacity;
            this.count_inside = count_inside;
            this.inside = inside;
        }

        public bool Get_active()
        {
            return this.active;
        }
        public void Set_active(bool active)
        {
            this.active = active;
        }
        public string Get_mode()
        {
            return this.mode;
        }
        public void Set_mode(string mode)
        {
            this.mode = mode;
        }
        public int Get_floor()
        {
            return this.floor;
        }
        public void Set_floor(int floor)
        {
            this.floor = floor;
        }
        public int Get_destination()
        {
            return this.destination;
        }
        public void Set_destination(int destination)
        {
            this.destination = destination;
        }
        public int Get_direction()
        {
            return this.direction;
        }
        public void Set_direction(int direction)
        {
            this.direction = direction;
        }
        public int Get_capacity()
        {
            return this.capacity;
        }
        public void Set_capacity(int capacity)
        {
            this.capacity = capacity;
        }
        public int Get_count_inside()
        {
            return this.count_inside;
        }
        public void Set_count_inside(int count_inside)
        {
            this.count_inside = count_inside;
        }
        public int Get_inside()
        {
            return this.inside;
        }
        public void Set_inside(int inside)
        {
            this.inside = inside;
        }
    }
}
