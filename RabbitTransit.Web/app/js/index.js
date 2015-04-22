const $ = require('jquery');
const _ = require('lodash');

// most jQuery plugins don't work unless jQuery is on the window as well :-/
window.$ = window.jQuery = $;
window._ = _;

require("angular");
require("angular-ui-router");

const ngModule = angular.module("app", ["ui.router"]);

ngModule.config(function ($stateProvider, $urlRouterProvider) {

    $stateProvider
        .state('home', {
            url: '/',
            template: '<app />'
        });

    $urlRouterProvider.otherwise('/');
});

require("./config")(ngModule);
require("./services")(ngModule);
require("./directives")(ngModule);