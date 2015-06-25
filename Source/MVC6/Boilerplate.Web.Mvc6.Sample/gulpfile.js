/// <binding Clean='clean' />

var gulp = require("gulp"),
    concat = require("gulp-concat"),            // https://www.npmjs.com/package/gulp-concat
    imagemin = require("gulp-imagemin"),        // https://www.npmjs.com/package/gulp-imagemin
    less = require("gulp-less"),                // https://www.npmjs.com/package/gulp-less
    minifyCss = require("gulp-minify-css"),     // https://www.npmjs.com/package/gulp-minify-css
    sass = require("gulp-sass"),                // 
    size = require("gulp-size"),                // https://www.npmjs.com/package/gulp-size
    sourcemaps = require('gulp-sourcemaps'),    // https://www.npmjs.com/package/gulp-sourcemaps
    svgmin = require("gulp-svgmin"),            // https://www.npmjs.com/package/gulp-svgmin
    uglify = require('gulp-uglify'),            // https://www.npmjs.com/package/gulp-uglify
    watch = require('gulp-watch'),              // https://www.npmjs.com/package/gulp-watch
    rimraf = require("rimraf"),                 // https://www.npmjs.com/package/rimraf
    fs = require("fs");

eval("var project = " + fs.readFileSync("./project.json"));

var paths = {
    bower: "./bower_components/",
    wwwroot: "./" + project.webroot,
    lib: "./" + project.webroot + "/lib/",
    css: "./" + project.webroot + "/css2/",
    img: "./" + project.webroot + "/img/",
    js: "./" + project.webroot + "/js/"
};

gulp.task("clean", function (cb) {
    rimraf(paths.lib, cb);
    //rimraf(paths.css, cb);
    //rimraf(paths.js, cb);
});

gulp.task("copy", ["clean"], function () {
    var bower = {
        "bootstrap": "bootstrap/dist/**/*.{js,map,css,ttf,svg,woff,eot}",
        "bootstrap-touch-carousel": "bootstrap-touch-carousel/dist/**/*.{js,css}",
        "hammer.js": "hammer.js/hammer*.{js,map}",
        "jquery": "jquery/jquery*.{js,map}",
        "jquery-validation": "jquery-validation/jquery.validate.js",
        "jquery-validation-unobtrusive": "jquery-validation-unobtrusive/jquery.validate.unobtrusive.js"
    }

    for (var destinationDir in bower) {
        gulp.src(paths.bower + bower[destinationDir])
          .pipe(gulp.dest(paths.lib + destinationDir));
    }
});

gulp.task("copy-fonts", [], function () {
    var bower = {
        "bootstrap": "bootstrap/dist/**/*.{ttf,svg,woff,eot}",
    }

    for (var destinationDir in bower) {
        gulp.src(paths.bower + bower[destinationDir])
          .pipe(gulp.dest(paths.css + destinationDir));
    }
});

gulp.task("copy-js", ["clean"], function () {
    var bower = {
        "bootstrap": "bootstrap/dist/**/*.{js,map}",
        "bootstrap-touch-carousel": "bootstrap-touch-carousel/dist/**/*.{js}",
        "hammer.js": "hammer.js/hammer*.{js,map}",
        "jquery": "jquery/jquery*.{js,map}",
        "jquery-validation": "jquery-validation/jquery.validate.js",
        "jquery-validation-unobtrusive": "jquery-validation-unobtrusive/jquery.validate.unobtrusive.js"
    }

    for (var destinationDir in bower) {
        gulp.src(paths.bower + bower[destinationDir])
          .pipe(gulp.dest(paths.js + destinationDir));
    }
});

gulp.task("optimize-css", [], function () {
    var bower = {
        "bootstrap": "bootstrap/dist/**/*.{map,css,less}",
        "bootstrap-touch-carousel": "bootstrap-touch-carousel/dist/**/*.{css,less}"
    }

    for (var destinationDir in bower) {
        gulp.src(paths.bower + bower[destinationDir])
            .pipe(sourcemaps.init({ loadMaps: true })) // Initialize the source maps, also load existing maps.
            .pipe(less({
                paths: [path.join(__dirname, 'less', 'includes')]
            }))
            .pipe(sourcemaps.write('/'))
            .pipe(concat('all.css'))
            .pipe(minifyCss())
            .pipe(gulp.dest(paths.css))
            .pipe(size());
    }
});

gulp.task("optimize-js", [], function () {
    var directories = {
        "root": "**/*.{js,map}",
    }

    for (var destinationDir in directories) {
        gulp.src(paths.js + directories[destinationDir])
            .pipe(uglify())
            .pipe(gulp.dest(paths.js))
            .pipe(size());
    }
});

gulp.task("optimize-img", [], function () {
    var directories = {
        "root": "**/*.{png,jpg,jpeg,gif,svg}",
    }

    for (var destinationDir in directories) {
        gulp.src(paths.img + directories[destinationDir])
            .pipe(imagemin())
            .pipe(gulp.dest(paths.img))
            .pipe(size());
    }
});

gulp.task("build", ["clean", "copy-fonts", "copy-js", "optimize-css", "optimize-js"], function () { });


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