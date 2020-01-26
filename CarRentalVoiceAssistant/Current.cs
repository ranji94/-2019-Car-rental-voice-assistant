using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CarRentalVoiceAssistant
{
    public sealed class Current
    {
        private static Current instance = null;
        private static readonly object padlock = new object();

        private Page page;
        public Page Page
        {
            get { return page; }
            set { page = value; }
        }

        public static Current Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Current();
                    }
                    return instance;
                }
            }
        }
    }
}
