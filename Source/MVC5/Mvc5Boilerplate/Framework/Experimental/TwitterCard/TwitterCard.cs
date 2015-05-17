namespace MvcBoilerplate.Framework
{
    using System;

    public class PlayerTwitterCard : TwitterCard
    {
        public PlayerTwitterCard(string username)
            : base(username)
        {
        }

        public override string Type
        {
            get { return "player"; }
        }
    }


    /// <summary>
    /// The App Card is a great way to represent mobile applications on Twitter and to drive installs. We designed 
    /// the App Card to allow for a name, description and icon, and also to highlight attributes such as the rating 
    /// and the price. This Card type is currently available on the twitter.com website, as well as iOS and Android 
    /// mobile clients. It is not yet available on mobile web. See https://dev.twitter.com/cards/types/app.
    /// </summary>
    public class AppTwitterCard : TwitterCard
    {
        public AppTwitterCard(string username)
            : base(username)
        {
        }

        public override string Type
        {
            get { return "app"; }
        }
    }

    public class ProductTwitterCard : CreatorTwitterCard
    {
        public ProductTwitterCard(string username)
            : base(username)
        {
        }

        public override string Type
        {
            get { return "product"; }
        }
    }


    /// <summary>
    /// The Gallery Card allows you to represent collections of photos within a Tweet. This Card type is 
    /// designed to let the user know that there’s more than just a single image at the URL shared, but 
    /// rather a gallery of related images. You can specify up to 4 different images to show in the gallery 
    /// card via the twitter:image[0-3] tags. You can also provide attribution to the photographer of the 
    /// gallery by specifying the value of the twitter:creator tag. See 
    /// https://dev.twitter.com/cards/types/gallery.
    /// </summary>
    public class GalleryTwitterCard : CreatorTwitterCard
    {
        public GalleryTwitterCard(string username)
            : base(username)
        {
        }

        /// <summary>
        /// Gets or sets the title of your content as it should appear in the card. You may specify an empty 
        /// string if you wish no title to render.
        /// </summary>
        public string Title
        { get; set; }

        public override string Type
        {
            get { return "gallery"; }
        }
    }

    /// <summary>
    /// The Photo Card puts the image front and center in the Tweet. Clicking on the photo expands it to a 
    /// richer, detailed view. On twitter.com and mobile clients, the image appears below the tweet text.
    /// See https://dev.twitter.com/cards/types/photo
    /// </summary>
    public class PhotoTwitterCard : CreatorTwitterCard
    {
        private readonly string image;

        public PhotoTwitterCard(string username, string image)
            : base(username)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            this.image = image;
        }

        /// <summary>
        /// Gets or sets the image. A URL to the image representing the content. Image must be less than 1MB in size.
        /// 
        /// 
        /// 
        /// 
        /// TODO
        /// Twitter will resize images, maintaining original aspect ratio to fit the following sizes:
        /// - Web: maximum height of 375px, maximum width of 435px
        /// - Mobile (non-retina displays): maximum height of 375px, maximum width of 280px
        /// - Mobile (retina displays): maximum height of 750px, maximum width of 560px
        /// Twitter will not create a photo card unless the twitter:image is of a minimum size of 280px wide by 150px tall. Images will not be cropped unless they have an exceptional aspect ratio. Images must be less than 1MB in size. Images will be fetched and proxied by Twitter to ensure a high quality of service and SSL security for users.
        /// Specifying the width and height using twitter:image:width and twitter:image:height helps us more accurately preserve the aspect ratio of the image when resizing.
        /// Photo Cards are the only type of card which support a blank title. To render no title for the photo card, explicitly include a blank title element. For example: <meta name="twitter:title" content="">.
        /// Animated gifs are currently supported in Twitter Cards via the Player Card.
        /// </summary>
        public string Image
        { get { return this.image; } }

        /// <summary>
        /// Gets or sets the height of the image in pixels to help Twitter accurately preserve the aspect ratio of 
        /// the image when resizing.
        /// </summary>
        public int ImageHeight
        { get; set; }

        /// <summary>
        /// Gets or sets the width of the image in pixels to help Twitter accurately preserve the aspect ratio of 
        /// the image when resizing.
        /// </summary>
        public int ImageWidth
        { get; set; }

        /// <summary>
        /// Gets or sets the title of your content as it should appear in the card. You may specify an empty 
        /// string if you wish no title to render.
        /// </summary>
        public string Title
        { get; set; }

        public override string Type
        {
            get { return "photo"; }
        }
    }

    /// <summary>
    /// The Summary Card can be used for many kinds of web content, from blog posts and news articles, to 
    /// products and restaurants. It is designed to give the reader a preview of the content before clicking 
    /// through to your website. See https://dev.twitter.com/cards/types/summary.
    /// </summary>
    public class SummaryTwitterCard : TwitterCard
    {
        private readonly string description;
        private readonly string title;

        public SummaryTwitterCard(string username, string title, string description)
            : base(username)
        {
            if (title == null)
            {
                throw new ArgumentNullException("title");
            }

            if (description == null)
            {
                throw new ArgumentNullException("description");
            }

            this.title = title;
        }

        /// <summary>
        /// Gets or sets the URL to a unique image representing the content of the page. Do not use a generic 
        /// image such as your website logo, author photo, or other image that spans multiple pages. The 
        /// image must be a minimum size of 120px by 120px and must be less than 1MB in file size. For an 
        /// expanded tweet and its detail page, the image will be cropped to a 4:3 aspect ratio and resized 
        /// to be displayed at 120px by 90px. The image will also be cropped and resized to 120px by 120px 
        /// for use in embedded tweets. If you are using Facebook's Open Graph og:image, do not use this.
        /// </summary>
        public string Image
        { get; set; }

        /// <summary>
        /// Gets the description that concisely summarizes the content of the page, as appropriate for 
        /// presentation within a Tweet. Do not re-use the title text as the description, or use this field 
        /// to describe the general services provided by the website. Description text will be truncated at 
        /// the word to 200 characters. If you are using Facebook's Open Graph og:description, do not use this.
        /// </summary>
        public string Description
        { get { return this.description; } }

        /// <summary>
        /// Gets the title of the summary. Title should be concise and will be truncated at 70 characters.
        /// If you are using Facebook's Open Graph og:title, do not use this.
        /// </summary>
        public string Title
        { get { return this.title; } }

        public string Url
        { get; set; }

        public override string Type
        {
            get { return "summary"; }
        }
    }

    /// <summary>
    /// The Summary Card with Large Image features a large, full-width prominent image alongside a tweet. 
    /// It is designed to give the reader a rich photo experience, and clicking on the image brings the 
    /// user to your website. On twitter.com and the mobile clients, the image appears below the tweet text.
    /// See https://dev.twitter.com/cards/types/summary-large-image.
    /// </summary>
    public class SummaryLargeImageTwitterCard : CreatorTwitterCard
    {
        private readonly string description;
        private readonly string title;

        public SummaryLargeImageTwitterCard(string username, string title, string description)
            : base(username)
        {
            if (title == null)
            {
                throw new ArgumentNullException("title");
            }

            if (description == null)
            {
                throw new ArgumentNullException("description");
            }

            this.title = title;
        }

        /// <summary>
        /// Gets or sets the URL to a unique image representing the content of the page. Do not use a generic 
        /// image such as your website logo, author photo, or other image that spans multiple pages. Images 
        /// for this Card should be at least 280px in width, and at least 150px in height. Image must be less 
        /// than 1MB in size. If you are using Facebook's Open Graph og:image, do not use this.
        /// </summary>
        public string Image
        { get; set; }

        /// <summary>
        /// Gets the description that concisely summarizes the content of the page, as appropriate for 
        /// presentation within a Tweet. Do not re-use the title text as the description, or use this field 
        /// to describe the general services provided by the website. Description text will be truncated at 
        /// the word to 200 characters. If you are using Facebook's Open Graph og:description, do not use this.
        /// </summary>
        public string Description
        { get { return this.description; } }

        /// <summary>
        /// Gets the title of the summary. Title should be concise and will be truncated at 70 characters.
        /// If you are using Facebook's Open Graph og:title, do not use this.
        /// </summary>
        public string Title
        { get { return this.title; } }

        public override string Type
        {
            get { return "summary_large_image"; }
        }
    }

    public abstract class CreatorTwitterCard : TwitterCard
    {
        public CreatorTwitterCard(string username)
            : base(username)
        {
        }

        /// <summary>
        /// Gets or sets the Twitter username of the creator of the content on the page e.g. @RehanSaeedUK. 
        /// This is an optional property.
        /// </summary>
        public string CreatorUsername
        { get; set; }
    }

    public abstract class TwitterCard
    {
        private readonly string username;

        public TwitterCard(string username)
        {
            if (username == null)
            {
                throw new ArgumentNullException("username");
            }

            this.username = username;
        }

        /// <summary>
        /// Gets the type of the Twitter card.
        /// </summary>
        public abstract string Type
        { get; }

        /// <summary>
        /// Gets the Twitter username associated with the page e.g. @RehanSaeedUK. This is a required 
        /// property.
        /// </summary>
        public string Username
        { get { return this.username; } }
    }

    // Multiple URLs in a Tweet
    // In some circumstances, users many want to tweet multiple URLs. Below is the order of how we treat multiple URLs when processing Cards:
    // Cards from pic.twitter.com and vine.com have preference over alternate domains
    // URLs are processed in order of appearance in the tweet, first to last
    // For more background, this order was requested from early partners who requested to use pic.twitter.com to show rich images and also allow linking to richer, on-brand content. We maintain this feature for posterity, but is subject to change moving forward.



    //@if(Model.TwitterCard != null)
    //{
    //    if (!(Model.TwitterCard is SummaryTwitterCard))
    //    {
    //        < meta name = "twitter:card" content = "@Model.TwitterCard.Type" >
    //    }
    //    < meta name = "twitter:site" content = "@Model.TwitterCard.Username" >

    //       CreatorTwitterCard creatorTwitterCard = Model.TwitterCard as CreatorTwitterCard;
    //    if (creatorTwitterCard != null)
    //    {
    //        < meta name = "twitter:creator" content = "@creatorTwitterCard.CreatorUsername" >
    //    }

    //    SummaryTwitterCard summaryTwitterCard = Model.TwitterCard as SummaryTwitterCard;
    //    if (summaryTwitterCard != null)
    //    {
    //        < meta name = "twitter:title" content = "@summaryTwitterCard.Title" >
    //           < meta name = "twitter:description" content = "@summaryTwitterCard.Description" >
    //              < meta name = "twitter:image" content = "@summaryTwitterCard.Image" >
    //    }
    //}

}
