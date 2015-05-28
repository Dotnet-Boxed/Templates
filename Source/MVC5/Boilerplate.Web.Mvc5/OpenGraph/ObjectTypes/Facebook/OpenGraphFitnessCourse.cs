namespace Boilerplate.Web.Mvc.OpenGraph
{
    using System.Text;

    /// <summary>
    /// This object type represents the user's activity contributing to a particular run, walk, or bike course. This object type is not part of the 
    /// Open Graph standard but is used by Facebook.
    /// </summary>
    public class OpenGraphFitnessCourse : OpenGraphMetadata
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphFitnessCourse" /> class.
        /// </summary>
        /// <param name="title">The title of the object as it should appear in the graph.</param>
        /// <param name="image">The default image.</param>
        public OpenGraphFitnessCourse(string title, OpenGraphImage image)
            : base(title, image)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphFitnessCourse" /> class.
        /// </summary>
        /// <param name="title">The title of the object as it should appear in the graph.</param>
        /// <param name="image">The default image.</param>
        /// <param name="url">The canonical URL of the object, used as its ID in the graph.</param>
        public OpenGraphFitnessCourse(string title, OpenGraphImage image, string url)
            : base(title, image, url)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets an integer representing the number of calories used during the course.
        /// </summary>
        public int? Calories { get; set; }

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace { get { return "fitness: http://ogp.me/ns/fitness#"; } }

        /// <summary>
        /// Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type { get { return OpenGraphType.FitnessCourse; } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <see cref="stringBuilder" /> containing the Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            base.ToString(stringBuilder);

            stringBuilder.AppendMeta("fitness:calories", this.Calories);

            // TODO - All subvalues are required except altitude
            //fitness:custom_unit_energy	CustomUnitQuantity	The energy used during the course
            //fitness:custom_unit_energy:value	Float	A quantity representing the energy used during the course, measured in a custom unit
            //fitness:custom_unit_energy:units	Reference	A custom unit of the energy used during the course
            //fitness:distance	Quantity	The distance of the course
            //fitness:distance:value	Float	A quantity representing the distance covered during the course
            //fitness:distance:units	Enum	The unit of the value representing the distance covered during the course
            //fitness:duration	Quantity	The duration taken on the course
            //fitness:duration:value	Float	A quantity representing the duration of the course
            //fitness:duration:units	Enum	The unit of the duration of the course
            //fitness:live_text	String	A string value displayed in stories if the associated action's end_time has not passed, such as an encouragement to friends to cheer the user on. The value is not rendered once the course has been completed.
            //fitness:metrics	Array<FitnessActivityDataPoint>	A struct of various metrics about the course
            //fitness:metrics:calories	Integer	An array of integers representing the number of calories used during distinct parts of the course
            //fitness:metrics:custom_unit_energy	CustomUnitQuantity	
            //fitness:metrics:distance	Quantity	
            //fitness:metrics:distance:value	Float	An array of quantities representing the distance covered during distinct parts of the course
            //fitness:metrics:distance:units	Enum	An array of the units of the values representing the distance covered during distinct parts of the course
            //fitness:metrics:location	GeoPoint	
            //fitness:metrics:location:latitude	Float	An array of the latitudes of the locations of distinct parts of the course
            //fitness:metrics:location:longitude	Float	An array of the longitudes of the locations of distinct parts of the course
            //fitness:metrics:location:altitude	Float	An array of the altitudes (in meters above sea level) of the locations of distinct parts of the course
            //fitness:metrics:steps	Integer	An array of integers representing the number of steps taken during distinct parts of the course
            //fitness:metrics:speed	Quantity	
            //fitness:metrics:speed:value	Float	An array of quantities representing the speed achieved during distinct parts of the course
            //fitness:metrics:speed:units	Enum	An array of the units of the values representing the speed achieved during distinct parts of the course
            //fitness:metrics:timestamp	DateTime	An array of the times of distinct parts of the course
            //fitness:metrics:pace	Quantity	
            //fitness:metrics:pace:value	Float	An array of quantities representing the pace achieved during distinct parts of the course
            //fitness:metrics:pace:units	Enum	An array of the units of the values representing the pace achieved during distinct parts of the course
            //fitness:pace	Quantity	The pace achieved on the course
            //fitness:pace:value	Float	A quantity representing the pace achieved during the course
            //fitness:pace:units	Enum	The unit of the value representing the pace achieved during the course
            //fitness:speed	Quantity	The speed achieved on the course
            //fitness:speed:value	Float	A quantity representing the speed achieved during the course
            //fitness:speed:units	Enum	The unit of the value representing the speed achieved during the course
            //fitness:splits	FitnessSplits	A struct of split times during the course
            //fitness:splits:unit	Enum	An array of the units used for splits during the course, either 'mi' or 'km'
            //fitness:splits:values	Array<Quantity>	
            //fitness:steps	Integer	No longer used
        }

        #endregion
    }
}
