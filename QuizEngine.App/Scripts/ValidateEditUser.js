function ValidateUser(selectedUserType, filledUsername) {

    var valid = true;
    var errString = [];

    if (filledUsername.length < 1) {
        valid = false;
        errString.push({ error: "Please fill the username field. \n" });
    }

    if ((document.getElementById("LoginMode").checked == true) && (valid == true)) {
        if (filledUsername.replace(/[^\\]/g, "").length != 1) {
            valid = false;
            errString.push({ error: "Not a valid domain address. \n" });
        }

        if ((filledUsername.indexOf("\\") > 64) || (filledUsername.length - filledUsername.indexOf("\\") > 253)) {
            valid = false;
            errString.push({ error: "The domain has too many characters. \n" });
        }    
    }
    if ((document.getElementById("LoginMode").checked == false) && (valid == true)) {
        var atpos = filledUsername.indexOf("@");
        var dotpos = filledUsername.lastIndexOf(".");
        if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= filledUsername.length) {
            valid = false;
            errString.push({ error: "Not a valid e-mail address. \n" });
        }

        if ((filledUsername.indexOf("@") > 64) || (filledUsername.length - filledUsername.indexOf("@") > 253)) {
            valid = false;
            errString.push({ error: "The email address has too many characters. \n" });
        }
    }


    return {
        isValid: valid,
        Errors: errString
    }
}


function VerifyUserExists() {
    var valid = true;
    var errString = [];

    errString.push({ error: "The username already exists. \n" });

    return {
        isValid: valid,
        Errors: errString
    }
}