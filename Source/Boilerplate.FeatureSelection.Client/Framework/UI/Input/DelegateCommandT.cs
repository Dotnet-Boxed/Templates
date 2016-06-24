namespace Framework.UI.Input
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;

    /// <summary>
    /// This class allows delegating the commanding logic to methods passed as parameters,
    /// and enables a View to bind commands to objects that are not part of the element tree.
    /// </summary>
    /// <typeparam name="T">The type of the command parameter.</typeparam>
    public sealed class DelegateCommand<T> : Command<T>
    {
        #region Fields

        private readonly Action<T> execute;
        private readonly Func<T, bool> canExecute;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand{T}"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        public DelegateCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand{T}"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        #endregion

        #region Command Members

        /// <summary>
        /// Determines whether this instance can execute.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        /// <returns>
        /// <c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanExecute(T parameter)
        {
            if (this.canExecute != null)
            {
                return this.canExecute(parameter);
            }

            return true;
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        public override void Execute(T parameter)
        {
            if (this.execute != null)
            {
                this.execute(parameter);
            }
        }

        #endregion
    }
}
