using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Projeakt2Interakcija
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string token { get; set; }
        public App()
        {
            token = "xVxT2PZ8qE3xaFFPjKw5~PT7xyJ7f6w08OEE_X-7h3w~AsmYdgAX6Z9YEnh9qtbQFKMcnoXIHYYd0KXzN8NSwjnvjx-S0zsy9ydMpfh3d8fr";
        }
    }
}
