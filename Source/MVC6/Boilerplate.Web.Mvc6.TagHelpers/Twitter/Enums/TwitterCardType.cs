namespace Boilerplate.Web.Mvc.TagHelpers.Twitter
{
    /// <summary>
    /// The type of Twitter card.
    /// </summary>
    public enum TwitterCardType
    {
        /// <summary>
        /// The App Card is a great way to represent mobile applications on Twitter and to drive installs. The app card 
        /// is designed to allow for a name, description and icon, and also to highlight attributes such as the rating 
        /// and the price. This Card type is currently available on the twitter.com website, as well as iOS and Android 
        /// mobile clients. It is not yet available on mobile web. See https://dev.twitter.com/cards/types/app.
        /// </summary>
        App,

        /// <summary>
        /// The Gallery Card allows you to represent collections of photos within a Tweet. This Card type is 
        /// designed to let the user know that there’s more than just a single image at the URL shared, but 
        /// rather a gallery of related images. You can specify up to 4 different images to show in the gallery 
        /// card. You can also provide attribution to the photographer of the  gallery by specifying the value of the creator tag. 
        /// See https://dev.twitter.com/cards/types/gallery.
        /// </summary>
        Gallery,

        /// <summary>
        /// The Photo Card puts the image front and center in the Tweet. Clicking on the photo expands it to a 
        /// richer, detailed view. On twitter.com and mobile clients, the image appears below the tweet text.
        /// See https://dev.twitter.com/cards/types/photo
        /// </summary>
        Photo,

        /// <summary>
        /// Video clips and audio streams have a special place on the Twitter platform thanks to the Player Card. By implementing a few HTML meta tags to 
        /// your website and following the Twitter Rules of the Road, you can deliver your rich media to users across the globe.
        /// Twitter must approve the use of the player card, find out more below.
        /// See https://dev.twitter.com/cards/types/player
        /// </summary>
        Player,

        /// <summary>
        /// The Product Card is a great way to represent product and retail items on Twitter. This Card type is designed to showcase your products via an 
        /// image, a description, and allow you to highlight two other key details about your product.
        /// See https://dev.twitter.com/cards/types/product
        /// </summary>
        Product,

        /// <summary>
        /// The Summary Card can be used for many kinds of web content, from blog posts and news articles, to 
        /// products and restaurants. It is designed to give the reader a preview of the content before clicking 
        /// through to your website. See https://dev.twitter.com/cards/types/summary.
        /// </summary>
        Summary,

        /// <summary>
        /// The Summary Card with Large Image features a large, full-width prominent image alongside a tweet. 
        /// It is designed to give the reader a rich photo experience, and clicking on the image brings the 
        /// user to your website. On twitter.com and the mobile clients, the image appears below the tweet text.
        /// See https://dev.twitter.com/cards/types/summary-large-image.
        /// </summary>
        SummaryLargeImage
    }
}
