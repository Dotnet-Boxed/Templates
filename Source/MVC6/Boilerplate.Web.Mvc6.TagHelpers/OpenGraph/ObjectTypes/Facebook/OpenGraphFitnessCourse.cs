namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.AspNet.Razor.TagHelpers;

    /// <summary>
    /// This object type represents the user's activity contributing to a particular run, walk, or bike course. This object type is not part of the 
    /// Open Graph standard but is used by Facebook.
    /// See https://developers.facebook.com/docs/reference/opengraph/object-type/fitness.course/
    /// </summary>
    [HtmlTargetElement(
        "open-graph-fitness-course", 
        Attributes = TitleAttributeName + "," + MainImageAttributeName, 
        TagStructure = TagStructure.WithoutEndTag)]
    public class OpenGraphFitnessCourse : OpenGraphMetadata
    {
        #region Constants

        private const string CaloriesAttributeName = "calories";
        private const string CustomUnitEnergyAttributeName = "custom-unit-energy";
        private const string DistanceAttributeName = "distance";
        private const string DurationAttributeName = "duration";
        private const string LiveTextAttributeName = "live-text";
        private const string MetricsAttributeName = "metrics";
        private const string PaceAttributeName = "pace";
        private const string SplitsAttributeName = "splits";
        private const string SpeedAttributeName = "speed";

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets an integer representing the number of calories used during the course.
        /// </summary>
        [HtmlAttributeName(CaloriesAttributeName)]
        public int? Calories { get; set; }

        /// <summary>
        /// Gets or sets the energy used during the course.
        /// </summary>
        [HtmlAttributeName(CustomUnitEnergyAttributeName)]
        public OpenGraphQuantity CustomUnitEnergy { get; set; }

        /// <summary>
        /// Gets or sets the distance of the course.
        /// </summary>
        [HtmlAttributeName(DistanceAttributeName)]
        public OpenGraphQuantity Distance { get; set; }

        /// <summary>
        /// Gets or sets the duration taken on the course.
        /// </summary>
        [HtmlAttributeName(DurationAttributeName)]
        public OpenGraphQuantity Duration { get; set; }

        /// <summary>
        /// Gets or sets a string value displayed in stories if the associated action's end_time has not passed, such as an encouragement to friends to cheer the user on. The value is not rendered once the course has been completed.
        /// </summary>
        [HtmlAttributeName(LiveTextAttributeName)]
        public string LiveText { get; set; }

        /// <summary>
        /// Gets or sets the metrics about the course.
        /// </summary>
        [HtmlAttributeName(MetricsAttributeName)]
        public IEnumerable<OpenGraphFitnessActivityDataPoint> Metrics { get; set; }

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace { get { return "fitness: http://ogp.me/ns/fitness#"; } }

        /// <summary>
        /// Gets or sets the pace achieved on the course.
        /// </summary>
        [HtmlAttributeName(PaceAttributeName)]
        public OpenGraphQuantity Pace { get; set; }

        /// <summary>
        /// Gets or sets the split times during the course.
        /// </summary>
        [HtmlAttributeName(SplitsAttributeName)]
        public IEnumerable<OpenGraphSplit> Splits { get; set; }

        /// <summary>
        /// Gets or sets the speed achieved on the course.
        /// </summary>
        [HtmlAttributeName(SpeedAttributeName)]
        public OpenGraphQuantity Speed { get; set; }

        /// <summary>
        /// Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type { get { return OpenGraphType.FitnessCourse; } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <paramref name="stringBuilder"/> containing the Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            base.ToString(stringBuilder);

            stringBuilder.AppendMetaPropertyContent("fitness:calories", this.Calories);

            if (this.CustomUnitEnergy != null)
            {
                stringBuilder.AppendMetaPropertyContent("fitness:custom_unit_energy:value", this.CustomUnitEnergy.Value);
                stringBuilder.AppendMetaPropertyContent("fitness:custom_unit_energy:units", this.CustomUnitEnergy.Units);
            }

            if (this.Distance != null)
            {
                stringBuilder.AppendMetaPropertyContent("fitness:distance:value", this.Distance.Value);
                stringBuilder.AppendMetaPropertyContent("fitness:distance:units", this.Distance.Units);
            }

            if (this.Duration != null)
            {
                stringBuilder.AppendMetaPropertyContent("fitness:duration:value", this.Duration.Value);
                stringBuilder.AppendMetaPropertyContent("fitness:duration:units", this.Duration.Units);
            }

            stringBuilder.AppendMetaPropertyContentIfNotNull("fitness:live_text", this.LiveText);

            if (this.Metrics != null)
            {
                foreach (OpenGraphFitnessActivityDataPoint metric in this.Metrics)
                {
                    stringBuilder.AppendMetaPropertyContentIfNotNull("fitness:metrics:calories", metric.Calories);

                    if (metric.CustomUnitEnergy != null)
                    {
                        stringBuilder.AppendMetaPropertyContent("fitness:metrics:custom_unit_energy:value", metric.CustomUnitEnergy.Value);
                        stringBuilder.AppendMetaPropertyContent("fitness:metrics:custom_unit_energy:units", metric.CustomUnitEnergy.Units);
                    }

                    if (metric.Distance != null)
                    {
                        stringBuilder.AppendMetaPropertyContent("fitness:metrics:distance:value", metric.Distance.Value);
                        stringBuilder.AppendMetaPropertyContent("fitness:metrics:distance:units", metric.Distance.Units);
                    }

                    if (metric.Location != null)
                    {
                        stringBuilder.AppendMetaPropertyContent("fitness:metrics:location:latitude", metric.Location.Latitude);
                        stringBuilder.AppendMetaPropertyContent("fitness:metrics:location:longitude", metric.Location.Longitude);
                        stringBuilder.AppendMetaPropertyContentIfNotNull("fitness:metrics:location:altitude", metric.Location.Altitude);
                    }

                    stringBuilder.AppendMetaPropertyContentIfNotNull("fitness:metrics:steps", metric.Steps);

                    if (metric.Speed != null)
                    {
                        stringBuilder.AppendMetaPropertyContent("fitness:metrics:speed:value", metric.Speed.Value);
                        stringBuilder.AppendMetaPropertyContent("fitness:metrics:speed:units", metric.Speed.Units);
                    }

                    stringBuilder.AppendMetaPropertyContentIfNotNull("fitness:metrics:timestamp", metric.Timestamp);

                    if (metric.Pace != null)
                    {
                        stringBuilder.AppendMetaPropertyContent("fitness:metrics:pace:value", metric.Pace.Value);
                        stringBuilder.AppendMetaPropertyContent("fitness:metrics:pace:units", metric.Pace.Units);
                    }
                }
            }

            if (this.Pace != null)
            {
                stringBuilder.AppendMetaPropertyContent("fitness:pace:value", this.Pace.Value);
                stringBuilder.AppendMetaPropertyContent("fitness:pace:units", this.Pace.Units);
            }

            if (this.Speed != null)
            {
                stringBuilder.AppendMetaPropertyContent("fitness:speed:value", this.Speed.Value);
                stringBuilder.AppendMetaPropertyContent("fitness:speed:units", this.Speed.Units);
            }

            if (this.Splits != null)
            {
                foreach (OpenGraphSplit split in this.Splits)
                {
                    stringBuilder.AppendMetaPropertyContent("fitness:splits:unit", split.IsMiles ? "mi" : "km");
                    foreach (OpenGraphQuantity value in split.Values)
                    {
                        stringBuilder.AppendMetaPropertyContent("fitness:splits:values:value", value.Value);
                        stringBuilder.AppendMetaPropertyContent("fitness:splits:values:units", value.Units);
                    }
                }
            }
        }

        #endregion
    }
}
