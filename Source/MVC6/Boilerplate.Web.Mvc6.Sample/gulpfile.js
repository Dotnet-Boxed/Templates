/// <binding Clean='clean' />

var gulp = require("gulp"),
    concat = require("gulp-concat"),            // https://www.npmjs.com/package/gulp-concat
    imagemin = require("gulp-imagemin"),        // https://www.npmjs.com/package/gulp-imagemin
    less = require("gulp-less"),                // https://www.npmjs.com/package/gulp-less
    minifyCss = require("gulp-minify-css"),     // https://www.npmjs.com/package/gulp-minify-css
    rename = require('gulp-rename'),            // https://www.npmjs.com/package/gulp-rename
    size = require("gulp-size"),                // https://www.npmjs.com/package/gulp-size
    sourcemaps = require('gulp-sourcemaps'),    // https://www.npmjs.com/package/gulp-sourcemaps
    svgmin = require("gulp-svgmin"),            // https://www.npmjs.com/package/gulp-svgmin
    uglify = require('gulp-uglify'),            // https://www.npmjs.com/package/gulp-uglify
    watch = require('gulp-watch'),              // https://www.npmjs.com/package/gulp-watch
    rimraf = require("rimraf"),                 // https://www.npmjs.com/package/rimraf
    fs = require("fs");

eval("var project = " + fs.readFileSync("./project.json"));

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

gulp.task("clean", function (cb) {
    rimraf(paths.css, function () { });
    rimraf(paths.fonts, function () { });
    rimraf(paths.js, cb);
});

gulp.task("build-css", [], function () {
    var allDirectories = [
        paths.content + "bootstrap.less",
        paths.bower + "bootstrap-touch-carousel/dist/**/*.{less}",
        paths.content + "Site.less"
    ];
    gulp.src(allDirectories)
        .pipe(sourcemaps.init())
        .pipe(less())
        .pipe(concat('all.css'))
        .pipe(minifyCss())
        .pipe(sourcemaps.write('/'))
        .pipe(gulp.dest(paths.css))
        .pipe(size());

    var fontAwesomeDirectory = paths.bower + "font-awesome-less/less/font-awesome.less";
    gulp.src(fontAwesomeDirectory)
        .pipe(sourcemaps.init())
        .pipe(less())
        .pipe(concat('font-awesome.css'))
        .pipe(minifyCss())
        .pipe(sourcemaps.write('/'))
        .pipe(gulp.dest(paths.css))
        .pipe(size());
});

gulp.task("build-fonts", [], function () {
    var directories = {
        "bootstrap": "bootstrap-less/**/*.{ttf,svg,woff,woff2,otf,eot}",
        "font-awesome": "font-awesome-less/**/*.{ttf,svg,woff,woff2,otf,eot}"
    };
    for (var directory in directories) {
        gulp.src(paths.bower + directories[directory])
            .pipe(rename(function (path) {
                path.dirname = "";
            }))
            .pipe(gulp.dest(paths.fonts + directory));
    }
});

gulp.task("build-js", [], function () {
    var bootstrapDirectory = "bootstrap-less/js/*.js";
    gulp.src(paths.bower + bootstrapDirectory)
        .pipe(sourcemaps.init())
        .pipe(concat("bootstrap.js"))
        .pipe(uglify())
        .pipe(sourcemaps.write('/'))
        .pipe(gulp.dest(paths.js + "bootstrap"))
        .pipe(size());

    var directories = {
        "bootstrap-touch-carousel": "bootstrap-touch-carousel/dist/js/bootstrap-touch-carousel.js",
        "hammer": "hammer.js/hammer.js",
        "jquery": "jquery/dist/jquery.js",
        "jquery-validation": "jquery-validation/dist/jquery.validate.js",
        "jquery-validation-unobtrusive": "jquery-validation-unobtrusive/jquery.validate.unobtrusive.js"
    }
    for (var directory in directories) {
        gulp.src(paths.bower + directories[directory])
            .pipe(sourcemaps.init())
            .pipe(uglify())
            .pipe(sourcemaps.write('/'))
            .pipe(gulp.dest(paths.js + directory))
            .pipe(size());
    }
});

gulp.task("build", ["clean", "build-css", "build-fonts", "build-js"], function () { });

gulp.task("compress-images", [], function () {
    var directories = [
        paths.img + "**/*.{png,jpg,jpeg,gif,svg}"
    ];

    gulp.src(directories)
        .pipe(imagemin())
        .pipe(gulp.dest(paths.img))
        .pipe(size());
});


// watch
//var gulp = require('gulp'),
//    watch = require('gulp-watch');

//gulp.task('stream', function () {
//    gulp.src('css/**/*.css')
//        .pipe(watch('css/**/*.css'))
//        .pipe(gulp.dest('build'));
//});

//gulp.task('callback', function () {
//    watch('css/**/*.css', function () {
//        gulp.src('css/**/*.css')
//            .pipe(watch('css/**/*.css'));
//    ));
//    });