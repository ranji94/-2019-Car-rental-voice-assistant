using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalVoiceAssistant
{
    public sealed class Reservation
    {
        private static Reservation instance = null;
        private static readonly object padlock = new object();

        private String make = "Opel";
        public String Make { 
            get { return make; }
            set { make = value; }
        }
        private String model = "Astra";
        public String Model {
            get { return model; }
            set { model = value; }
        }
        private String fromDate;
        public String FromDate {
            get { return fromDate; }
            set { fromDate = value; }
        }
        private String toDate;
        public String ToDate {
            get { return toDate; }
            set { toDate = value; }
        }
        private String price;
        public String Price {
            get { return price; }
            set { price = value; }
        }
        private String personalName;
        public String PersonalName {
            get { return personalName; }
            set { personalName = value; }
        }
        private String surname;
        public String Surname {
            get { return surname; }
            set { surname = value; }
        }

        public static Reservation Instance {
            get {
                lock (padlock) {
                    if (instance == null) {
                        instance = new Reservation();
                    }
                    return instance;
                }
            }
        }
    }
}
