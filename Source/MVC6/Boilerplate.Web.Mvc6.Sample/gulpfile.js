/// <binding Clean="clean" BeforeBuild="build" ProjectOpened="watch"/>
// The above line of code enables Visual Studio to automatically start Gulp tasks at certain key moments. The 'clean'
// task is run on solution clean, the 'build' task is run on solution build and the 'watch' task is run on opening the 
// solution. You can also edit the above using the Task Runner Explorer window in Visual Studio (See 
// http://docs.asp.net/en/latest/client-side/using-gulp.html)

// Set up imported packages.
var gulp = require("gulp"),
    fs = require("fs"),                         // npm file system API (https://nodejs.org/api/fs.html)
    concat = require("gulp-concat"),            // Concatenate files (https://www.npmjs.com/package/gulp-concat/)
	csslint = require("gulp-csslint"),			// CSS linter (https://www.npmjs.com/package/gulp-csslint/)
    gulpif = require("gulp-if"),                // If statement (https://www.npmjs.com/package/gulp-if/)
    imagemin = require("gulp-imagemin"),        // Optimizes images (https://www.npmjs.com/package/gulp-imagemin/)
    jshint = require("gulp-jshint"),            // JavaScript linter (https://www.npmjs.com/package/gulp-jshint/)
    minifyCss = require("gulp-minify-css"),     // Minifies CSS (https://www.npmjs.com/package/gulp-minify-css/)
    rename = require("gulp-rename"),            // Renames file paths (https://www.npmjs.com/package/gulp-rename/)
    size = require("gulp-size"),                // Prints size of files to console (https://www.npmjs.com/package/gulp-size/)
    sourcemaps = require("gulp-sourcemaps"),    // Creates source map files (https://www.npmjs.com/package/gulp-sourcemaps/)
    uglify = require("gulp-uglify"),            // Minifies JavaScript (https://www.npmjs.com/package/gulp-uglify/)
    gutil = require("gulp-util"),               // Gulp utilities (https://www.npmjs.com/package/gulp-util/)
    merge = require("merge-stream"),            // Merges one or more gulp streams into one (https://www.npmjs.com/package/merge-stream/)
    psi = require("psi"),                       // Google PageSpeed performance tester (https://www.npmjs.com/package/psi/)
    rimraf = require("rimraf"),                 // Deletes files and folders (https://www.npmjs.com/package/rimraf/)
	sass = require('gulp-sass'),				// Compile SCSS to CSS (https://www.npmjs.com/package/gulp-sass/)
	scsslint = require('gulp-scss-lint'),		// SASS linter (https://www.npmjs.com/package/gulp-scss-lint/)
    tslint = require("gulp-tslint"),            // TypeScript linter (https://www.npmjs.com/package/gulp-tslint/)
    typescript = require("gulp-tsc"),           // TypeScript compiler (https://www.npmjs.com/package/gulp-tsc/)
    project = require("./project.json");        // Read the project.json file into the project variable.

// Holds information about the hosting environment.
var environment = {
    // The names of the different environments.
    development: "Development",
    staging: "Staging",
    production: "Production",
    // Gets the current hosting environment the application is running under. This comes from the environment variables.
    current: function () { return process.env.ASPNET_ENV || this.development },
    // Are we running under the development environment.
    isDevelopment: function () { return this.current() === this.development; },
    // Are we running under the staging environment.
    isStaging: function () { return this.current() === this.staging; },
    // Are we running under the production environment.
    isProduction: function () { return this.current() === this.production; }
};
// The URL to your deployed site e.g. "http://example.com". This is used by the Google PageSpeed tasks.
var siteUrl = undefined;

// Initialize directory paths.
var paths = {
    // Source Folder Paths
    bower: "./bower_components/",
    scripts: "Scripts/",
    styles: "Styles/",

    // Destination Folder Paths
    wwwroot: "./" + project.webroot + "/",
    css: "./" + project.webroot + "/css/",
    fonts: "./" + project.webroot + "/fonts/",
    img: "./" + project.webroot + "/img/",
    js: "./" + project.webroot + "/js/"
};

/*
 * Handles errors by logging them to the task runner explorer console.
 */
var handleError = function (error) {
    gutil.log(gutil.colors.red(error));
}

/*
 * Deletes all files and folders within the css directory.
 */
gulp.task("clean-css", function (cb) {
    return rimraf(paths.css, cb);           // Deletes the files and folders under the path.
});

/*
 * Deletes all files and folders within the fonts directory.
 */
gulp.task("clean-fonts", function (cb) {
    return rimraf(paths.fonts, cb);         // Deletes the files and folders under the path.
});

/*
 * Deletes all files and folders within the js directory.
 */
gulp.task("clean-js", function (cb) {
    return rimraf(paths.js, cb);            // Deletes the files and folders under the path.
});

/*
 * Deletes all files and folders within the css, fonts and js directories.
 */
gulp.task("clean", ["clean-css", "clean-fonts", "clean-js"]);

/*
 * Builds the CSS for the site.
 */
gulp.task("build-css", ["lint-css"], function () {

    // An array containing objects required to build a single CSS file.
    var sources = [
        {
            // name - The name of the final CSS file to build.
            name: "font-awesome.css",
            // paths - An array of paths to CSS or SASS files which will be compiled to CSS, concatenated and minified 
            // to create a file with the above file name.
            paths: [
                paths.bower + "font-awesome/scss/font-awesome.scss"
            ]
        },
        {
            name: "site.css",
            paths: [
                paths.styles + "site.scss",
				// Unfortunately, bootstrap-touch-carousel does not provide a SASS version of their library. See 
				// https://github.com/ixisio/bootstrap-touch-carousel/issues/51 for more information.
				paths.bower + "bootstrap-touch-carousel/dist/css/bootstrap-touch-carousel.css"
            ]
        }
    ];

    var tasks = sources.map(function (source) { // For each set of source files in the sources.
        return gulp                             // Return the stream.
            .src(source.paths)                  // Start with the source paths.
            .pipe(gulpif(
                environment.isDevelopment(),    // If running in the development environment.
                sourcemaps.init()))             // Set up the generation of .map source files for the CSS.
			.pipe(gulpif("**/*.{css,scss}", sass()))  // If the file is a SASS (.scss) file, compile it to CSS (.css).
            .pipe(concat(source.name))          // Concatenate CSS files into a single CSS file with the specified name.
            .pipe(size({                        // Write the size of the file to the console before minification.
                    title: "Before: " + source.name 
                }))
            .pipe(gulpif(
                !environment.isDevelopment(),   // If running in the staging or production environment.
                minifyCss({                     // Minifies the CSS.
                    keepSpecialComments: 0      // Remove all comments.
                })))
            .pipe(size({                        // Write the size of the file to the console after minification.
                    title: "After:  " + source.name
                }))
            .pipe(gulpif(
                environment.isDevelopment(),    // If running in the development environment.
                sourcemaps.write(".")))         // Generates source .map files for the CSS.
            .pipe(gulp.dest(paths.css))         // Saves the CSS file to the specified destination path.
            .on("error", handleError);          // Handle any errors.
    });

    return merge(tasks);                        // Combine multiple streams to one and return it so the task can be chained.
});

/*
 * Builds the font files for the site.
 */
gulp.task("build-fonts", function () {

    var sources = [
        {
            // The name of the folder the fonts will be output to.
            name: "bootstrap",
            // The source directory to get the font files from. Note that we support all font file types.
            path: paths.bower + "bootstrap-sass/**/*.{ttf,svg,woff,woff2,otf,eot}"
        },
        {
            name: "font-awesome",
            path: paths.bower + "font-awesome/**/*.{ttf,svg,woff,woff2,otf,eot}"
        }
    ];

    var tasks = sources.map(function (source) { // For each set of source files in the sources.
        return gulp                             // Return the stream.
            .src(source.path)                   // Start with the source paths.
            .pipe(rename(function (path) {      // Rename the path to remove an unnecessary directory.
                path.dirname = "";
            }))
            .pipe(gulp.dest(paths.fonts))       // Saves the font files to the specified destination path.
            .on("error", handleError);          // Handle any errors.
    });

    return merge(tasks);                        // Combine multiple streams to one and return it so the task can be chained.
});

/*
 * Builds the JavaScript files for the site.
 */
gulp.task("build-js", ["lint-js"], function () {

    // An array containing objects required to build a single JavaScript file.
    var sources = [
        {
            // name - The name of the final JavaScript file to build.
            name: "bootstrap.js",
            // paths - An array of paths to JavaScript or TypeScript files which will be concatenated and minified to 
            // create a file with the above file name.
            paths: [
                // Feel free to remove any parts of Bootstrap you don't use.
                paths.bower + "bootstrap-sass/assets/javascripts/bootstrap/transition.js",
                paths.bower + "bootstrap-sass/assets/javascripts/bootstrap/alert.js",
                paths.bower + "bootstrap-sass/assets/javascripts/bootstrap/button.js",
                paths.bower + "bootstrap-sass/assets/javascripts/bootstrap/carousel.js",
                paths.bower + "bootstrap-sass/assets/javascripts/bootstrap/collapse.js",
                paths.bower + "bootstrap-sass/assets/javascripts/bootstrap/dropdown.js",
                paths.bower + "bootstrap-sass/assets/javascripts/bootstrap/modal.js",
                paths.bower + "bootstrap-sass/assets/javascripts/bootstrap/tooltip.js",
                paths.bower + "bootstrap-sass/assets/javascripts/bootstrap/popover.js",
                paths.bower + "bootstrap-sass/assets/javascripts/bootstrap/scrollspy.js",
                paths.bower + "bootstrap-sass/assets/javascripts/bootstrap/tab.js",
                paths.bower + "bootstrap-sass/assets/javascripts/bootstrap/affix.js"
            ]
        },
        {
            name: "bootstrap-touch-carousel.js",
            paths: paths.bower + "bootstrap-touch-carousel/dist/js/bootstrap-touch-carousel.js"
        },
        {
            name: "hammer.js",
            paths: paths.bower + "hammer.js/hammer.js"
        },
        {
            name: "jquery.js",
            paths: paths.bower + "jquery/dist/jquery.js"
        },
        {
            name: "jquery-validate.js",
            paths: paths.bower + "jquery-validation/dist/jquery.validate.js"
        },
        {
            name: "jquery-validate-unobtrusive.js",
            paths: paths.bower + "jquery-validation-unobtrusive/jquery.validate.unobtrusive.js"
        },
        {
            name: "modernizr.js",
            paths: paths.bower + "modernizr/modernizr.js"
        },
        {
            name: "site.js",
            paths: [
                paths.scripts + "fallback/styles.js",
                paths.scripts + "fallback/scripts.js",
                paths.scripts + "site.js"
            ]
        }
    ];

    var tasks = sources.map(function (source) { // For each set of source files in the sources.
        return gulp                             // Return the stream.
            .src(source.paths)                  // Start with the source paths.
            .pipe(gulpif(
                environment.isDevelopment(),    // If running in the development environment.
                sourcemaps.init()))             // Set up the generation of .map source files for the JavaScript.
            .pipe(gulpif("**/*.ts", typescript()))  // If the file is a TypeScript (.ts) file, compile it to JavaScript (.js).
            .pipe(concat(source.name))          // Concatenate JavaScript files into a single file with the specified name.
            .pipe(size({                        // Write the size of the file to the console before minification.
                    title: "Before: " + source.name
                }))
            .pipe(gulpif(
                !environment.isDevelopment(),   // If running in the staging or production environment.
                uglify()))                      // Minifies the JavaScript.
            .pipe(size({                        // Write the size of the file to the console after minification.
                    title: "After:  " + source.name
                }))
            .pipe(gulpif(
                environment.isDevelopment(),    // If running in the development environment.
                sourcemaps.write(".")))         // Generates source .map files for the JavaScript.
            .pipe(gulp.dest(paths.js))          // Saves the JavaScript file to the specified destination path.
            .on("error", handleError);          // Handle any errors.
    });

    return merge(tasks);                        // Combine multiple streams to one and return it so the task can be chained.
});

/*
 * Cleans and builds the CSS, Font and JavaScript files for the site.
 */
gulp.task("build", ["build-css", "build-fonts", "build-js"]);

/*
 * Optimizes and compresses the GIF, JPG, PNG and SVG images for the site.
 */
gulp.task("optimize-images", function () {

    // An array of paths to images.
    var sources = [
        paths.img + "**/*.{png,jpg,jpeg,gif,svg}"
    ];

    return gulp
        .src(sources)                       // Start with the source paths.
        .pipe(size({                        // Write the size of the file to the console before minification.
            title: "Before: "
        }))
        .pipe(imagemin({                    // Optimize the images.
            multipass: true,                // Optimize svg multiple times until it's fully optimized.
            optimizationLevel: 7            // The level of optimization (0 to 7) to make, the higher the slower it is.
        }))
        .pipe(gulp.dest(paths.img))         // Saves the image files to the specified destination path.
        .pipe(size({                        // Write the size of the file to the console after minification.
            title: "After:  "
        }))
        .on("error", handleError);          // Handle any errors.
});

/*
 * Watch the styles folder for changes to .css, or .scss files. Build the CSS if something changes.
 */
gulp.task("watch-css", function () {
    return gulp
        .watch(paths.styles + "**/*.{css,scss}", ["build-css"])    // Watch the styles folder for file changes.
        .on("change", function (event) {        // Log the change to the console.
            gutil.log(gutil.colors.blue("File " + event.path + " was " + event.type + ", build-css task started."));
        });
});

/*
 * Watch the scripts folder for changes to .js or .ts files. Build the JavaScript if something changes.
 */
gulp.task("watch-js", function () {
    return gulp
        .watch(paths.scripts + "**/*.{js,ts}", ["build-js"])     // Watch the scripts folder for file changes.
        .on("change", function (event) {        // Log the change to the console.
            gutil.log(gutil.colors.blue("File " + event.path + " was " + event.type + ", build-js task started."));
        });
});

/*
 * Watch the styles and scripts folders for changes. Build the CSS and JavaScript if something changes.
 */
gulp.task("watch", ["watch-css", "watch-js"]);

/*
 * Report warnings and errors in your CSS and SCSS files (lint them) under the Styles folder.
 */
gulp.task("lint-css", function () {
    var cssTask = gulp.src(paths.styles + "**/*.{css}")
        .pipe(csslint())
		.pipe(csslint.reporter());
	var scssTask = gulp.src(paths.styles + "**/*.{scss}")
        .pipe(scsslint());
	return merge([cssTask, scssTask]);
});

/*
 * Report warnings and errors in your JavaScript and TypeScript files (lint them) under the Scripts folder.
 */
gulp.task("lint-js", function () {
    var jsTask = gulp.src(paths.scripts + "**/*.js")
        .pipe(jshint())
        .pipe(jshint.reporter("default"));
    var tsTask = gulp.src(paths.scripts + "**/*.ts")
       .pipe(tslint())
       .pipe(tslint.report("verbose"));
    return merge([jsTask, tsTask]);
});

/*
 * Report warnings and errors in your styles and scripts (lint them).
 */
gulp.task("lint", ["lint-css", "lint-js"]);

function pageSpeed(strategy, cb) {
    if (siteUrl === undefined) {
        return cb("siteUrl is undefined. Google PageSpeed requires a URL to your deployed site.");
    }

    return psi(
        siteUrl,
        {
            // Use the "nokey" option to try out Google PageSpeed Insights as part of your build process. For more 
            // frequent use, register for your own API key. See  
            // https://developers.google.com/speed/docs/insights/v1/getting_started

            // key: "[Your Google PageSpeed API Key Here]"
            nokey: "true",
            strategy: strategy,
        },
        function (err, data) {
            console.log(data.score);
            console.log(data.pageStats);
        });
}

/*
 * Measure the performance of your site for mobiles using Google PageSpeed. Prefer using this test to the desktop test.
 */
gulp.task("pagespeed-mobile", function (cb) {
    return pageSpeed("mobile", cb);
});

/*
 * Measure the performance of your site for desktops using Google PageSpeed.
 */
gulp.task("pagespeed-desktop", function (cb) {
    return pageSpeed("desktop", cb);
});

/*
 * The default gulp task. This is useful for scenarios where you are not using Visual Studio. Does a full clean and 
 * build before watching for any file changes.
 */
gulp.task("default", ["clean", "build", "watch"]);