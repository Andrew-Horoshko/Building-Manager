using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuidingManager
{
    class Apartment
    {
        public int ApartmentNumber { get; set; }
        public string OwnersSurname { get; set; }
        public string OwnersName { get; set; }
        public double WaterUsedInCuboMetres { get; set; }
        public double ElectrisityUsedInKiloWats { get; set; }
        private int Year  { set; get; }
        public Apartment(int num , string name , string surname ,double  waterusage ,double electrisityusage)
        {
            ApartmentNumber = num;
            OwnersName = name;
            OwnersSurname = surname;
            WaterUsedInCuboMetres = waterusage;
            ElectrisityUsedInKiloWats = electrisityusage;
        }
       public  Apartment() { }
    }
}
