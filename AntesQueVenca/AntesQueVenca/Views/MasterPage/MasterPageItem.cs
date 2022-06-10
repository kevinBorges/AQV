using System;
using Xamarin.Forms.Internals;

namespace AntesQueVenca.Views.MasterPage
{
    [Preserve(AllMembers = true)]
    public class MasterPageItem
    {
        public string Title { get; set; }

        public string IconSource { get; set; }

        public Type TargetType { get; set; }
    }
}
