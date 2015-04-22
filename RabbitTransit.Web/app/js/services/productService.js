export default ngModule => {

    ngModule.factory("productService", function ($http) {
        return {
            getProducts: getProducts
        };

        function getProducts() {
            return $http.get("/products").then(function (data) {
                return data.data;
            });
        }
    });
};