using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuidingManager
{/// <summary>
/// Class for storing and using apartment info 
/// </summary>
    class Apartment
    {
        public int ApartmentNumber { get; set; }
        public string OwnersSurname { get; set; }
        public string OwnersName { get; set; }
        public double WaterUsedInCuboMetres { get; set; }
            
             

        public double ElectrisityUsedInKiloWats { get; set; }
        //Constructor with parametres
        public Apartment(int num , string name , string surname ,double  waterusage ,double electrisityusage)
        {
            ApartmentNumber = num;
            OwnersName = name;
            OwnersSurname = surname;
            WaterUsedInCuboMetres = waterusage;
            ElectrisityUsedInKiloWats = electrisityusage;
        }
        //Base constructor
       public  Apartment() { }
        //Constructor for copy
       public Apartment(Apartment previousAppartment)
        {
            ApartmentNumber = previousAppartment.ApartmentNumber;
            OwnersSurname = previousAppartment.OwnersSurname;
            OwnersName = previousAppartment.OwnersName;
            WaterUsedInCuboMetres = previousAppartment.WaterUsedInCuboMetres;
            ElectrisityUsedInKiloWats = previousAppartment.ElectrisityUsedInKiloWats;
        }
    }
}
