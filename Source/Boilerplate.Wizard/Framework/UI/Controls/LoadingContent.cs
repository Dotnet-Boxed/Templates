namespace Framework.UI.Controls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// Switches between a loading screen and some content.
    /// </summary>
    [TemplateVisualState(Name = LoadingContent.LoadedState, GroupName = LoadingContent.CommonStateGroup)]
    [TemplateVisualState(Name = LoadingContent.UnloadedState, GroupName = LoadingContent.CommonStateGroup)]
    public sealed class LoadingContent : HeaderedContentControl
    {
        #region Dependency Properties

        /// <summary>
        /// The opacity of the loading animation.
        /// </summary>
        public static readonly DependencyProperty ContentOpacityProperty = DependencyProperty.Register(
            "ContentOpacity",
            typeof(double),
            typeof(LoadingContent),
            new PropertyMetadata(0.0d));

        /// <summary>
        /// The fill.
        /// </summary>
        public static readonly DependencyProperty FillProperty = DependencyProperty.Register(
            "Fill",
            typeof(Brush),
            typeof(LoadingContent),
            new PropertyMetadata(null));

        /// <summary>
        /// Whether or not this instance is loaded.
        /// </summary>
        public static readonly DependencyProperty IsContentLoadedProperty = DependencyProperty.Register(
            "IsContentLoaded",
            typeof(bool),
            typeof(LoadingContent),
            new PropertyMetadata(true, OnIsLoadedChanged));

        #endregion

        #region Constants

        private const string LoadingAnimationElement = "LoadingAnimationElement";
        private const string CommonStateGroup = "CommonStates";
        private const string LoadedState = "Loaded";
        private const string UnloadedState = "Unloaded";

        #endregion

        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="LoadingContent"/> class.
        /// </summary>
        public LoadingContent()
        {
            this.DefaultStyleKey = typeof(LoadingContent);
            this.Header = "L o a d i n g";
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the opacity of the content.
        /// </summary>
        /// <value>The content opacity.</value>
        public double ContentOpacity
        {
            get { return (double)this.GetValue(ContentOpacityProperty); }
            set { this.SetValue(ContentOpacityProperty, value); }
        }

        /// <summary>
        /// Gets or sets the ellipse fill.
        /// </summary>
        /// <value>The ellipse fill.</value>
        public Brush Fill
        {
            get { return (Brush)this.GetValue(FillProperty); }
            set { this.SetValue(FillProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is loaded.
        /// </summary>
        /// <value><c>true</c> if this instance is loaded; otherwise, <c>false</c>.</value>
        public bool IsContentLoaded
        {
            get { return (bool)this.GetValue(IsContentLoadedProperty); }
            set { this.SetValue(IsContentLoadedProperty, value); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.IsContentLoaded)
            {
                VisualStateManager.GoToState(this, LoadedState, true);
            }
            else
            {
                VisualStateManager.GoToState(this, UnloadedState, true);
            }
        }

        #endregion

        #region Private Static Methods

        /// <summary>
        /// Called when the is loaded is changed.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnIsLoadedChanged(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            LoadingContent loadingContent = (LoadingContent)dependencyObject;

            if (loadingContent.IsContentLoaded)
            {
                VisualStateManager.GoToState(loadingContent, LoadedState, true);
            }
            else
            {
                VisualStateManager.GoToState(loadingContent, UnloadedState, true);
            }
        }

        #endregion
    }
}