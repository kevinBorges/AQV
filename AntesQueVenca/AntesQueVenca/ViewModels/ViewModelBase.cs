using AntesQueVenca.Domain.Exceptions;
using AntesQueVenca.Interfaces;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AntesQueVenca.ViewModels
{
    public class ViewModelBase<T> : INotifyPropertyChanged
     where T : class, new()
    {
        private T _entity;
        
        public IMessage Message { get; set; }
        public INavigation Navigation { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private bool _isEnable;
        private bool _isRefreshing;
        private bool _isVisible;
        private double _Opacity;

        public bool isEnable
        {
            get
            {
                return _isEnable;
            }
            set
            {
                _isEnable = value;
                RaisePropertyChanged("isEnable");
            }
        }

        public bool isVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                _isVisible = value;
                RaisePropertyChanged("isVisible");
            }
        }

        public bool isRefreshing
        {
            get
            {
                return _isRefreshing;
            }
            set
            {
                _isRefreshing = value;
                RaisePropertyChanged("isRefreshing");
            }
        }

        public double Opacity
        {
            get
            {
                return _Opacity;
            }
            set
            {
                _Opacity = value;
                RaisePropertyChanged("Opacity");
            }
        }

        public ViewModelBase()
        {
            Opacity = 1;
            Entity = new T();
            isEnable = true;
        }

        public T Entity
        {
            get
            {
                return _entity;
            }
            set
            {
                _entity = value;
                RaisedPropertyChanged(() => Entity);
            }
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }

        protected void RaisedPropertyChanged<TT>(Expression<Func<TT>> expression)
        {
            var member = expression.Body as MemberExpression;
            var propertyInfo = member.Member as PropertyInfo;

            if (propertyInfo != null)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyInfo.Name));
            }
        }

        protected bool CheckInternet()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
                return true;
            else
                return false;
        }

        public void HasInternet()
        {
            if (!CheckInternet())
                throw new ValidationException("Por favor, verifique sua conexão e tente novamente.");
        }

        public void BlockControls()
        {
            isEnable = false;
            Opacity = 0.2;
            isRefreshing = true;
        }

        public void UnlockControls()
        {
            isEnable = true;
            Opacity = 1;
            isRefreshing = false;
        }
    }
}
