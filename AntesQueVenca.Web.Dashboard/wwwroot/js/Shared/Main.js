function CallMethod(url, parameters, successCallback, errorCallback) {
    $.ajax({
        type: 'POST',
        url: url,
        data: parameters,
        //contentType: 'application/json;',
        contentType: 'application/x-www-form-urlencoded',
        dataType: 'json',
        success: function (data) {
            successCallback(data);
        },
        error: errorCallback
    });
}

//function CallMethodSingleParameter(url, propertyName, parameter, successCallback, errorCallback) {
//    alert(url);
//    alert(parameter);

//    $.ajax({
//        type: 'POST',
//        url: url,
//        data: '?name=' + parameter,//+ propertyName + '=' + parameter,
//        contentType: 'application/x-www-form-urlencoded',
//        dataType: 'json',
//        success: function (data) {
//            successCallback(data);
//        },
//        error: errorCallback
//    });
//}