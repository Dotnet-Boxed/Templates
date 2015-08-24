namespace Framework.UI.Input
{
    using System.Windows.Input;

    /// <summary>
    /// <see cref="ICommand"/> base class.
    /// </summary>
    public abstract class Command : CommandBase
    {
        #region CommandBase Members

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        public override bool CanExecute(object parameter)
        {
            return this.CanExecute();
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public override void Execute(object parameter)
        {
            this.Execute();
        } 

        #endregion

        #region Public Methods

        /// <summary>
        /// Determines whether this instance can execute.
        /// </summary>
        /// <returns>
        /// <c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool CanExecute()
        {
            return true;
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public abstract void Execute();

        #endregion
    }
}
