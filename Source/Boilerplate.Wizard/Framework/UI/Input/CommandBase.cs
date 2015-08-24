namespace Framework.UI.Input
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;

    /// <summary>
    /// <see cref="ICommand"/> base class.
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandBase"/> class.
        /// </summary>
        protected CommandBase()
        {
        }

        #endregion

        #region ICommand Members

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        #endregion

        #region ICommand Members

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        public abstract bool CanExecute(object parameter);

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public abstract void Execute(object parameter);

        #endregion

        #region Public Methods

        /// <summary>
        /// Raises the can execute changed event.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            this.OnCanExecuteChanged();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Called when can execute is changed.
        /// </summary>
        protected virtual void OnCanExecuteChanged()
        {
            EventHandler eventHandler = this.CanExecuteChanged;

            if (eventHandler != null)
            {
                eventHandler(this, EventArgs.Empty);
            }
        }

        #endregion
    }
}
