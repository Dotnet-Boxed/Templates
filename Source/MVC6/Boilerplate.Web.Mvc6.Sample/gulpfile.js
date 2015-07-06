/// <binding Clean="clean" />

// Set up imported packages.
var gulp = require("gulp"),
    fs = require("fs"),                         // NPM file system API (https://nodejs.org/api/fs.html)
    concat = require("gulp-concat"),            // Concatenate files (https://www.npmjs.com/package/gulp-concat)
    imagemin = require("gulp-imagemin"),        // Optimizes images (https://www.npmjs.com/package/gulp-imagemin)
    less = require("gulp-less"),                // Compile LESS to CSS (https://www.npmjs.com/package/gulp-less)
    minifyCss = require("gulp-minify-css"),     // Minifies CSS (https://www.npmjs.com/package/gulp-minify-css)
    rename = require("gulp-rename"),            // Renames file paths (https://www.npmjs.com/package/gulp-rename)
    size = require("gulp-size"),                // Prints size of files to console (https://www.npmjs.com/package/gulp-size)
    sourcemaps = require("gulp-sourcemaps"),    // Creates source map files (https://www.npmjs.com/package/gulp-sourcemaps)
    uglify = require("gulp-uglify"),            // Minifies JavaScript (https://www.npmjs.com/package/gulp-uglify)
    gutil = require("gulp-util"),               // Gulp utilities (https://www.npmjs.com/package/gulp-util)
    merge = require("merge-stream"),            // Merges one or more gulp streams into one (https://www.npmjs.com/package/merge-stream)
    rimraf = require("rimraf");                 // Deletes files and folders (https://www.npmjs.com/package/rimraf)

// Read the project.json file into the project variable.
eval("var project = " + fs.readFileSync("./project.json"));
// The environment is hard coded for now. We can use the node environment (process.env.NODE_ENV) or ASP.NET hosting
// environment but there is no way to do that yet. See https://github.com/aspnet/Home/issues/724.
var environment = "Development";
// The names of the different environments.
var environmentName = { development: "Development", staging: "Staging", production: "Production" };

// Initialize directory paths.
var paths = {
    // Source Folder Paths
    bower: "./bower_components/",
    content: "Content/",
    scripts: "Scripts/",

    // Destination Folder Paths
    wwwroot: "./" + project.webroot,
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
gulp.task("build-css", function () {

    // An array containing objects required to build a single CSS file.
    var sources = [
        {
            // name - The name of the final CSS file to build.
            name: "font-awesome.css",
            // paths - An array of paths to LESS files which will be compiled to CSS, concatenated and minified to 
            //         create a file with the above file name.
            paths: [
                paths.bower + "font-awesome-less/less/font-awesome.less"
            ]
        },
        {
            name: "site.css",
            paths: [
                paths.bower + "bootstrap-less/less/bootstrap.less",
                paths.bower + "bootstrap-touch-carousel/src/less/carousel.less",
                paths.content + "site.less"
            ]
        }
    ];

    var tasks = sources.map(function (source) { // For each set of source files in the sources.
        return gulp                             // Return the stream.
            .src(source.paths)                  // Start with the source paths.
            .pipe(environment === environmentName.development ? // If running in the development environment.
                sourcemaps.init() :             // Set up the generation of .map source files for the CSS.
                gutil.noop())                   // Else, do nothing.
                .pipe(less())                   // Compile the specified LESS files to CSS.
                .pipe(concat(source.name))      // Concatenate CSS files into a single CSS file with the specified name.
                .pipe(size({                    // Write the size of the file to the console before minification.
                    title: "Before: " + source.name
                }))
                .pipe(environment !== environmentName.development ? // If running in the staging or production environment.
                    minifyCss({                 // Minifies the CSS.
                        keepSpecialComments: 0  // Remove all comments.
                    }) : 
                    gutil.noop())               // Else, do nothing.
                .pipe(size({                    // Write the size of the file to the console after minification.
                    title: "After:  " + source.name
                }))
            .pipe(environment === environmentName.development ? // If running in the development environment.
                sourcemaps.write(".") :         // Generates source .map files for the CSS.
                gutil.noop())                   // Else, do nothing.
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
            path: paths.bower + "bootstrap-less/**/*.{ttf,svg,woff,woff2,otf,eot}"
        },
        {
            name: "font-awesome",
            path: paths.bower + "font-awesome-less/**/*.{ttf,svg,woff,woff2,otf,eot}"
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
gulp.task("build-js", function () {

    // An array containing objects required to build a single JavaScript file.
    var sources = [
        {
            // name - The name of the final JavaScript file to build.
            name: "bootstrap.js",
            // paths - An array of paths to JavaScript files which will be concatenated and minified to create a file 
            //         with the above file name.
            paths: [
                // Feel free to remove any parts of Bootstrap you don't use.
                paths.bower + "bootstrap-less/js/transition.js",
                paths.bower + "bootstrap-less/js/alert.js",
                paths.bower + "bootstrap-less/js/button.js",
                paths.bower + "bootstrap-less/js/carousel.js",
                paths.bower + "bootstrap-less/js/collapse.js",
                paths.bower + "bootstrap-less/js/dropdown.js",
                paths.bower + "bootstrap-less/js/modal.js",
                paths.bower + "bootstrap-less/js/tooltip.js",
                paths.bower + "bootstrap-less/js/popover.js",
                paths.bower + "bootstrap-less/js/scrollspy.js",
                paths.bower + "bootstrap-less/js/tab.js",
                paths.bower + "bootstrap-less/js/affix.js"
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
            .pipe(environment === environmentName.development ? // If running in the development environment.
                sourcemaps.init() :             // Set up the generation of .map source files for the JavaScript.
                gutil.noop())                   // Else, do nothing.
                .pipe(concat(source.name))      // Concatenate JavaScript files into a single file with the specified name.
                .pipe(size({                    // Write the size of the file to the console before minification.
                    title: "Before: " + source.name
                }))
                .pipe(environment !== environmentName.development ? // If running in the staging or production environment.
                    uglify() :                  // Minifies the JavaScript.
                    gutil.noop())               // Else, do nothing.
                .pipe(size({                    // Write the size of the file to the console after minification.
                    title: "After:  " + source.name
                }))
            .pipe(environment === environmentName.development ? // If running in the development environment.
                sourcemaps.write(".") :         // Generates source .map files for the JavaScript.
                gutil.noop())                   // Else, do nothing.
            .pipe(gulp.dest(paths.js))          // Saves the JavaScript file to the specified destination path.
            .on("error", handleError);          // Handle any errors.
    });

    return merge(tasks);                        // Combine multiple streams to one and return it so the task can be chained.
});

/*
 * Cleans and builds the CSS, Font and JavaScript files for the site.
 */
gulp.task("build", ["clean", "build-css", "build-fonts", "build-js", "watch"]);

/*
 * Optimizes and compresses the GIF, JPG, PNG and SVG images for the site.
 */
gulp.task("compress-images", [], function () {

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
 * Watch the content and scripts folders for changes. Build the CSS or JavaScript if something changes.
 */
gulp.task("watch", function () {
    return gulp
        .watch(paths.content, ["build-css"])    // Watch the content folder and execute the build-css task if something changes.
        .watch(paths.scripts, ["build-js"])     // Watch the scripts folder and execute the build-js task if something changes.
        .on("change", function (event) {        // Log the change to the console.
            gutil.log(gutil.colors.blue("File " + event.path + " was " + event.type + ", build-css task started."));
        });
});