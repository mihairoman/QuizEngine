function ValidateExpression(textToValidate) {
    var validator = /^[A-Za-z{1,50}]+$/;
    if (validator.test(textToValidate) && textToValidate.length <= 50) {
        return 1;
    } else {
        return -1;
    }
};