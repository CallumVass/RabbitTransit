'use strict';

var config = require('../config');
var gulp = require('gulp');
var webserver = require('gulp-webserver');

gulp.task('watch', function () {

    gulp.src(['nancy/views', 'build'])
        .pipe(webserver({
            port: config.serverport,
            livereload: true,
            proxies: [
                {
                    source: '/',
                    target: 'http://localhost:9000'
                }
            ]
        }));

    // Scripts are automatically watched and rebundled by Watchify inside Browserify task
    gulp.watch(config.scripts.src, ['lint']);
    gulp.watch(config.styles.src, ['styles']);
    gulp.watch(config.images.src, ['images']);
    gulp.watch(config.fonts.src, ['fonts']);
    gulp.watch(config.views.watch, ['views']);

});