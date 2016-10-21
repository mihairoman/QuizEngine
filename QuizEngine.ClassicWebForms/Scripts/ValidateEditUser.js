function ValidateUser(selectedUserType, filledUsername) {

    var valid = true;
    var errString = [];

    if (filledUsername.length < 1) {
        valid = false;
        errString.push({ error: UserResources.UsernameEmptyError });
    }

    if ((document.getElementById("LoginMode").checked == true) && (valid == true)) {
        if (filledUsername.replace(/[^\\]/g, "").length != 1) {
            valid = false;
            errString.push({ error: UserResources.UsernameNotDomainError });
        }

        if ((filledUsername.indexOf("\\") > 64) || (filledUsername.length - filledUsername.indexOf("\\") > 253)) {
            valid = false;
            errString.push({ error: UserResources.UsernameDomainTooLongError });
        }    
    }
    if ((document.getElementById("LoginMode").checked == false) && (valid == true)) {
        var atpos = filledUsername.indexOf("@");
        var dotpos = filledUsername.lastIndexOf(".");
        if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= filledUsername.length) {
            valid = false;
            errString.push({ error: UserResources.UsernameNotEmailError });
        }

        if ((filledUsername.indexOf("@") > 64) || (filledUsername.length - filledUsername.indexOf("@") > 253)) {
            valid = false;
            errString.push({ error: UserResources.UsernameEmailTooLongError });
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

    errString.push({ error: UserResources.UsernameAlreadyExistsError });

    return {
        isValid: valid,
        Errors: errString
    }
}