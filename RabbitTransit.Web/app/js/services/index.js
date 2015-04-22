export default ngModule => {
    require("./signalRHubProxy")(ngModule);
    require("./productService")(ngModule);
};