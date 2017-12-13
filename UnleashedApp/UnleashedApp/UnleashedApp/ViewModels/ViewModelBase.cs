using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace UnleashedApp.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Property changed event.
        /// </summary>
        /// <param name="propertyName">Name of control to update</param>
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Event for when IsBusy changes
        /// </summary>
        public event EventHandler IsBusyChanged;

        /// <summary>
        /// Event for when IsValid changes
        /// </summary>
        public event EventHandler IsValidChanged;

        readonly List<string> errors = new List<string>();
        bool isBusy = false;

        /// <summary>
        /// Default constructor
        /// </summary>
        public ViewModelBase()
        {
            //Make sure validation is performed on startup
            Validate();
        }

        /// <summary>
        /// Returns true if the current state of the ViewModel is valid
        /// </summary>
        public bool IsValid => errors.Count == 0;

        /// <summary>
        /// A list of errors if IsValid is false
        /// </summary>
        protected List<string> Errors => errors;

        /// <summary>
        /// An aggregated error message
        /// </summary>
        public virtual string Error
        {
            get
            {
                return errors.Aggregate(new StringBuilder(), (b, s) => b.AppendLine(s)).ToString().Trim();
            }
        }

        /// <summary>
        /// Protected method for validating the ViewModel
        /// - Fires PropertyChanged for IsValid and Errors
        /// </summary>
        protected virtual void Validate()
        {
            OnPropertyChanged("IsValid");
            OnPropertyChanged("Errors");

            IsValidChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Other viewmodels should call this when overriding Validate, to validate each property
        /// </summary>
        /// <param name="validate">Func to determine if a value is valid</param>
        /// <param name="error">The error message to use if not valid</param>
        protected virtual void ValidateProperty(Func<bool> validate, string error)
        {
            if (validate())
            {
                if (!Errors.Contains(error))
                    Errors.Add(error);
            }
            else
            {
                Errors.Remove(error);
            }
        }

        /// <summary>
        /// Value indicating if a spinner should be shown
        /// </summary>
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                if (isBusy != value)
                {
                    isBusy = value;

                    OnPropertyChanged(nameof(IsBusy));
                    OnIsBusyChanged();
                }
            }
        }

        /// <summary>
        /// Other viewmodels can override this if something should be done when busy
        /// </summary>
        protected virtual void OnIsBusyChanged()
        {
            IsBusyChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Navigation Service
        /// </summary>
        public static INavigationService NavigationService { get; set; }

        /// <summary>
        /// Messaging system
        /// </summary>
        public static IMessagingCenter MessagingCenter { get; set; }

        /// <summary>
        /// Initialise MessagingCenter and NavigationService.
        /// </summary>
        /// <param name="navigationService">Navigation service instance to use.</param>
        /// <param name="messagingCenter">Messagingcenter instance to use.</param>
        public void InitialiseComponents(IMessagingCenter messagingCenter, INavigationService navigationService)
        {
            if (NavigationService == null)
                NavigationService = navigationService;

            if (MessagingCenter == null)
                MessagingCenter = messagingCenter;
        }
    }
}
