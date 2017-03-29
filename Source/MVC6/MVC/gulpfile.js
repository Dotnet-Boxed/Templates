/// <binding Clean='clean' BeforeBuild='build' AfterBuild='test' ProjectOpened='watch'/>
// The above line of code enables Visual Studio to automatically start Gulp tasks at certain key moments. The 'clean'
// task is run on solution clean, the 'build' task is run on solution build and the 'watch' task is run on opening the
// solution. You can also edit the above using the Task Runner Explorer window in Visual Studio
// (See http://docs.asp.net/en/latest/client-side/using-gulp.html)
'use strict';   // Enable strict mode for JavaScript (See https://developer.mozilla.org/en/docs/Web/JavaScript/Reference/Strict_mode).

// Set up imported packages.
var gulp = require('gulp'),
    fs = require('fs'),                         // NPM file system API (https://nodejs.org/api/fs.html)
    autoprefixer = require('gulp-autoprefixer'),// Auto-prefix CSS (https://www.npmjs.com/package/gulp-autoprefixer)
    concat = require('gulp-concat'),            // Concatenate files (https://www.npmjs.com/package/gulp-concat/)
    csslint = require('gulp-csslint'),          // CSS linter (https://www.npmjs.com/package/gulp-csslint/)
    cssnano = require('gulp-cssnano'),          // Minifies CSS (https://www.npmjs.com/package/gulp-cssnano/)
    gulpif = require('gulp-if'),                // If statement (https://www.npmjs.com/package/gulp-if/)
    imagemin = require('gulp-imagemin'),        // Optimizes images (https://www.npmjs.com/package/gulp-imagemin/)
    // $Start-JavaScriptCodeStyle$
    jscs = require('gulp-jscs'),                // JavaScript style linter (https://www.npmjs.com/package/gulp-jscs)
    // $End-JavaScriptCodeStyle$
    // $Start-JavaScriptHint$
    jshint = require('gulp-jshint'),            // JavaScript linter (https://www.npmjs.com/package/gulp-jshint/)
    // $End-JavaScriptHint$
    // $Start-JavaScriptTestFramework-Mocha$
    mocha = require('gulp-mocha-phantomjs'),    // JavaScript test runner (https://www.npmjs.com/package/gulp-mocha-phantomjs/).
    // $End-JavaScriptTestFramework-Mocha$
    plumber = require('gulp-plumber'),          // Handles Gulp errors (https://www.npmjs.com/package/gulp-plumber)
    rename = require('gulp-rename'),            // Renames file paths (https://www.npmjs.com/package/gulp-rename/)
    // $Start-ApplicationInsights$
    replace = require('gulp-replace-task'),     // String replace (https://www.npmjs.com/package/gulp-replace-task/)
    // $End-ApplicationInsights$
    size = require('gulp-size'),                // Prints size of files to console (https://www.npmjs.com/package/gulp-size/)
    sourcemaps = require('gulp-sourcemaps'),    // Creates source map files (https://www.npmjs.com/package/gulp-sourcemaps/)
    uglify = require('gulp-uglify'),            // Minifies JavaScript (https://www.npmjs.com/package/gulp-uglify/)
    gutil = require('gulp-util'),               // Gulp utilities (https://www.npmjs.com/package/gulp-util/)
    merge = require('merge-stream'),            // Merges one or more gulp streams into one (https://www.npmjs.com/package/merge-stream/)
    // $Start-GooglePageSpeed$
    psi = require('psi'),                       // Google PageSpeed performance tester (https://www.npmjs.com/package/psi/)
    // $End-GooglePageSpeed$
    rimraf = require('rimraf'),                 // Deletes files and folders (https://www.npmjs.com/package/rimraf/)
    sass = require('gulp-sass'),                // Compile SCSS to CSS (https://www.npmjs.com/package/gulp-sass/)
    sasslint = require('gulp-sass-lint'),       // SASS linter (https://www.npmjs.com/package/gulp-sass-lint/)
    // $Start-TypeScript$
    tslint = require('gulp-tslint'),            // TypeScript linter (https://www.npmjs.com/package/gulp-tslint/)
    typescript = require('gulp-typescript'),    // TypeScript compiler (https://www.npmjs.com/package/gulp-typescript/)
    // $End-TypeScript$
    _ = require('autostrip-json-comments'),     // Strips JSON comments so the next two lines work (https://www.npmjs.com/package/autostrip-json-comments)
    config = require('./config.json'),          // Read the config.json file into the config variable.
    hosting = require('./hosting.json'),        // Read the hosting.json file into the hosting variable.
    launch = require('./Properties/launchSettings.json'); // Read the launchSettings.json file into the launch variable.

// Holds information about the hosting environment.
var environment = {
    // The names of the different environments.
    development: 'Development',
    staging: 'Staging',
    production: 'Production',
    // Gets the current hosting environment the app is running under. Looks for the ASPNETCORE_ENVIRONMENT environment
    // variable, if not found looks at the launchSettings.json file which if not found defaults to Development.
    current: function () {
        return process.env.ASPNETCORE_ENVIRONMENT ||
            (launch && launch.profiles['IIS Express'].environmentVariables.ASPNETCORE_ENVIRONMENT) ||
            this.development;
    },
    // Are we running under the development environment.
    isDevelopment: function () {
        return this.current() === this.development;
    },
    // Are we running under the staging environment.
    isStaging: function () {
        return this.current() === this.staging;
    },
    // Are we running under the production environment.
    isProduction: function () {
        return this.current() === this.production;
    }
};
gutil.log('Current Environment: ' + environment.current());

// The URL to your deployed site e.g. 'http://example.com'. This is used by the Google PageSpeed tasks.
var siteUrl = undefined;

// Initialize directory paths.
var paths = {
    // Source Directory Paths
    nodeModules: './node_modules/',
    scripts: 'Scripts/',
    styles: 'Styles/',
    // $Start-JavaScriptTestFramework-Mocha$
    tests: 'Tests/',
    // $End-JavaScriptTestFramework-Mocha$

    // Destination Directory Paths
    wwwroot: './' + hosting.webroot + '/',
    css: './' + hosting.webroot + '/css/',
    fonts: './' + hosting.webroot + '/fonts/',
    img: './' + hosting.webroot + '/img/',
    js: './' + hosting.webroot + '/js/'
};

// $Start-TypeScript$
// A TypeScript project is used to enable faster incremental compilation, rather than recompiling everything from
// scratch each time. Each resulting compiled file has it's own project which is stored in the typeScriptProjects array.
var typeScriptProjects = [];
function getTypeScriptProject(name) {
    var item;
    typeScriptProjects.forEach(function (typeScriptProject) {
        if (typeScriptProject.name === name) {
            item = typeScriptProject;
        }
    });

    if (item === undefined) {
        // Use the tsconfig.json file to specify how TypeScript (.ts) files should be compiled to JavaScript (.js).
        var typeScriptProject = typescript.createProject('tsconfig.json');
        item = {
            name: name,
            project: typeScriptProject
        };
        typeScriptProjects.push(item);
    }

    return item.project;
}

// $End-TypeScript$
// Initialize the mappings between the source and output files.
var sources = {
    // An array containing objects required to build a single CSS file.
    css: [
        {
            // name - The name of the final CSS file to build.
            name: 'font-awesome.css',
            // copy - Just copy the file and don't run it through the minification pipeline.
            copy: true,
            // paths - The path to the file to copy.
            paths: paths.nodeModules + 'font-awesome/css/font-awesome.min.css'
        },
        {
            name: 'site.css',
            // paths - An array of paths to CSS or SASS files which will be compiled to CSS, concatenated and minified
            // to create a file with the above file name.
            paths: [
                paths.styles + 'site.scss'
            ]
        }
    ],
    // An array containing objects required to copy font files.
    fonts: [
        {
            // The name of the folder the fonts will be output to.
            name: 'bootstrap',
            // The source directory to get the font files from. Note that we support all font file types.
            path: paths.nodeModules + 'bootstrap-sass/**/*.{ttf,svg,woff,woff2,otf,eot}'
        },
        {
            name: 'font-awesome',
            path: paths.nodeModules + 'font-awesome/**/*.{ttf,svg,woff,woff2,otf,eot}'
        }
    ],
    // An array of paths to images to be optimized.
    img: [
        paths.img + '**/*.{png,jpg,jpeg,gif,svg}'
    ],
    // An array containing objects required to build a single JavaScript file.
    js: [
        // $Start-ApplicationInsights$
        {
            name: 'application-insights.js',
            paths: paths.nodeModules + 'ApplicationInsights-JS/JavaScript/JavaScriptSDK/snippet.js',
            // replacements - Replacements to be made in the files above.
            replacements: [
                {
                    // match - The string or regular expression to find.
                    match: 'CDN_PATH',
                    // replacement - The string or function used to make the replacement.
                    replacement: config.ApplicationInsights.CdnUrl
                },
                {
                    match: 'INSTRUMENTATION_KEY',
                    replacement: config.ApplicationInsights.InstrumentationKey
                }
            ]
        },
        // $End-ApplicationInsights$
        {
            // name - The name of the final JavaScript file to build.
            name: 'bootstrap.js',
            // copy - Just copy the file and don't run it through the minification pipeline.
            copy: true,
            // paths - A single or array of paths to JavaScript or TypeScript files which will be concatenated and
            // minified to create a file with the above file name.
            paths: paths.nodeModules + 'bootstrap-sass/assets/javascripts/bootstrap.min.js'
        },
        {
            name: 'jquery.js',
            copy: true,
            paths: paths.nodeModules + 'jquery/dist/jquery.min.js'
        },
        {
            name: 'jquery-validate.js',
            copy: true,
            paths: paths.nodeModules + 'jquery-validation/dist/jquery.validate.min.js'
        },
        {
            name: 'jquery-validate-unobtrusive.js',
            paths: paths.nodeModules + 'jquery-validation-unobtrusive/jquery.validate.unobtrusive.js'
        },
        {
            name: 'site.js',
            paths: [
                paths.scripts + 'fallback/styles.js',
                paths.scripts + 'fallback/scripts.js',
                paths.scripts + 'site.js'
            ]
        }
    ]
};

// Initialize the mappings between the source and output files.
var lintSources = {
    css: paths.styles + '**/*.css',
    scss: paths.styles + '**/*.scss',
    // $Start-JavaScriptLint$
    js: paths.scripts + '**/*.js',
    // $Start-TypeScript$
    ts: paths.scripts + '**/*.ts'
    // $End-TypeScript$
    // $End-JavaScriptLint$
};

// Calls and returns the result from the gulp-size plugin to print the size of the stream. Makes it more readable.
function sizeBefore(title) {
    return size({
        title: 'Before: ' + title
    });
}
function sizeAfter(title) {
    return size({
        title: 'After: ' + title
    });
}

/*
 * Deletes all files and folders within the css directory.
 */
gulp.task('clean-css', function (cb) {
    return rimraf(paths.css, cb);
});

/*
 * Deletes all files and folders within the fonts directory.
 */
gulp.task('clean-fonts', function (cb) {
    return rimraf(paths.fonts, cb);
});

/*
 * Deletes all files and folders within the js directory.
 */
gulp.task('clean-js', function (cb) {
    return rimraf(paths.js, cb);
});

/*
 * Deletes all files and folders within the css, fonts and js directories.
 */
gulp.task('clean', ['clean-css', 'clean-fonts', 'clean-js']);

/*
 * Report warnings and errors in your CSS and SCSS files (lint them) under the Styles folder.
 */
gulp.task('lint-css', function () {
    return merge([                              // Combine multiple streams to one and return it so the task can be chained.
        gulp.src(lintSources.css)               // Start with the source .css files.
            .pipe(plumber())                    // Handle any errors.
            .pipe(csslint())                    // Get any CSS linting errors.
            .pipe(csslint.formatter()),         // Report any CSS linting errors to the console.
        gulp.src(lintSources.scss)              // Start with the source .scss files.
            .pipe(plumber())                    // Handle any errors.
            .pipe(sasslint())                   // Run SCSS linting.
            .pipe(sasslint.format())            // Report any SCSS linting errors to the console.
            .pipe(sasslint.failOnError())       // Fail the task if an error is found.
    ]);
});

// $Start-JavaScriptLint$
/*
 * Report warnings and errors in your JavaScript files (lint them) under the Scripts folder.
 */
gulp.task('lint-js', function () {
    return merge([                              // Combine multiple streams to one and return it so the task can be chained.
        // $Start-JavaScriptHint$
        gulp.src(lintSources.js)                // Start with the source .js files.
            .pipe(plumber())                    // Handle any errors.
            .pipe(jshint())                     // Get any JavaScript linting errors.
            .pipe(jshint.reporter('default', {  // Report any JavaScript linting errors to the console.
                verbose: true
            })),
        // $End-JavaScriptHint$
        // $Start-TypeScript$
        gulp.src(lintSources.ts)                // Start with the source .ts files.
            .pipe(plumber())                    // Handle any errors.
            .pipe(tslint({                      // Get any TypeScript linting errors.
                formatter: "verbose"            // Use a verbose output.
            }))
            .pipe(tslint.report()),             // Report any TypeScript linting errors to the console.
        // $End-TypeScript$
        // $Start-JavaScriptCodeStyle$
        gulp.src(lintSources.js)                // Start with the source .js files.
            .pipe(plumber())                    // Handle any errors.
            .pipe(jscs())                       // Get and report any JavaScript style linting errors to the console.
        // $End-JavaScriptCodeStyle$
    ]);
});

// $End-JavaScriptLint$
/*
 * Report warnings and errors in your styles and scripts (lint them).
 */
gulp.task('lint', [
    'lint-css',
    // $Start-JavaScriptLint$
    'lint-js'
    // $End-JavaScriptLint$
]);

/*
 * Builds the CSS for the site.
 */
gulp.task('build-css', ['lint-css'], function () {
    var tasks = sources.css.map(function (source) { // For each set of source files in the sources.
        if (source.copy) {                          // If we are only copying files.
            return gulp
                .src(source.paths)                  // Start with the source paths.
                .pipe(rename({                      // Rename the file to the source name.
                    basename: source.name,
                    extname: ''
                }))
                .pipe(gulp.dest(paths.css));        // Saves the CSS file to the specified destination path.
        }
        else {
            return gulp                             // Return the stream.
                .src(source.paths)                  // Start with the source paths.
                .pipe(plumber())                    // Handle any errors.
                .pipe(gulpif(
                    environment.isDevelopment(),    // If running in the development environment.
                    sourcemaps.init()))             // Set up the generation of .map source files for the CSS.
                .pipe(gulpif('**/*.scss', sass()))  // If the file is a SASS (.scss) file, compile it to CSS (.css).
                .pipe(autoprefixer({                // Auto-prefix CSS with vendor specific prefixes.
                    browsers: [
                        '> 1%',                     // Support browsers with more than 1% market share.
                        'last 2 versions'           // Support the last two versions of browsers.
                    ]
                }))
                .pipe(concat(source.name))          // Concatenate CSS files into a single CSS file with the specified name.
                .pipe(sizeBefore(source.name))      // Write the size of the file to the console before minification.
                .pipe(gulpif(
                    !environment.isDevelopment(),   // If running in the staging or production environment.
                    cssnano()))                     // Minifies the CSS.
                .pipe(sizeAfter(source.name))       // Write the size of the file to the console after minification.
                .pipe(gulpif(
                    environment.isDevelopment(),    // If running in the development environment.
                    sourcemaps.write('.')))         // Generates source .map files for the CSS.
                .pipe(gulp.dest(paths.css));        // Saves the CSS file to the specified destination path.
        }
    });
    return merge(tasks);                            // Combine multiple streams to one and return it so the task can be chained.
});

/*
 * Builds the font files for the site.
 */
gulp.task('build-fonts', function () {
    var tasks = sources.fonts.map(function (source) { // For each set of source files in the sources.
        return gulp                             // Return the stream.
            .src(source.path)                   // Start with the source paths.
            .pipe(plumber())                    // Handle any errors.
            .pipe(rename(function (path) {      // Rename the path to remove an unnecessary directory.
                path.dirname = '';
            }))
            .pipe(gulp.dest(paths.fonts));      // Saves the font files to the specified destination path.
    });
    return merge(tasks);                        // Combine multiple streams to one and return it so the task can be chained.
});

/*
 * Builds the JavaScript files for the site.
 */
gulp.task('build-js', [
    // $Start-JavaScriptLint$
    'lint-js'
    // $End-JavaScriptLint$
],
function () {
    var tasks = sources.js.map(function (source) {  // For each set of source files in the sources.
        if (source.copy) {                          // If we are only copying files.
            return gulp
                .src(source.paths)                  // Start with the source paths.
                .pipe(rename({                      // Rename the file to the source name.
                    basename: source.name,
                    extname: ''
                }))
                .pipe(gulp.dest(paths.js));         // Saves the JavaScript file to the specified destination path.
        }
        else {
            return gulp                             // Return the stream.
                .src(source.paths)                  // Start with the source paths.
                .pipe(plumber())                    // Handle any errors.
                .pipe(gulpif(
                    environment.isDevelopment(),    // If running in the development environment.
                    sourcemaps.init()))             // Set up the generation of .map source files for the JavaScript.
                // $Start-ApplicationInsights$
                .pipe(gulpif(
                    source.replacements !== undefined, // If the source has a replacement to be made.
                    replace({                       // Carry out the specified find and replace.
                        patterns: source.replacements,
                        usePrefix: false
                    })))
                // $End-ApplicationInsights$
                // $Start-TypeScript$
                .pipe(gulpif(                       // If the file is a TypeScript (.ts) file.
                    '**/*.ts',
                    getTypeScriptProject(source.name)())) // Compile TypeScript (.ts) to JavaScript (.js) using the specified options.
                // $End-TypeScript$
                .pipe(concat(source.name))          // Concatenate JavaScript files into a single file with the specified name.
                .pipe(sizeBefore(source.name))      // Write the size of the file to the console before minification.
                .pipe(gulpif(
                    !environment.isDevelopment(),   // If running in the staging or production environment.
                    uglify()))                      // Minifies the JavaScript.
                .pipe(sizeAfter(source.name))       // Write the size of the file to the console after minification.
                .pipe(gulpif(
                    environment.isDevelopment(),    // If running in the development environment.
                    sourcemaps.write('.')))         // Generates source .map files for the JavaScript.
                .pipe(gulp.dest(paths.js));         // Saves the JavaScript file to the specified destination path.
        }
    });
    return merge(tasks);                            // Combine multiple streams to one and return it so the task can be chained.
});

/*
 * Cleans and builds the CSS, Font and JavaScript files for the site.
 */
gulp.task('build', ['build-css', 'build-fonts', 'build-js']);

// $Start-JavaScriptTestFramework-Mocha$
gulp.task('test', function () {
    return gulp
        .src(paths.tests + 'mocha.html')
        .pipe(mocha());
});

// $End-JavaScriptTestFramework-Mocha$
/*
 * Optimizes and compresses the GIF, JPG, PNG and SVG images for the site.
 */
gulp.task('optimize-images', function () {
    return gulp
        .src(sources.img)                   // Start with the source paths.
        .pipe(plumber())                    // Handle any errors.
        .pipe(sizeBefore())                 // Write the size of the file to the console before minification.
        .pipe(imagemin({                    // Optimize the images.
            multipass: true,                // Optimize SVG multiple times until it's fully optimized.
            optimizationLevel: 7            // The level of optimization (0 to 7) to make, the higher the slower it is.
        }))
        .pipe(gulp.dest(paths.img))         // Saves the image files to the specified destination path.
        .pipe(sizeAfter());                 // Write the size of the file to the console after minification.
});

/*
 * Watch the styles folder for changes to .css, or .scss files. Build the CSS if something changes.
 */
gulp.task('watch-css', function () {
    return gulp
        .watch(
            paths.styles + '**/*.{css,scss}',   // Watch the styles folder for file changes.
            ['clean-css', 'build-css'])         // Run the build-css task if a file changes.
        .on('change', function (event) {        // Log the change to the console.
            gutil.log(gutil.colors.blue('File ' + event.path + ' was ' + event.type + ', build-css task started.'));
        });
});

/*
 * Watch the scripts folder for changes to .js or .ts files. Build the JavaScript if something changes.
 */
gulp.task('watch-js', function () {
    return gulp
        .watch(
            paths.scripts + '**/*.{js,ts}',     // Watch the scripts folder for file changes.
            ['clean-js', 'build-js'])           // Run the build-js task if a file changes.
        .on('change', function (event) {        // Log the change to the console.
            gutil.log(gutil.colors.blue('File ' + event.path + ' was ' + event.type + ', build-js task started.'));
        });
});

// $Start-JavaScriptTestFramework-Mocha$
/*
 * Watch the scripts and tests folder for changes to .js or .ts files. Run the JavaScript tests if something changes.
 */
gulp.task('watch-tests', function () {
    return gulp
        .watch([
            paths.scripts + '**/*.{js,ts}',     // Watch the scripts folder for file changes.
            paths.tests + '**/*.{js,ts}'        // Watch the tests folder for file changes.
        ],
        ['test'])                               // Run the test task if a file changes.
        .on('change', function (event) {        // Log the change to the console.
            gutil.log(gutil.colors.blue('File ' + event.path + ' was ' + event.type + ', test task started.'));
        });
});

// $End-JavaScriptTestFramework-Mocha$
/*
 * Watch the styles and scripts folders for changes. Build the CSS and JavaScript if something changes.
 */
gulp.task('watch', ['watch-css', 'watch-js']);

// $Start-GooglePageSpeed$
function pageSpeed(strategy, cb) {
    if (siteUrl === undefined) {
        return cb('siteUrl is undefined. Google PageSpeed requires a URL to your deployed site.');
    }

    return psi(
        siteUrl,
        {
            // Use the 'nokey' option to try out Google PageSpeed Insights as part of your build process. For more
            // frequent use, register for your own API key. See
            // https://developers.google.com/speed/docs/insights/v1/getting_started
            // key: '[Your Google PageSpeed API Key Here]'
            nokey: 'true',
            strategy: strategy
        },
        function (err, data) {
            console.log(data.score);
            console.log(data.pageStats);
        });
}

/*
 * Measure the performance of your site for mobiles using Google PageSpeed. Prefer using this test to the desktop test.
 */
gulp.task('pagespeed-mobile', function (cb) {
    return pageSpeed('mobile', cb);
});

/*
 * Measure the performance of your site for desktops using Google PageSpeed.
 */
gulp.task('pagespeed-desktop', function (cb) {
    return pageSpeed('desktop', cb);
});

// $End-GooglePageSpeed$
/*
 * The default gulp task. This is useful for scenarios where you are not using Visual Studio. Does a full clean and
 * build before watching for any file changes.
 */
gulp.task(
    'default',
    [
        'clean',
        'build',
        // $Start-JavaScriptTestFramework-Mocha$
        'test',
        // $End-JavaScriptTestFramework-Mocha$
        'watch'
    ]);