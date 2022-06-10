using System;
using System.Collections.Generic;
using System.Text;

namespace AntesQueVenca.Models
{
    public class AppPage
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public AppPageType Type { get; set; }
        public object ViewModel { get; set; }
    }

    public enum AppPageType
    {
        Products=1,
        Orders=2,
        Home=3,
        Profile=4,
        Help=5
    }
}
