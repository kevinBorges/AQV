using AntesQueVenca.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AntesQueVenca.ViewModels
{
    public class ProfileViewModel
    {
        public ObservableCollection<ProfilePage> ProfilePages { get; private set; }
        public ObservableCollection<ProfilePage> InformationPages { get; private set; }

        public ProfileViewModel()
        {
            ProfilePages = new ObservableCollection<ProfilePage>(new List<ProfilePage>
            {
                new ProfilePage
                {
                    PageId=1,
                    PageTitle = "Endereços",
                    PageIcon = "addressesIcon"
                },
                new ProfilePage
                {
                    PageId=2,
                    PageTitle = "Pagamentos",
                    PageIcon = "paymentsIcon"
                },
                new ProfilePage
                {
                    PageId=3,
                    PageTitle = "Meus pedidos",
                    PageIcon = "myOrdersIcon"
                },
                new ProfilePage
                {
                    PageId=4,
                    PageTitle = "Sugerir mercado",
                    PageIcon = "sugestMarketIcon"
                }
            });

            InformationPages = new ObservableCollection<ProfilePage>(new List<ProfilePage> {
                new ProfilePage
                {
                    PageId=5,
                    PageTitle = "Seja um parceiro",
                    PageIcon = "partnersIcon"
                },
                new ProfilePage
                {
                    PageId=6,
                    PageTitle = "Termos e condições",
                    PageIcon = "termsIcon"
                }
            });
    }
    }
}
