namespace Framework.UI.Input
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;

    /// <summary>
    /// This class allows delegating the commanding logic to methods passed as parameters,
    /// and enables a View to bind commands to objects that are not part of the element tree.
    /// </summary>
    public sealed class DelegateCommand : Command
    {
        #region Fields

        private readonly Action execute;
        private readonly Func<bool> canExecute;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        public DelegateCommand(Action execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        #endregion

        #region Command Members

        /// <summary>
        /// Determines whether this instance can execute.
        /// </summary>
        /// <returns>
        /// <c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanExecute()
        {
            if (this.canExecute != null)
            {
                return this.canExecute();
            }

            return true;
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public override void Execute()
        {
            if (this.execute != null)
            {
                this.execute();
            }
        }

        #endregion
    }
}