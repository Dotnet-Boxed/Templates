/// <binding Clean='clean' />

var gulp = require("gulp"),
  minifycss = require("gulp-minify-css"),
  concat = require("gulp-concat"),
  uglify = require('gulp-uglify'),
  watch = require('gulp-watch'),
  rimraf = require("rimraf"),
  fs = require("fs");

eval("var project = " + fs.readFileSync("./project.json"));

var paths = {
  bower: "./bower_components/",
  lib: "./" + project.webroot + "/lib/",
  //css: "./" + project.webroot + "/css/",
  //js: "./" + project.webroot + "/js/"
};

gulp.task("clean", function (cb) {
  rimraf(paths.lib, cb);
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

// http://www.davepaquette.com/archive/2015/05/05/web-optimization-development-and-production-in-asp-net-mvc6.aspx
//gulp.task("minifycss", function () {
//    return gulp.src([paths.css + "/*.css",
//                     "!" + paths.css + "/*.min.css"])
//            .pipe(minifycss())
//            .pipe(concat("site.min.css"))
//            .pipe(gulp.dest(paths.css));
//});

// http://www.davepaquette.com/archive/2014/10/08/how-to-use-gulp-in-visual-studio.aspx

//gulp.task('uglifyjs', ['clean'], function () {

//    return gulp.src(config.js)
//      .pipe(uglify())
//      .pipe(concat('site.min.js'))
//      .pipe(gulp.dest('app/'));
//});

//var config = {
//    //Include all js files but exclude any min.js files
//    src: ['app/**/*.js', '!app/**/*.min.js'],
//}
//gulp.task('watch', function () {
//    return gulp.watch(config.src, ['scripts']);
//});