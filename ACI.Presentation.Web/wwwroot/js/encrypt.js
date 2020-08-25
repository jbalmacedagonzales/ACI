var key = CryptoJS.enc.Utf8.parse("8080808080808080");
var iv = CryptoJS.enc.Utf8.parse("8080808080808080");

function fnEncrypt(vpassword) {
    var encryptedPassword = CryptoJS.AES.encrypt
        (
            CryptoJS.enc.Utf8.parse(vpassword), key,
            {
                keySize: 128 / 8,
                iv: iv,
                mode: CryptoJS.mode.CBC,
                padding: CryptoJS.pad.Pkcs7
            });
    if (vpassword != "") {
        vpassword = encryptedPassword + "";
    }
    return vpassword;
}

function fnSendPwdEncrypt(
    submitCssClassName,
    inputPassword, inputConfirmPassword = null,
    hfPassword, hfConfirmPassword = null, isLogin = false) {

    var password;
    var confirmPassword;

    $("." + submitCssClassName).on("click", function () {
        password = fnEncrypt($("#" + inputPassword).val());
        confirmPassword = fnEncrypt($("#" + inputConfirmPassword).val());
        $("#" + hfPassword).val(password);
        $("#" + hfConfirmPassword).val(confirmPassword);

        if (isLogin) {
            password = fnEncrypt($("#" + inputPassword).val());
            $("#" + hfPassword).val(password);
        }
    });
}