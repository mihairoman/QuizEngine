'use strict';
function GetServiceMethodFullName(url, serviceName, serviceMethod) {
    return String.format("{0}/{1}/{2}",url, serviceName, serviceMethod);

}
var PEService = function () {
    this.url = "http://localhost/QuizEngineWebservice";

    this.ExecuteCall = function (serviceName, serviceMethod, data, onSuccess, onError) {
        //Get the full url of the service method to be called
        var fullUrl = GetServiceMethodFullName(this.url, serviceName, serviceMethod);

        var context = SP.ClientContext.get_current();
        var request = new SP.WebRequestInfo();
        request.set_url(fullUrl);
        request.set_method("POST");
        request.set_headers({
            "Content-Type": "application/json",
            "dataType": "json"

        });
        request.set_body(data);
        var response = SP.WebProxy.invoke(context, request);

        //// Set the event handlers and invoke the request.        
        context.executeQueryAsync(successHandler, errorHandler);

        // Event handler for the success event.       
        function successHandler() {
            if (response.get_statusCode() == 200)
                onSuccess(response.get_body());
            else
                onError(response.get_body());
        }

        // Event handler for the error event.       
        function errorHandler() {

            //there is a specific error from Sharepoint Preview that goes away after page refresh
            //errorCode = -2146232832
            //errorTypeName = "Microsoft.SharePoint.SPException"
            //errorMessage = "The assembly Microsoft.SharePoint.Portal.Proxy, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c does not support app authentication."

            if (arguments[1] && arguments[1].get_errorTypeName() == "Microsoft.SharePoint.SPException" && arguments[1].get_errorCode() == -2146232832)
                window.location.reload(true);
            else
                onError(response.get_body());
        }
    }
}
