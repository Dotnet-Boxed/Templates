namespace Framework.ComponentModel
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Notifies subscribers that a property in this instance is changing or has changed.
    /// </summary>
    public abstract class NotifyPropertyChanges : Disposable, INotifyPropertyChanged
    {
        #region Private Events

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Protected Methods

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Assert(
                string.IsNullOrEmpty(propertyName) ||
                (this.GetType().GetRuntimeProperty(propertyName) != null),
                "Check that the property name exists for this instance.");

            PropertyChangedEventHandler eventHandler = this.PropertyChanged;

            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyNames">The property names.</param>
        protected void OnPropertyChanged(params string[] propertyNames)
        {
            if (propertyNames == null)
            {
                throw new ArgumentNullException(nameof(propertyNames));
            }

            foreach (string propertyName in propertyNames)
            {
                this.OnPropertyChanged(propertyName);
            }
        }

        /// <summary>
        /// Sets the value of the property to the specified value if it has changed.
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="currentValue">The current value of the property.</param>
        /// <param name="newValue">The new value of the property.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns><c>true</c> if the property was changed, otherwise <c>false</c>.</returns>
        protected bool SetProperty<TProp>(
            ref TProp currentValue,
            TProp newValue,
            [CallerMemberName] string propertyName = null)
        {
            this.ThrowIfDisposed();

            if (!object.Equals(currentValue, newValue))
            {
                currentValue = newValue;
                this.OnPropertyChanged(propertyName);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets the value of the property to the specified value if it has changed.
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="currentValue">The current value of the property.</param>
        /// <param name="newValue">The new value of the property.</param>
        /// <param name="propertyNames">The names of all properties changed.</param>
        /// <returns><c>true</c> if the property was changed, otherwise <c>false</c>.</returns>
        protected bool SetProperty<TProp>(
            ref TProp currentValue,
            TProp newValue,
            params string[] propertyNames)
        {
            this.ThrowIfDisposed();

            if (!object.Equals(currentValue, newValue))
            {
                currentValue = newValue;
                this.OnPropertyChanged(propertyNames);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets the value of the property to the specified value if it has changed.
        /// </summary>
        /// <param name="equal">A function which returns <c>true</c> if the property value has changed, otherwise <c>false</c>.</param>
        /// <param name="action">The action where the property is set.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns><c>true</c> if the property was changed, otherwise <c>false</c>.</returns>
        protected bool SetProperty(
            Func<bool> equal, 
            Action action,
            [CallerMemberName] string propertyName = null)
        {
            this.ThrowIfDisposed();

            if (equal())
            {
                return false;
            }

            action();
            this.OnPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Sets the value of the property to the specified value if it has changed.
        /// </summary>
        /// <param name="equal">A function which returns <c>true</c> if the property value has changed, otherwise <c>false</c>.</param>
        /// <param name="action">The action where the property is set.</param>
        /// <param name="propertyNames">The property names.</param>
        /// <returns><c>true</c> if the property was changed, otherwise <c>false</c>.</returns>
        protected bool SetProperty(
            Func<bool> equal, 
            Action action,
            params string[] propertyNames)
        {
            this.ThrowIfDisposed();

            if (equal())
            {
                return false;
            }

            action();
            this.OnPropertyChanged(propertyNames);

            return true;
        }

        #endregion
    }
}
