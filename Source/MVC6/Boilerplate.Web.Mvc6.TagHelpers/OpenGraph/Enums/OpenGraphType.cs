namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    /// <summary>
    /// An Open Graph type.
    /// </summary>
    public enum OpenGraphType
    {
        /// <summary>
        /// This object represents an article on a website. It is the preferred type for blog posts and news stories.
        /// This object type is part of the Open Graph standard.
        /// See http://ogp.me/
        /// See https://developers.facebook.com/docs/reference/opengraph/object-type/article/
        /// </summary>
        Article,

        /// <summary>
        /// This object represents a physical book or e-book. This object type is part of the Open Graph standard but
        /// Facebook uses the books.book object type instead which requires an ISBN number.
        /// See http://ogp.me/
        /// </summary>
        Book,

        /// <summary>
        /// This object represents a single author of a book. This object type is not part of the Open Graph standard but is used by Facebook.
        /// See https://developers.facebook.com/docs/reference/opengraph/object-type/books.author/
        /// </summary>
        BooksAuthor,

        /// <summary>
        /// This object represents a single book or publication. This is an appropriate type for ebooks, as well as traditional paperback or hardback books.
        /// This object type is not part of the Open Graph standard but is used by Facebook.
        /// See https://developers.facebook.com/docs/reference/opengraph/object-type/books.book/
        /// </summary>
        BooksBook,

        /// <summary>
        /// This object type represents the genre of a book or publication. This object type is not part of the Open Graph standard but is used by Facebook.
        /// See https://developers.facebook.com/docs/reference/opengraph/object-type/books.genre/
        /// </summary>
        BooksGenre,

        /// <summary>
        /// This object type represents a place of business that has a location, operating hours and contact information. This object type is not part of 
        /// the Open Graph standard but is used by Facebook.
        /// See https://developers.facebook.com/docs/reference/opengraph/object-type/business.business/
        /// </summary>
        Business,

        /// <summary>
        /// This object type represents the user's activity contributing to a particular run, walk, or bike course. This object type is not part of the 
        /// Open Graph standard but is used by Facebook.
        /// See https://developers.facebook.com/docs/reference/opengraph/object-type/fitness.course/
        /// </summary>
        FitnessCourse,

        /// <summary>
        /// This object type represents a specific achievement in a game. An app must be in the 'Games' category in App Dashboard to be able to use this 
        /// object type. Every achievement has a game:points value associate with it. This is not related to the points the user has scored in the game, but 
        /// is a way for the app to indicate the relative importance and scarcity of different achievements: * Each game gets a total of 1,000 points to 
        /// distribute across its achievements * Each game gets a maximum of 1,000 achievements * Achievements which are scarcer and have higher point 
        /// values will receive more distribution in Facebook's social channels. For example, achievements which have point values of less than 10 will get 
        /// almost no distribution. Apps should aim for between 50-100 achievements consisting of a mix of 50 (difficult), 25 (medium), and 10 (easy) point 
        /// value achievements Read more on how to use achievements in this guide. This object type is not part of the Open Graph standard but is used by 
        /// Facebook.
        /// See https://developers.facebook.com/docs/reference/opengraph/object-type/game.achievement/
        /// </summary>
        GameAchievement,

        /// <summary>
        /// This object represents a music album; in other words, an ordered collection of songs from an artist or a collection of artists. An album can 
        /// comprise multiple discs. This object type is part of the Open Graph standard.
        /// See http://ogp.me/
        /// See https://developers.facebook.com/docs/reference/opengraph/object-type/music.album/
        /// </summary>
        MusicAlbum,

        /// <summary>
        /// This object represents a music playlist, an ordered collection of songs from a collection of artists. This object type is part of the Open 
        /// Graph standard.
        /// See http://ogp.me/
        /// See https://developers.facebook.com/docs/reference/opengraph/object-type/music.playlist/
        /// </summary>
        MusicPlaylist,

        /// <summary>
        /// This object represents a 'radio' station of a stream of audio. The audio properties should be used to identify the location of the stream itself. 
        /// This object type is part of the Open Graph standard.
        /// See http://ogp.me/
        /// See https://developers.facebook.com/docs/reference/opengraph/object-type/music.radio_station/
        /// </summary>
        MusicRadioStation,

        /// <summary>
        /// This object represents a single song. This object type is part of the Open Graph standard.
        /// See http://ogp.me/
        /// https://developers.facebook.com/docs/reference/opengraph/object-type/music.song/
        /// </summary>
        MusicSong,

        /// <summary>
        /// This object type represents a place - such as a venue, a business, a landmark, or any other location which can be identified by longitude and 
        /// latitude. This object type is not part of the Open Graph standard but is used by Facebook.
        /// See https://developers.facebook.com/docs/reference/opengraph/object-type/place/
        /// </summary>
        Place,

        /// <summary>
        /// This object type represents a product. This includes both virtual and physical products, but it typically represents items that are available in 
        /// an online store. This object type is not part of the Open Graph standard but is used by Facebook.
        /// See https://developers.facebook.com/docs/reference/opengraph/object-type/product/
        /// </summary>
        Product,

        /// <summary>
        /// This object type represents a group of product items. This object type is not part of the Open Graph standard but is used by Facebook.
        /// See https://developers.facebook.com/docs/reference/opengraph/object-type/product.group/
        /// </summary>
        ProductGroup,

        /// <summary>
        /// This object type represents a product item. This object type is not part of the Open Graph standard but is used by Facebook.
        /// See https://developers.facebook.com/docs/reference/opengraph/object-type/product.item/
        /// </summary>
        ProductItem,

        /// <summary>
        /// This object type represents a person. While appropriate for celebrities, artists, or musicians, this object type can be used for the profile of 
        /// any individual. This object type is part of the Open Graph standard.
        /// See http://ogp.me/
        /// See https://developers.facebook.com/docs/reference/opengraph/object-type/profile/
        /// </summary>
        Profile,

        /// <summary>
        /// This object type represents a restaurant at a specific location. This object type is not part of the Open Graph standard but is used by Facebook.
        /// See https://developers.facebook.com/docs/reference/opengraph/object-type/restaurant.restaurant/
        /// </summary>
        Restaurant,

        /// <summary>
        /// This object type represents a restaurant's menu. A restaurant can have multiple menus, and each menu has multiple sections.
        /// This object type is not part of the Open Graph standard but is used by Facebook.
        /// See https://developers.facebook.com/docs/reference/opengraph/object-type/restaurant.menu/
        /// </summary>
        RestaurantMenu,

        /// <summary>
        /// This object type represents a single item on a restaurant's menu. Every item belongs within a menu section.
        /// This object type is not part of the Open Graph standard but is used by Facebook.
        /// See https://developers.facebook.com/docs/reference/opengraph/object-type/restaurant.menu_item/
        /// </summary>
        RestaurantMenuItem,

        /// <summary>
        /// This object type represents a restaurant's menu. A restaurant can have multiple menus, and each menu has multiple sections.
        /// This object type is not part of the Open Graph standard but is used by Facebook.
        /// See https://developers.facebook.com/docs/reference/opengraph/object-type/restaurant.menu_section/
        /// </summary>
        RestaurantMenuSection,

        /// <summary>
        /// This object type represents an episode of a TV show and contains references to the actors and other professionals involved in its production. 
        /// An episode is defined by us as a full-length episode that is part of a series. This type must reference the series this it is part of.
        /// This object type is part of the Open Graph standard.
        /// See http://ogp.me/
        /// See https://developers.facebook.com/docs/reference/opengraph/object-type/video.episode/
        /// </summary>
        VideoEpisode,

        /// <summary>
        /// This object type represents a movie, and contains references to the actors and other professionals involved in its production. A movie is 
        /// defined by us as a full-length feature or short film. Do not use this type to represent movie trailers, movie clips, user-generated video 
        /// content, etc. This object type is part of the Open Graph standard.
        /// See http://ogp.me/
        /// See https://developers.facebook.com/docs/reference/opengraph/object-type/video.movie/
        /// </summary>
        VideoMovie,

        /// <summary>
        /// This object type represents a generic video, and contains references to the actors and other professionals involved in its production. For 
        /// specific types of video content, use the video.movie or video.tv_show object types. This type is for any other type of video content not 
        /// represented elsewhere (eg. trailers, music videos, clips, news segments etc.). This object type is part of the Open Graph standard.
        /// See http://ogp.me/
        /// See https://developers.facebook.com/docs/reference/opengraph/object-type/video.other/
        /// </summary>
        VideoTvShow,

        /// <summary>
        /// This object type represents a TV show, and contains references to the actors and other professionals involved in its production. For individual 
        /// episodes of a series, use the video.episode object type. A TV show is defined by us as a series or set of episodes that are produced under the 
        /// same title (eg. a television or online series). This object type is part of the Open Graph standard.
        /// See http://ogp.me/
        /// See https://developers.facebook.com/docs/reference/opengraph/object-type/video.tv_show/
        /// </summary>
        VideoOther,

        /// <summary>
        /// An object representing a website. This object type is part of the Open Graph standard.
        /// See http://ogp.me/
        /// </summary>
        Website
    }
}
