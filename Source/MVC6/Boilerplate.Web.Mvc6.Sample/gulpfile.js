/// <binding Clean='clean' />

var gulp = require("gulp"),
    concat = require("gulp-concat"),            // https://www.npmjs.com/package/gulp-concat
    imagemin = require("gulp-imagemin"),        // https://www.npmjs.com/package/gulp-imagemin
    less = require("gulp-less"),                // https://www.npmjs.com/package/gulp-less
    minifyCss = require("gulp-minify-css"),     // https://www.npmjs.com/package/gulp-minify-css
    rename = require('gulp-rename'),            // https://www.npmjs.com/package/gulp-rename
    size = require("gulp-size"),                // https://www.npmjs.com/package/gulp-size
    sourcemaps = require('gulp-sourcemaps'),    // https://www.npmjs.com/package/gulp-sourcemaps
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
    rimraf(paths.js, function () { });
});

gulp.task("build-css", function () {
    var sources = [
        {
            name: "font-awesome.css",
            paths: [
                paths.bower + "font-awesome-less/less/font-awesome.less"
            ]
        },
        {
            name: "site.css",
            paths: [
                paths.content + "bootstrap.less",
                paths.bower + "bootstrap-touch-carousel/src/less/carousel.less",
                paths.content + "Site.less"
            ]
        }
    ];
    for (var sourceIndex in sources) {
        var source = sources[sourceIndex];
        gulp.src(source.paths)
            .pipe(sourcemaps.init())
            .pipe(less())
            .pipe(concat(source.name))
            .pipe(minifyCss())
            .pipe(sourcemaps.write('/'))
            .pipe(gulp.dest(paths.css))
            .pipe(size());
    }
});

gulp.task("build-fonts", function () {
    var sources = [
        { name: "bootstrap", path: "bootstrap-less/**/*.{ttf,svg,woff,woff2,otf,eot}" },
        { name: "font-awesome", path: "font-awesome-less/**/*.{ttf,svg,woff,woff2,otf,eot}" }
    ];
    for (var sourceIndex in sources) {
        var source = sources[sourceIndex];
        gulp.src(paths.bower + source.path)
            .pipe(rename(function (path) {
                path.dirname = "";
            }))
            .pipe(gulp.dest(paths.fonts + source.name));
    }
});

gulp.task("build-js", function () {
    var sources = [
        {
            name: "bootstrap.js",
            path: [
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
        { name: "bootstrap-touch-carousel.js", path: paths.bower + "bootstrap-touch-carousel/dist/js/bootstrap-touch-carousel.js" },
        { name: "hammer.js", path: paths.bower + "hammer.js/hammer.js" },
        { name: "jquery.js", path: paths.bower + "jquery/dist/jquery.js" },
        { name: "jquery-validate.js", path: paths.bower + "jquery-validation/dist/jquery.validate.js" },
        { name: "jquery-validate-unobtrusive.js", path: paths.bower + "jquery-validation-unobtrusive/jquery.validate.unobtrusive.js" }
    ];
    for (var sourceIndex in sources) {
        var source = sources[sourceIndex];
        gulp.src(source.path)
            .pipe(sourcemaps.init())
            .pipe(concat(source.name))
            .pipe(uglify())
            .pipe(sourcemaps.write('/'))
            .pipe(gulp.dest(paths.js))
            .pipe(size());
    }
});

gulp.task("build", ["build-css", "build-fonts", "build-js"]);

gulp.task("compress-images", [], function () {
    var sources = [
        paths.img + "**/*.{png,jpg,jpeg,gif,svg}"
    ];
    gulp.src(sources)
        .pipe(imagemin())
        .pipe(gulp.dest(paths.img))
        .pipe(size());
});

gulp.watch(paths.content, ["build"]);
gulp.watch(paths.scripts, ["build"]);