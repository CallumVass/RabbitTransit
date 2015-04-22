export default ngModule => {
    ngModule.directive("app", function (signalRHubProxy, productService) {
        return {
            restrict: "E",
            template: require("./app.html"),
            controllerAs: "vm",
            controller: function () {
                var vm = this;
                vm.products = [];

                productService.getProducts().then(function (data) {
                    vm.products = data;
                });

                var client = signalRHubProxy("stockHub");

                client.on("stockUpdated", function (data) {
                    console.log(data);
                    _.forEach(vm.products, function (product) {
                        if (product.id === data.Id) {
                            product.stockLevel = data.StockLevel;
                            product.lastUpdated = data.LastUpdated;
                        }
                    });
                });

                client.connection.disconnected(function () {
                    setTimeout(function () {
                        client.connection.start();
                    }, 5000);
                });
            }
        };
    });
};